using FichaEpid;
using ImageResizer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proyecto_Final_5to_Semestre_Web
{
    public partial class frmNuevaImagen : System.Web.UI.Page
    {
        protected static Funciones fn;
        protected static PaginaMaestra mst;
        protected static string DireccionMaestra = @"C:\Users\Compufire\Desktop\imagenes";
        protected static int contadorveces = 1;
        protected static string palabranueva;
        protected static string palabraantigua;
        protected static int contadorNuevaCorrecta = 1;
        protected static string idCaptcha;
        protected void Page_Load(object sender, EventArgs e)
        {
            fn = new Funciones();
            mst = (PaginaMaestra)Master;
            if (!IsPostBack)
            {
                Traducir();
                LlenarHistorial();
                pnlVista.Visible = false;
            }
        }
        protected void Traducir()
        {
            btnsubir.Text = "Confirmar";
            txtCapchaCon.Text = "";
            txtCapchaNoCon.Text = "";
            lbltituloCapcha.Text = "1 de 3";

            NuevoRegistro();
        }

        protected string GenerarTextoCapcha()
        {
            Random ra = new Random();
            int noc = ra.Next(6, 10);
            string cap = "";
            int tot = 0;
            do
            {
                int ch = ra.Next(48, 123);
                if ((ch >= 48 && ch <= 57) || (ch >= 65 && ch <= 90) || (ch >= 97 && ch <= 122))
                {
                    cap = cap + (char)ch;
                    tot++;
                    if (tot == noc)
                        break;
                }
            } while (true);
            lblCaptcha.Text = cap;
            return cap;
        }

        protected void btnsubir_Click(object sender, EventArgs e)
        {
            string palabracomun = txtCapchaCon.Text;
            string palabranocomun = txtCapchaNoCon.Text;


            if (contadorveces == 1)
            {
                if (palabraantigua == palabracomun)
                {
                    if (!string.IsNullOrEmpty(palabranocomun))
                    {
                        palabranueva = palabranocomun;
                        contadorNuevaCorrecta++;
                        contadorveces++;
                        txtCapchaCon.Text = "";
                        txtCapchaNoCon.Text = "";
                        lbltituloCapcha.Text = "2 de 3";
                    }
                    else
                    {
                        mst.MostrarMensaje("llenar el espacio", 1);
                    }
                }
                else
                {
                    mst.MostrarMensaje("Verifique la escritura", 1);
                }
            }
            else if (contadorveces == 2)
            {
                if (palabraantigua == palabracomun)
                {
                    if (!string.IsNullOrEmpty(palabranocomun))
                    {
                        if (palabranocomun == palabranueva)
                        {
                            contadorNuevaCorrecta++;
                            contadorveces++;
                            txtCapchaCon.Text = "";
                            txtCapchaNoCon.Text = "";
                            lbltituloCapcha.Text = "3 de 3";
                            flpDragAndDrop.Visible = true;
                        }
                        else
                        {
                            mst.MostrarMensaje("El texto no coincide", 1);
                        }
                    }
                    else
                    {
                        mst.MostrarMensaje("Debe llenar el espacio", 1);
                    }
                }
                else
                {
                    mst.MostrarMensaje("Verifique la escritura", 1);
                }
            }
            else if (contadorveces == 3)
            {
                if (palabraantigua == palabracomun)
                {
                    if (palabranueva == palabranocomun && contadorNuevaCorrecta == 3)
                    {
                        String sql = "UPDATE saApps.Captchas " +
                        "SET Mostrar= '1', Campo_Informacion='"+palabranocomun+"' WHERE Id_Captcha=" + idCaptcha;
                        fn.ejecutarSQL(sql);
                    }

                    if (flpDragAndDrop.HasFile == null || string.IsNullOrEmpty(flpDragAndDrop.PostedFile.FileName))
                    {
                        mst.MostrarMensaje("Ningun archivo seleccionado", 1);
                    }
                    else
                    {

                        string extension = Path.GetExtension(flpDragAndDrop.PostedFile.FileName);
                        string rutaFoto = Path.GetDirectoryName(flpDragAndDrop.PostedFile.FileName);
                        
                        switch (extension.ToLower())
                        {
                            case ".jpg":
                                transferirFoto(rutaFoto);
                                break;
                            case "":
                                mst.MostrarMensaje("Ningun archivo seleccionado", 1);
                                break;
                            default:
                                mst.MostrarMensaje("Extension Invalida", 1);
                                break;
                        }
                    }
                }
            }
        }

        protected void transferirFoto(string fotoactual)
        {
            DateTime dtFechaNombre = DateTime.Now;
            string nombreNuevoArchivo = "IMG_" + dtFechaNombre.ToString("yyyyMMddHHmmss") + ".jpg";

            string direccionFinal = Path.Combine(DireccionMaestra, nombreNuevoArchivo);

            flpDragAndDrop.SaveAs(direccionFinal);

            AgregarImagen(direccionFinal, 1, dtFechaNombre);
            AgregarImagen(direccionFinal, 2, dtFechaNombre);
            AgregarImagen(direccionFinal, 3, dtFechaNombre);
            AgregarImagen(direccionFinal, 4, dtFechaNombre);
            AgregarImagen(direccionFinal, 5, dtFechaNombre);
            AgregarImagen(direccionFinal, 6, dtFechaNombre);

            int id = nuevoRegistroImagen();
            AgregarRegistroBD(id, direccionFinal, dtFechaNombre);
            idCaptcha = "";
            LlenarHistorial();

            NuevoRegistro();
            lbltituloCapcha.Text = "1 de 3";

            contadorveces = 1;
            contadorNuevaCorrecta = 1;
            Response.Redirect("frmNuevaImagen.aspx");

        }
        protected void NuevoRegistro()
        {

            palabraantigua = GenerarTextoCapcha();
            string sql = " SELECT TOP(1) Id_Captcha ID FROM saApps.Captchas WHERE Activo = 1 AND Mostrar = '0' ORDER BY Id_Captcha ASC ";
            string obtener = fn.obtienePalabra(sql, "ID");
            idCaptcha = obtener;

            sql = "SELECT TOP(1) Url ID FROM saApps.Captchas WHERE Activo = 1 AND Id_Captcha=" + obtener;
            string url = fn.obtienePalabra(sql, "ID");
            flpDragAndDrop.Visible = false;
            imgCaptchaNoCon.Attributes["src"] = "Imagen.ashx?imageID=" + url;

            txtCapchaCon.Text = "";
            txtCapchaNoCon.Text = "";
        }
        protected string Miniatura(DateTime dt, int numero)
        {
            return "IMG_" + dt.ToString("yyyyMMddHHmmss") + "_miniatura" + numero.ToString() + ".jpg";
        }
        protected void AgregarRegistroBD(int id, string direccion, DateTime dt)
        {
            try
            {
                string sql = "INSERT INTO saApps.Imagenes(Id_Imagen, Usuario, Fecha_Creacion, Activo, Imagen_1, Imagen_2, Imagen_3, Imagen_4, Imagen_5, Imagen_6) " +
                    "VALUES(" + id.ToString() + ",'" + Sesion.U + "',GETDATE(),1,'" + Miniatura(dt, 1) + "','" + Miniatura(dt, 2) + "','" + Miniatura(dt, 3) + "','" + Miniatura(dt, 4) + "','" + Miniatura(dt, 5) + "','" + Miniatura(dt, 6) + "')";
                fn.ejecutarSQL(sql);
            }
            catch (Exception ex)
            {
                mst.MostrarMensaje("Se ha encontrado el siguiente error:" + ex.Message, 3);
            }
        }
        protected int nuevoRegistroImagen()
        {
            string sql = "SELECT COUNT(*) conteo FROM saApps.Imagenes";
            int contador = fn.ObtenerEntero(sql, "conteo");
            if (contador > 0)
            {
                sql = "SELECT MAX(Id_Imagen)+1 conteo FROM saApps.Imagenes";
                return fn.ObtenerEntero(sql, "conteo");
            }
            return 1;
        }
        protected int nuevoRegistroCaptcha()
        {
            string sql = "SELECT COUNT(*) conteo FROM saApps.Captchas";
            int contador = fn.ObtenerEntero(sql, "conteo");
            if (contador > 0)
            {
                sql = "SELECT MAX(Id_Captcha)+1 conteo FROM saApps.Captchas";
                return fn.ObtenerEntero(sql, "conteo");
            }
            return 1;
        }
        protected void AgregarImagen(string direccionFinal, int numero, DateTime dtFechaNombre)
        {
            Bitmap nb = new Bitmap(direccionFinal);
            int x = numero % 2 == 0 ? (int)nb.Width / 2 : 0;
            int y = 0;

            int tercios = (int)nb.Height / 3;
            string nombreArchivoMini = "IMG_" + dtFechaNombre.ToString("yyyyMMddHHmmss") + "_miniatura" + numero.ToString() + ".jpg";
            string direccionMiniFinal = Path.Combine(DireccionMaestra, nombreArchivoMini);

            if (numero == 3 || numero == 4)
            {
                y = tercios;
            }
            else if (numero == 5 || numero == 6)
            {
                y = (int)tercios * 2;
            }

            Bitmap mp = CropCenter(nb, (int)nb.Width / 2, tercios, x, y);
            mp.Save(direccionMiniFinal);

            int idFicha = nuevoRegistroCaptcha();

            string sql = "INSERT INTO Captchas " +
            "VALUES('" + nombreArchivoMini + "', " + idFicha.ToString() + ", '" + Sesion.U + "', GETDATE(), 1, NULL, NULL, '0')";
            fn.ejecutarSQL(sql);
        }
        public Bitmap CropCenter(Bitmap src, int targetWidth, int targetHeight, int x, int y)
        {

            Rectangle area = new Rectangle(x, y, targetWidth, targetHeight);

            return src.Clone(area, src.PixelFormat);
        }
        protected void LlenarHistorial()
        {
            string sql = "SELECT Id_Imagen ID, Usuario 'Registrado Por', " +
            "Fecha_Creacion Fecha," +
            "CASE WHEN Imagen_1 IS NOT NULL THEN 'Si' ELSE 'No'  END NIT, CASE WHEN Imagen_2 IS NOT NULL THEN 'Si' ELSE 'No'  END Fecha, " +
            "CASE WHEN Imagen_3 IS NOT NULL THEN 'Si' ELSE 'No'  END 'Articulo 1', CASE WHEN Imagen_4 IS NOT NULL THEN 'Si' ELSE 'No'  END 'Cantidad 1', " +
            "CASE WHEN Imagen_5 IS NOT NULL THEN 'Si' ELSE 'No'  END 'Articulo 2', CASE WHEN Imagen_6 IS NOT NULL THEN 'Si' ELSE 'No'  END 'Cantidad 2' " +
            "FROM saApps.Imagenes " +
            "WHERE Usuario = '" + Sesion.U + "' AND Activo = 1";
            fn.LLenarGrid(sql, gvhistorial);
        }

        protected void gvhistorial_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdSelect")
            {
                pnlVista.Visible = false;
            }
        }
    }
}
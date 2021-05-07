using FichaEpid;
using IronOcr;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Proyecto_Final_5to_Semestre_Web
{
    public partial class frmModificarImagen : System.Web.UI.Page
    {
        protected static Funciones fn;
        protected void Page_Load(object sender, EventArgs e)
        {
            fn = new Funciones();
            if (!IsPostBack)
            {

            }
        }

        protected void lnkNuevo_Click(object sender, EventArgs e)
        {
            int reportesGenerados = 1;
            int contador = 1;
            string sql = "SELECT COUNT(*) cn FROM saApps.Captchas WHERE Mostrar='1'";
            int contadorCaptcha = fn.ObtenerEntero(sql, "cn");
            pnl.Controls.Clear();
            if (contadorCaptcha == 0)
            {
                pnl.Controls.Add(new LiteralControl("No tiene Registros"));
            }
            else
            {
                DataTable dt = new DataTable();
                sql = "SELECT * FROM saApps.Captchas WHERE Mostrar='1' AND Id_Captcha>1 ORDER BY Id_Captcha ASC";
                fn.LlenarDataTable(sql, dt);

                foreach (DataRow row in dt.Rows)
                {
                    string valor = row["Campo_Informacion"].ToString();
                    switch (contador)
                    {
                        case 1:
                            pnl.Controls.Add(new LiteralControl("------------Reporte "+reportesGenerados+" --------------<br>"));
                            pnl.Controls.Add(new LiteralControl("<b>NIT: </b>"+valor+ "<br>"));
                            contador++;
                            break;
                        case 2:
                            pnl.Controls.Add(new LiteralControl("<b>Fecha: </b>" + valor + "<br>"));
                            contador++;
                            break;
                        case 3:
                            pnl.Controls.Add(new LiteralControl("<b>Descripcion Producto 1: </b>" + valor + "<br>"));
                            contador++;
                            break;
                        case 4:
                            pnl.Controls.Add(new LiteralControl("<b>Cantidad Articulo 1: </b>" + valor + "<br>"));
                            contador++;
                            break;
                        case 5:
                            pnl.Controls.Add(new LiteralControl("<b>Descripcion Producto 2: </b>" + valor + "<br>"));
                            contador++;
                            break;
                        case 6:
                            pnl.Controls.Add(new LiteralControl("<b>Cantidad Articulo 2:</b> " + valor + "<br>"));
                            pnl.Controls.Add(new LiteralControl("--------------------- --------------<br>"));
                            contador=1;
                            reportesGenerados++;
                            break;
                    }
                }
            }
        }
    }
}
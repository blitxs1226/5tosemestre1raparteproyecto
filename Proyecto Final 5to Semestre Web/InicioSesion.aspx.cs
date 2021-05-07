using FichaEpid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proyecto_Final_5to_Semestre_Web
{
    public partial class InicioSesion : System.Web.UI.Page
    {
        protected static Funciones fn;
        protected void Page_Load(object sender, EventArgs e)
        {
            fn = new Funciones();
            if (!Page.IsPostBack)
            {

            }
        }

        protected void lnkIngresar_Click(object sender, EventArgs e)
        {
            if (txtusuario.Text.Length > 0 && txtPassword.Text.Length > 0)
            {
                string sql = "SELECT COUNT(*) conteo FROM saApps.Usuarios WHERE Nombre ='" + txtusuario.Text + "' AND Contraseña='" + txtPassword.Text + "'";
                int contador = fn.ObtenerEntero(sql, "conteo");
                if (contador > 0)
                {
                    Sesion.U = txtusuario.Text;
                    Sesion.NombrePagina = "Página Principal";
                    Response.Redirect("PaginaPrincipal.aspx");
                }
                else
                {
                    MostrarMensaje("Usuario o Contraseña incorrectos",1);
                }
            }
            else
            {
                MostrarMensaje("Por favor debe llenar ambos campos", 1);
            }
        }
        public void MostrarMensaje(String desc, int estado = 2)
        {
        
            string[] tipoalerta = { "info", "warning", "success", "error" };
            string alerta = "toastr." + tipoalerta[estado] + "('" + desc + "', 'Aplicación Captcha', {" +
            "'progressBar': true," +
            "'positionClass': 'toast-bottom-right'," +
                "'timeOut': '10000'," +
                "'extendedTimeOut': '10000'" +
            "});";
            Page.ClientScript.RegisterStartupScript(GetType(), "mostrar", alerta, true);
        }

    }
}
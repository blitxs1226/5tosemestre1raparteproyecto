using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proyecto_Final_5to_Semestre_Web
{
    public partial class PaginaMaestra : System.Web.UI.MasterPage
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Sesion.U))
            {
                Response.Redirect("InicioSesion.aspx");
            }
            if (!Page.IsPostBack)
            {
                Traducir();
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
        protected void Traducir()
        {
            lblTituloCaptcha.Text = @"<i class=""fad fa-images""></i> Catpcha";
            lnkAgregarImagen.Text = @"<i class=""fas fa-plus - circle""></i> Agregar Imagen";
            lnkModCaptchat.Text = @"<i class=""fas fa-pen""></i> Generar Reporte";

            lblusuario.Text = @"<i class=""fad fa-user""></i> " + Sesion.U;
            lnkCerrarSesion.Text = @"<i class=""fas fa-power-off""></i> Cerrar Sesión";
            lnkPagMaestra.Text = @"<i class=""fad fa-home""></i> Página Principal";
            lblTituloPagina.Text = Sesion.NombrePagina;
        }

        protected void lnkPagMaestra_Click(object sender, EventArgs e)
        {
            Sesion.NombrePagina = "Página Principal";
            Response.Redirect("PaginaPrincipal.aspx");
        }

        protected void lnkAgregarImagen_Click(object sender, EventArgs e)
        {
            Sesion.NombrePagina = "Agregar Nueva Imagen";
            Response.Redirect("frmNuevaImagen.aspx");
        }

        protected void lnkModCaptchat_Click(object sender, EventArgs e)
        {
            Sesion.NombrePagina = "Modificar Captcha";
            Response.Redirect("frmModificarImagen.aspx");
        }

        protected void lnkCerrarSesion_Click(object sender, EventArgs e)
        {
            Sesion.U = "";
            Response.Redirect("InicioSesion.aspx");
        }
    }
}
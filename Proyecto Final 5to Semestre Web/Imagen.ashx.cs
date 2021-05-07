using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Familias3._1
{
    /// <summary>
    /// Summary description for Imagen
    /// </summary>
    public class Imagen : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string imageFile = null;
            string path = null;
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;


            imageFile = request.QueryString["imageID"];
            if (!string.IsNullOrEmpty(imageFile))
            {
                response.ContentType = "image/jpeg";
                path = @"C:\Users\Compufire\Desktop\imagenes\" + imageFile;
                try
                {
                    response.WriteFile(path);
                }
                catch (Exception e)
                {
                   //ClientScript.RegisterStartupScript(this.GetType(), "yourMessage", "alert('Error en foto, envie mensaje a Sistemas:');window.location ='../SearchProf.aspx';", true);
                }
            }
            else
                throw new ArgumentNullException();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI.WebControls;

namespace FichaEpid
{
    public class Funciones
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        string ConnectionString2 = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection con2 = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

        string ConnectionString3 = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection con3 = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

        static String correoOrigen = "SolicitudesIT@fundacionfde.onmicrosoft.com"; //Tu dirección de correo (Outlook).
        static String contraseñaCorreoOrigen = "S0l1c1tud3s.IT"; //Contraseña.
        public void ReportesFechasMes(TextBox txtinicio, TextBox txtfin)
        {
            DateTime hoy = DateTime.Today;
            DateTime fechaMesAnterior = hoy.AddMonths(-1);
            DateTime InicioMes = fechaMesAnterior.AddDays(-fechaMesAnterior.Day + 1);
            DateTime FinMes = fechaMesAnterior.AddDays(-fechaMesAnterior.Day + (int)DateTime.DaysInMonth(fechaMesAnterior.Year, fechaMesAnterior.Month));

            txtinicio.Text = InicioMes.ToString("yyyy/MM/dd");
            txtfin.Text = FinMes.ToString("yyyy/MM/dd");

        }
        public void ReportesFechasSemana(TextBox txtinicio, TextBox txtfin)
        {
            DateTime hoy = DateTime.Today;

            int diaActual = (int)hoy.DayOfWeek;
            int diferenciaDias = 7 + diaActual;

            DateTime HaceunaSemana = hoy.AddDays(-diferenciaDias);
            DateTime FinHaceunaSemana = hoy.AddDays(-diaActual - 1);

            txtinicio.Text = HaceunaSemana.ToString("dd/MM/yyyy");
            txtfin.Text = FinHaceunaSemana.ToString("dd/MM/yyyy");
        }
        public int verificarUltimas4Psw(String user, String pass)
        {
            string sql = "DECLARE @SI INT DECLARE @PAS varchar(30) SET @PAS = (SELECT CdContraseñaUsuario.Contraseña FROM CdContraseñaUsuario "+
"WHERE Usuario= '"+user+"') DECLARE @PAS1 varchar(30) SET @PAS1 = (SELECT CdContraseñaUsuario.Contraseña1 "+
"FROM CdContraseñaUsuario WHERE Usuario='"+user+"') DECLARE @PAS2 varchar(30) SET @PAS2 = "+
"(SELECT CdContraseñaUsuario.Contraseña2 FROM CdContraseñaUsuario WHERE Usuario='"+user+"') DECLARE @PAS3 varchar(30) "+
"SET @PAS3 = (SELECT CdContraseñaUsuario.Contraseña3 FROM CdContraseñaUsuario WHERE Usuario='"+user+"') "+
"if '"+pass+"' = "+
"@PAS OR '"+pass+"' = @PAS1 OR '"+pass+"' = @PAS2 OR '"+pass+"' = @PAS3 BEGIN SET @SI=0 END ELSE BEGIN SET @SI=1 END SELECT @SI conteo";

            return ObtenerEnteroSeguridad(sql,"conteo");
        }
        public int ejecutarSQL(String sql)
        {

            SqlCommand cmd = null;
            cmd = new SqlCommand(sql, con);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                string correo,asunto,cuerpo;
                correo = "javier_minches@hotmail.com";
                asunto = "Error Encontrado";
                cuerpo = "Error: "+ex.Message+"<br>"+
                         "Consulta: "+sql;

                enviarCorreo(correo, asunto, cuerpo);
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }
        public int ejecutarSQLSeguridad(String sql)
        {

            SqlCommand cmd = null;
            cmd = new SqlCommand(sql, con2);

            try
            {
                con2.Open();
                cmd.ExecuteNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                string correo, asunto, cuerpo;
                correo = "javier_minches@hotmail.com";
                asunto = "Error Encontrado";
                cuerpo = "Error: " + ex.Message + "<br>" +
                         "Consulta: " + sql;

                enviarCorreo(correo, asunto, cuerpo);
                throw ex;
            }
            finally
            {
                con2.Close();
            }
        }
        public void LLenarGrid(string sql, GridView gv)
        {
            try
            {
                DataTable tabledata = new DataTable();
                SqlConnection conexion = new SqlConnection(ConnectionString);
                conexion.Open();
                SqlDataAdapter adaptador = new SqlDataAdapter(sql, conexion);
                DataSet setDatos = new DataSet();
                adaptador.Fill(setDatos, "listado");
                tabledata = setDatos.Tables["listado"];
                conexion.Close();
                gv.DataSource = tabledata;
                gv.DataBind();
            }catch(Exception ex){
                string correo, asunto, cuerpo;
                correo = "javier_minches@hotmail.com";
                asunto = "Error Encontrado";
                cuerpo = "Error: " + ex.Message + "<br>" +
                         "Consulta: " + sql;

                enviarCorreo(correo, asunto, cuerpo);
            }
        }
        public int ObtenerEntero(String SQL, String title)
        {
            SqlDataAdapter daUser;
            DataTableReader adap;
            DataTable tableData = new DataTable();
            int temp;
            try
            {
                con.Open();
                daUser = new SqlDataAdapter(SQL, ConnectionString);
                daUser.Fill(tableData);
                adap = new DataTableReader(tableData);
                con.Close();
                DataRow row = tableData.Rows[0];
                temp = Convert.ToInt32(row[title]);
                return temp;
            }
            catch (Exception ex)
            {
                temp = -1;
                string correo, asunto, cuerpo;
                correo = "javier_minches@hotmail.com";
                asunto = "Error Encontrado";
                cuerpo = "Error: " + ex.Message + "<br>" +
                         "Consulta: " + SQL;

                enviarCorreo(correo, asunto, cuerpo);
                return temp;
            }
        }
        public int ObtenerEnteroSeguridad(String SQL, String title)
        {
            SqlDataAdapter daUser;
            DataTableReader adap;
            DataTable tableData = new DataTable();
            int temp;
            try
            {
                con2.Open();
                daUser = new SqlDataAdapter(SQL, ConnectionString2);
                daUser.Fill(tableData);
                adap = new DataTableReader(tableData);
                con2.Close();
                DataRow row = tableData.Rows[0];
                temp = Convert.ToInt32(row[title]);
                return temp;
            }
            catch (Exception ex)
            {
                string correo, asunto, cuerpo;
                correo = "javier_minches@hotmail.com";
                asunto = "Error Encontrado";
                cuerpo = "Error: " + ex.Message + "<br>" +
                         "Consulta: " + SQL;

                enviarCorreo(correo, asunto, cuerpo);
                temp = -1;
                return temp;
            }
        }
        public DataTable LlenarDataTable(String SQL, DataTable tabledata)
        {
            try
            {
                con.Open();
                SqlCommand comando = new SqlCommand(SQL, con);
                SqlDataAdapter adaptador = new SqlDataAdapter();
                adaptador.SelectCommand = comando;
                adaptador.Fill(tabledata);
                con.Close();
                return tabledata;
            }
            catch (Exception ex)
            {
                string correo, asunto, cuerpo;
                correo = "javier_minches@hotmail.com";
                asunto = "Error Encontrado";
                cuerpo = "Error: " + ex.Message + "<br>" +
                         "Consulta: " + SQL;

                enviarCorreo(correo, asunto, cuerpo);
                DataTable d = new DataTable();
                return d;
            }
        }
        public DataTable LlenarDataTableSeguridad(String SQL, DataTable tabledata)
        {
            try
            {
                con2.Open();
                SqlCommand comando = new SqlCommand(SQL, con2);
                SqlDataAdapter adaptador = new SqlDataAdapter();
                adaptador.SelectCommand = comando;
                adaptador.Fill(tabledata);
                con2.Close();
                return tabledata;
            }
            catch (Exception ex)
            {
                string correo, asunto, cuerpo;
                correo = "javier_minches@hotmail.com";
                asunto = "Error Encontrado";
                cuerpo = "Error: " + ex.Message + "<br>" +
                         "Consulta: " + SQL;

                enviarCorreo(correo, asunto, cuerpo);
                DataTable d = new DataTable();
                return d;
            }
        }
        public string obtienePalabra(String sql, String titulo)
        {
            try
            {
                SqlDataAdapter daUser = new SqlDataAdapter();
                DataTableReader adap;
                DataTable tableData = new DataTable();
                string temp = "";


                con.Open();
                daUser = new SqlDataAdapter(sql, ConnectionString);
                daUser.Fill(tableData);
                adap = new DataTableReader(tableData);
                con.Close();
                temp = Convert.ToString(tableData.Rows[0][titulo]);

                return temp;
            }
            catch (Exception ex)
            {
                string correo, asunto, cuerpo;
                correo = "javier_minches@hotmail.com";
                asunto = "Error Encontrado";
                cuerpo = "Error: " + ex.Message + "<br>" +
                         "Consulta: " + sql;

                enviarCorreo(correo, asunto, cuerpo);
                string temp = "||||||";
                return temp + ex.Message;
            }
        }
        public string obtienePalabraSeguridad(String sql, String titulo)
        {
            try
            {
                SqlDataAdapter daUser = new SqlDataAdapter();
                DataTableReader adap;
                DataTable tableData = new DataTable();
                string temp = "";


                con2.Open();
                daUser = new SqlDataAdapter(sql, ConnectionString2);
                daUser.Fill(tableData);
                adap = new DataTableReader(tableData);
                con2.Close();
                temp = Convert.ToString(tableData.Rows[0][titulo]);

                return temp;
            }
            catch (Exception ex)
            {
                string correo, asunto, cuerpo;
                correo = "javier_minches@hotmail.com";
                asunto = "Error Encontrado";
                cuerpo = "Error: " + ex.Message + "<br>" +
                         "Consulta: " + sql;

                enviarCorreo(correo, asunto, cuerpo);
                string temp = "||||||";
                return temp + ex.Message;
            }
        }
        public void cambiarContraseña(String usuario, String contraseña)
        {
            SqlConnection conexion = new SqlConnection(ConnectionString2);
            conexion.Open();
            //String comandoString = @"UPDATE FwEmployeePassword SET Pass3 = Pass2, Pass2 = Pass1, Pass1 = Password, Password = @psw, PasswordDate = GETDATE() WHERE EmployeeId = @usuario";
            String comandoString = @"UPDATE CdContraseñaUsuario SET Contraseña3=Contraseña2,Contraseña2=Contraseña1,Contraseña1=Contraseña,Contraseña=@psw,FechaContraseña=GETDATE() WHERE Usuario=@usuario";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@psw", contraseña);
            comando.Parameters.AddWithValue("@usuario", usuario);
            comando.ExecuteNonQuery();
            conexion.Close();
        }
        public int verificarNombrePsw(String nueva, String usuario)
        {

            string sql = "SELECT NombreCompleto CompleteName FROM dbo.CdUsuario  WHERE Usuario= '" + usuario + "'";
            string nombre2 = obtienePalabraSeguridad(sql, "CompleteName");
            string[] nombre3 = nombre2.Split(' ');
            int conteo3 = 0;
            foreach (string numero in nombre3)
            {

                string sql3 = "SELECT COUNT(*) conteo WHERE '" + nueva + "' like('%" + numero + "%')";
                string numero5 = obtienePalabraSeguridad(sql3, "conteo");
                int conteo6 = string.IsNullOrEmpty(numero5)?0: Convert.ToInt32(numero5);
                conteo3 = conteo3 + conteo6;

                string remplazo = numero.Replace("á", "a");
                remplazo = remplazo.Replace("é", "e");
                remplazo = remplazo.Replace("í", "i");
                remplazo = remplazo.Replace("ó", "o");
                remplazo = remplazo.Replace("ú", "u");
                remplazo = remplazo.Replace("Á", "A");
                remplazo = remplazo.Replace("É", "E");
                remplazo = remplazo.Replace("Í", "I");
                remplazo = remplazo.Replace("Ó", "O");
                remplazo = remplazo.Replace("Ú", "U");
                string sql2 = "SELECT COUNT(*) conteo WHERE '" + nueva + "' like('%" + remplazo + "%')";
                string numero4 = obtienePalabraSeguridad(sql2, "conteo");
                int conteo2 = Convert.ToInt32(numero4);
                conteo3 = conteo3 + conteo2;

            }
            return conteo3;
        }
        public void enviarCorreo(string correoDestino, string asunto, string mensaje)
        {
            SmtpClient client = new SmtpClient("smtp-mail.outlook.com");
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            System.Net.NetworkCredential credentials =
                new System.Net.NetworkCredential(correoOrigen, contraseñaCorreoOrigen);
            client.EnableSsl = true;
            client.Credentials = credentials;
            try
            {
                var mail = new MailMessage(correoOrigen.Trim(), correoDestino.Trim());
                mail.Subject = asunto;
                mail.Body = mensaje;
                client.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        public void llenarcombo(string sql, DropDownList ddl, string codigo, string descripcion)
        {
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, ConnectionString);
                DataTable datos = new DataTable();
                adapter.Fill(datos);
                ddl.DataSource = datos;
                ddl.DataValueField = codigo;
                ddl.DataTextField = descripcion;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                ddl.SelectedIndex = 0;
            }
            catch (Exception ex) {
                string correo, asunto, cuerpo;
                correo = "javier_minches@hotmail.com";
                asunto = "Error Encontrado";
                cuerpo = "Error: " + ex.Message + "<br>" +
                         "Consulta: " + sql;

                enviarCorreo(correo, asunto, cuerpo);
            }
        }
        public void llenarcombofam(string sql, DropDownList ddl, string codigo, string descripcion)
        {
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, ConnectionString2);
                DataTable datos = new DataTable();
                adapter.Fill(datos);
                ddl.DataSource = datos;
                ddl.DataValueField = codigo;
                ddl.DataTextField = descripcion;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                ddl.SelectedIndex = 0;
            }
            catch (Exception ex) {
                string correo, asunto, cuerpo;
                correo = "javier_minches@hotmail.com";
                asunto = "Error Encontrado";
                cuerpo = "Error: " + ex.Message + "<br>" +
                         "Consulta: " + sql;

                enviarCorreo(correo, asunto, cuerpo);
            }
        }


        public void llenarcombofamGen(string sql, DropDownList ddl, string codigo, string descripcion)
        {
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, ConnectionString2);
                DataTable datos = new DataTable();
                adapter.Fill(datos);
                ddl.DataSource = datos;
                ddl.DataValueField = codigo;
                ddl.DataTextField = descripcion;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                ddl.SelectedIndex = 0;
            }
            catch (Exception ex) {
                string correo, asunto, cuerpo;
                correo = "javier_minches@hotmail.com";
                asunto = "Error Encontrado";
                cuerpo = "Error: " + ex.Message + "<br>" +
                         "Consulta: " + sql;

                enviarCorreo(correo, asunto, cuerpo);
            }
        }
        public Boolean esApadrinado(String sitio, String miembro)
        {
            String sql = "SELECT COUNT(*) AS Total FROM dbo.Member M " +
                        " WHERE RecordStatus = ' ' AND Project = '" + sitio + "' AND Memberid = " + miembro + " AND AffiliationStatus = 'AFIL'";
            if (ObtenerEntero(sql, "Total") > 0)
            {
                return true;
            }
            return false;
        }
        public DataTable obtenerDatos(String sitio, String idMiembro, String idioma)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(ConnectionString3);
            conexion.Open();
            //String comandoString = @"SELECT FirstNames, LastNames, PreferredName, Gender, BirthDate, FamilyId, dbo.fn_GEN_infoUltimoAñoEscolar(@sitio, @idMiembro, @idioma) AS Grado, Semaphore, LiveDead, Literacy, Id, Telefono, Fase, AffiliationType, AffiliationStatus, AffiliationStatusDate, Edad 
            //      FROM fn_GEN_MemberGetInfoEd(@sitio, @idMiembro, @idioma, @now)";
            String comandoString = @"SELECT  M.FirstNames, M.LastNames, M.PreferredName, 
                                    CASE WHEN @idioma = 'ES' THEN cdLD.DescSpanish ELSE cdLD.DescEnglish END AS LiveDead, Case when @idioma = 'ES' then cdGen.DescSpanish else cdGen.DescEnglish end as gender, 
                                    CASE WHEN @idioma = 'ES' THEN cdAT.DescSpanish ELSE cdAT.DescEnglish END AS AffiliationType,
                                    CASE WHEN @idioma = 'ES' THEN cdAS.DescSpanish ELSE cdAS.DescEnglish END AS AffiliationStatus,
                                    CASE WHEN dbo.fn_AFIL_faseDesafil(M.Project, M.MemberId) IS NULL THEN '0'  ELSE dbo.fn_AFIL_faseDesafil(M.Project, M.MemberId)  END Fase, 
                                    (SELECT dbo.fn_GEN_FormatDate(M.AffiliationStatusDate, @idioma)) AS AffiliationStatusDate, 
                                    (SELECT dbo.fn_GEN_FormatDate(M.BirthDate, @idioma)) AS BirthDate, 
                                     CASE WHEN @idioma = 'ES' THEN dbo.fn_GEN_CalcularEdad(BirthDate) ELSE dbo.fn_GEN_CalculateAge(BirthDate) END AS Edad, M.LastFamilyId as FamilyId ,
                                    (SELECT MES.EducSemaphore FROM dbo.MemberEducationSemaphore MES WHERE MES.RecordStatus = ' ' 
                                     AND MES.EndDate IS NULL AND MES.Project = M.Project AND MES.MemberId = M.MemberId) AS Semaphore, 
                                    dbo.fn_GEN_infoUltimoAñoEscolar(@sitio, @idMiembro, @idioma) AS Grado,
                                    M.OfficialId as Id, M.CellularPhoneNumber as Telefono, Case when @idioma = 'ES' then cdLt.DescSpanish  else cdLt.DescEnglish end as Literacy
                                    FROM dbo.Member M INNER JOIN dbo.CdLiveDead cdLD ON M.LiveDead = cdLD.Code 
                                    LEFT OUTER JOIN dbo.CdAffiliationStatus cdAS ON M.AffiliationStatus = cdAS.Code 
                                    LEFT OUTER JOIN dbo.CdAffiliationType cdAT ON M.AffiliationType = cdAT.Code 
                                    LEFT OUTER JOIN dbo.MemberEducationYear MEY ON M.Project = MEY.Project AND M.MemberId = MEY.MemberId 
                                                    AND M.RecordStatus = MEY.RecordStatus AND MEY.SchoolYear = dbo.fn_BECA_ultimoAñoEscolar(M.Project, m.MemberId) AND MEY.Grade =
                                                    (SELECT MAX(MEYM.Grade) AS MaxGrade
                                                     FROM  dbo.MemberEducationYear MEYM
                                                     WHERE MEYM.RecordStatus = ' ' AND MEYM.SchoolYear = dbo.fn_BECA_ultimoAñoEscolar(M.Project, m.MemberId) AND MEYM.Project = MEY.Project AND 
                                                           MEYM.MemberId = MEY.MemberId
                                                     GROUP BY MEYM.Project, MEYM.MemberId)
                                     LEFT OUTER JOIN dbo.CdGrade cdG ON MEY.Grade = cdG.Code
                                     LEFT OUTER JOIN dbo.CdEducationStatus cdES ON MEY.Status = cdES.Code
                                     LEFT JOIN dbo.CdLiteracy cdLt on M.Literacy = cdLt.Code
                                     LEFT JOIN dbo.CdGender cdGen on M.Gender = cdGen.Code
                                     WHERE M.RecordStatus = ' ' AND M.Project = @sitio AND M.MemberId = @idMiembro";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@idMiembro", idMiembro);
            comando.Parameters.AddWithValue("@idioma", idioma);
            comando.Parameters.AddWithValue("@now", now);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }
        public DataTable obtenerActivos(String sitio, String idFamilia, String idioma)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(ConnectionString3);
            conexion.Open();
            String comandoString = @"SELECT M.MemberId, M.FirstNames + ' ' + M.LastNames AS Nombre,
                    CASE WHEN @idioma = 'es'
                        THEN cdMRT.DescSpanish 
                        ELSE cdMRT.DescEnglish
                    END Relacion,
                    CASE WHEN @idioma = 'es'
                        THEN cdAS.DescSpanish
                        ELSE cdAS.DescEnglish
                    END AfilStatus,
                    (SELECT dbo.fn_GEN_FormatDate(M.BirthDate,@idioma)) AS BirthDate,
                    CASE WHEN @idioma = 'es'
                        THEN cdAT.DescSpanish
                        ELSE cdAT.DescEnglish
                    END TipoAfil,
                    CASE WHEN @idioma = 'es'
                     THEN cdOA.DescSpanish
                     ELSE cdOA.DescEnglish
                 END OtraAfil, CellularPhoneNumber FROM dbo.Member M 
            INNER JOIN dbo.FamilyMemberRelation FMR ON M.Project = FMR.Project AND M.MemberId = FMR.MemberId AND M.LastFamilyId = FMR.FamilyId AND M.RecordStatus = FMR.RecordStatus 
            INNER JOIN dbo.CdFamilyMemberRelationType cdMRT ON FMR.Type = cdMRT.Code 
            LEFT OUTER JOIN dbo.CdOtherAffiliation cdOA ON M.OtherAffiliation = cdOA.Code 
            LEFT OUTER JOIN dbo.CdAffiliationStatus cdAS ON M.AffiliationStatus = cdAS.Code 
            LEFT OUTER JOIN dbo.CdAffiliationType cdAT ON M.AffiliationType = cdAT.Code 
            WHERE M.RecordStatus = ' ' AND M.Project = @sitio AND FMR.FamilyId = @idFamilia AND FMR.InactiveReason IS NULL
            ORDER BY cdMRT.DisplayOrder";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@idFamilia", idFamilia);
            comando.Parameters.AddWithValue("@idioma", idioma);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
          
        }
        public DataTable obtenerDatosF(String sitio, String idFamilia, String idioma)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(ConnectionString3);
            conexion.Open();
            String comandoString = @"SELECT  
                CASE WHEN @idioma='es' 
                    THEN CdGeographicArea.DescSpanish 
                ELSE CdGeographicArea.DescEnglish
                    END AS Area, Family.Pueblo, Address, TelephoneNumber AS Phone, 
                CASE WHEN @idioma='es' 
                    THEN CdEthnicity.DescSpanish 
                ELSE CdEthnicity.DescEnglish 
                END AS Etnia,
                CASE WHEN @idioma='es' 
                    THEN CdAffiliationStatus.DescSpanish 
                ELSE CdAffiliationStatus.DescEnglish 
                END AS AfilEstado, 
                (SELECT dbo.fn_GEN_FormatDate(AffiliationStatusDate,@idioma)) AS AfilEstadoDate,
                Classification,
                (SELECT dbo.fn_GEN_FormatDate(ClassificationDate,@idioma)) AS ClassifDate,
                CdGeographicPueblo.Region, RFaroNumber, EmployeeId TS, YEAR(AffiliationStatusDate) AS AfilYear
            FROM Family  LEFT JOIN CdGeographicArea ON Family.Project = CdGeographicArea.Project AND Family.Area = CdGeographicArea.Code 
            LEFT JOIN CdEthnicity ON Family.Ethnicity = CdEthnicity.Code 
            LEFT JOIN CdAffiliationStatus ON Family.AffiliationStatus = CdAffiliationStatus.Code 
            LEFT JOIN CdGeographicPueblo ON Family.Pueblo = CdGeographicPueblo.Pueblo 
			LEFT JOIN FamilyEmployeeRelation FER ON FER.RecordStatus = Family.RecordStatus AND FER.Project = Family.Project AND FER.FamilyId = Family.FamilyId  AND FER.EndDate IS NULL
            WHERE Family.FamilyId LIKE @idFamilia AND Family.RecordStatus LIKE ' ' AND Family.Project LIKE @sitio";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@idFamilia", idFamilia);
            comando.Parameters.AddWithValue("@idioma", idioma);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }
    }
}
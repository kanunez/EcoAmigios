using EcoAmigios.Class;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EcoAmigios.Forms
{
    public partial class Registro_Correo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        protected void BtCorreo_Click(object sender, EventArgs e)
        {
            int randomNumber = new Random().Next(1000, 9999);
            LabelCodigo.Text = randomNumber.ToString();
            if (TbCorreo.Text != "")
            {

                try
                {
                    string body = "" +
                        "<body>" +
                            "<h1>Bienvenido a EcoAmigos!</h1>" +
                            "<h4>Querido Usuario " + Session["Nombre_Usu"].ToString() + " " + Session["Apellido_Usu"].ToString() + ".</h4>" +
                            "<span>Le enviamos el codigo de verificacion.</span>" +
                            "<br/><br/><span>Porfavor diligenciarlo en el formulario para su registro exitoso!</span>" +
                            "<br/><br/><font size = 5>CODIGO:</font>" +
                            "<br/><br/><font size = 5>              " + randomNumber + "</font>" +
                            "<br/><br/><span>¡Buen Dia!</span>" +
                        "</body> ";

                    string EmailOrigen = "ecoamigos420@gmail.com";
                    string EmailDestino = TbCorreo.Text;
                    string contraseña = "Ecoamigos123456789";

                    MailMessage omailMessage = new MailMessage(EmailOrigen, EmailDestino, "Codigo de Verificacion", body);

                    omailMessage.IsBodyHtml = true;

                    SmtpClient osmtpClient = new SmtpClient("smtp.gmail.com");
                    osmtpClient.EnableSsl = true;
                    osmtpClient.UseDefaultCredentials = false;
                    //osmtpClient.Host = "smtp.gmail.com";
                    osmtpClient.Port = 587;
                    osmtpClient.Credentials = new System.Net.NetworkCredential(EmailOrigen, contraseña);

                    osmtpClient.Send(omailMessage);

                    osmtpClient.Dispose();

                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Revise su correo');", true);
                    BtCorreo.Visible = false;
                    TbVerificacion.Visible = true;

                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Verifique que el correo este bien escrito y que tenga conexion a internet!!!');", true);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Ingrese un correo valido');", true);
            }
        }

        protected void BtSiguiente_Click(object sender, EventArgs e)
        {
            if (TbCorreo.Text != "" || TbVerificacion.Text != "" || TbUsuario.Text != "")
            {
                //conexion MongoDB
                var client = new MongoClient("mongodb://127.0.0.1:27017");
                //Conexion base de datoa
                var database = client.GetDatabase("EcoAmigos");
                //Conexion collections
                var UsuarioCollections = database.GetCollection<Usuario>("EAV_USUARIOS");
                //Mostrar Datos
                var ListUsuario = UsuarioCollections.Find(d => d.Usuario_Usu == TbUsuario.Text).ToList();
                if (ListUsuario.Count == 0)
                {
                    if (TbVerificacion.Text == LabelCodigo.Text)
                    {
                        Session["Correo_Usu"] = TbCorreo.Text;
                        Session["Usuario_Usu"] = TbUsuario.Text;
                        Response.Redirect("Registro_Contraseña.aspx");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('El codigo de verificacion no coincide!!');", true);
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('El usuario ya existe');", true);
                }
                
            }
        }

        protected void BtRegresar_Click(object sender, EventArgs e)
        {
            Session["Correo_Usu"] = null;
            Response.Redirect("Registro.aspx");
        }
        
    }
}
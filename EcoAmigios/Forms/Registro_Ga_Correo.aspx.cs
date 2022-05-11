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
    public partial class Registro_GA_Correo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnVerificacion_Click(object sender, EventArgs e)
        {
            int randomNumber = new Random().Next(1000, 9999);
            LabelCodigo.Text = randomNumber.ToString();
            if (TbGCorreo.Text != "")
            {

                try
                {
                    string body = "" +
                        "<body>" +
                            "<h1>Bienvenido a EcoAmigos!</h1>" +
                            "<h4>Grupo Ambientalista " + Session["Nombre_Grupo"].ToString() + ".</h4>" +
                            "<span>Le enviamos el codigo de verificacion.</span>" +
                            "<br/><br/><span>Porfavor diligenciarlo en el formulario para su registro exitoso!</span>" +
                            "<br/><br/><font size = 5>CODIGO:</font>" +
                            "<br/><br/><font size = 5>              " + randomNumber + "</font>" +
                            "<br/><br/><span>¡Buen Dia!</span>" +
                        "</body> ";

                    string EmailOrigen = "ecoamigos420@gmail.com";
                    string EmailDestino = TbGCorreo.Text;
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
                    BtnVerificacion.Visible = false;
                    TbGVerificacion.Visible = true;
                    BtnSiguiente.Visible = true;

                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Verifique que el correo este bien escrito y que tenga conexion a internet!!!\n " + ex +"');", true);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Ingrese un correo valido');", true);
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Session["Correo_Grupo"] = null;
            Response.Redirect("Registro_Ga");
        }

        protected void BtnSiguiente_Click(object sender, EventArgs e)
        {
            if (TbGCorreo.Text != "" || TbGVerificacion.Text != "" )
            {
                if (TbGVerificacion.Text == LabelCodigo.Text)
                {
                    Session["Correo_Grupo"] = TbGCorreo.Text;
                    Response.Redirect("Registro_Ga_Contraseña.aspx");                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('El codigo de verificacion no coincide!!');", true);
                }
            }
        }
    }
}
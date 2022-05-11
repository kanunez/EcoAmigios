using EcoAmigios.Class;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EcoAmigios.Forms
{
    public partial class Crear_P_Grupo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TbGIdentificacion.Text = Session["Id_Grupo"].ToString();
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (ListGtipo.SelectedIndex != 0)
            {
                if (TbGNombre.Text != "" || TbGDescripcion.Text != "")
                {
                    //conexion MongoDB
                    var client = new MongoClient("mongodb://127.0.0.1:27017");
                    //Conexion base de datoa
                    var database = client.GetDatabase("EcoAmigos");
                    //Conexion collections
                    var GrupoCollections = database.GetCollection<Pagina>("EAV_PAGINA");
                    //Mostrar Datos
                    var ListGrupo = GrupoCollections.Find(d => d.Nombre_Pagina == TbGIdentificacion.Text).ToList();

                    if (ListGrupo.Count == 0)
                    {
                        var GrupoA = new Pagina() { ID_Grupo_Ambiental = Int64.Parse(Session["Id_Grupo"].ToString()), Nombre_Pagina = TbGNombre.Text, Tipo_Pagina = ListGtipo.Text, Descripcion_Pagina = TbGDescripcion.Text, Logo_Grupo = FileLogo.FileName };
                        GrupoCollections.InsertOne(GrupoA);

                        SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=EcoAmigos;Integrated Security=True");

                        try
                        {
                            conn.Open();
                            string query = "insert into EAV_PAGINA values('" + Int64.Parse(Session["Id_Grupo"].ToString()) + "','" + TbGNombre.Text + "','" + ListGtipo.Text + "','" + TbGDescripcion.Text + "','" + FileLogo.FileName + "')";
                            SqlCommand ejecutor = new SqlCommand(query, conn);
                            ejecutor.ExecuteNonQuery();

                        }
                        catch (Exception ex)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + ex + "');", true);
                        }
                        finally
                        {
                            conn.Close();
                        }
                        Response.Redirect("Inicio_Grupo.aspx");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Ya existe una pagina con este nombre!!!');", true);
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Recuerde llenar todos los datos!!!');", true);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Seleccione un tipo de pagina!!');", true);
            }
        }

        protected void BtnRegresar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Inicio_Grupo.aspx");
        }
    }
}
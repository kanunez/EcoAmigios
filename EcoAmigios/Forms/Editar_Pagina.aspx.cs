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
    public partial class Editar_Pagina : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            if(UploadP.HasFile == true)
            {
                LabelImg.Text = UploadP.FileName.ToString();
                ImageP.ImageUrl = "~/Imagenes/" + LabelImg.Text;
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Seleccione la imagen a cambiar');", true);
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            
                if ( TbDescP.Text != "" && tipo_pagina.SelectedIndex != 0)
                {
                    //conexion MongoDB
                    var client = new MongoClient("mongodb://127.0.0.1:27017");
                    //Conexion base de datoa
                    var database = client.GetDatabase("EcoAmigos");
                    //Conexion collections
                    var GrupoCollections = database.GetCollection<Pagina>("EAV_PAGINA");
                    //Mostrar Datos
                    var ListGrupo = GrupoCollections.Find(d => d.Nombre_Pagina == Session["Nombre_Pag"].ToString()).ToList();
                    //agregar datos
                    var Pagina = new Pagina() {id = ListGrupo[0].id, Nombre_Pagina = TbNombreP.Text, Descripcion_Pagina = TbDescP.Text, ID_Grupo_Ambiental = ListGrupo[0].ID_Grupo_Ambiental, Logo_Grupo = LabelImg.Text, Tipo_Pagina = tipo_pagina.Text };
                    //Actualizar Datos
                    GrupoCollections.ReplaceOne(d => d.Nombre_Pagina == TbNombreP.Text,Pagina);


                    SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=EcoAmigos;Integrated Security=True");
                    SqlCommand cmd = new SqlCommand("UPDATE EAV_PAGINA SET Descripcion_Pagina = '" + TbDescP.Text + "',Tipo_Pagina = '" + tipo_pagina.Text + "', Logo_Grupo = '" + LabelImg.Text + "' WHERE Nombre_Pagina = '" + TbNombreP.Text + "'", conn);
                    conn.Open();

                    try
                    {
                        cmd.ExecuteNonQuery();
                        Response.Redirect("Inicio_Grupo.aspx");
                    }
                    catch (Exception EX)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + EX + "');", true);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No puede dejar espacios en blanco');", true);
                }
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            if(TbTituloP.Text != "" || TbContP.Text != "" || UploadPubli.HasFile == true)
            {
                //conexion MongoDB
                var client = new MongoClient("mongodb://127.0.0.1:27017");
                //Conexion base de datoa
                var database = client.GetDatabase("EcoAmigos");
                //Conexion collections
                var PubliCollections = database.GetCollection<Publicacion>("EAV_PUBLIBACIONES");
                //Mostrar Datos
                var ListGrupo = PubliCollections.Find(d => d.Titutlo_Publi == TbTituloP.Text).ToList();

                if (ListGrupo.Count == 0)
                {
                    //agregar datos
                    var Publicacion = new Publicacion() { Nombre_Pag = Session["Nombre_Pag"].ToString(), Titutlo_Publi = TbTituloP.Text, Contenido_Publi = TbContP.Text, Logo_Publi = UploadPubli.FileName.ToString() };
                    PubliCollections.InsertOne(Publicacion);

                    SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=EcoAmigos;Integrated Security=True");
                    try
                    {
                        conn.Open();
                        string query = "insert into EAV_PUBLIBACIONES values('" + Session["Nombre_Pag"].ToString() + "','" + TbTituloP.Text + "','" + TbContP.Text + "','" + UploadPubli.FileName.ToString() + "')";
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
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Esta publicacion ya existe!!');", true);
                }
                
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Recuerda no dejar espacios en blanco, tampoco olvides cargar la imagen!!');", true);
            }
        }

        protected void BtnGuardarEvento_Click(object sender, EventArgs e)
        {
            if (TbTituloEvento.Text != "" || TbContEvento.Text != "" || TbFecha.Text != "")
            {
                //conexion MongoDB
                var client = new MongoClient("mongodb://127.0.0.1:27017");
                //Conexion base de datoa
                var database = client.GetDatabase("EcoAmigos");
                //Conexion collections
                var EventosCollections = database.GetCollection<Eventos>("EAV_EVENTOS");
                //Mostrar Datos
                var ListGrupo = EventosCollections.Find(d => d.Titutlo_Evento == TbTituloEvento.Text).ToList();

                if(ListGrupo.Count == 0)
                {
                    //agregar datos
                    var Eventos = new Eventos() { Nombre_Pag = Session["Nombre_Pag"].ToString(), Titutlo_Evento = TbTituloEvento.Text, Contenido_Evento = TbContEvento.Text, Fecha_Evento = TbFecha.Text };
                    EventosCollections.InsertOne(Eventos);

                    SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=EcoAmigos;Integrated Security=True");
                    try
                    {
                        conn.Open();
                        string query = "insert into EAV_EVENTOS values('" + Session["Nombre_Pag"].ToString() + "','" + TbTituloEvento.Text + "','" + TbContEvento.Text + "','" + TbFecha.Text + "')";
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
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Este Evento ya existe!!');", true);
                }
                
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Recuerda no dejar espacios en blanco, tampoco olvides cargar la imagen!!');", true);
            }
        }

        protected void IBEPagina_Click(object sender, ImageClickEventArgs e)
        {
            //conexion MongoDB
            var client = new MongoClient("mongodb://127.0.0.1:27017");
            //Conexion base de datoa
            var database = client.GetDatabase("EcoAmigos");
            //Conexion collections
            var GrupoCollections = database.GetCollection<Pagina>("EAV_PAGINA");
            //Mostrar Datos
            var ListGrupo = GrupoCollections.Find(d => d.Nombre_Pagina == Session["Nombre_Pag"].ToString()).ToList();

            TbNombreP.Text = ListGrupo[0].Nombre_Pagina.ToString();
            LabelImg.Text = ListGrupo[0].Logo_Grupo.ToString();
            MultiView1.ActiveViewIndex = 1;
        }

        protected void IBEEvePubli_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.ActiveViewIndex = 3;
        }

        protected void IBRegresar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Inicio_Grupo.aspx");
        }

        protected void IBCPublioEve_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.ActiveViewIndex = 2;
        }

        protected void IBRegresar1_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }

        protected void BtnGuardarPubli_Click(object sender, EventArgs e)
        {
            LabeltiP.Text = DropDownListPublicacion.Text;
            
            SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=EcoAmigos;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("UPDATE EAV_PUBLIBACIONES SET ContenidoPubli = '" + TbContP0.Text + "' WHERE Titulo_Publi = '" + LabeltiP.Text + "'", conn);
            conn.Open();

            try
            {
                cmd.ExecuteNonQuery();
                Response.Redirect("Inicio_Grupo.aspx");
            }
            catch (Exception EX)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + EX + "');", true);
            }
            finally
            {
                conn.Close();
            }
            
        }

        protected void BtnGuardarEventos_Click(object sender, EventArgs e)
        {
            Labelti.Text = DropDownListTituloE.Text;

            SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=EcoAmigos;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("UPDATE EAV_EVENTOS SET Contenido_Evento = '" + TbContEvento0.Text + "', Fecha_Evento = '" + TbFecha0.Text + "' WHERE Titulo_Evento = '" + Labelti.Text + "'", conn);
            conn.Open();

            try
            {
                cmd.ExecuteNonQuery();
                Response.Redirect("Inicio_Grupo.aspx");
            }
            catch(Exception EX)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + EX + "');", true);
            }
            finally
            {
                conn.Close();
            }
        }

        protected void btnBorrar_Click(object sender, EventArgs e)
        {
            //conexion MongoDB
            var client = new MongoClient("mongodb://127.0.0.1:27017");
            //Conexion base de datoa
            var database = client.GetDatabase("EcoAmigos");
            //Conexion collections
            var GrupoCollections = database.GetCollection<Pagina>("EAV_PAGINA");
            var PubliCollections = database.GetCollection<Publicacion>("EAV_PUBLIBACIONES");
            var EveCollections = database.GetCollection<Publicacion>("EAV_EVENTOS");
            //Mostrar Datos
            var ListGrupo = GrupoCollections.Find(d => d.Nombre_Pagina == Session["Nombre_Pag"].ToString()).ToList();
            //Borrar Datos
            GrupoCollections.DeleteOne(d => d.Nombre_Pagina == Session["Nombre_Pag"].ToString());
            PubliCollections.DeleteOne(d => d.Nombre_Pag == Session["Nombre_Pag"].ToString());
            EveCollections.DeleteOne(d => d.Nombre_Pag == Session["Nombre_Pag"].ToString());

            SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=EcoAmigos;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("DELETE FROM EAV_PAGINA WHERE Nombre_Pagina = '" + Session["Nombre_Pag"].ToString() + "'" +
                "DELETE FROM EAV_EVENTOS WHERE Nombre_Pag = '" + Session["Nombre_Pag"].ToString() + "'" +
                "DELETE FROM EAV_PUBLIBACIONES WHERE Nombre_Pag = '" + Session["Nombre_Pag"].ToString() + "'", conn);
            conn.Open();

            try
            {
                cmd.ExecuteNonQuery();
                Response.Redirect("Inicio_Grupo.aspx");

            }
            catch (Exception EX)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + EX + "');", true);
            }
            finally
            {
                conn.Close();
            }
        }

        protected void btnBorrarP_Click(object sender, EventArgs e)
        {
            string titulop = DropDownListPublicacion.Text;
            //conexion MongoDB
            var client = new MongoClient("mongodb://127.0.0.1:27017");
            //Conexion base de datoa
            var database = client.GetDatabase("EcoAmigos");
            //Conexion collections
            var GrupoCollections = database.GetCollection<Publicacion>("EAV_PUBLIBACIONES");
            //Borrar Datos
            GrupoCollections.DeleteOne(d => d.Titutlo_Publi == titulop);

            SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=EcoAmigos;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("DELETE FROM EAV_PUBLIBACIONES WHERE Titulo_Publi = '" + titulop + "'", conn);
            conn.Open();

            try
            {
                cmd.ExecuteNonQuery();
                Response.Redirect("Inicio_Grupo.aspx");
            }
            catch (Exception EX)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + EX + "');", true);
            }
            finally
            {
                conn.Close();
            }
        }

        protected void btnBorrarE_Click(object sender, EventArgs e)
        {
            //conexion MongoDB
            var client = new MongoClient("mongodb://127.0.0.1:27017");
            //Conexion base de datoa
            var database = client.GetDatabase("EcoAmigos");
            //Conexion collections
            var GrupoCollections = database.GetCollection<Eventos>("EAV_EVENTOS");
            //Borrar Datos
            GrupoCollections.DeleteOne(d => d.Titutlo_Evento == DropDownListTituloE.Text);

            SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=EcoAmigos;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("DELETE FROM EAV_EVENTOS WHERE Titulo_Evento = '" + DropDownListTituloE.Text + "'", conn);
            conn.Open();

            try
            {
                cmd.ExecuteNonQuery();
                Response.Redirect("Inicio_Grupo.aspx");
            }
            catch (Exception EX)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + EX + "');", true);
            }
            finally
            {
                conn.Close();
            }
        }

        protected void IBVer_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Ver_Pagina.aspx");
        }
    }
}
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
            //conexion MongoDB
            var client = new MongoClient("mongodb://127.0.0.1:27017");
            //Conexion base de datoa
            var database = client.GetDatabase("EcoAmigos");
            //Conexion collections
            var GrupoCollections = database.GetCollection<Pagina>("EAV_PAGINA");
            //Mostrar Datos
            var ListGrupo = GrupoCollections.Find(d => d.Nombre_Pagina == Session["Nombre_Pag"].ToString()).ToList();

            tipo_pagina.Text = ListGrupo[0].Tipo_Pagina.ToString();
            TbNombreP.Text = ListGrupo[0].Nombre_Pagina.ToString();
            TbDescP.Text = ListGrupo[0].Descripcion_Pagina.ToString();
            ImageP.ImageUrl = "~/Imagenes/" + ListGrupo[0].Logo_Grupo.ToString();
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
            if (LabelImg.Text != "")
            {
                if (tipo_pagina.SelectedIndex != 0 || TbNombreP.Text != "" || TbDescP.Text != "")
                {
                    //conexion MongoDB
                    var client = new MongoClient("mongodb://127.0.0.1:27017");
                    //Conexion base de datoa
                    var database = client.GetDatabase("EcoAmigos");
                    //Conexion collections
                    var GrupoCollections = database.GetCollection<Pagina>("EAV_PAGINA");
                    //Mostrar Datos
                    var ListGrupo = GrupoCollections.Find(d => d.Nombre_Pagina == Session["Nombre_Pag"].ToString()).ToList();
                    //Datos
                    var Pagina = new Pagina() { id = ListGrupo[0].id.ToString(), ID_Grupo_Ambiental = Int64.Parse(ListGrupo[0].ID_Grupo_Ambiental.ToString()), Nombre_Pagina = TbNombreP.Text, Descripcion_Pagina = TbDescP.Text, Logo_Grupo = LabelImg.Text };
                    //Actualizar
                    GrupoCollections.ReplaceOne(d => d.Nombre_Pagina == Session["Nombre_Pag"].ToString(), Pagina);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No puede dejar campos en blanco!!');", true);
                }
            }
            else
            {
                if (TbNombreP.Text != "" || TbDescP.Text != "")
                {
                    //conexion MongoDB
                    var client = new MongoClient("mongodb://127.0.0.1:27017");
                    //Conexion base de datoa
                    var database = client.GetDatabase("EcoAmigos");
                    //Conexion collections
                    var GrupoCollections = database.GetCollection<Pagina>("EAV_PAGINA");
                    //Mostrar Datos
                    var ListGrupo = GrupoCollections.Find(d => d.Nombre_Pagina == Session["Nombre_Pag"].ToString()).ToList();
                    //Datos
                    var Pagina = new Pagina() { id = ListGrupo[0].id.ToString(), ID_Grupo_Ambiental = Int64.Parse(ListGrupo[0].ID_Grupo_Ambiental.ToString()), Nombre_Pagina = TbNombreP.Text, Descripcion_Pagina = TbDescP.Text, Logo_Grupo = ListGrupo[0].Logo_Grupo.ToString() };
                    //Actualizar
                    GrupoCollections.ReplaceOne(d => d.Nombre_Pagina == Session["Nombre_Pag"].ToString(), Pagina);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No puede dejar campos en blanco!!');", true);
                }
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

        protected void BtnMostrarPublicacion_Click(object sender, EventArgs e)
        {
            //conexion MongoDB
            var client = new MongoClient("mongodb://127.0.0.1:27017");
            //Conexion base de datoa
            var database = client.GetDatabase("EcoAmigos");
            //Conexion collections
            var PubliCollections = database.GetCollection<Publicacion>("EAV_PUBLIBACIONES");
            //Mostrar Datos
            var ListGrupo = PubliCollections.Find(d => d.Titutlo_Publi == DropDownListPublicacion.Text).ToList();

            TbContP0.Text = ListGrupo[0].Contenido_Publi.ToString();
            
            Response.Redirect("Inicio_Grupo.aspx");
        }

        protected void BtnMostrarEvento_Click(object sender, EventArgs e)
        {
            //conexion MongoDB
            var client = new MongoClient("mongodb://127.0.0.1:27017");
            //Conexion base de datoa
            var database = client.GetDatabase("EcoAmigos");
            //Conexion collections
            var EventoCollections = database.GetCollection<Eventos>("EAV_EVENTOS");
            //Mostrar Datos
            var ListEve = EventoCollections.Find(d => d.Titutlo_Evento == DropDownListTituloE.Text).ToList();

            TbFecha0.Text = ListEve[0].Fecha_Evento.ToString();
            Tipo_Evento0.Text = ListEve[0].Titutlo_Evento.ToString();
            TbContEvento0.Text = ListEve[0].Contenido_Evento.ToString();
        }

        protected void BtnGuardarPubli_Click(object sender, EventArgs e)
        {
            var client = new MongoClient("mongodb://127.0.0.1:27017");
            //Conexion base de datoa
            var database = client.GetDatabase("EcoAmigos");
            //Conexion collections
            var PubliCollections = database.GetCollection<Publicacion>("EAV_PUBLICACION");
            //Mostrar
            var ListPubli = PubliCollections.Find(d => d.Titutlo_Publi == DropDownListPublicacion.Text).ToList();
            //Datos
            var publicacion = new Publicacion() { id = ListPubli[0].id.ToString(), Titutlo_Publi = DropDownListPublicacion.Text, Logo_Publi = ListPubli[0].Logo_Publi.ToString(), Nombre_Pag = ListPubli[0].Nombre_Pag.ToString() };
            //Actualizar
            PubliCollections.ReplaceOne(d => d.Titutlo_Publi == DropDownListPublicacion.ToString(), publicacion);
            SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=EcoAmigos;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("UPDATE EAV_PUBLIBACIONES SET(Nombre_Pag = '" + ListPubli[0].Nombre_Pag.ToString() + "', Titulo_Publi = '" + DropDownListPublicacion.Text + "', ContenidoPubli = '" + TbContP0.Text + "', Logo_Publi = '" + ListPubli[0].Logo_Publi.ToString() + "' WHERE Titulo_Publi = '" + DropDownListPublicacion.Text + "')", conn);
            conn.Open();

            try
            {
                cmd.ExecuteNonQuery();
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
            var client = new MongoClient("mongodb://127.0.0.1:27017");
            //Conexion base de datoa
            var database = client.GetDatabase("EcoAmigos");
            //Conexion collections
            var EveCollections = database.GetCollection<Eventos>("EAV_EVENTOS");
            //Mostrar
            var ListEve = EveCollections.Find(d => d.Titutlo_Evento == DropDownListTituloE.Text).ToList();
            //Datos
            var Eventos = new Eventos() { id = ListEve[0].id.ToString(), Titutlo_Evento = DropDownListTituloE.Text, Fecha_Evento = TbFecha0.Text, Nombre_Pag = ListEve[0].Nombre_Pag.ToString(), Contenido_Evento = TbContEvento0.Text };
            //Actualizar
            EveCollections.ReplaceOne(d => d.Titutlo_Evento == DropDownListTituloE.ToString(), Eventos);
            SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=EcoAmigos;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("UPDATE EAV_EVENTOS SET (Nombre_Pag = '" + ListEve[0].Nombre_Pag.ToString() + "',Titulo_Evento = '" + DropDownListTituloE.Text + "',Contenido_Evento = '" + TbContEvento0.Text + "',Fecha_Evento = '" + TbFecha0.Text + "') WHERE Titulo_Evento = '" + DropDownListTituloE.Text + "'", conn);
            conn.Open();

            try
            {
                cmd.ExecuteNonQuery();
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
    }
}
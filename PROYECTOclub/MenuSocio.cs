using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace PROYECTOclub
{
    public partial class MenuSocio : Form
    {
        
        MySqlConnection Conexion = null;
        String Cadenaconexion;
        /*Estros dos parametros siempre deben de existir, en todos tus form*/
        String correo = ""; //En esta variable se va a guardar el correo que envies desde el login
        int perfil = 0; //El '2' será para socios, el '1' para admin
        //String id_actividad = "";
        public static String imagenfoto; //variable global, la podemos usar en cualquier lado
        public static String nombre_usuario; //la usamos para listas y generar credencial y esto es el correo del usuario

        public MenuSocio(String correo, int perfil)
        {
            InitializeComponent();

            this.correo = correo; //La variable de arriba 'correo' recibe el correo de tu form login
            this.perfil = perfil; //La variable perfil en este caso resibe un 2 porque es socio
            nombre_usuario = correo;
            recuperUsuario(correo); //mandamos a traer el médoto reuperarUsusario()
            pictureBox1.Image = Image.FromFile(imagenfoto);
            
            
        }

        private void generarCredencialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenerarCredencial generarcredencial = new GenerarCredencial(correo, perfil);
            generarcredencial.MdiParent = this;
            generarcredencial.Show();
            //this.Hide();

        }

        private void listaDeCompañerosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListaCompaneros listacompaneros = new ListaCompaneros(correo, perfil);
            listacompaneros.MdiParent = this;
            listacompaneros.Show();
            //this.Hide();
        }
        
        
        private void MenuSocio_Load(object sender, EventArgs e)
        {
            label1.Text = correo;

            Conexion = new MySqlConnection();


            Cadenaconexion = "Server=localhost;User id=root; Database=proyecto; Password=";
            Conexion.ConnectionString = Cadenaconexion;


            MySqlCommand comandobus = new MySqlCommand("Select nombre from socio where correo= '" + correo + "'");
            comandobus.Connection = Conexion;
            Conexion.Open();

            MySqlDataReader myreader = comandobus.ExecuteReader(); //busca en cada fil aque exista los datos 

            try
            {
                if (myreader.HasRows)  //Lee las filas
                {
                    while (myreader.Read())
                    {
                        try
                        {
                          
                            label3.Text = myreader.GetString(0);
                        }
                        catch (System.ArgumentException)
                        {
                            MessageBox.Show("Error", "Consulta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }

                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Se ha producido un error " + err + " ");
            }
            Conexion.Close();
            
        }

        public void recuperUsuario(String correo)
        {


            MySqlConnection conexion = null;
            conexion = new MySqlConnection();
            String cadenaconexion = "Server=localhost;User id=root; Database=proyecto;Password=;";//preparar la ruta
            conexion.ConnectionString = cadenaconexion;
            MySqlCommand bus = new MySqlCommand("Select foto from socio where correo='" + correo + "'");
            bus.Connection = conexion;
            conexion.Open();
            MySqlDataReader myreader = bus.ExecuteReader();
            try
            {
                if (myreader.HasRows)
                {
                    while (myreader.Read())
                    {
                        imagenfoto = myreader[0].ToString();
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("" + error);
            }
            conexion.Close();
            

        }

        private void opcionesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cerrarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea cerrar sesión?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Form1 loi = new Form1();
                loi.Show();
                this.Hide();
                //generarcredencial.Close();
                //listacompaneros.Close();
            } 
            
        }

        private void salirDeAplicaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("¿Desea salir de la aplicación?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }   
        }
    }
}

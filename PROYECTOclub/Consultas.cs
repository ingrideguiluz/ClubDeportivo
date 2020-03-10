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
    public partial class Consultas : Form
    {

        String correo = ""; //En esta variable se va a guardar el correo que envies desde el login
        //int perfil = 0; //El '2' será para socios, el '1' para admin

        public Consultas()
        {
            InitializeComponent();
        }
        string elimina = "";
        private void btnAdmin_Click(object sender, EventArgs e)
        {
            elimina = "admin";
            MySqlConnection Conexion = new MySqlConnection();
            string Cadenaconexion;
            Cadenaconexion = "Server=localhost;User id=root; Database=proyecto;Password=;";

            Conexion.ConnectionString = Cadenaconexion;
            Conexion.Open();
            MySqlDataAdapter comandobus = new MySqlDataAdapter("SELECT nombre,fecha_nac,correo,telefono from admin ", Conexion);
            DataTable dt = new System.Data.DataTable();
            comandobus.Fill(dt);
            dataConsul.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = dataConsul.Rows.Add();


                dataConsul.Rows[n].Cells[0].Value = false;
                dataConsul.Rows[n].Cells[1].Value = item["nombre"].ToString();
                dataConsul.Rows[n].Cells[2].Value = item["fecha_nac"].ToString();
                dataConsul.Rows[n].Cells[3].Value = item["correo"].ToString();
                dataConsul.Rows[n].Cells[6].Value = item["telefono"].ToString();

            }
            Conexion.Close();

        }

        private void btnSocio_Click(object sender, EventArgs e)
        {
            elimina = "socio";
            MySqlConnection Conexion = new MySqlConnection();
            string Cadenaconexion;
            Cadenaconexion = "Server=localhost;User id=root; Database=proyecto;Password=;";

            Conexion.ConnectionString = Cadenaconexion;
            Conexion.Open();
            MySqlDataAdapter comandobus = new MySqlDataAdapter("SELECT nombre,fecha_nac,correo,id_actividad,horario,telefono,fecha_insc,estatus from socio ", Conexion);
            DataTable datos = new System.Data.DataTable();
            comandobus.Fill(datos);
            dataConsul.Rows.Clear();
            foreach (DataRow item in datos.Rows)
            {
                int n = dataConsul.Rows.Add();


                dataConsul.Rows[n].Cells[0].Value = false;
                dataConsul.Rows[n].Cells[1].Value = item["nombre"].ToString();
                dataConsul.Rows[n].Cells[2].Value = item["fecha_nac"].ToString();
                dataConsul.Rows[n].Cells[3].Value = item["correo"].ToString();
                dataConsul.Rows[n].Cells[4].Value = recuperActividad(Convert.ToInt32(item["id_actividad"]));
                dataConsul.Rows[n].Cells[5].Value = item["horario"].ToString();
                dataConsul.Rows[n].Cells[6].Value = item["telefono"].ToString();
                dataConsul.Rows[n].Cells[7].Value = item["fecha_insc"].ToString();
                if (item["estatus"].ToString() == "1")
                {
                    dataConsul.Rows[n].Cells[8].Value = "Pagado";
                }
                else if (item["estatus"].ToString() == "0")
                {
                    dataConsul.Rows[n].Cells[8].Value = "Debe";
                }
            }
            Conexion.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*MenuAdministrador adm = new MenuAdministrador(correo, perfil);
            adm.Show();
            this.Hide();*/
            this.Close();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            MySqlConnection Conexion = new MySqlConnection();
            string Cadenaconexion;
            Cadenaconexion = "Server=localhost;User id=root; Database=proyecto;Password=;";
            Conexion.ConnectionString = Cadenaconexion;
            //string correo = "" ;
            DialogResult result = MessageBox.Show("Estas seguro que deseas eliminar los campos seleccionados?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                
                foreach (DataGridViewRow item in dataConsul.Rows)
                {
                    DataGridViewCheckBoxCell check = (DataGridViewCheckBoxCell)item.Cells[0];
                    if (Convert.ToBoolean(check.Value))
                    {
                        correo = Convert.ToString(item.Cells[3].Value);
                    }
                    //MessageBox.Show(correo);
                    //MessageBox.Show(elimina);
                    Conexion.Open();
                    MySqlCommand cmd = new MySqlCommand("delete from " + elimina + " where correo = '" + correo + "';");
                    cmd.Connection = Conexion;
                    cmd.ExecuteScalar();

                    Conexion.Close();
                }
                MessageBox.Show("Elementos eliminados con exito");
                dataConsul.Rows.Clear();
                //btnSocio_Click(sender, e);

            }

            else if (result == DialogResult.No)
            {
                MessageBox.Show("Asegurese de quien desea eliminar");
            }
        }

        private void Consultas_Load(object sender, EventArgs e)
        {

        }

        public String recuperActividad(int id_actividad)
        {

            String actividad = "";
            MySqlConnection conexion = null;
            conexion = new MySqlConnection();
            String cadenaconexion = "Server=localhost;User id=root; Database=proyecto;Password=;";//preparar la ruta
            conexion.ConnectionString = cadenaconexion;
            MySqlCommand bus = new MySqlCommand("Select nombre from actividad where  id_actividad=" + id_actividad + ";");
            bus.Connection = conexion;
            conexion.Open();
            MySqlDataReader myreader = bus.ExecuteReader();
            try
            {
                if (myreader.HasRows)
                {
                    while (myreader.Read())
                    {
                        actividad = myreader[0].ToString();
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("" + error);
            }
            conexion.Close();
            return actividad;

        }
    }
}

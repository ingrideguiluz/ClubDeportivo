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
    public partial class BuscarSocio : Form
    {
        String correo = ""; //En esta variable se va a guardar el correo que envies desde el login
        int perfil = 0; //El '2' será para socios, el '1' para admin
        public static String actividad = "";
        
        public BuscarSocio(String correoSocio, int perfil)
        {
            InitializeComponent();
            //CargaComboActividades();
            this.correo = correoSocio; //La variable de arriba 'correo' recibe el correo de tu form login
            this.perfil = perfil; //La variable perfil en este caso resibe un 1 porque es admin

        }
        
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            txtID.Text = "";
            txtCorreo.Text = "";
            txtNombre.Text = "";
            cmbPregunta.Text = null;
            cmbHorario.Text = null;
            txtRespuesta.Text = "";
            txtContra.Text = "";
            txtInscripcion.Text = "";
            txtTel.Text = "";
            cmbAct.Text = null;
            txtNacimiento.Text = "";
            
            //string inscripcion = dtpNac.Value.ToString("yyyy-MM-dd");
            //string fechaNac = dtpNac.Value.ToString("yyyy-MM-dd");

            String correo = txtBuscaCorreo.Text;

            MySqlConnection Conexion = new MySqlConnection();
            String Cadenaconexion;
            Cadenaconexion = "Server=localhost;User id=root;Database=proyecto;Password=;";
            Conexion.ConnectionString = Cadenaconexion;

            //MySqlCommand comandobus = new MySqlCommand("Select id_socio,nombre,correo,CONVERT(aes_decrypt(contrasena,aes_decrypt(respuesta ,'root'))USING utf8),CAST(aes_decrypt(pregunta,'" + correo + "')AS CHAR) AS pregunta,CONVERT(aes_decrypt(respuesta ,'root')USING utf8),fecha_nac,telefono,horario,id_actividad,fecha_insc,estatus,foto From socio WHERE  correo ='" + correo + "';");
            MySqlCommand comandobus = new MySqlCommand("Select s.id_socio,s.nombre,s.correo,CONVERT(aes_decrypt(s.contrasena,aes_decrypt(s.respuesta ,'root'))USING utf8),CAST(aes_decrypt(s.pregunta,'" + correo + "')AS CHAR) AS pregunta,CONVERT(aes_decrypt(s.respuesta ,'root')USING utf8),s.fecha_nac,s.telefono,s.horario,a.id_actividad,s.fecha_insc,s.estatus,s.foto,s.id_actividad From actividad a,socio s WHERE  correo ='" + correo + "' AND a.id_actividad=s.id_actividad;");

            comandobus.Connection = Conexion;
            Conexion.Open();
            MySqlDataReader myreader = comandobus.ExecuteReader();
            CargaComboActividades();
            try
            {

                if (myreader.HasRows)
                {
                    while (myreader.Read())
                    {
                        DateTime fecha1 = DateTime.Parse(myreader.GetValue(6).ToString()); //linea de fecha de nacimiento
                        DateTime fecha2 = DateTime.Parse(myreader.GetValue(10).ToString());//linea de fecha de inscripción
                        txtID.Text = myreader.GetValue(0).ToString();
                        txtNombre.Text = myreader.GetValue(1).ToString();
                        txtCorreo.Text = myreader.GetValue(2).ToString();
                        txtContra.Text = myreader.GetValue(3).ToString();
                        cmbPregunta.Text = myreader.GetValue(4).ToString();
                        txtRespuesta.Text = myreader.GetValue(5).ToString();
                        txtNacimiento.Text = fecha1.ToString("dd-MM-yyyy"); //linea de fecha de nacimiento
                        txtTel.Text = myreader.GetValue(7).ToString();
                        //cmbHorario.Text = myreader.GetValue(8).ToString();
                        cmbHorario.SelectedItem = myreader.GetValue(8).ToString();
                        //cmbAct.Text = (myreader.GetValue(9).ToString());
                        cmbAct.SelectedValue = (myreader.GetValue(9).ToString()); 
                        txtInscripcion.Text = fecha2.ToString("dd-MM-yyyy");   //linea de fecha de inscripción
                        
                        //txtAct.Text = Actividad();

                        string estatus = myreader.GetValue(11).ToString();
                        if (estatus == "1")
                        {
                            rbtPagado.Checked = true;
                            //rbtAdeudo.Visible = false;
                        }
                        else if(estatus=="0")
                        {
                            rbtAdeudo.Checked = true;
                            //rbtPagado.Visible = false;
                        }
                        pictureBox1.Image = Image.FromFile(Convert.ToString(myreader.GetValue(12)));


                    }
                }
                else
                {
                    MessageBox.Show("Usuario Incorrecto");
                }

            }
            catch (Exception err)
            {

                MessageBox.Show("se ha producido un error" + err + "");
            }
            Conexion.Close();
            
           
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            string id = txtID.Text;
            string correo = txtCorreo.Text;
            string nombre = txtNombre.Text;
            string pregunta = cmbPregunta.Text;
            string respuesta = txtRespuesta.Text;
            string contrasena = txtContra.Text;
            string inscripcion = txtInscripcion.Text;
            string telefono = txtTel.Text;
            string horario = cmbHorario.Text;         
            string fechaNac = txtNacimiento.Text;
            string sIdAct = "";
            
            int estatus = 0;
            if (rbtPagado.Checked == true)
            {
                estatus = 1;
            }
            else if (rbtAdeudo.Checked == true)
            {
                estatus = 0;
            }
            if (cmbAct.SelectedIndex == -1)
            {
                MessageBox.Show("No hay ninguna actividad seleccionada", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                sIdAct = cmbAct.SelectedValue.ToString();
                Modifica(id, correo, nombre, pregunta, respuesta, contrasena, inscripcion, telefono, horario, fechaNac, sIdAct, estatus);
            }
        }

        private void Modifica(String psIdSocio, String psCorreo, String psNombre, String psPregunta, String psRespuesta, String psContra, String psIns, String psTel, String psHorario, String psFechaNac, String psIdAct, int piEstatus)
        {
            MySqlConnection conexion = new MySqlConnection();
            string Cadenaconexion;
            Cadenaconexion = "Server=localhost;User id=root; Database=proyecto;Password=;";
            conexion.ConnectionString = Cadenaconexion;

            DialogResult resultado = MessageBox.Show("¿Desea modificar el usuario: " + txtBuscaCorreo.Text, "Seguro", MessageBoxButtons.OKCancel);
            if (resultado == DialogResult.OK)
            {
                try
                {
                    conexion.Open();
                    string cadena = ("UPDATE socio SET nombre='" + psNombre + "', contrasena=aes_encrypt('" + psContra + "','" + psRespuesta + "'),pregunta=aes_encrypt('" + psPregunta + "','" + psCorreo + "'),respuesta=aes_encrypt('" + psRespuesta + "','root'),telefono='" + psTel + "',horario='" + psHorario + "',id_actividad='" + psIdAct + "',fecha_nac='" + psFechaNac + "',fecha_insc='" + psIns + "',estatus='" + piEstatus + "' WHERE id_socio='" + psIdSocio + "';");

                    //string cadena = ("UPDATE socio SET correo='" + correo + "',nombre='" + nombre + "', contrasena=aes_encrypt('" + contrasena + "','" + respuesta + "'),pregunta=aes_encrypt('" + pregunta + "','" + correo + "'),respuesta=aes_encrypt('" + respuesta + "','root'),telefono='" + telefono + "',horario='" + horario + "',id_actividad='" + actividad + "' WHERE id_socio='" + id + "';");
                    MySqlCommand comando = new MySqlCommand(cadena, conexion);

                    comando.Connection = conexion;
                    comando.ExecuteNonQuery();

                    MessageBox.Show("Se han modificado correctamente");
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Error de tipo: " + ex);
                }
                conexion.Close();

            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            MySqlConnection conexion = new MySqlConnection();
            string Cadenaconexion;
            Cadenaconexion = "Server=localhost;User id=root; Database=proyecto;Password=;";
            conexion.ConnectionString = Cadenaconexion;

            DialogResult resultado = MessageBox.Show("¿Desea ELIMINAR el usuario: " + txtBuscaCorreo.Text, "Seguro", MessageBoxButtons.OKCancel);
            if (resultado == DialogResult.OK)
            {
                try
                {
                    conexion.Open();
                    string cadena = ("DELETE FROM socio WHERE correo='" + txtBuscaCorreo.Text + "';");
                    MySqlCommand comando = new MySqlCommand(cadena, conexion);

                    comando.Connection = conexion;
                    

                    comando.ExecuteNonQuery();

                    MessageBox.Show("Se ha eliminado correctamente");
                    limpiarCampos();
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Error de tipo: " + ex);
                }
                conexion.Close();

            }

        }
        private void limpiarCampos()
        {
            txtID.Text = "";
            txtCorreo.Text = "";
            txtNombre.Text = "";
            cmbPregunta.Text = ("");
            txtRespuesta.Text = "";
            txtInscripcion.Text = "";
            txtTel.Text = "";
            cmbHorario.Text = "";
            cmbAct.Text = "";
            txtNacimiento.Text = "";
            txtContra.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /**MenuAdministrador ma = new MenuAdministrador(correo, perfil);
            ma.Show();
            this.Hide();*/
            this.Close();
        }
        private void CargaComboActividades()
        {
            //Descripcion: Cargar el combobox de actividades dependiendo del horario que se elija, cuando el programa inicie, no habra ningun horario seleccionado
            //por lo tanto el query sera sin filtro, es decir, traera todos los registros de la tabla actividad
            String sQuery = "SELECT id_actividad, nombre FROM actividad";
            String sHorario = "";
            if (cmbHorario.SelectedIndex > -1)//Se valida que haya un objeto seleccionado del combobox
            {
                sHorario = cmbHorario.SelectedItem.ToString().Trim();
                sQuery += " WHERE horario ='" + sHorario + "'"; //Se concatena el horario elejido al Query que se va a ejecutar en la base de datos
            }

            String Cadenaconexion;
            //cadena de conexion
            MySqlConnection conexion = new MySqlConnection(); //objeto de conexion
            Cadenaconexion = "Server=localhost;User id=root; Database=proyecto;Password=;";//Conexion a la base de datos
            conexion.ConnectionString = Cadenaconexion; //a la cadena de conexion se le agrega la conexion

            MySqlCommand comando = new MySqlCommand(sQuery); //Asignar el query al objeto Command
            comando.Connection = conexion;  //Abrir la conexion a la base de datos             

            MySqlDataAdapter da1 = new MySqlDataAdapter(comando);
            DataTable dt = new DataTable();
            da1.Fill(dt); //Aqui el query que se ejecuto se guarda en el objeto DataTable
            cmbAct.ValueMember = "id_actividad";//Valor
            cmbAct.DisplayMember = "nombre";//Lo que muestra el combobox
            cmbAct.DataSource = dt;//Es necesario para llenar el combobox
            cmbAct.SelectedIndex = -1;//Se pone para que no se seleccione ningun objeto del combobox
            if (dt.Rows.Count > 0)//Se valida que si la consulta tiene registros entonces abra el combobox de Actividades
            {
                if (cmbHorario.SelectedIndex > -1)
                {
                    cmbAct.Focus();
                    cmbAct.DroppedDown = true;
                }
            }
            else//Sino tiene registros entonces mandara este mensaje
            {
                MessageBox.Show("No hay ninguna actividad en el horario seleccionado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cmbHorario_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CargaComboActividades();
        }
       
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;//importar

namespace PROYECTOclub
{
    public partial class AltaAdministrador : Form
    {
        //String correo = ""; //En esta variable se va a guardar el correo que envies desde el login
        //int perfil = 0; //El '2' será para socios, el '1' para admin
        public AltaAdministrador()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            String sCorreo = txtCorreo.Text.Trim();
            String sNombre = txtNombre.Text.Trim();
            String sContra = txtContra.Text.Trim();
            String sContraConfir = txtContraConf.Text.Trim();
            String sPregunta ="";
            String sRespuesta = txtRespuesta.Text.Trim();
            //String sFechaNac = dtpFechaNac.Text.Trim();
            String sFechaNac = dtpFechaNac.Value.ToString("dd-MM-yyyy");
            String sTel = txtTel.Text.Trim();
            Boolean bValidaUsuario = false;

            //validar que los campos esten llenos
            if(sCorreo == "")
            {
                MessageBox.Show("Ingresa el correo", "Validacion de campos", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                txtCorreo.Focus();
            }else if(sNombre == "")
            {
                MessageBox.Show("Ingresa el nombre", "Validacion de campos", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                txtNombre.Focus();
            }
            else if (sContra == "")
            {
                MessageBox.Show("Ingresa la contrasena", "Validacion de campos", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                txtContra.Focus();
            }
            else if (sContraConfir == "")
            {
                MessageBox.Show("Ingresa la confirmacion de la contrasena", "Validacion de campos", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                txtContraConf.Focus();
            }
            else if (cmbPregunta.SelectedItem == null)
            {
                MessageBox.Show("Elige una pregunta secreta", "Validacion de campos", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                cmbPregunta.Focus();
                cmbPregunta.DroppedDown = true;
            }
            else if (sRespuesta == "")
            {
                MessageBox.Show("Ingresa la respuesta", "Validacion de campos", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                txtRespuesta.Focus();
            }
            else if (sFechaNac == "")
            {
                MessageBox.Show("Ingresa la fecha de nacimiento", "Validacion de campos", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                dtpFechaNac.Focus();
            }
            else if (sTel == "")
            {
                MessageBox.Show("Ingresa el telefono", "Validacion de campos", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                txtTel.Focus();
            }
            else if (validaciones.ValidarCorreo(sCorreo) == false)
            {

                txtCorreo.Focus();
            }
            
            else
            {
                sPregunta = cmbPregunta.SelectedItem.ToString().Trim();
                //validar que la contrasena y la confirmacion sean iguales
                if(sContra != sContraConfir){
                    MessageBox.Show("Las contrasenas no coinciden", "Validacion de campos", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    txtContraConf.Clear();
                    txtContra.Focus();
                }
                else
                {
                    if (MessageBox.Show("Desea agregar el usuario?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        bValidaUsuario = ValidaExisteUsuario(sCorreo);
                        //llamamos al metodo que hara el insert a la base de datos, previamente validando los campos.
                        if (bValidaUsuario == false)
                        {
                            MessageBox.Show("El correo: " + sCorreo + " ya se encuentra registrado", "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            RegistraAdmin(sCorreo, sNombre, sContra, sPregunta, sRespuesta, sFechaNac, sTel);
                        }
                    }
                }
            }
        }
        private void RegistraAdmin(String psCorreo, String psNombre, String psContra, String psPregunta, String psRespuesta, String psFechaNac, String psTel)
        {
            if(MessageBox.Show("Desea agregar el usuario?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes){
                MySqlConnection conexion = new MySqlConnection(); //objeto de conexion
                string Cadenaconexion; //variable para recibir los valores
                //cadena de conexion que va a recibir
                Cadenaconexion = "Server=localhost;User id=root; Database=proyecto;Password=;";//preparar la ruta
                conexion.ConnectionString = Cadenaconexion; //a la cadena de conexion se le agrega la conexion
                String sInsert = "INSERT INTO admin(correo,nombre,contrasena,pregunta,respuesta,fecha_nac,telefono) values " +
                               "(@correo,@nombre,aes_encrypt(@contrasena,@respuesta),aes_encrypt(@pregunta,@correo),aes_encrypt(@respuesta,'root'),@fechaNac,@Tel)";
              //  String sInsert = "INSERT INTO admin(correo,nombre,contrasena,pregunta,respuesta,fecha_nac,telefono) values "+
                //    "(@correo,@nombre,aes_encrypt(@contrasena,@respuesta),@pregunta,aes_encrypt(@respuesta,'s3cret'),@fechaNac,@Tel)";

                MySqlCommand comando = new MySqlCommand(sInsert); //query comando
                comando.Connection = conexion;
                //creacion del objeto
                MySqlParameter parametro1 = new MySqlParameter(); //dealaracion del obejto
                parametro1.ParameterName = "@correo"; //asigancion
                parametro1.Value = psCorreo;

                //creacion del objeto
                MySqlParameter parametro2 = new MySqlParameter(); //dealaracion del obejto
                parametro2.ParameterName = "@nombre"; //asigancion
                parametro2.Value = psNombre;

                MySqlParameter parametro3 = new MySqlParameter(); //dealaracion del obejto
                parametro3.ParameterName = "@contrasena"; //asigancion
                parametro3.Value = psContra;

                MySqlParameter parametro4 = new MySqlParameter(); //dealaracion del obejto
                parametro4.ParameterName = "@pregunta"; //asigancion
                parametro4.Value = psPregunta;

                MySqlParameter parametro5 = new MySqlParameter(); //dealaracion del obejto
                parametro5.ParameterName = "@respuesta"; //asigancion
                parametro5.Value = psRespuesta;

                MySqlParameter parametro6 = new MySqlParameter(); //dealaracion del obejto
                parametro6.ParameterName = "@fechaNac"; //asigancion
                parametro6.Value = psFechaNac;

                MySqlParameter parametro7 = new MySqlParameter(); //dealaracion del obejto
                parametro7.ParameterName = "@Tel"; //asigancion
                parametro7.Value = psTel;

                comando.Parameters.Add(parametro1);
                comando.Parameters.Add(parametro2);
                comando.Parameters.Add(parametro3);
                comando.Parameters.Add(parametro4);
                comando.Parameters.Add(parametro5);
                comando.Parameters.Add(parametro6);
                comando.Parameters.Add(parametro7);
                try
                {
                    conexion.Open(); //abrir conexion
                    int iRegAfec = comando.ExecuteNonQuery(); //ejecutar query
                    if (iRegAfec == 1)
                    {
                        MessageBox.Show("Usuario agregado correctamente", "Confirmacion", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        limpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show("El usuario no fue agregado", "Confirmacion", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    }
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
            txtCorreo.Clear();
            txtNombre.Clear();
            txtContra.Clear();
            txtContraConf.Clear();
            cmbPregunta.SelectedIndex = -1;
            txtRespuesta.Clear();
            dtpFechaNac.Value = DateTime.Now;
            txtTel.Clear();
            txtCorreo.Focus();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            /*MenuAdministrador ma = new MenuAdministrador(correo, perfil);
            ma.Show();*/
            this.Close();
        }

        private void txtCorreo_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void txtTel_KeyPress(object sender, KeyPressEventArgs e)
        {
            validaciones.soloNumeros(e);
        }
        private Boolean ValidaExisteUsuario(String psCorreo)
        {
            int iCanRegSocio = 0;
            Boolean bValidacion = false;
            String sQuery = "SELECT id_admin, nombre FROM admin where correo='" + psCorreo + "'";
            String Cadenaconexion;
            //cadena de conexion
            MySqlConnection conexion = new MySqlConnection(); //objeto de conexion
            Cadenaconexion = "Server=localhost;User id=root; Database=proyecto;Password=;";//Conexion a la base de datos
            conexion.ConnectionString = Cadenaconexion; //a la cadena de conexion se le agrega la conexion
            MySqlCommand comando = new MySqlCommand(sQuery); //Asignar el query al objeto Command
            comando.Connection = conexion;  //Abrir la conexion a la base de datos             
            MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
            DataTable dtConsulta = new DataTable();
            adapter.Fill(dtConsulta); //Aqui el query que se ejecuto se guarda en el objeto DataTable
            int iCantRegAdmin = dtConsulta.Rows.Count;
            if (iCantRegAdmin == 0)
            {
                sQuery = "SELECT id_socio, nombre FROM socio where correo='" + psCorreo + "'";
                MySqlCommand comando2 = new MySqlCommand(sQuery); //Asignar el query al objeto Command   
                comando2.Connection = conexion;  //Abrir la conexion a la base de datos             
                MySqlDataAdapter adapter2 = new MySqlDataAdapter(comando2);
                dtConsulta = new DataTable();
                adapter2.Fill(dtConsulta); //Aqui el query que se ejecuto se guarda en el objeto DataTable
                iCanRegSocio = dtConsulta.Rows.Count;
                if (iCanRegSocio > 0)
                {
                    bValidacion = false;
                }
                else
                {
                    bValidacion = true;
                }
            }
            else if (iCantRegAdmin > 0)
            {
                bValidacion = false;
            }
            conexion.Close();
            return bValidacion;
        }
    }

    
}

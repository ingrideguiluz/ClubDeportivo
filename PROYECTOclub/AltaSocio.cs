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

//using System.IO;

namespace PROYECTOclub
{
    public partial class AltaSocio : Form
    {
        String sNombreArchivo="";


        /*Estros dos parametros siempre deben de existir, en todos tus form*/

        String correo = ""; //En esta variable se va a guardar el correo que envies desde el login
        int perfil = 0; //El '2' será para socios, el '1' para admin


        public AltaSocio(String correoSocio, int perfil)
        {
            InitializeComponent();
            CargaComboActividades();
            DateTime fecha = DateTime.Now;
            String sFecha = fecha.ToString("dd-MM-yyyy");
            DateTime fechaIns = DateTime.Now;
            String sFechaIns = fecha.ToString("dd-MM-yyyy");
           // lblFecha.Text = "Fecha: " + sFecha;
            this.correo = correoSocio; //La variable de arriba 'correo' recibe el correo de tu form login
            this.perfil = perfil; //La variable perfil en este caso resibe un 1 porque es admin
        }

        private void btnBuscarFoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog getImage = new OpenFileDialog();
            //getImage.
            ofdBuscarFoto.InitialDirectory = "C:\\Users\\lizar\\Pictures";
            ofdBuscarFoto.Filter = "Archivos de imagen(*.jpg)(*.jpeg)|*.jpg;*.jpeg|PNG(*.png)|*.png";
            if(ofdBuscarFoto.ShowDialog()==DialogResult.OK)
            {
                pbFoto.ImageLocation = ofdBuscarFoto.FileName;
                sNombreArchivo = ofdBuscarFoto.FileName;
            }
            else
            {
                MessageBox.Show("No se selecciono imagen", "sin seleccion");
            }
        }

        private void cmbHorarioAct_SelectionChangeCommitted(object sender, System.EventArgs e)
        {//Este es el evento cuando el usuario cambia un elemento del combobox horario
            CargaComboActividades();
        }

        private void btnGuardar_Click(object sender, System.EventArgs e)
        {
            String sCorreo = txtCorreo.Text.Trim();
            String sNombre = txtNombre.Text.Trim();
            String sContra = txtContra.Text.Trim();
            String sContraConfir = txtContraConf.Text.Trim();
            String sPregunta = "";
            String sRespuesta = txtRespuesta.Text.Trim();
            //String sFechaNac = dtpFechaNac.Text.Trim();
            String sFechaNac = dtpFecha.Value.ToString("dd-MM-yyyy");
            String sFechaIns = dtpFechaIns.Value.ToString("dd-MM-yyyy");
            String sTel = txtTel.Text.Trim();
            String sHorario = "";
            int iId_Actividad = 0;
            int estatus = 0;
            Boolean bValidaMetodo = false;

            //validar que los campos esten llenos
            if (sCorreo == "")
            {
                MessageBox.Show("Ingresa el correo", "Validacion de campos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCorreo.Focus();
            }
            else if (sNombre == "")
            {
                MessageBox.Show("Ingresa el nombre", "Validacion de campos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
            }
            else if (sContra == "")
            {
                MessageBox.Show("Ingresa la contrasena", "Validacion de campos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtContra.Focus();
            }
            else if (sContraConfir == "")
            {
                MessageBox.Show("Ingresa la confirmacion de la contrasena", "Validacion de campos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtContraConf.Focus();
            }
            else if (cmbPregunta.SelectedItem == null)
            {
                MessageBox.Show("elige una pregunta secreta", "Validacion de campos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbPregunta.Focus();
                cmbPregunta.DroppedDown = true;
            }
            else if (sRespuesta == "")
            {
                MessageBox.Show("Ingresa la respuesta", "Validacion de campos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRespuesta.Focus();
            }
            else if (sFechaNac == "")
            {
                MessageBox.Show("Ingresa la fecha de nacimiento", "Validacion de campos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpFecha.Focus();
            }
            else if (sTel == "")
            {
                MessageBox.Show("Ingresa el telefono", "Validacion de campos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTel.Focus();
            }
            else if (cmbHorarioAct.SelectedItem == null)
            {
                MessageBox.Show("Elige un horario", "Validacion de campos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbHorarioAct.Focus();
                cmbHorarioAct.DroppedDown = true;
            }
            else if (cmbAct.SelectedIndex == -1)
            {
                MessageBox.Show("Elige una actividad", "Validacion de campos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbAct.Focus();
                cmbAct.DroppedDown = true;
            }
            else if (sFechaIns == "")
            {
                MessageBox.Show("Ingresa la fecha de inscripcion", "Validacion de campos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpFechaIns.Focus();
            }
            else if (rbtAdeudo.Checked==false && rbtPagado.Checked==false)
            {
                MessageBox.Show("Elige un estatus de pago", "Validacion de campos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if(sNombreArchivo==""){
                 MessageBox.Show("Elige una foto", "Validacion de campos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (validaciones.ValidarCorreo(sCorreo) == false)
            {

                txtCorreo.Focus();
            }
            else
            {            
                //validar que la contrasena y la confirmacion sean iguales
                if (sContra != sContraConfir)
                {
                    MessageBox.Show("Las contrasenas no coinciden", "Validacion de campos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtContraConf.Clear();
                    txtContra.Focus();
                }
                else
                {
                    if(rbtPagado.Checked==true){
                        estatus = 1;
                    }else if(rbtAdeudo.Checked==true){
                        estatus = 0;
                    }
                    //llamamos al metodo que hara el insert a la base de datos, previamente validando los campos.
                    sPregunta = cmbPregunta.SelectedItem.ToString().Trim();
                    iId_Actividad = Int32.Parse(cmbAct.SelectedValue.ToString());
                    sHorario = cmbHorarioAct.SelectedItem.ToString().Trim();
                    if (MessageBox.Show("Desea agregar el usuario?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes){
                        bValidaMetodo = ValidaExisteUsuario(sCorreo);
                        if(bValidaMetodo==true){
                            guardarSocio(sCorreo, sNombre, sContra, sPregunta, sRespuesta, sFechaNac, sTel, sHorario, sFechaIns, iId_Actividad, sNombreArchivo, estatus);
                        }
                        else
                        {
                            MessageBox.Show("El usuario: "+sCorreo+" ya existe", "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }                        
                    }                    
                }
            }
        }

        private void guardarSocio(String psCorreo, String psNombre, String psContra, String psPregunta, String psRespuesta, String psFechaNac, String psTel, String psHorario,String psFechaIns, int piId_Actividad, String psFoto,int piEstatus)
        {

            DateTime fecha = DateTime.Now;
            String sFecha = fecha.ToString("dd-MM-yyyy");
            DateTime fechaIns = DateTime.Now;
            String sFechaIns = fechaIns.ToString("dd-MM-yyyy");
            MySqlConnection conexion = new MySqlConnection(); //objeto de conexion
            String Cadenaconexion; //variable para recibir los valores
            //cadena de conexion que va a recibir
            Cadenaconexion = "Server=localhost;User id=root; Database=proyecto;Password=;";//preparar la ruta
            conexion.ConnectionString = Cadenaconexion; //a la cadena de conexion se le agrega la conexion
            String sInsert = "INSERT INTO socio(correo,nombre,contrasena,pregunta,respuesta,fecha_nac,telefono,horario,id_actividad,foto,estatus,fecha_insc) values " +
                                "(@correo,@nombre,aes_encrypt(@contrasena,@respuesta),aes_encrypt(@pregunta,@correo),aes_encrypt(@respuesta,'root'),@fechaNac,@Tel,@horario,@idAct,@foto,@estatus,@fechaInsc)";
            //String sInsert = "INSERT INTO socio(nombre,fecha_nac,foto,correo,contrasena,pregunta,respuesta,id_actividad,horario,telefono,fecha_insc,estado) values (@nombre,@fechaNac,@foto,@correo,aes_encrypt(@contrasena,@respuesta), @pregunta,aes_encrypt(@respuesta,'s3cret'),@idAct,@horario,@Tel,@fechaInsc,@estatus)";

            MySqlCommand comando = new MySqlCommand(sInsert); //query comando
            comando.Connection = conexion;

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

            MySqlParameter parametro8 = new MySqlParameter(); //dealaracion del obejto
            parametro8.ParameterName = "@horario"; //asigancion
            parametro8.Value = psHorario;

            MySqlParameter parametro9 = new MySqlParameter(); //dealaracion del obejto
            parametro9.ParameterName = "@idAct"; //asigancion
            parametro9.Value = piId_Actividad;

            MySqlParameter parametro10 = new MySqlParameter(); //dealaracion del obejto
            parametro10.ParameterName = "@foto"; //asigancion
            parametro10.Value = psFoto;

            MySqlParameter parametro11 = new MySqlParameter(); //dealaracion del obejto
            parametro11.ParameterName = "@estatus"; //asigancion
            parametro11.Value = piEstatus;

            MySqlParameter parametro12 = new MySqlParameter(); //dealaracion del obejto
            parametro12.ParameterName = "@fechaInsc"; //asigancion
            parametro12.Value = psFechaIns;

            comando.Parameters.Add(parametro1);
            comando.Parameters.Add(parametro2);
            comando.Parameters.Add(parametro3);
            comando.Parameters.Add(parametro4);
            comando.Parameters.Add(parametro5);
            comando.Parameters.Add(parametro6);
            comando.Parameters.Add(parametro7);
            comando.Parameters.Add(parametro8);
            comando.Parameters.Add(parametro9);
            comando.Parameters.Add(parametro10);
            comando.Parameters.Add(parametro11);
            comando.Parameters.Add(parametro12);

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

        private void CargaComboActividades()
        {
            //Descripcion: Cargar el combobox de actividades dependiendo del horario que se elija, cuando el programa inicie, no habra ningun horario seleccionado
            //por lo tanto el query sera sin filtro, es decir, traera todos los registros de la tabla actividad
            String sQuery = "SELECT id_actividad, nombre FROM actividad";
            String sHorario = "";
            if (cmbHorarioAct.SelectedIndex > -1)//Se valida que haya un objeto seleccionado del combobox
            {
                sHorario = cmbHorarioAct.SelectedItem.ToString().Trim();
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
                if (cmbHorarioAct.SelectedIndex > -1)
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

        private void limpiarCampos()
        {
            txtCorreo.Clear();
            txtNombre.Clear();
            txtContra.Clear();
            txtContraConf.Clear();
            cmbPregunta.SelectedIndex = -1;
            txtRespuesta.Clear();
            dtpFecha.Value = DateTime.Now;
            txtTel.Clear();
            cmbHorarioAct.SelectedIndex = -1;
            cmbAct.SelectedIndex = -1;
            txtCorreo.Focus();
            rbtAdeudo.Checked = false;
            rbtPagado.Checked = false;
            pbFoto.Image = null;
        }

        private void btn_regresar_Click(object sender, System.EventArgs e)
        {
            /*MenuAdministrador admin = new MenuAdministrador(correo, perfil);
            this.Hide();
            admin.Show();*/
            this.Close();
        }

        private void txtTel_KeyPress(object sender, KeyPressEventArgs e)
        {
            validaciones.soloNumeros(e);
        }

        private void cmbAct_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}

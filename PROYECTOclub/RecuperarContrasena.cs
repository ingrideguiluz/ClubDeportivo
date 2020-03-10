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
    public partial class RecuperarContrasena : Form
    {
        public RecuperarContrasena()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String sCorreo = txtCorreo.Text.Trim();
            String sPregunta = txtPregunta.Text.Trim();
            String sRespuesta = txtRespuesta.Text.Trim();
            //validar que los campos esten llenos
            if (sCorreo == "")
            {
                MessageBox.Show("Ingresa el correo", "Validacion de campos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCorreo.Focus();
            }
            
            else if (sPregunta == "")
            {
                MessageBox.Show("El correo ingresado no existe", "Validacion de campos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPregunta.Focus();
            }
            else if (sRespuesta == "")
            {
                MessageBox.Show("Ingresa la respuesta", "Validacion de campos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRespuesta.Focus();
            }
            else
            {
                //llamamos al metodo que hara el logueo, previamente validando los campos.
                Recupera(sCorreo, sPregunta, sRespuesta);
            }
        }
            private void Recupera(String psCorreo, String psPregunta, String psRespuesta)
            {
            String sSelectAdmin = "";
            String sRespuesta = "";
            String sContrasena = "";
            String sSelectSocio = "";
            MySqlConnection conexion = new MySqlConnection(); //objeto de conexion
            string Cadenaconexion; //variable para recibir los valores
            //cadena de conexion que va a recibir
            Cadenaconexion = "Server=localhost;User id=root; Database=proyecto;Password=;";//preparar la ruta
            conexion.ConnectionString = Cadenaconexion; //a la cadena de conexion se le agrega la conexion
            sSelectAdmin = "SELECT nombre, CAST(aes_decrypt(respuesta,'root')AS CHAR) AS respuesta FROM admin WHERE correo='" + psCorreo + "'";
            MySqlCommand comandoAdmin = new MySqlCommand(sSelectAdmin); //query comando
            comandoAdmin.Connection = conexion;
            conexion.Open();
            MySqlDataAdapter adapter = new MySqlDataAdapter(comandoAdmin);
            DataTable dtConsultaAdmin = new DataTable();
            adapter.Fill(dtConsultaAdmin); //Aqui el query que se ejecuto se guarda en el objeto DataTable
            int iCantRegAdmin = dtConsultaAdmin.Rows.Count;
            if (iCantRegAdmin == 1)
            {
                sRespuesta = dtConsultaAdmin.Rows[0]["respuesta"].ToString().Trim();
                if(psRespuesta==sRespuesta){
                    sSelectAdmin = "SELECT nombre, CAST(aes_decrypt(contrasena,'"+sRespuesta+"')AS CHAR) AS contrasena FROM admin WHERE correo='" + psCorreo + "'";
                    MySqlCommand comando2 = new MySqlCommand(sSelectAdmin); //query comando 
                    comando2.Connection = conexion;
                    MySqlDataAdapter adapter2 = new MySqlDataAdapter(comando2);
                    dtConsultaAdmin = new DataTable();
                    adapter2.Fill(dtConsultaAdmin); //Aqui el query que se ejecuto se guarda en el objeto DataTable
                    sContrasena = dtConsultaAdmin.Rows[0]["contrasena"].ToString().Trim();
                    txtContrasena.Text = sContrasena;
                }
                else
                {
                    MessageBox.Show("La respuesta ingresada no es correcta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                sSelectSocio = "SELECT nombre, CAST(aes_decrypt(respuesta,'root')AS CHAR) AS respuesta FROM socio WHERE correo='" + psCorreo + "'";
                MySqlCommand comandoSocio = new MySqlCommand(sSelectSocio); //query comando
                comandoSocio.Connection = conexion;                
                MySqlDataAdapter adapterSocio = new MySqlDataAdapter(comandoSocio);
                DataTable dtConsultaSocio = new DataTable();
                adapterSocio.Fill(dtConsultaSocio); //Aqui el query que se ejecuto se guarda en el objeto DataTable
                int iCantRegSocio = dtConsultaSocio.Rows.Count;
                if (iCantRegSocio == 1)
                {
                    sRespuesta = dtConsultaSocio.Rows[0]["respuesta"].ToString().Trim();
                    if (psRespuesta == sRespuesta)
                    {
                        sSelectSocio = "SELECT nombre, CAST(aes_decrypt(contrasena,'" + sRespuesta + "')AS CHAR) AS contrasena FROM socio WHERE correo='" + psCorreo + "'";
                        MySqlCommand comando2 = new MySqlCommand(sSelectSocio); //query comando 
                        comando2.Connection = conexion;
                        MySqlDataAdapter adapter3 = new MySqlDataAdapter(comando2);
                        dtConsultaSocio = new DataTable();
                        adapter3.Fill(dtConsultaSocio); //Aqui el query que se ejecuto se guarda en el objeto DataTable
                        sContrasena = dtConsultaSocio.Rows[0]["contrasena"].ToString().Trim();
                        txtContrasena.Text = sContrasena;
                    }
                    else
                    {
                        MessageBox.Show("La respuesta ingresada no es correcta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("El correo ingresado no pertenece a ningun usuario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
               }
            }

           
            private void obtenerPregunta(String psCorreo)
            {
                String sPregunta = "";
                MySqlConnection conexion = new MySqlConnection(); //objeto de conexion
                string Cadenaconexion; //variable para recibir los valores
                //cadena de conexion que va a recibir
                Cadenaconexion = "Server=localhost;User id=root; Database=proyecto;Password=;";//preparar la ruta
                conexion.ConnectionString = Cadenaconexion; //a la cadena de conexion se le agrega la conexion
                String sSelectAdmin = "SELECT nombre, CAST(aes_decrypt(pregunta,'" + psCorreo + "')AS CHAR) AS pregunta FROM admin WHERE correo='" + psCorreo + "'";
                MySqlCommand comando = new MySqlCommand(sSelectAdmin); //query comando
                comando.Connection = conexion;
                conexion.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
                DataTable dtConsultaAdmin = new DataTable();
                adapter.Fill(dtConsultaAdmin); //Aqui el query que se ejecuto se guarda en el objeto DataTable
                int iCantReg = dtConsultaAdmin.Rows.Count;
                if (iCantReg == 0)
                {
                    //si el correo no esta en la tabla admin va a la de socio
                    String sSelectSocio = "SELECT nombre, CAST(aes_decrypt(pregunta,'" + psCorreo + "')AS CHAR) AS pregunta FROM socio WHERE correo='" + psCorreo + "'";
                    MySqlCommand comando2 = new MySqlCommand(sSelectSocio); //query comando 
                    comando2.Connection = conexion;
                    MySqlDataAdapter adapter2 = new MySqlDataAdapter(comando2);
                    DataTable dtConsultaSocio = new DataTable();
                    adapter2.Fill(dtConsultaSocio); //Aqui el query que se ejecuto se guarda en el objeto DataTable
                    int iCantRegSocio = dtConsultaSocio.Rows.Count;
                    if (iCantRegSocio == 0)
                    {
                        MessageBox.Show("El usuario no existe", "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (iCantRegSocio == 1)
                    {
                        //si si existe compara contrasenas para entrar
                        sPregunta = dtConsultaSocio.Rows[0]["pregunta"].ToString();
                        txtPregunta.Text = sPregunta.Trim();
                    }
                }
                else if (iCantReg == 1)
                {
                    //si si esta en admin compara contrasenas para entrar
                    sPregunta = dtConsultaAdmin.Rows[0]["pregunta"].ToString();
                    txtPregunta.Text = sPregunta.Trim();
                }
            }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 fm=new Form1();
            fm.Show();
            this.Hide();
        }

        private void txtCorreo_Leave(object sender, EventArgs e)
        {
            
        }

        private void txtRespuesta_Enter(object sender, EventArgs e)
        {
            String sCorreo = txtCorreo.Text.Trim();
            if (sCorreo == "")
            {
                MessageBox.Show("Ingresa el correo", "Validacion de campos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCorreo.Focus();
            }
            else
            {
                obtenerPregunta(sCorreo);
            }
        }
    }
}
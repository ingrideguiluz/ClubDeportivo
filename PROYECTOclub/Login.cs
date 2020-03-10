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
    public partial class Form1 : Form
    {        
        public Form1()
        {
            InitializeComponent();
            
        }
        
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            String sCorreo = txtCorreo.Text.Trim();
            String sContra = txtContra.Text.Trim();

            //validar que los campos esten llenos
            if (sCorreo == "")
            {
                MessageBox.Show("Ingresa el correo", "Validacion de campos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCorreo.Focus();
            }
            else if (sContra == "")
            {
                MessageBox.Show("Ingresa la contraseña", "Validacion de campos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtContra.Focus();
            }
            else if (validaciones.ValidarCorreo(sCorreo) == false)
            {

                txtCorreo.Focus();
            }
            else
            {
                //llamamos al metodo que hara el logueo, previamente validando los campos.
                LogueaAdmin(sCorreo, sContra);                
            }
        }
            private void LogueaAdmin(String psCorreo, String psContra){
                //Boolean bResultado = false;
                String respuesta, contra = "";
                
                MySqlConnection conexion = new MySqlConnection(); //objeto de conexion
                string Cadenaconexion; //variable para recibir los valores
                //cadena de conexion que va a recibir
                Cadenaconexion = "Server=localhost;User id=root; Database=proyecto;Password=;";//preparar la ruta
                conexion.ConnectionString = Cadenaconexion; //a la cadena de conexion se le agrega la conexion
                String sSelectAdmin = "SELECT correo,respuesta, CAST(aes_decrypt(respuesta,'root')AS CHAR) AS respuesta FROM admin WHERE correo='" + psCorreo + "'";
                MySqlCommand comandoAdmin = new MySqlCommand(sSelectAdmin); //query comando
                comandoAdmin.Connection = conexion;
                conexion.Open();
                MySqlDataAdapter da1 = new MySqlDataAdapter(comandoAdmin);
                DataTable dtConsultaAdmin = new DataTable();
                da1.Fill(dtConsultaAdmin); //Aqui el query que se ejecuto se guarda en el objeto DataTable
                int iCantReg = dtConsultaAdmin.Rows.Count;
                if(iCantReg==0){
                    //si el correo no esta en la tabla admin va a la de socio
                    String sSelectSocio = "SELECT correo,respuesta, CAST(aes_decrypt(respuesta,'root') AS CHAR) AS respuesta FROM socio WHERE correo='" + psCorreo + "'";
                    MySqlCommand comandoSocio = new MySqlCommand(sSelectSocio); //query comando
                    comandoSocio.Connection = conexion;
                    MySqlDataAdapter da2 = new MySqlDataAdapter(comandoSocio);
                    DataTable dtConsultaSocio = new DataTable();
                    da2.Fill(dtConsultaSocio); //Aqui el query que se ejecuto se guarda en el objeto DataTable
                    int iCantRegSocio = dtConsultaSocio.Rows.Count;
                    if (iCantRegSocio == 0)
                    {
                        MessageBox.Show("El usuario no existe", "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (iCantRegSocio == 1)
                    {
                        //si si existe compara contrasenas para entrar
                        respuesta = dtConsultaSocio.Rows[0][2].ToString();
                        sSelectSocio = "SELECT contrasena, CAST(aes_decrypt(contrasena,'" + respuesta + "') AS CHAR)  AS contrasena FROM socio WHERE correo='" + psCorreo + "';";
                        comandoSocio = new MySqlCommand(sSelectSocio); //query comando
                        comandoSocio.Connection = conexion;
                        da2 = new MySqlDataAdapter(comandoSocio);
                        dtConsultaSocio = new DataTable();
                        da2.Fill(dtConsultaSocio); //Aqui el query que se ejecuto se guarda en el objeto DataTable
                        contra = dtConsultaSocio.Rows[0][1].ToString();
                        if(contra==psContra){
                            this.Hide();
                            //Aqiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiii
                            MenuSocio menuSocio = new MenuSocio(psCorreo, 2); //Haces la sobre carga de parametos para cargar el form 'Menu Socio'
                            //Aqui terminaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                            //Ve a tu form MenuSocio y checa como el metodo contructor recive los parametros anteriores
                            menuSocio.Show();
                        }
                        else
                        {
                            MessageBox.Show("La contraseña es incorrecta", "Validacion de campos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtContra.Focus();
                        }
                    }
                }else if(iCantReg==1){
                    //si si esta en admin compara contrasenas para entrar
                    respuesta = dtConsultaAdmin.Rows[0][2].ToString();
                    sSelectAdmin = "SELECT contrasena, CAST(aes_decrypt(contrasena,'" + respuesta + "') AS CHAR)  AS contrasena FROM admin WHERE correo='" + psCorreo + "';";
                    comandoAdmin = new MySqlCommand(sSelectAdmin); //query comando
                    comandoAdmin.Connection = conexion;
                    da1 = new MySqlDataAdapter(comandoAdmin);
                    dtConsultaAdmin = new DataTable();
                    da1.Fill(dtConsultaAdmin); //Aqui el query que se ejecuto se guarda en el objeto DataTable
                    contra = dtConsultaAdmin.Rows[0][1].ToString();
                    if (contra == psContra)
                    {
                        this.Hide();
                        //Aquiiiiiiiiiiiiiiiiiiiiiiiii tambiennnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnn
                        MenuAdministrador menuAdmin = new MenuAdministrador(psCorreo, 1); //Igual hacemos la misma sobre carga del pasado metodo y tambien modificamos el contructir del form 'Menu Admin'
                        //Aqui terminaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                        menuAdmin.Show();
                    }
                    else
                    {
                        MessageBox.Show("La contraseña es incorrecta", "Validacion de campos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtContra.Focus();
                    }                   
                }             
                conexion.Close();                
            }

            private void btnSalir_Click(object sender, EventArgs e)
            {
                if (MessageBox.Show("¿Desea salir de la aplicación?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Application.Exit();        
                }                
            }

            private void btnRecuperar_Click(object sender, EventArgs e)
            {

            }

            private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
            {
                RecuperarContrasena rc = new RecuperarContrasena();
                rc.Show();
                this.Hide();
            }                    
    }
}

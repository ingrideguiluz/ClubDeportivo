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
using SpreadsheetLight;

namespace PROYECTOclub
{
    public partial class FiltrarSocio : Form
    {
        MySqlConnection conexion = null;
        String cadenaConexion;
        public FiltrarSocio()
        {
            InitializeComponent();
            cargaComboActividad();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            String sConsulta = "SELECT id_socio,a.nombre,s.nombre,fecha_nac,telefono,correo,a.horario,s.fecha_insc,s.estatus FROM socio s INNER JOIN actividad a ON s.id_actividad = a.id_actividad WHERE 1=1";
            if(comboBox1.SelectedIndex>-1){
                sConsulta += " AND a.nombre='" + comboBox1.SelectedValue.ToString().Trim() + "'";
            }
            if (comboBox2.SelectedIndex > -1)
            {
                sConsulta += " AND a.horario='" + comboBox2.SelectedItem.ToString().Trim() + "'";
            }

            if(comboBox1.SelectedIndex==-1 && comboBox2.SelectedIndex==-1){
                MessageBox.Show("Seleccione un filtro", "Validacion de campos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                buscarSocio(sConsulta);
            }
        }
       
        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cargaComboActividad()
        {
            //Objetivo: Cargar el combobox de Actividad, con una consulta a la base de datos para obtener todas las actividades registradas en la tabla actividad
            String sQuery = "select distinct (nombre) as nombre from actividad;";                        
            String Cadenaconexion;
            //cadena de conexion
            MySqlConnection conexion = new MySqlConnection(); //objeto de conexion
            Cadenaconexion = "Server=localhost;User id=root; Database=proyecto;Password=;";//Conexion a la base de datos
            conexion.ConnectionString = Cadenaconexion; //a la cadena de conexion se le agrega la conexion

            MySqlCommand comando = new MySqlCommand(sQuery); //Asignar el query al objeto Command
            comando.Connection = conexion;  //Abrir la conexion a la base de datos             

            MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
            DataTable dtDatos = new DataTable();
            adapter.Fill(dtDatos); //Aqui el query que se ejecuto se guarda en el objeto DataTable
            comboBox1.ValueMember = "nombre";//Valor
            comboBox1.DisplayMember = "nombre";//Lo que muestra el combobox
            comboBox1.DataSource = dtDatos;//Es necesario para llenar el combobox
            comboBox1.SelectedIndex = -1;//Se pone para que no se seleccione ningun objeto del combobox
            
        }

        private void buscarSocio(String psQuery)
        {
            //Objetivo: Buscar en la tabla socio dependiendo de los filtros que el usuario selecciono, recibe como parametro el query ya armado con los filtros
            
            //DateTime fecha = DateTime.Now;
            conexion = new MySqlConnection();

            cadenaConexion = "Server=localhost;User id= root; Database=proyecto; Password=";
            conexion.ConnectionString = cadenaConexion;
            MySqlCommand comandobusqueda = new MySqlCommand(psQuery);
            comandobusqueda.Connection = conexion;
            conexion.Open();
            MySqlDataReader myreader = comandobusqueda.ExecuteReader(); //ROWS FILAS
            //rows busca en cada fila si existen los datos dentro de la bd y tabla
            dataGridView1.Rows.Clear();
            try
            {
                if (myreader.HasRows)
                {
                    while (myreader.Read())
                    {
                        string estado=myreader[8].ToString();
                        if(estado=="1")
                        {
                            estado="Pagado";
                        }
                        else if(estado=="0")
                        {
                            estado = "Adeudo";
                        }
                        dataGridView1.Rows.Add(myreader[0], myreader[1], myreader[2], myreader[3], myreader[4], myreader[5], myreader[6],myreader[7],estado);
                        
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Se ha producido un error" + err + "");
            }
            conexion.Close();
        }

        private void btnImprimirExcel_Click(object sender, EventArgs e)
        {
            SLDocument sl = new SLDocument();
            SLStyle style = new SLStyle();
            style.Font.FontSize = 14;
            style.Font.Bold = true;
            style.Font.FontColor = Color.SpringGreen;
            
            int iC = 1;
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {

                sl.SetCellValue(1, iC, column.HeaderText.ToString());
                sl.SetCellStyle(1, iC, style);
                iC++;
            }


            int IR = 2;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                sl.SetCellValue(IR, 1, row.Cells[0].Value.ToString());
                sl.SetCellValue(IR, 2, row.Cells[1].Value.ToString());
                sl.SetCellValue(IR, 3, row.Cells[2].Value.ToString());
                sl.SetCellValue(IR, 4, row.Cells[3].Value.ToString());
                sl.SetCellValue(IR, 5, row.Cells[4].Value.ToString());
                sl.SetCellValue(IR, 6, row.Cells[5].Value.ToString());
                sl.SetCellValue(IR, 7, row.Cells[6].Value.ToString());
                sl.SetCellValue(IR, 8, row.Cells[7].Value.ToString());
                sl.SetCellValue(IR, 9, row.Cells[8].Value.ToString());
                IR++;
            }

            sl.SaveAs(@"C:\Users\lizar\Documents\Filtrar.xlsx"); /// ubicación de liberias EXCEL
            MessageBox.Show("ARCHIVO DE EXCEL CREADO CON ÉXITO!");
        }
    }
}

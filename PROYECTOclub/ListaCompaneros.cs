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
using System.Text.RegularExpressions;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
namespace PROYECTOclub
{
    public partial class ListaCompaneros : Form
    {

       
        String correo = ""; //En esta variable se va a guardar el correo que envies desde el login
        int perfil = 0; //El '2' será para socios, el '1' para admin
        MySqlConnection Conexion = new MySqlConnection();
        String Cadenaconexion = "Server=localhost;User id=root;Database=proyecto;Password='';";
       
        public ListaCompaneros(String correoSocio, int perfil)
        {
            InitializeComponent();
            this.correo = correoSocio; //La variable de arriba 'correo' recibe el correo de tu form login
            this.perfil = perfil; //La variable perfil en este caso resibe un 1 porque es admin
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //
            dataGridView1.Rows.Clear();
            Document documento = new Document();
            PdfWriter wri = PdfWriter.GetInstance(documento, new FileStream("C:/Users/lizar/Documents/" + MenuSocio.nombre_usuario + "-compañeros.pdf", FileMode.Create));
            documento.Open();

            PdfPTable table = new PdfPTable(6);

            //table.DefaultCell.Border = 0;
            PdfPCell imagen = new PdfPCell(new Paragraph());
            iTextSharp.text.Image im = iTextSharp.text.Image.GetInstance("C:/Users/lizar/Desktop/ImagenesClub/logo.png");
            /*float porc = 80 / im.Width;
            im.ScalePercent(porc * 100);*/
            imagen.Colspan = 6;
            imagen.AddElement(im);
            table.AddCell(imagen);


            PdfPCell celdaInicial = (new PdfPCell(new Paragraph("Lista de Compañeros", FontFactory.GetFont("arial", 14, BaseColor.WHITE))) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE });
            celdaInicial.BackgroundColor = BaseColor.GREEN;
            celdaInicial.Colspan = 6;//combina celdas debe tener el mismo tamaño o menor rowspan filas colpan columnas
            /*celdaInicial.BorderWidthBottom = 0;
            celdaInicial.BorderWidthLeft = 0;
            celdaInicial.BorderWidthTop = 0;
            celdaInicial.BorderWidthRight=0;*/
            table.AddCell(celdaInicial);

            PdfPCell datos00 = (new PdfPCell(new Paragraph("#", FontFactory.GetFont("arial", 12, BaseColor.BLACK))) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE });
            datos00.Colspan = 1;
            datos00.Rowspan = 1;
            table.AddCell(datos00);
            PdfPCell datos0 = (new PdfPCell(new Paragraph("Correo" , FontFactory.GetFont("arial", 12, BaseColor.BLACK))) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE });
            datos0.Colspan = 1;
            datos0.Rowspan = 1;
            table.AddCell(datos0);
            PdfPCell datos1 = (new PdfPCell(new Paragraph("Nombre", FontFactory.GetFont("arial", 12, BaseColor.BLACK))) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE });
            datos1.Colspan = 1;
            datos1.Rowspan = 1;
            table.AddCell(datos1);
            PdfPCell datos2 = (new PdfPCell(new Paragraph("Actividad", FontFactory.GetFont("arial", 12, BaseColor.BLACK))) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE });
            datos2.Colspan = 1;
            datos2.Rowspan = 1;
            table.AddCell(datos2);
            PdfPCell datos3 = (new PdfPCell(new Paragraph("Horario", FontFactory.GetFont("arial", 12, BaseColor.BLACK))) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE });
            datos3.Colspan = 1;
            datos3.Rowspan = 1;
            table.AddCell(datos3);
            PdfPCell datos4 = (new PdfPCell(new Paragraph("Estatus", FontFactory.GetFont("arial", 12, BaseColor.BLACK))) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE });
            datos4.Colspan = 1;
            datos4.Rowspan = 1;
            table.AddCell(datos4);
          
            Conexion.ConnectionString = Cadenaconexion;
            int id = Convert.ToInt32(recuperaIDActividad(MenuSocio.nombre_usuario));
            MySqlDataAdapter comandobus = new MySqlDataAdapter("SELECT correo,nombre,id_actividad,horario,estatus FROM socio WHERE id_actividad=" + id + ";", Conexion);
            DataTable dt = new System.Data.DataTable();
            comandobus.Fill(dt);
            int contador = 1;
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                //int contador = 1;
                dataGridView1.Rows[n].Cells[0].Value = contador + "";
                
                dataGridView1.Rows[n].Cells[1].Value = item["correo"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["nombre"].ToString();
                dataGridView1.Rows[n].Cells[3].Value = recuperActividad(Convert.ToInt32(item["id_actividad"]));
                dataGridView1.Rows[n].Cells[4].Value = item["horario"].ToString();
                if (item["estatus"].ToString() == "1")
                {
                    dataGridView1.Rows[n].Cells[5].Value = "Pagado";
                }
                else if (item["estatus"].ToString() == "0")
                {
                    dataGridView1.Rows[n].Cells[5].Value = "Debe";
                }
                //contador++;


                PdfPCell datos66 = (new PdfPCell(new Paragraph(contador + "", FontFactory.GetFont("arial", 12, BaseColor.BLACK))) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE });
                datos66.Colspan = 1;
                datos66.Rowspan = 1;
                table.AddCell(datos66);
                PdfPCell datos6 = (new PdfPCell(new Paragraph( item["correo"].ToString(), FontFactory.GetFont("arial", 12, BaseColor.BLACK))) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE });
                datos6.Colspan = 1;
                datos6.Rowspan = 1;
                table.AddCell(datos6);
                PdfPCell datos7 = (new PdfPCell(new Paragraph(item["nombre"].ToString(), FontFactory.GetFont("arial", 12, BaseColor.BLACK))) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE });
                datos7.Colspan = 1;
                datos7.Rowspan = 1;
                table.AddCell(datos7);
                PdfPCell datos8 = (new PdfPCell(new Paragraph(recuperActividad(Convert.ToInt32(item["id_actividad"])), FontFactory.GetFont("arial", 12, BaseColor.BLACK))) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE });
                datos8.Colspan = 1;
                datos8.Rowspan = 1;
                table.AddCell(datos8);
                PdfPCell datos9 = (new PdfPCell(new Paragraph(item["horario"].ToString(), FontFactory.GetFont("arial", 12, BaseColor.BLACK))) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE });
                datos9.Colspan = 1;
                datos9.Rowspan = 1;
                table.AddCell(datos9);
                if (item["estatus"].ToString() == "1")
                {
                    PdfPCell datos10 = (new PdfPCell(new Paragraph("Pagado", FontFactory.GetFont("arial", 12, BaseColor.BLACK))) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE });
                    datos10.Colspan = 1;
                    datos10.Rowspan = 1;
                    table.AddCell(datos10);
                }
                if (item["estatus"].ToString() == "0")
                {
                    PdfPCell datos10 = (new PdfPCell(new Paragraph("Debe", FontFactory.GetFont("arial", 12, BaseColor.BLACK))) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE });
                    datos10.Colspan = 1;
                    datos10.Rowspan = 1;
                    table.AddCell(datos10);
                }
                contador++;

            }

           
            
            documento.Add(table);
            documento.Close();
            MessageBox.Show("Se creo el pdf con exito");
        }

        private void ListaCompaneros_Load(object sender, EventArgs e)
        {
            //Aqui va mi codigo
            Conexion.ConnectionString = Cadenaconexion;
            int id = Convert.ToInt32(recuperaIDActividad(MenuSocio.nombre_usuario));
            MySqlDataAdapter comandobus = new MySqlDataAdapter("SELECT correo,nombre,id_actividad,horario,estatus FROM socio WHERE id_actividad=" + id + ";", Conexion);
            DataTable dt = new System.Data.DataTable();
            comandobus.Fill(dt);
            int contador = 1;
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
               
                dataGridView1.Rows[n].Cells[0].Value = contador + "";

               
                dataGridView1.Rows[n].Cells[1].Value = item["correo"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["nombre"].ToString();
                dataGridView1.Rows[n].Cells[3].Value = recuperActividad(Convert.ToInt32(item["id_actividad"]));
                dataGridView1.Rows[n].Cells[4].Value = item["horario"].ToString();
                dataGridView1.Rows[n].Cells[5].Value = item["estatus"].ToString();
                if (item["estatus"].ToString() == "1")
                {
                    dataGridView1.Rows[n].Cells[5].Value = "Pagado";
                }
                else if (item["estatus"].ToString() == "0")
                {
                    dataGridView1.Rows[n].Cells[5].Value = "Debe";
                }
                contador++;

            }

        }
       
        private void button2_Click(object sender, EventArgs e)
        {
            

        }

        
        private void button4_Click(object sender, EventArgs e)
        {
            //Para regresar a tu form 'MenuAmin' Hacemos lo mismo que en el login
            //Siembre debes de pasarle los paramettros
            /*MenuSocio admin = new MenuSocio(correo, perfil);
            this.Hide();
            admin.Show();*/
            this.Close();
        }

        //método para recuperar el id d ela actividad del socio activo
        public static String recuperaIDActividad(String correo){
            MySqlConnection conexion = null;
            conexion = new MySqlConnection();
            String cadenaconexion = "Server=localhost;User id=root; Database=proyecto;Password=;";//preparar la ruta
            conexion.ConnectionString = cadenaconexion;
            MySqlCommand bus = new MySqlCommand("Select id_actividad from socio where correo='" + correo + "'");
            bus.Connection = conexion;
            conexion.Open();
            MySqlDataReader myreader = bus.ExecuteReader();
            String id = "";
            try
            {
                if (myreader.HasRows)
                {
                    while (myreader.Read())
                    {
                        id = myreader[0].ToString();
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("" + error);
            }
            conexion.Close();
            return id;
        }

        //metodo para recuperar el nombre de la actividad
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

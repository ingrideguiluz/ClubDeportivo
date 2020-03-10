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

using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace PROYECTOclub
{
    public partial class GenerarCredencial : Form
    {
        MySqlConnection conexion = new MySqlConnection("Server=localhost;User id=root; Database=proyecto; Password=;");
        String correo = ""; //En esta variable se va a guardar el correo que envies desde el login
        int perfil = 0; //El '2' será para socios, el '1' para admin
        public static String actividad = "";
        public GenerarCredencial(String correoSocio, int perfil)
        {
            InitializeComponent();
            this.correo = correoSocio; //La variable de arriba 'correo' recibe el correo de tu form login
            this.perfil = perfil; //La variable perfil en este caso resibe un 1 porque es admin
            cargar_datos();
            pictureBox1.Image = System.Drawing.Image.FromFile(MenuSocio.imagenfoto);

        }

        private void cargar_datos()
        {
            MySqlCommand comando1 = new MySqlCommand("SELECT correo, nombre, fecha_nac, telefono, horario, id_actividad, fecha_insc, estatus FROM socio WHERE correo=@correo");
            comando1.Connection =  conexion;

            MySqlParameter parametro1 = new MySqlParameter();
            parametro1.ParameterName = "@correo";
            parametro1.Value = correo;
            comando1.Parameters.Add(parametro1);

            try
            {
                conexion.Open();

                MySqlDataAdapter sda = new MySqlDataAdapter(comando1);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                if (dt.Rows.Count == 1)
                {
                    DateTime fecha1 =DateTime.Parse(dt.Rows[0][2].ToString());
                    DateTime fecha2 = DateTime.Parse(dt.Rows[0][6].ToString());
                    recuperUsuario(dt.Rows[0][5].ToString());
                    textBox1.Text = dt.Rows[0][0].ToString();
                    textBox2.Text = dt.Rows[0][1].ToString();
                    textBox3.Text = fecha1.ToString("dd-MM-yyyy"); //fecha imprime la hora
                    textBox4.Text = dt.Rows[0][3].ToString();
                    textBox5.Text = dt.Rows[0][4].ToString();
                    textBox6.Text = dt.Rows[0][5].ToString();//Es este el de la actividad
                    textBox6.Text = actividad;

                    textBox7.Text = fecha2.ToString("dd-MM-yyyy"); //fecha imprime la hora

                    if (dt.Rows[0][7].ToString() == "1")
                    {
                        radioButton1.Checked = true;
                    }
                    else if (dt.Rows[0][7].ToString() == "0")
                    {
                        radioButton2.Checked = true;
                    }
                    else
                    {
                        radioButton1.Checked = true;
                    }

                    
                }
                else
                {
                    MessageBox.Show("Usuario Invalido", "Correo Inexistente", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexion.Close();
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {

            //
            Document documento = new Document();
            PdfWriter wri = PdfWriter.GetInstance(documento, new FileStream("C:/Users/lizar/Documents/" + MenuSocio.nombre_usuario + "-reporte.pdf", FileMode.Create));
            documento.Open();

            PdfPTable table = new PdfPTable(4);
            //table.DefaultCell.Border = 0;
            PdfPCell imagen = new PdfPCell(new Paragraph());
            iTextSharp.text.Image im = iTextSharp.text.Image.GetInstance("C:/Users/lizar/Desktop/ImagenesClub/logo.png");
            /*float porc = 80 / im.Width;
            im.ScalePercent(porc * 100);*/
            imagen.Colspan = 4;
            imagen.AddElement(im);
            table.AddCell(imagen);


            PdfPCell celdaInicial = (new PdfPCell(new Paragraph("Credencial de Socio", FontFactory.GetFont("arial", 14, BaseColor.WHITE))) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE });
            celdaInicial.BackgroundColor = BaseColor.GREEN;
            celdaInicial.Colspan = 4;//combina celdas debe tener el mismo tamaño o menor rowspan filas colpan columnas
            /*celdaInicial.BorderWidthBottom = 0;
            celdaInicial.BorderWidthLeft = 0;
            celdaInicial.BorderWidthTop = 0;
            celdaInicial.BorderWidthRight=0;*/
            table.AddCell(celdaInicial);

            PdfPCell imagen2 = new PdfPCell(new Paragraph());
            iTextSharp.text.Image im2 = iTextSharp.text.Image.GetInstance(MenuSocio.imagenfoto);//imagend el socio
            imagen2.AddElement(im2);
            float por = 50 / im.Width;
            im.ScalePercent(por * 100);
            imagen2.Colspan = 1;
            imagen2.Rowspan = 2;
            /*imagen2.BorderWidthBottom = 0;
            imagen2.BorderWidthLeft = 0;
            imagen2.BorderWidthTop = 0;
            imagen2.BorderWidthRight = 0;*/
            table.AddCell(imagen2);


            PdfPCell nombre = (new PdfPCell(new Paragraph("Correo:  " + textBox1.Text, FontFactory.GetFont("arial", 12, 1, BaseColor.BLACK))) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE });
            nombre.Colspan = 3;
            nombre.Rowspan = 1;
            /*nombre.BorderWidthBottom = 0;
            nombre.BorderWidthLeft = 0;
            nombre.BorderWidthTop = 0;
            nombre.BorderWidthRight = 0;*/
            table.AddCell(nombre);

            PdfPCell correo = (new PdfPCell(new Paragraph("Nombre:" + textBox2.Text, FontFactory.GetFont("arial", 12, BaseColor.BLACK))) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE });
            correo.Colspan = 3;
            correo.Rowspan = 1;
            table.AddCell(correo);

            PdfPCell fechanaci = (new PdfPCell(new Paragraph("Fecha de Nacimiento:" + textBox3.Text, FontFactory.GetFont("arial", 12, BaseColor.BLACK))) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE });
            fechanaci.Colspan = 4;
            fechanaci.Rowspan = 1;
            table.AddCell(fechanaci);

            PdfPCell tip = new PdfPCell(new Paragraph(" Status: "));
            tip.Colspan = 2;
            tip.Rowspan = 1;
            table.AddCell(tip);

            if (radioButton1.Checked == true)
            {
                PdfPCell tip2 = (new PdfPCell(new Paragraph(radioButton1.Text , FontFactory.GetFont("arial", 12, BaseColor.BLACK))) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE });
                tip2.Colspan = 2;
                tip2.Rowspan = 1;
                table.AddCell(tip2);
            }
            if (radioButton2.Checked == true)
            {
                PdfPCell tip2 = (new PdfPCell(new Paragraph(radioButton2.Text , FontFactory.GetFont("arial", 12, BaseColor.BLACK))) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE });
                tip2.Colspan = 2;
                tip2.Rowspan = 1;
                table.AddCell(tip2);
            }


            PdfPCell actividad = new PdfPCell(new Paragraph(" Actividad: "));
            actividad.Colspan = 2;
            actividad.Rowspan = 1;
            table.AddCell(actividad);

            PdfPCell actividad2 = (new PdfPCell(new Paragraph( textBox6.Text, FontFactory.GetFont("arial", 12, 1, BaseColor.BLACK))) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE });
            actividad2.Colspan = 2;
            actividad2.Rowspan = 1;
            table.AddCell(actividad2);

            PdfPCell horario = new PdfPCell(new Paragraph(" Horario:  "));
            horario.Colspan = 2;
            horario.Rowspan = 1;
            table.AddCell(horario);

            PdfPCell horario2 = (new PdfPCell(new Paragraph( textBox5.Text, FontFactory.GetFont("arial", 12, 1, BaseColor.BLACK))) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE });
            horario2.Colspan = 2;
            horario2.Rowspan = 1;
            table.AddCell(horario2);



            PdfPCell tiempo = (new PdfPCell(new Paragraph("Fecha de Inscripción: " + textBox7.Text, FontFactory.GetFont("arial", 12, 1, BaseColor.BLACK))) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE });
            tiempo.Colspan = 4;
            tiempo.Rowspan = 1;
            table.AddCell(tiempo);


            documento.Add(table);
            documento.Close();
            MessageBox.Show("Se creo el pdf con exito");
        }

        private void button2_Click(object sender, EventArgs e)
        {

            //Para regresar al form 'MenuAmin' Hacemos lo mismo que en el login
            //Siembre debemos de pasarle los paramettros
            /*MenuSocio admin = new MenuSocio(correo, perfil);
            this.Hide();
            admin.Show();
            */
            this.Close();
        }

        private void GenerarCredencial_Load(object sender, EventArgs e)
        {

        }
        public void recuperUsuario(String id_actividad)
        {


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
            

        }
    }
    
}

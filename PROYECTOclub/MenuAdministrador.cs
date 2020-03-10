using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROYECTOclub
{
    public partial class MenuAdministrador : Form
    {

    
        String correo = ""; //En esta variable se va a guardar el correo que envies desde el login
        int perfil = 0; //El '2' será para socios, el '1' para admin
        

        public MenuAdministrador(String correo, int perfil)
        {
            InitializeComponent();
            this.correo = correo; //La variable de arriba 'correo' recibe el correo de tu form login
            this.perfil = perfil; //La variable perfil en este caso resibe un 1 porque es admin
        }


        //Para utilizar otro form seria de la misma forma

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AltaAdministrador altaAdmin = new AltaAdministrador();
            altaAdmin.Show();
            this.Hide();
            
        }

        private void ConsultasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Consultas consul = new Consultas();
            consul.MdiParent = this;
            consul.Show();
        }


        private void BuscarSocioToolStripMenuItem_Click(object sender, EventArgs e)
        {

            BuscarSocio bs = new BuscarSocio(correo, perfil);
            bs.MdiParent = this;
            bs.Show();
        }

       
        private void altasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AltaAdministrador altaAdmin = new AltaAdministrador();
            altaAdmin.MdiParent = this;
            altaAdmin.Show();
            //this.Hide();
        }

        private void filtarSocioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FiltrarSocio filtrarsocio = new FiltrarSocio();
            filtrarsocio.MdiParent = this;
            filtrarsocio.Show();
        }

        private void AltasSocioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AltaSocio altaSocio = new AltaSocio(correo, perfil);
            altaSocio.MdiParent = this;
            altaSocio.Show();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void MenuAdministrador_Load(object sender, EventArgs e)
        {

        }

        private void opcionesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cerrarSesiónToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea cerrar sesión?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Form1 log = new Form1();
                log.Show();
                this.Hide();
                //consul.Close();
                //bs.Close();
                //filtrarsocio.Close();
                //altaSocio.Close();
                //altaAdmin.Close();
            } 


        }
        /*void Form_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = false;
            Application.Exit();
        }*/

        private void salirDeLaAplicaciónToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("¿Desea salir de la aplicación?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
                
            }  
        }


    }
}

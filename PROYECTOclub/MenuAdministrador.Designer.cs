namespace PROYECTOclub
{
    partial class MenuAdministrador
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuAdministrador));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.opcionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.altasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.consultasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.socioToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.buscarSociosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actividadesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filtarSocioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oPCIONESToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.cerrarSesiónToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.salirDeLaAplicaciónToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.opcionesToolStripMenuItem,
            this.socioToolStripMenuItem1,
            this.oPCIONESToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1385, 27);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // opcionesToolStripMenuItem
            // 
            this.opcionesToolStripMenuItem.BackColor = System.Drawing.Color.GreenYellow;
            this.opcionesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.altasToolStripMenuItem,
            this.consultasToolStripMenuItem});
            this.opcionesToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.opcionesToolStripMenuItem.Name = "opcionesToolStripMenuItem";
            this.opcionesToolStripMenuItem.Size = new System.Drawing.Size(58, 23);
            this.opcionesToolStripMenuItem.Text = "ALTAS";
            this.opcionesToolStripMenuItem.Click += new System.EventHandler(this.opcionesToolStripMenuItem_Click);
            // 
            // altasToolStripMenuItem
            // 
            this.altasToolStripMenuItem.BackColor = System.Drawing.Color.LimeGreen;
            this.altasToolStripMenuItem.Name = "altasToolStripMenuItem";
            this.altasToolStripMenuItem.Size = new System.Drawing.Size(182, 24);
            this.altasToolStripMenuItem.Text = "Administradores";
            this.altasToolStripMenuItem.Click += new System.EventHandler(this.altasToolStripMenuItem_Click);
            // 
            // consultasToolStripMenuItem
            // 
            this.consultasToolStripMenuItem.BackColor = System.Drawing.Color.LimeGreen;
            this.consultasToolStripMenuItem.Name = "consultasToolStripMenuItem";
            this.consultasToolStripMenuItem.Size = new System.Drawing.Size(182, 24);
            this.consultasToolStripMenuItem.Text = "Socios";
            this.consultasToolStripMenuItem.Click += new System.EventHandler(this.AltasSocioToolStripMenuItem_Click);
            // 
            // socioToolStripMenuItem1
            // 
            this.socioToolStripMenuItem1.BackColor = System.Drawing.Color.Chartreuse;
            this.socioToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buscarSociosToolStripMenuItem,
            this.actividadesToolStripMenuItem,
            this.filtarSocioToolStripMenuItem});
            this.socioToolStripMenuItem1.Name = "socioToolStripMenuItem1";
            this.socioToolStripMenuItem1.Size = new System.Drawing.Size(96, 23);
            this.socioToolStripMenuItem1.Text = "CONSULTAS";
            // 
            // buscarSociosToolStripMenuItem
            // 
            this.buscarSociosToolStripMenuItem.BackColor = System.Drawing.Color.LimeGreen;
            this.buscarSociosToolStripMenuItem.Name = "buscarSociosToolStripMenuItem";
            this.buscarSociosToolStripMenuItem.Size = new System.Drawing.Size(187, 24);
            this.buscarSociosToolStripMenuItem.Text = "Consulta General";
            this.buscarSociosToolStripMenuItem.Click += new System.EventHandler(this.ConsultasToolStripMenuItem_Click);
            // 
            // actividadesToolStripMenuItem
            // 
            this.actividadesToolStripMenuItem.BackColor = System.Drawing.Color.LimeGreen;
            this.actividadesToolStripMenuItem.Name = "actividadesToolStripMenuItem";
            this.actividadesToolStripMenuItem.Size = new System.Drawing.Size(187, 24);
            this.actividadesToolStripMenuItem.Text = "Buscar Socio";
            this.actividadesToolStripMenuItem.Click += new System.EventHandler(this.BuscarSocioToolStripMenuItem_Click);
            // 
            // filtarSocioToolStripMenuItem
            // 
            this.filtarSocioToolStripMenuItem.BackColor = System.Drawing.Color.LimeGreen;
            this.filtarSocioToolStripMenuItem.Name = "filtarSocioToolStripMenuItem";
            this.filtarSocioToolStripMenuItem.Size = new System.Drawing.Size(187, 24);
            this.filtarSocioToolStripMenuItem.Text = "Filtar Socio";
            this.filtarSocioToolStripMenuItem.Click += new System.EventHandler(this.filtarSocioToolStripMenuItem_Click);
            // 
            // oPCIONESToolStripMenuItem1
            // 
            this.oPCIONESToolStripMenuItem1.BackColor = System.Drawing.Color.GreenYellow;
            this.oPCIONESToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cerrarSesiónToolStripMenuItem1,
            this.salirDeLaAplicaciónToolStripMenuItem1});
            this.oPCIONESToolStripMenuItem1.Name = "oPCIONESToolStripMenuItem1";
            this.oPCIONESToolStripMenuItem1.Size = new System.Drawing.Size(88, 23);
            this.oPCIONESToolStripMenuItem1.Text = "OPCIONES";
            // 
            // cerrarSesiónToolStripMenuItem1
            // 
            this.cerrarSesiónToolStripMenuItem1.BackColor = System.Drawing.Color.ForestGreen;
            this.cerrarSesiónToolStripMenuItem1.Name = "cerrarSesiónToolStripMenuItem1";
            this.cerrarSesiónToolStripMenuItem1.Size = new System.Drawing.Size(203, 24);
            this.cerrarSesiónToolStripMenuItem1.Text = "Cerrar sesión";
            this.cerrarSesiónToolStripMenuItem1.Click += new System.EventHandler(this.cerrarSesiónToolStripMenuItem1_Click);
            // 
            // salirDeLaAplicaciónToolStripMenuItem1
            // 
            this.salirDeLaAplicaciónToolStripMenuItem1.BackColor = System.Drawing.Color.Firebrick;
            this.salirDeLaAplicaciónToolStripMenuItem1.Name = "salirDeLaAplicaciónToolStripMenuItem1";
            this.salirDeLaAplicaciónToolStripMenuItem1.Size = new System.Drawing.Size(203, 24);
            this.salirDeLaAplicaciónToolStripMenuItem1.Text = "Salir de la aplicación";
            this.salirDeLaAplicaciónToolStripMenuItem1.Click += new System.EventHandler(this.salirDeLaAplicaciónToolStripMenuItem1_Click);
            // 
            // MenuAdministrador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGreen;
            this.ClientSize = new System.Drawing.Size(1385, 706);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MenuAdministrador";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MenuAdministrador";
            this.Load += new System.EventHandler(this.MenuAdministrador_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem opcionesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem altasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem consultasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem socioToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem buscarSociosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem actividadesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filtarSocioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oPCIONESToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem cerrarSesiónToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem salirDeLaAplicaciónToolStripMenuItem1;
    }
}
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CustomControlsProject {
    partial class Aggiungi {
        /// <summary> 
        /// Variabile di progettazione necessaria.
        /// </summary>
        public System.ComponentModel.IContainer components = null;
        public System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.OpenFileDialog openFileDialog1;
        public System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Label label9;
        public System.Windows.Forms.TextBox txtSella;
        public System.Windows.Forms.NumericUpDown NAB;
        public System.Windows.Forms.Label lblChange;
        public System.Windows.Forms.Label label8;
        public System.Windows.Forms.NumericUpDown NCilometraggio;
        public System.Windows.Forms.Label label7;
        public System.Windows.Forms.CheckBox chkKm0;
        public System.Windows.Forms.CheckBox chkUsato;
        public System.Windows.Forms.Label label6;
        public System.Windows.Forms.NumericUpDown NPotenza;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.NumericUpDown NCilindrata;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox txtModello;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Button btnAggiungi;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.NumericUpDown NPrezzo;
        public System.Windows.Forms.Label label10;
        public System.Windows.Forms.ComboBox cmbMarca;
        public System.Windows.Forms.DateTimePicker dateTimePicker1;
        /// <summary> 
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione componenti

        /// <summary> 
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare 
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Aggiungi));
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtColore = new System.Windows.Forms.TextBox();
            this.txtTarga = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnImage = new System.Windows.Forms.Button();
            this.cmbMarca = new System.Windows.Forms.ComboBox();
            this.NPrezzo = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.btnSfoglia = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.txtSella = new System.Windows.Forms.TextBox();
            this.NAB = new System.Windows.Forms.NumericUpDown();
            this.lblChange = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.NCilometraggio = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.chkKm0 = new System.Windows.Forms.CheckBox();
            this.chkUsato = new System.Windows.Forms.CheckBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.NPotenza = new System.Windows.Forms.NumericUpDown();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.NCilindrata = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.txtModello = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAggiungi = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NPrezzo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NAB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NCilometraggio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NPotenza)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NCilindrata)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtColore);
            this.panel1.Controls.Add(this.txtTarga);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.btnImage);
            this.panel1.Controls.Add(this.cmbMarca);
            this.panel1.Controls.Add(this.NPrezzo);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.btnSfoglia);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.txtSella);
            this.panel1.Controls.Add(this.NAB);
            this.panel1.Controls.Add(this.lblChange);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.NCilometraggio);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.chkKm0);
            this.panel1.Controls.Add(this.chkUsato);
            this.panel1.Controls.Add(this.dateTimePicker1);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.NPotenza);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.NCilindrata);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtModello);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnAggiungi);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1020, 539);
            this.panel1.TabIndex = 27;
            // 
            // txtColore
            // 
            this.txtColore.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.txtColore.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtColore.ForeColor = System.Drawing.SystemColors.Window;
            this.txtColore.Location = new System.Drawing.Point(451, 169);
            this.txtColore.Name = "txtColore";
            this.txtColore.Size = new System.Drawing.Size(177, 24);
            this.txtColore.TabIndex = 60;
            // 
            // txtTarga
            // 
            this.txtTarga.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.txtTarga.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTarga.ForeColor = System.Drawing.SystemColors.Window;
            this.txtTarga.Location = new System.Drawing.Point(149, 104);
            this.txtTarga.Name = "txtTarga";
            this.txtTarga.Size = new System.Drawing.Size(177, 24);
            this.txtTarga.TabIndex = 58;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label11.Location = new System.Drawing.Point(69, 107);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(50, 18);
            this.label11.TabIndex = 59;
            this.label11.Text = "Targa:";
            // 
            // btnImage
            // 
            this.btnImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.btnImage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnImage.BackgroundImage")));
            this.btnImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnImage.Location = new System.Drawing.Point(451, 18);
            this.btnImage.Name = "btnImage";
            this.btnImage.Size = new System.Drawing.Size(179, 62);
            this.btnImage.TabIndex = 57;
            this.btnImage.TabStop = false;
            this.btnImage.UseVisualStyleBackColor = false;
            this.btnImage.Click += new System.EventHandler(this.BunifuImageButton1_Click);
            // 
            // cmbMarca
            // 
            this.cmbMarca.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbMarca.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbMarca.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.cmbMarca.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbMarca.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMarca.ForeColor = System.Drawing.SystemColors.Window;
            this.cmbMarca.FormattingEnabled = true;
            this.cmbMarca.Location = new System.Drawing.Point(149, 18);
            this.cmbMarca.Name = "cmbMarca";
            this.cmbMarca.Size = new System.Drawing.Size(176, 26);
            this.cmbMarca.Sorted = true;
            this.cmbMarca.TabIndex = 1;
            // 
            // NPrezzo
            // 
            this.NPrezzo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.NPrezzo.DecimalPlaces = 2;
            this.NPrezzo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NPrezzo.ForeColor = System.Drawing.SystemColors.Window;
            this.NPrezzo.Location = new System.Drawing.Point(451, 205);
            this.NPrezzo.Maximum = new decimal(new int[] {
            -559939584,
            902409669,
            54,
            0});
            this.NPrezzo.Name = "NPrezzo";
            this.NPrezzo.Size = new System.Drawing.Size(179, 24);
            this.NPrezzo.TabIndex = 6;
            this.NPrezzo.ThousandsSeparator = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label10.Location = new System.Drawing.Point(376, 208);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(60, 18);
            this.label10.TabIndex = 51;
            this.label10.Text = "Prezzo:";
            // 
            // btnSfoglia
            // 
            this.btnSfoglia.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.btnSfoglia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSfoglia.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSfoglia.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSfoglia.Location = new System.Drawing.Point(451, 251);
            this.btnSfoglia.Name = "btnSfoglia";
            this.btnSfoglia.Size = new System.Drawing.Size(179, 35);
            this.btnSfoglia.TabIndex = 10;
            this.btnSfoglia.Text = "Sfoglia";
            this.btnSfoglia.UseVisualStyleBackColor = false;
            this.btnSfoglia.Click += new System.EventHandler(this.Sfoglia_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label9.Location = new System.Drawing.Point(393, 259);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 18);
            this.label9.TabIndex = 49;
            this.label9.Text = "Foto:";
            // 
            // txtSella
            // 
            this.txtSella.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.txtSella.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSella.ForeColor = System.Drawing.SystemColors.Window;
            this.txtSella.Location = new System.Drawing.Point(150, 239);
            this.txtSella.Name = "txtSella";
            this.txtSella.Size = new System.Drawing.Size(176, 24);
            this.txtSella.TabIndex = 5;
            // 
            // NAB
            // 
            this.NAB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.NAB.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NAB.ForeColor = System.Drawing.SystemColors.Window;
            this.NAB.Location = new System.Drawing.Point(150, 239);
            this.NAB.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.NAB.Name = "NAB";
            this.NAB.Size = new System.Drawing.Size(176, 24);
            this.NAB.TabIndex = 5;
            this.NAB.ThousandsSeparator = true;
            // 
            // lblChange
            // 
            this.lblChange.AutoSize = true;
            this.lblChange.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChange.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblChange.Location = new System.Drawing.Point(23, 242);
            this.lblChange.Name = "lblChange";
            this.lblChange.Size = new System.Drawing.Size(20, 18);
            this.lblChange.TabIndex = 45;
            this.lblChange.Text = "...";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label8.Location = new System.Drawing.Point(379, 172);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 18);
            this.label8.TabIndex = 44;
            this.label8.Text = "Colore:";
            // 
            // NCilometraggio
            // 
            this.NCilometraggio.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.NCilometraggio.Enabled = false;
            this.NCilometraggio.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NCilometraggio.ForeColor = System.Drawing.SystemColors.Window;
            this.NCilometraggio.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.NCilometraggio.Location = new System.Drawing.Point(150, 191);
            this.NCilometraggio.Maximum = new decimal(new int[] {
            276447232,
            23283,
            0,
            0});
            this.NCilometraggio.Name = "NCilometraggio";
            this.NCilometraggio.Size = new System.Drawing.Size(176, 24);
            this.NCilometraggio.TabIndex = 4;
            this.NCilometraggio.ThousandsSeparator = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label7.Location = new System.Drawing.Point(22, 193);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(112, 18);
            this.label7.TabIndex = 41;
            this.label7.Text = "Chilometraggio:";
            // 
            // chkKm0
            // 
            this.chkKm0.AutoSize = true;
            this.chkKm0.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkKm0.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkKm0.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.chkKm0.Location = new System.Drawing.Point(179, 294);
            this.chkKm0.Name = "chkKm0";
            this.chkKm0.Size = new System.Drawing.Size(146, 22);
            this.chkKm0.TabIndex = 12;
            this.chkKm0.Text = "Chilometro ZERO";
            this.chkKm0.UseVisualStyleBackColor = true;
            // 
            // chkUsato
            // 
            this.chkUsato.AutoSize = true;
            this.chkUsato.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkUsato.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkUsato.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.chkUsato.Location = new System.Drawing.Point(67, 294);
            this.chkUsato.Name = "chkUsato";
            this.chkUsato.Size = new System.Drawing.Size(67, 22);
            this.chkUsato.TabIndex = 11;
            this.chkUsato.Text = "Usato";
            this.chkUsato.UseVisualStyleBackColor = true;
            this.chkUsato.CheckedChanged += new System.EventHandler(this.ChkUsato_CheckedChanged);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CalendarForeColor = System.Drawing.SystemColors.ControlLight;
            this.dateTimePicker1.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.dateTimePicker1.CalendarTitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.dateTimePicker1.CalendarTitleForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.dateTimePicker1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Location = new System.Drawing.Point(150, 145);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(176, 24);
            this.dateTimePicker1.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label6.Location = new System.Drawing.Point(9, 150);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(125, 18);
            this.label6.TabIndex = 37;
            this.label6.Text = "Immatricolazione:";
            // 
            // NPotenza
            // 
            this.NPotenza.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.NPotenza.DecimalPlaces = 2;
            this.NPotenza.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NPotenza.ForeColor = System.Drawing.SystemColors.Window;
            this.NPotenza.Location = new System.Drawing.Point(451, 127);
            this.NPotenza.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.NPotenza.Name = "NPotenza";
            this.NPotenza.Size = new System.Drawing.Size(179, 24);
            this.NPotenza.TabIndex = 8;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(645, 18);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(346, 317);
            this.pictureBox1.TabIndex = 55;
            this.pictureBox1.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label5.Location = new System.Drawing.Point(369, 130);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 18);
            this.label5.TabIndex = 35;
            this.label5.Text = "Potenza:";
            // 
            // NCilindrata
            // 
            this.NCilindrata.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.NCilindrata.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NCilindrata.ForeColor = System.Drawing.SystemColors.Window;
            this.NCilindrata.Location = new System.Drawing.Point(451, 86);
            this.NCilindrata.Maximum = new decimal(new int[] {
            6000,
            0,
            0,
            0});
            this.NCilindrata.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.NCilindrata.Name = "NCilindrata";
            this.NCilindrata.Size = new System.Drawing.Size(179, 24);
            this.NCilindrata.TabIndex = 2;
            this.NCilindrata.ThousandsSeparator = true;
            this.NCilindrata.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label4.Location = new System.Drawing.Point(363, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 18);
            this.label4.TabIndex = 33;
            this.label4.Text = "Cilindrata:";
            // 
            // txtModello
            // 
            this.txtModello.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.txtModello.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtModello.ForeColor = System.Drawing.SystemColors.Window;
            this.txtModello.Location = new System.Drawing.Point(149, 65);
            this.txtModello.Name = "txtModello";
            this.txtModello.Size = new System.Drawing.Size(176, 24);
            this.txtModello.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label3.Location = new System.Drawing.Point(68, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 18);
            this.label3.TabIndex = 31;
            this.label3.Text = "Modello:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(79, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 18);
            this.label2.TabIndex = 30;
            this.label2.Text = "Marca:";
            // 
            // btnAggiungi
            // 
            this.btnAggiungi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAggiungi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.btnAggiungi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAggiungi.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAggiungi.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnAggiungi.Location = new System.Drawing.Point(814, 488);
            this.btnAggiungi.Name = "btnAggiungi";
            this.btnAggiungi.Size = new System.Drawing.Size(177, 35);
            this.btnAggiungi.TabIndex = 13;
            this.btnAggiungi.Text = "Aggiungi";
            this.btnAggiungi.UseVisualStyleBackColor = false;
            this.btnAggiungi.Click += new System.EventHandler(this.BtnAggiungi_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(344, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 18);
            this.label1.TabIndex = 27;
            this.label1.Text = "Tipo veicolo:";
            // 
            // Aggiungi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.Controls.Add(this.panel1);
            this.Name = "Aggiungi";
            this.Size = new System.Drawing.Size(1020, 539);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NPrezzo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NAB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NCilometraggio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NPotenza)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NCilindrata)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Button btnSfoglia;
        private Button btnImage;
        private TextBox txtTarga;
        private Label label11;
        private TextBox txtColore;
    }
}

using System.Windows.Forms;

namespace CustomControlsProject {
    partial class CardDetails {
        /// <summary> 
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        /// <summary> 
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare 
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblPrezzo = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.dgvDettagli = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblMarcaModello = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDettagli)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblPrezzo
            // 
            this.lblPrezzo.AutoSize = true;
            this.lblPrezzo.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrezzo.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblPrezzo.Location = new System.Drawing.Point(12, 50);
            this.lblPrezzo.Name = "lblPrezzo";
            this.lblPrezzo.Size = new System.Drawing.Size(31, 29);
            this.lblPrezzo.TabIndex = 26;
            this.lblPrezzo.Text = "...";
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(125)))), ((int)(((byte)(168)))));
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnPrint.Location = new System.Drawing.Point(667, 454);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(173, 23);
            this.btnPrint.TabIndex = 25;
            this.btnPrint.Text = "Esporta in Word";
            this.btnPrint.UseVisualStyleBackColor = false;
            // 
            // dgvDettagli
            // 
            this.dgvDettagli.AllowUserToAddRows = false;
            this.dgvDettagli.AllowUserToDeleteRows = false;
            this.dgvDettagli.AllowUserToResizeColumns = false;
            this.dgvDettagli.AllowUserToResizeRows = false;
            this.dgvDettagli.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDettagli.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDettagli.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.dgvDettagli.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDettagli.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDettagli.ColumnHeadersVisible = false;
            this.dgvDettagli.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.dgvDettagli.Font = new System.Drawing.Font("Verdana", 10F);
            this.dgvDettagli.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.dgvDettagli.Location = new System.Drawing.Point(17, 94);
            this.dgvDettagli.Name = "dgvDettagli";
            this.dgvDettagli.RowHeadersVisible = false;
            this.dgvDettagli.RowTemplate.Height = 35;
            this.dgvDettagli.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDettagli.Size = new System.Drawing.Size(326, 340);
            this.dgvDettagli.TabIndex = 24;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Column2";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // lblMarcaModello
            // 
            this.lblMarcaModello.AutoSize = true;
            this.lblMarcaModello.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMarcaModello.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblMarcaModello.Location = new System.Drawing.Point(12, 9);
            this.lblMarcaModello.Name = "lblMarcaModello";
            this.lblMarcaModello.Size = new System.Drawing.Size(31, 29);
            this.lblMarcaModello.TabIndex = 22;
            this.lblMarcaModello.Text = "...";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.pictureBox1.Location = new System.Drawing.Point(401, 50);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(439, 386);
            this.pictureBox1.TabIndex = 21;
            this.pictureBox1.TabStop = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button1.Location = new System.Drawing.Point(488, 454);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(173, 23);
            this.button1.TabIndex = 27;
            this.button1.Text = "Esci";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.Esci_Click);
            // 
            // CardDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblPrezzo);
            this.Controls.Add(this.dgvDettagli);
            this.Controls.Add(this.lblMarcaModello);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.pictureBox1);
            this.Location = new System.Drawing.Point(14, 1);
            this.Name = "CardDetails";
            this.Size = new System.Drawing.Size(853, 489);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDettagli)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        public System.Windows.Forms.OpenFileDialog openFileDialog1;//APERTURA FILE (DETTAGLI)
        public System.Windows.Forms.Button btnPrint;//BOTTONE STAMPA (DETTAGLI)
        public System.Windows.Forms.DataGridView dgvDettagli;//DGV (DETTAGLI)
        public System.Windows.Forms.DataGridViewTextBoxColumn Column1;//COLONNA NOME DATI (DETTAGLI)
        public System.Windows.Forms.DataGridViewTextBoxColumn Column2;//COLONNA DATI MODIFICABILI (DETTAGLI)
        public System.Windows.Forms.Label lblMarcaModello;//MARCA+SPAZIO+MODELLO (DETTAGLI)
        public System.Windows.Forms.PictureBox pictureBox1;//IMMAGINE (DETTAGLI)
        public System.Windows.Forms.Label lblPrezzo;//PREZZO+€ (DETTAGLI)
        public Button button1;
    }
}

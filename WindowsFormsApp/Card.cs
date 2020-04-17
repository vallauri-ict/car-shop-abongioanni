using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.VisualBasic;
using OpenXmlDllProject;
using VenditaVeicoliDllProject;
using Risorse;
using Color = System.Drawing.Color;

namespace WindowsFormsApp {
    public partial class Card : UserControl {
        private Veicolo mezzo;
        private TabControl addTo;
        private TabPage tDettagli = null;
        private Veicolo Mezzo { get => this.mezzo; set => this.mezzo = value; }
        private TabControl AddTo { get => this.addTo; set => this.addTo = value; }
        private readonly FlowLayoutPanel PnlMain;

        private System.Windows.Forms.OpenFileDialog openFileDialog1;//APERTURA FILE (DETTAGLI)
        private System.Windows.Forms.Button btnPrint;//BOTTONE STAMPA (DETTAGLI)
        private System.Windows.Forms.Button btnCancel;//BOTTONE ANNULLA (DETTAGLI)
        private System.Windows.Forms.Button btnSell;//BOTTONE VENDUTO (DETTAGLI)
        private System.Windows.Forms.DataGridView dgvDettagli;//DGV (DETTAGLI)
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;//COLONNA NOME DATI (DETTAGLI)
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;//COLONNA DATI MODIFICABILI (DETTAGLI)
        private System.Windows.Forms.Label lblMarcaModello;//MARCA+SPAZIO+MODELLO (DETTAGLI)
        private System.Windows.Forms.PictureBox pictureBox1;//IMMAGINE (DETTAGLI)
        private System.Windows.Forms.Label lblPrezzo;//PREZZO+€ (DETTAGLI)

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.FillRoundedRectangle(new SolidBrush(Color.FromArgb(37, 37, 38)), 0, 0, this.Width, this.Height, 10);
        }

        public Card(Veicolo mezzo, FlowLayoutPanel pnlMain, TabControl addTo)
        {
            InitializeComponent();
            this.Mezzo = mezzo;
            this.PnlMain = pnlMain;
            this.btnDelete.Click += new EventHandler(this.BtnDelete_Click);//CLICK ELIMINA
            this.AddTo = addTo as TabControl;
            pnlMain.Controls.Add(this);
            Image image;
            try
            {
                using (Stream stream = File.OpenRead(this.Mezzo.ImagePath))
                {
                    image = System.Drawing.Image.FromStream(stream);
                }
            }
            catch
            {
                using (Stream stream = File.OpenRead(Path.Combine(FormMain.imageFolderPath, "noimg.png")))
                {
                    image = System.Drawing.Image.FromStream(stream);
                }
            }
            this.pcb.BackgroundImage = image;
            this.pcb.BackgroundImageLayout = ImageLayout.Zoom;
            this.lbl.Text = $"{(this.Mezzo.Marca.ToUpper() == "MERCEDES-BENZ" ? "MB" : this.Mezzo.Marca)} {this.Mezzo.Modello} - {this.Mezzo.Targa} {this.Mezzo.Stato}";
            SizeFont(this.lbl);
        }

        private void SizeFont(Label lbl)
        {
            string txt = lbl.Text;
            if (txt.Length > 0)
            {
                int best_size = 100;
                int wid = lbl.DisplayRectangle.Width - 3;
                int hgt = lbl.DisplayRectangle.Height - 3;
                using (Graphics gr = lbl.CreateGraphics())
                {
                    for (int i = 1; i <= 100; i++)
                    {
                        System.Drawing.Font test_font = new System.Drawing.Font(lbl.Font.FontFamily, i);
                        SizeF text_size = gr.MeasureString(txt, test_font);
                        if ((text_size.Width > wid) || (text_size.Height > hgt))
                        {
                            best_size = i - 1;
                            break;
                        }
                    }
                }
                lbl.Font = new System.Drawing.Font(lbl.Font.FontFamily, best_size);
            }
        }//ADATTA LA DIMENSIONE DEL FONT ALLA DIMENSIONE DEL CONTENITORE

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Sei sicuro di voler eliminare {this.Mezzo.Marca} {this.Mezzo.Modello}?", "Elimina", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                EliminaCard();
        }//ELIMINA DELL' OGGETTO GRAFICO E DEL VEICOLO DALLA LISTA

        private void EliminaCard()
        {
            FormMain.modifica = true;
            SerializableBindingList<Veicolo> lista = (FormMain.listaVeicoli.Contains(this.Mezzo) ? FormMain.listaVeicoli : FormMain.listaVeicoliAggiunti);
            lista.Remove(this.Mezzo);
            this.PnlMain.Controls.Remove(this);
            if (this.tDettagli != null)
                this.AddTo.TabPages.Remove(this.tDettagli);
            this.tDettagli = null;
            this.pcb.BackgroundImage.Dispose();
            if (!this.Mezzo.ImagePath.Contains("noimg.png") && this.Mezzo.ImagePath != "")
            {
                FormMain.deletePaths.Add(this.Mezzo.ImagePath);
            }
            FormMain.listaVeicoliEliminati.Add(this.Mezzo);
        }

        private void Visualizza_Click(object sender, EventArgs e)
        {
            if (this.tDettagli == null)
            {
                this.tDettagli = new TabPage();
                this.AddTo.TabPages.Add(this.tDettagli);
                this.AddTo.SelectTab(this.tDettagli);
                this.tDettagli.Text = this.Mezzo.Marca + " " + this.Mezzo.Modello;
                Panel p = new Panel
                {
                    Dock = DockStyle.Fill,
                    AutoScroll = true,
                    BackColor = System.Drawing.Color.FromArgb(45, 45, 45)
                };
                this.tDettagli.Controls.Add(p);

                //CREO LA GRAFICA DINAMICAMENTE (CODICE PRESO DA UNA FORM RIMOSSA)
                #region Windows Form Designer generated code
                this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
                this.btnPrint = new System.Windows.Forms.Button();
                this.btnCancel = new System.Windows.Forms.Button();
                this.dgvDettagli = new System.Windows.Forms.DataGridView();
                this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
                this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
                this.lblMarcaModello = new System.Windows.Forms.Label();
                this.pictureBox1 = new System.Windows.Forms.PictureBox();
                this.lblPrezzo = new System.Windows.Forms.Label();
                this.btnSell = new Button();
                ((System.ComponentModel.ISupportInitialize)(this.dgvDettagli)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
                this.SuspendLayout();
                // 
                // openFileDialog1
                // 
                this.openFileDialog1.FileName = "openFileDialog1";
                // 
                // btnPrint
                // 
                this.btnPrint.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
                this.btnPrint.BackColor = System.Drawing.Color.FromArgb(50, 125, 168);
                this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                this.btnPrint.ForeColor = System.Drawing.SystemColors.ControlLightLight;
                this.btnPrint.Name = "btnPrint";
                this.btnPrint.Size = new System.Drawing.Size(173, 23);
                this.btnPrint.Location = new System.Drawing.Point(p.Width - this.btnCancel.Width - 135 - this.btnPrint.Width, p.Height - this.btnPrint.Height - 20);
                this.btnPrint.TabIndex = 25;
                this.btnPrint.Text = "Esporta in Word";
                this.btnPrint.UseVisualStyleBackColor = false;
                this.btnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
                // 
                // btnCancel
                // 
                this.btnCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
                this.btnCancel.BackColor = System.Drawing.Color.FromArgb(250, 47, 47);
                this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                this.btnCancel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
                this.btnCancel.Name = "btnCancel";
                this.btnCancel.Size = new System.Drawing.Size(173, 23);
                this.btnCancel.Location = new System.Drawing.Point(p.Width - this.btnPrint.Width - 20, p.Height - this.btnCancel.Height - 20);
                this.btnCancel.TabIndex = 25;
                this.btnCancel.Text = "Esci";
                this.btnCancel.UseVisualStyleBackColor = false;
                this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
                // 
                // btnSell
                // 
                this.btnSell.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
                this.btnSell.BackColor = System.Drawing.Color.FromArgb(37, 37, 38);
                this.btnSell.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                this.btnSell.ForeColor = System.Drawing.SystemColors.ControlLightLight;
                this.btnSell.Name = "btnSell";
                this.btnSell.Size = new System.Drawing.Size(173, 23);
                this.btnSell.Location = new System.Drawing.Point(p.Width - this.btnSell.Width - this.btnCancel.Width - 60 - this.btnPrint.Width, p.Height - this.btnSell.Height - 20);
                this.btnSell.TabIndex = 25;
                this.btnSell.Text = "Venduto";
                this.btnSell.UseVisualStyleBackColor = false;
                this.btnSell.Click += new System.EventHandler(this.BtnSell_Click);
                // 
                // dgvDettagli
                // 
                this.dgvDettagli.AllowUserToAddRows = false;
                this.dgvDettagli.AllowUserToDeleteRows = false;
                this.dgvDettagli.AllowUserToResizeColumns = false;
                this.dgvDettagli.AllowUserToResizeRows = false;
                this.dgvDettagli.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right);
                this.dgvDettagli.BackgroundColor = System.Drawing.Color.FromArgb(45, 45, 45);
                this.dgvDettagli.BorderStyle = System.Windows.Forms.BorderStyle.None;
                this.dgvDettagli.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                this.dgvDettagli.ColumnHeadersVisible = false;
                this.dgvDettagli.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
                this.dgvDettagli.GridColor = System.Drawing.Color.FromArgb(37, 37, 38);
                this.dgvDettagli.Location = new System.Drawing.Point(17, 96);
                this.dgvDettagli.Name = "dgvDettagli";
                this.dgvDettagli.RowHeadersVisible = false;
                this.dgvDettagli.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
                this.dgvDettagli.Size = new System.Drawing.Size(360, 340);
                this.dgvDettagli.TabIndex = 24;
                this.dgvDettagli.Font = new System.Drawing.Font("Verdana", 10);
                this.dgvDettagli.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                this.dgvDettagli.RowTemplate.Height = 35;
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
                this.lblMarcaModello.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
                this.lblMarcaModello.ForeColor = System.Drawing.SystemColors.ControlLightLight;
                this.lblMarcaModello.Location = new System.Drawing.Point(12, 9);
                this.lblMarcaModello.Name = "lblMarcaModello";
                this.lblMarcaModello.Size = new System.Drawing.Size(31, 29);
                this.lblMarcaModello.TabIndex = 22;
                this.lblMarcaModello.Text = "...";
                this.lblMarcaModello.Click += new System.EventHandler(this.LblMarcaModello_Click);
                // 
                // pictureBox1
                // 
                this.pictureBox1.Top = 30;
                this.pictureBox1.Name = "pictureBox1";
                this.pictureBox1.Anchor = AnchorStyles.Right;
                this.pictureBox1.Size = new System.Drawing.Size(this.tDettagli.Width / 2, this.tDettagli.Height / 2);
                this.pictureBox1.Left = p.Width - this.pictureBox1.Width - 20;
                this.pictureBox1.TabIndex = 21;
                this.pictureBox1.TabStop = false;
                this.pictureBox1.Click += new System.EventHandler(this.PictureBox1_Click);
                // 
                // lblPrezzo
                // 
                this.lblPrezzo.AutoSize = true;
                this.lblPrezzo.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
                this.lblPrezzo.ForeColor = System.Drawing.SystemColors.ControlLightLight;
                this.lblPrezzo.Location = new System.Drawing.Point(12, 50);
                this.lblPrezzo.Name = "lblPrezzo";
                this.lblPrezzo.Size = new System.Drawing.Size(31, 29);
                this.lblPrezzo.TabIndex = 26;
                this.lblPrezzo.Text = "...";
                this.lblPrezzo.Click += new System.EventHandler(this.LblPrezzo_Click);
                p.Controls.Add(this.lblPrezzo);
                p.Controls.Add(this.btnPrint);
                p.Controls.Add(this.btnCancel);
                p.Controls.Add(this.btnSell);
                p.Controls.Add(this.dgvDettagli);
                p.Controls.Add(this.lblMarcaModello);
                p.Controls.Add(this.pictureBox1);
                #endregion
                this.pictureBox1.DoubleClick += new System.EventHandler(this.PictureBox1_DoubleClick);//DOPPIO CLICK IMMAGINE
                this.tDettagli.Text = this.lblMarcaModello.Text = $"{this.Mezzo.Marca} {this.Mezzo.Modello}";//TITOLO
                this.lblPrezzo.Text = this.Mezzo.GetPrezzo();//PREZZO
                try//SE L'IMMAGINE ESISTE VIENE VISUALIZZUATA ALTRIMENTI VIENE VISUALIZZATA UN'IMMAGINE PLACEHOLDER
                {
                    this.pictureBox1.BackgroundImage = this.pcb.BackgroundImage;
                    this.pictureBox1.BackgroundImageLayout = ImageLayout.Zoom;
                }
                catch
                {
                    this.pictureBox1.BackgroundImage = Image.FromFile(Path.Combine(FormMain.imageFolderPath, "noimg.png"));
                    this.pictureBox1.BackgroundImageLayout = ImageLayout.Zoom;
                }
                Tuple<string, string>[] s =//INSIEME DI DATI DA VISULIZZARE
                {
                    new Tuple<string, string>("Targa",this.Mezzo.Targa),
                    new Tuple<string, string>("Cilindrata",this.Mezzo.Cilindrata.ToString("C").Split(',')[0] + " cc"),
                    new Tuple<string, string>("Potenza",this.Mezzo.PotenzaKw.ToString("C").Split(',')[0] + " Kw"),
                    new Tuple<string, string>("Immatricolazione",this.Mezzo.Immatricolazione.ToShortDateString()),
                    new Tuple<string, string>("Stato",this.Mezzo.Stato),
                    new Tuple<string, string>("Km Zero",this.Mezzo.IsKmZero?"Vero":"Falso"),
                    new Tuple<string, string>("Chilometraggio",this.Mezzo.KmPercorsi.ToString("C").Split(',')[0] + " Km"),
                    new Tuple<string, string>("Colore",this.Mezzo.Colore),
                    new Tuple<string, string>(this.Mezzo is Auto?"AirBag":"Marca Sella",this.Mezzo is Auto?(this.Mezzo as Auto).NumeroAirBag.ToString():(this.Mezzo as Moto).MarcaSella),
                };
                this.dgvDettagli.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(45, 45, 45);
                for (int i = 0; i < s.Length; i++)
                    this.dgvDettagli.Rows.Add(s[i].Item1, s[i].Item2);//VENGONO AGGIUNTE LE CELLE
                Array aListofKnownColors = Enum.GetValues(typeof(KnownColor));
                foreach (DataGridViewRow row in this.dgvDettagli.Rows)
                {
                    row.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(45, 45, 45);
                    row.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
                    row.Selected = false;
                }
                this.dgvDettagli.ClearSelection();
                this.dgvDettagli.SelectionChanged += new EventHandler(this.DgvDettagli_SelectionChanged);

                Image image;
                try
                {
                    using (Stream stream = File.OpenRead(this.Mezzo.ImagePath))
                    {
                        image = System.Drawing.Image.FromStream(stream);
                    }
                }
                catch
                {
                    using (Stream stream = File.OpenRead(Path.Combine(FormMain.imageFolderPath, "noimg.png")))
                    {
                        image = System.Drawing.Image.FromStream(stream);
                    }
                }
                this.pcb.BackgroundImage = image;
                this.pcb.BackgroundImageLayout = ImageLayout.Zoom;
                this.lbl.Text = $"{(this.Mezzo.Marca.ToUpper() == "MERCEDES-BENZ" ? "MB" : this.Mezzo.Marca)} {this.Mezzo.Modello} - {this.Mezzo.Targa} {this.Mezzo.Stato}";
            }
            else
                this.AddTo.SelectedTab = this.tDettagli;
        }//APRE TAB CON SPECIFICHE

        private void BtnSell_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Vuoi vendere {this.Mezzo.Marca} {this.Mezzo.Modello}?", "Vendita", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    FormMain.modifica = true;
                    FormMain.storicoAggiunti.AddVendita(this.Mezzo, Convert.ToDouble(Interaction.InputBox("Prezzo Vendita:", "Vendita", this.Mezzo.Prezzo.ToString())), DateTime.Now, MessageBox.Show("Il veicolo è già stato consegnato?", "Vendita", MessageBoxButtons.YesNo) == DialogResult.Yes, Interaction.InputBox("Inserisci il Codice fiscale del cliente"));
                }
                catch
                {
                    FormMain.storicoAggiunti.AddVendita(this.Mezzo, this.Mezzo.Prezzo, DateTime.Now, MessageBox.Show("Il veicolo è già stato consegnato?", "Vendita", MessageBoxButtons.YesNo) == DialogResult.Yes, Interaction.InputBox("Inserisci il Codice fiscale del cliente"));
                }
                SerializableBindingList<Veicolo> lista = (FormMain.listaVeicoli.Contains(this.Mezzo) ? FormMain.listaVeicoli : FormMain.listaVeicoliAggiunti);
                lista.Remove(this.Mezzo);
                this.PnlMain.Controls.Remove(this);
                if (this.tDettagli != null)
                    this.AddTo.TabPages.Remove(this.tDettagli);
                this.tDettagli = null;
                if (FormMain.storicoControl != null)
                {
                    FormMain.storicoControl.dgv.Rows.Add(FormMain.storicoAggiunti.GetVenditaLast().MezzoVenduto.Marca + " " + FormMain.storicoAggiunti.GetVenditaLast().MezzoVenduto.Modello + " - " + (FormMain.storicoAggiunti.GetVenditaLast().MezzoVenduto.Stato),
                        FormMain.storicoAggiunti.GetVenditaLast().DataVendita.ToShortDateString(),
                        FormMain.storicoAggiunti.GetVenditaLast().PrezzoVendita.ToString("C"),
                        FormMain.storicoAggiunti.GetVenditaLast().IsConsegnato);
                    if (FormMain.storicoControl.dgv != null)
                        FormMain.storicoControl.dgv.ClearSelection();
                }

            }
        }

        private void LblMarcaModello_Click(object sender, EventArgs e)
        {
            this.Mezzo.Marca = Interaction.InputBox("Inserisci nuova Marca:", "Modifica");
            this.Mezzo.Modello = Interaction.InputBox("Inserisci nuovo Modello:", "Modifica", this.Mezzo.Modello);
            this.lblMarcaModello.Text = this.Mezzo.Marca + " " + this.Mezzo.Modello;
            this.lbl.Text = this.Mezzo.Marca + " " + this.Mezzo.Modello + " - " + this.Mezzo.Prezzo.ToString("C").Split(',')[0] + "€ " + (this.Mezzo.Stato);
        }//MODIFICA MARCA E MODELLO

        public void BtnCancel_Click(object sender, EventArgs e)
        {
            this.AddTo.TabPages.Remove(this.tDettagli);
            this.tDettagli = null;
            this.openFileDialog1 = null;
            this.btnPrint = null;
            this.btnCancel = null;
            this.dgvDettagli = null;
            this.Column1 = null;
            this.Column2 = null;
            this.lblMarcaModello = null;
            this.btnSell = null;
            this.pictureBox1 = null;
            this.lblPrezzo = null;
        }//CHIUSURA TAB

        private void PictureBox1_DoubleClick(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                FormMain.deletePaths.Add(this.Mezzo.ImagePath);
                this.Mezzo.ImagePath = this.openFileDialog1.FileName;
                if (!File.Exists(Path.Combine(FormMain.imageFolderPath, this.Mezzo.ImagePath.Split('\\')[this.Mezzo.ImagePath.Split('\\').Length - 1])))
                    File.Copy(this.Mezzo.ImagePath, Path.Combine(FormMain.imageFolderPath, this.Mezzo.ImagePath.Split('\\')[this.Mezzo.ImagePath.Split('\\').Length - 1]));
                this.Mezzo.ImagePath = Path.Combine(@".\www\images",this.Mezzo.ImagePath.Split('\\')[this.Mezzo.ImagePath.Split('\\').Length - 1]);
                this.pictureBox1.BackgroundImage = Image.FromFile(this.Mezzo.ImagePath);
                this.pictureBox1.BackgroundImageLayout = ImageLayout.Zoom;
                
            }
        }//APERTURA FILE DIALOG CAMBIO IMMAGINE

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + $"\\{this.Mezzo.Marca} {this.Mezzo.Modello} {this.Mezzo.Stato}.docx";
                WordprocessingDocument doc = Word.CreateWordFile("SALONE VENDITA VEICOLI NUOVI E USATI", Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"{this.Mezzo.Marca} {this.Mezzo.Modello} {this.Mezzo.Stato}.docx");
                var mainPart = doc.MainDocumentPart.Document;
                Body body = mainPart.GetFirstChild<Body>();
                string[] datiMezzo = this.Mezzo.GetDatiIntoAtringArray();
                string[][] contenuto = new string[][]
                {
                        new string[]{"Marca",datiMezzo[0]},
                        new string[]{"Modello",datiMezzo[1]},
                        new string[]{"Targa",datiMezzo[10]},
                        new string[]{"Cilindrata",datiMezzo[2]+" cc"},
                        new string[]{"Potenza",datiMezzo[3]+" Kw"},
                        new string[]{"Immatricolazione",datiMezzo[4]},
                        new string[]{"Stato",datiMezzo[5].ToUpper()=="TRUE"?"Usato":"Nuovo"},
                        new string[]{"KmZERO",datiMezzo[6].ToUpper()=="TRUE"?"Si":"No"},
                        new string[]{"Chilometraggio",datiMezzo[7]+" Km"},
                        new string[]{"Colore",datiMezzo[8]},
                        new string[]{"Prezzo",datiMezzo[9]}
                };
                // Append a table
                body.Append(Word.CreateTable(contenuto));
                //ERRORE IMMAGINE GIA' IN USA DA UN'ALTRO PROCESSO
                //OpenXmlUtils.InsertPicture(doc, ".\\www\\"+this.Mezzo.ImagePath.Substring(1)); 
                doc.Dispose();
                MessageBox.Show("Il documento è pronto!");
                Process.Start(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problemi col documento. Se è aperto da un altro programma, chiudilo e riprova..." + ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvDettagli_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                string v = this.dgvDettagli.CurrentCell.Value.ToString();
                string m = "";
                try
                {
                    switch (this.dgvDettagli.CurrentCell.RowIndex)
                    {
                        case 1:
                            m = Interaction.InputBox("Inserisci modifica alla cilindrata (cc):", "Modifica", this.Mezzo.Cilindrata.ToString());
                            this.Mezzo.Cilindrata = Convert.ToInt32(m.ToString().Split(' ')[0]);
                            this.dgvDettagli.CurrentRow.Cells[1].Value = this.Mezzo.Cilindrata.ToString("C").Split(',')[0] + " cc";
                            break;
                        case 2:
                            m = Interaction.InputBox("Inserisci modifica alla Potenza (Kw):", "Modifica", this.Mezzo.PotenzaKw.ToString());
                            this.Mezzo.PotenzaKw = Convert.ToDouble(m.ToString().Split(' ')[0]);
                            this.dgvDettagli.CurrentRow.Cells[1].Value = this.Mezzo.PotenzaKw.ToString("C").Split(',')[0] + " Kw";
                            break;
                        case 3:
                            m = Interaction.InputBox("Inserisci modifica alla data di immatricolazione:", "Modifica", this.Mezzo.Immatricolazione.ToShortDateString());
                            this.Mezzo.Immatricolazione = Convert.ToDateTime(m);
                            this.dgvDettagli.CurrentRow.Cells[1].Value = this.Mezzo.Immatricolazione.ToShortDateString();
                            break;
                        case 4:
                            this.Mezzo.IsUsato = !this.Mezzo.IsUsato;
                            this.dgvDettagli.CurrentRow.Cells[1].Value = (this.Mezzo.Stato);
                            this.lbl.Text = $"{(this.Mezzo.Marca.ToUpper() == "MERCEDES-BENZ" ? "MB" : this.Mezzo.Marca)} {this.Mezzo.Modello} - {this.Mezzo.Targa} {this.Mezzo.Stato}";
                            break;
                        case 5:
                            this.Mezzo.IsKmZero = !this.Mezzo.IsKmZero;
                            this.dgvDettagli.CurrentRow.Cells[1].Value = (this.Mezzo.IsKmZero ? "Vero" : "Falso");
                            break;
                        case 6:
                            m = Interaction.InputBox("Inserisci modifica al Chilometraggio (Km):", "Modifica", this.Mezzo.KmPercorsi.ToString());
                            this.Mezzo.KmPercorsi = Convert.ToInt32(m.ToString().Split(' ')[0]);
                            this.dgvDettagli.CurrentRow.Cells[1].Value = this.Mezzo.KmPercorsi.ToString("C").Split(',')[0] + " Km";
                            break;
                        case 7:           //VA IN ERRORE NON SO PER QUALE MOTIVO
                            m = Interaction.InputBox("Scegli colore", "Modifica",this.Mezzo.Colore);//crea metodo statico input che ritorna stringa
                            this.dgvDettagli.CurrentRow.Cells[1].Value = this.Mezzo.Colore = m;
                            break;
                        case 8:
                            if (this.Mezzo is Auto)
                            {
                                m = Interaction.InputBox("Inserisci modifica al numero di airbag:", "Modifica", (this.Mezzo as Auto).NumeroAirBag.ToString());
                                (this.Mezzo as Auto).NumeroAirBag = Convert.ToInt32(m);
                                this.dgvDettagli.CurrentRow.Cells[1].Value = m;
                            }
                            else
                            {
                                m = Interaction.InputBox("Inserisci modifica la marca della sella:", "Modifica", (this.Mezzo as Moto).MarcaSella.ToString());
                                (this.Mezzo as Moto).MarcaSella = m;
                                this.dgvDettagli.CurrentRow.Cells[1].Value = m;
                            }
                            break;
                    }
                    FormMain.modifica = true;
                }
                catch (Exception ex)
                {
                    this.dgvDettagli.CurrentRow.Cells[1].Value = v;//SE CI SONO ERRORI VERRA' RIMESSO IL VALORE DI DEFAULT
                    if (m != "")
                        MessageBox.Show($"Devi inserire un valore Valido! {ex.Message}", "Errore!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                this.dgvDettagli.SelectionChanged -= this.DgvDettagli_SelectionChanged;
                this.dgvDettagli.ClearSelection();
                this.dgvDettagli.SelectionChanged += new EventHandler(this.DgvDettagli_SelectionChanged);
            }
            catch { }
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            if ((e as MouseEventArgs).Button == MouseButtons.Right)
                File.Copy(Path.Combine(FormMain.dati, this.Mezzo.ImagePath.Substring(2)), Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
        }//DOWNLOAD IMMAGINE (RIGUARDARE)

        private void LblPrezzo_Click(object sender, EventArgs e)
        {
            try
            {
                this.Mezzo.Prezzo = Convert.ToInt32(Interaction.InputBox("Inserisci nuovo prezzo:", "Modifica", ""));
                (sender as Label).Text = this.Mezzo.GetPrezzo();
            }
            catch { }
        }//MODIFICA PREZZO
    }
}

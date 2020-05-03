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
using Color = System.Drawing.Color;

namespace CustomControlsProject {
    public partial class Card : UserControl {
        private CardDetails cardDetail;

        private Veicolo mezzo;
        private Veicolo Mezzo { get => this.mezzo; set => this.mezzo = value; }

        public delegate void Dc(Veicolo v, Card card, CardDetails c);
        public event Dc CardDeleted;

        public delegate void Sc(Veicolo v, CardDetails c);
        public event Sc CardShowed;

        private void EliminaCard()
        {
            CardDeleted(this.Mezzo, this, cardDetail);
            this.pcb.BackgroundImage.Dispose();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.FillRoundedRectangle(new SolidBrush(Color.FromArgb(37, 37, 38)), 0, 0, this.Width, this.Height, 10);
        }

        public Card(Veicolo mezzo)
        {
            InitializeComponent();
            this.Mezzo = mezzo;
            this.btnDelete.Click += new EventHandler(this.BtnDelete_Click);//CLICK ELIMINA
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
                image = Properties.Resources.NO_IMG;
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



        private void Visualizza_Click(object sender, EventArgs e)
        {
            cardDetail = new CardDetails(this.Mezzo);
            cardDetail.lblPrezzo.Text = this.Mezzo.GetPrezzo();//PREZZO
            try//SE L'IMMAGINE ESISTE VIENE VISUALIZZUATA ALTRIMENTI VIENE VISUALIZZATA UN'IMMAGINE PLACEHOLDER
            {
                cardDetail.pictureBox1.BackgroundImage = this.pcb.BackgroundImage;
                cardDetail.pictureBox1.BackgroundImageLayout = ImageLayout.Zoom;
            }
            catch
            {
                cardDetail.pictureBox1.BackgroundImage = CustomControlsProject.Properties.Resources.NO_IMG;
                cardDetail.pictureBox1.BackgroundImageLayout = ImageLayout.Zoom;
            }
            cardDetail.dgvDettagli.SelectionChanged += new EventHandler(this.DgvDettagli_SelectionChanged);
            cardDetail.btnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            cardDetail.lblMarcaModello.Click += new System.EventHandler(this.LblMarcaModello_Click);
            cardDetail.lblPrezzo.Click += new System.EventHandler(this.LblPrezzo_Click);

            CardShowed(this.Mezzo, cardDetail);
        }//APRE TAB CON SPECIFICHE

        private void LblMarcaModello_Click(object sender, EventArgs e)
        {
            this.Mezzo.Marca = Interaction.InputBox("Inserisci nuova Marca:", "Modifica");
            this.Mezzo.Modello = Interaction.InputBox("Inserisci nuovo Modello:", "Modifica", this.Mezzo.Modello);
            cardDetail.lblMarcaModello.Text = this.Mezzo.Marca + " " + this.Mezzo.Modello;
            this.lbl.Text = this.Mezzo.Marca + " " + this.Mezzo.Modello + " - " + this.Mezzo.Prezzo.ToString("C").Split(',')[0] + "€ " + (this.Mezzo.Stato);
        }//MODIFICA MARCA E MODELLO

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
            string v = cardDetail.dgvDettagli.CurrentCell.Value.ToString();
            string m = "";
            try
            {
                switch (cardDetail.dgvDettagli.CurrentCell.RowIndex)
                {
                    case 1:
                        m = Interaction.InputBox("Inserisci modifica alla cilindrata (cc):", "Modifica", this.Mezzo.Cilindrata.ToString());
                        this.Mezzo.Cilindrata = Convert.ToInt32(m.ToString().Split(' ')[0]);
                        cardDetail.dgvDettagli.CurrentRow.Cells[1].Value = this.Mezzo.Cilindrata.ToString("C").Split(',')[0] + " cc";
                        break;
                    case 2:
                        m = Interaction.InputBox("Inserisci modifica alla Potenza (Kw):", "Modifica", this.Mezzo.PotenzaKw.ToString());
                        this.Mezzo.PotenzaKw = Convert.ToDouble(m.ToString().Split(' ')[0]);
                        cardDetail.dgvDettagli.CurrentRow.Cells[1].Value = this.Mezzo.PotenzaKw.ToString("C").Split(',')[0] + " Kw";
                        break;
                    case 3:
                        m = Interaction.InputBox("Inserisci modifica alla data di immatricolazione:", "Modifica", this.Mezzo.Immatricolazione.ToShortDateString());
                        this.Mezzo.Immatricolazione = Convert.ToDateTime(m);
                        cardDetail.dgvDettagli.CurrentRow.Cells[1].Value = this.Mezzo.Immatricolazione.ToShortDateString();
                        break;
                    case 4:
                        this.Mezzo.IsUsato = !this.Mezzo.IsUsato;
                        cardDetail.dgvDettagli.CurrentRow.Cells[1].Value = (this.Mezzo.Stato);
                        this.lbl.Text = $"{(this.Mezzo.Marca.ToUpper() == "MERCEDES-BENZ" ? "MB" : this.Mezzo.Marca)} {this.Mezzo.Modello} - {this.Mezzo.Targa} {this.Mezzo.Stato}";
                        break;
                    case 5:
                        this.Mezzo.IsKmZero = !this.Mezzo.IsKmZero;
                        cardDetail.dgvDettagli.CurrentRow.Cells[1].Value = (this.Mezzo.IsKmZero ? "Vero" : "Falso");
                        break;
                    case 6:
                        m = Interaction.InputBox("Inserisci modifica al Chilometraggio (Km):", "Modifica", this.Mezzo.KmPercorsi.ToString());
                        this.Mezzo.KmPercorsi = Convert.ToInt32(m.ToString().Split(' ')[0]);
                        cardDetail.dgvDettagli.CurrentRow.Cells[1].Value = this.Mezzo.KmPercorsi.ToString("C").Split(',')[0] + " Km";
                        break;
                    case 7:           //VA IN ERRORE NON SO PER QUALE MOTIVO
                        m = Interaction.InputBox("Scegli colore", "Modifica", this.Mezzo.Colore);//crea metodo statico input che ritorna stringa
                        cardDetail.dgvDettagli.CurrentRow.Cells[1].Value = this.Mezzo.Colore = m;
                        break;
                    case 8:
                        if (this.Mezzo is Auto)
                        {
                            m = Interaction.InputBox("Inserisci modifica al numero di airbag:", "Modifica", (this.Mezzo as Auto).NumeroAirBag.ToString());
                            (this.Mezzo as Auto).NumeroAirBag = Convert.ToInt32(m);
                            cardDetail.dgvDettagli.CurrentRow.Cells[1].Value = m;
                        }
                        else
                        {
                            m = Interaction.InputBox("Inserisci modifica la marca della sella:", "Modifica", (this.Mezzo as Moto).MarcaSella.ToString());
                            (this.Mezzo as Moto).MarcaSella = m;
                            cardDetail.dgvDettagli.CurrentRow.Cells[1].Value = m;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                cardDetail.dgvDettagli.CurrentRow.Cells[1].Value = v;//SE CI SONO ERRORI VERRA' RIMESSO IL VALORE DI DEFAULT
                if (m != "")
                    MessageBox.Show($"Devi inserire un valore Valido! {ex.Message}", "Errore!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            cardDetail.dgvDettagli.SelectionChanged -= this.DgvDettagli_SelectionChanged;
            cardDetail.dgvDettagli.ClearSelection();
            cardDetail.dgvDettagli.SelectionChanged += new EventHandler(this.DgvDettagli_SelectionChanged);

        }

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

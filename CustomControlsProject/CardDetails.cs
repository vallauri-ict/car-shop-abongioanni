using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using VenditaVeicoliDllProject;

namespace CustomControlsProject {
    public partial class CardDetails : UserControl {

        public Veicolo Mezzo;
        public TabPage tabPage;

        public delegate void Esci(CardDetails c, TabPage t);
        public event Esci TabChiusa;

        public CardDetails(Veicolo v)
        {
            InitializeComponent();
            this.Mezzo = v;
            Tuple<string, string>[] s =//INSIEME DI DATI DA vISULIZZARE
            {
                    new Tuple<string, string>("Targa",v.Targa),
                    new Tuple<string, string>("Cilindrata",v.Cilindrata.ToString("C").Split(',')[0] + " cc"),
                    new Tuple<string, string>("Potenza",v.PotenzaKw.ToString("C").Split(',')[0] + " Kw"),
                    new Tuple<string, string>("Immatricolazione",v.Immatricolazione.ToShortDateString()),
                    new Tuple<string, string>("Stato",v.Stato),
                    new Tuple<string, string>("Km Zero",v.IsKmZero?"vero":"Falso"),
                    new Tuple<string, string>("Chilometraggio",v.KmPercorsi.ToString("C").Split(',')[0] + " Km"),
                    new Tuple<string, string>("Colore",v.Colore),
                    new Tuple<string, string>(v is Auto?"AirBag":"Marca Sella",v is Auto?(v as Auto).NumeroAirBag.ToString():(v as Moto).MarcaSella),
                };
            this.dgvDettagli.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(45, 45, 45);
            for (int i = 0; i < s.Length; i++)
                this.dgvDettagli.Rows.Add(s[i].Item1, s[i].Item2);//VENGONO AGGIUNTE LE CELLE
            foreach (DataGridViewRow row in this.dgvDettagli.Rows)
            {
                row.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(45, 45, 45);
                row.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
                row.Selected = false;
            }
            this.dgvDettagli.ClearSelection();
            this.lblMarcaModello.Text = this.Mezzo.Marca + " " + this.Mezzo.Modello;


            Image image;
            try
            {
                using (Stream stream = File.OpenRead(v.ImagePath))
                {
                    image = System.Drawing.Image.FromStream(stream);
                }
            }
            catch
            {
                image = CustomControlsProject.Properties.Resources.NO_IMG;
            }
        }

        public TabPage GetTabPage()
        {
            tabPage = new TabPage();
            tabPage.Controls.Add(this);
            this.Dock = DockStyle.Fill;
            tabPage.Text = this.lblMarcaModello.Text;
            tabPage.Name = this.Mezzo.Targa;
            return tabPage;
        }

        private void Esci_Click(object sender, EventArgs e)
        {
            TabChiusa(this, this.tabPage);
        }
    }
}

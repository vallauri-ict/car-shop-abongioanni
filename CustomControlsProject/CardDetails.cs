using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using VenditaVeicoliDllProject;

namespace CustomControlsProject {
    public partial class CardDetails : UserControl {

        public Veicolo Mezzo;
        public TabPage tabPage;

        public delegate void Close(CardDetails c, TabPage t);
        public event Close TabChiusa;

        public delegate void ImageEdited(CardDetails c);
        public event ImageEdited ImmagineCambiata;

        public CardDetails(Veicolo v)
        {
            InitializeComponent();
            this.Mezzo = v;
            Tuple<string, string>[] values =//INSIEME DI DATI DA vISULIZZARE
            {
                    new Tuple<string, string>("Targa",v.Targa),
                    new Tuple<string, string>("Cilindrata",v.Cilindrata.ToString().Split(',')[0] + " cc"),
                    new Tuple<string, string>("Potenza",v.PotenzaKw.ToString().Split(',')[0] + " Kw"),
                    new Tuple<string, string>("Immatricolazione",v.Immatricolazione.ToShortDateString()),
                    new Tuple<string, string>("Stato",v.Stato),
                    v.IsKmZero?new Tuple<string, string>("Km Zero","vero"):null,
                    new Tuple<string, string>("Chilometraggio",v.KmPercorsi.ToString().Split(',')[0] + " Km"),
                    new Tuple<string, string>("Colore",v.Colore),
                    new Tuple<string, string>(v is Auto?"AirBag":"Marca Sella",v is Auto?(v as Auto).NumeroAirBag.ToString():(v as Moto).MarcaSella),
                };
            this.dgvDettagli.DefaultCellStyle.SelectionBackColor = Color.FromArgb(45, 45, 45);
            for (int i = 0; i < values.Length; i++)
                if(values[i]!=null)
                    this.dgvDettagli.Rows.Add(values[i].Item1, values[i].Item2);//VENGONO AGGIUNTE LE CELLE
            foreach (DataGridViewRow row in this.dgvDettagli.Rows)
            {
                row.DefaultCellStyle.BackColor = Color.FromArgb(45, 45, 45);
                row.DefaultCellStyle.ForeColor = Color.White;
                row.Selected = false;
            }
            this.dgvDettagli.ClearSelection();
            this.lblPrezzo.Text = this.Mezzo.GetPrezzo();
            this.lblMarcaModello.Text = this.Mezzo.Marca + " " + this.Mezzo.Modello;


            Image image= CustomControlsProject.Properties.Resources.NO_IMG; ;
            try
            {
                using (Stream stream = File.OpenRead(v.ImagePath))
                {
                    image = System.Drawing.Image.FromStream(stream);
                }
            }
            catch
            { }
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

        private void PictureBox1_DoubleClick(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                Image image = CustomControlsProject.Properties.Resources.NO_IMG; ;
                try
                {
                    using (Stream stream = File.OpenRead(openFileDialog1.FileName))
                    {
                        image = System.Drawing.Image.FromStream(stream);
                        this.Mezzo.ImagePath = openFileDialog1.FileName;
                    }
                }
                catch
                { }
                this.pictureBox1.Image = image;
                ImmagineCambiata(this);
            }
        }
    }
}

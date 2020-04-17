using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Risorse;
using VenditaVeicoliDllProject;

namespace WindowsFormsApp {
    public partial class Aggiungi : UserControl {
        private readonly FlowLayoutPanel pnlMain;
        private readonly TabControl Tb;

        public Aggiungi(FlowLayoutPanel pnl, TabControl t)
        {
            InitializeComponent();
            this.pnlMain = pnl;
            this.Tb = t;
            this.txtSella.Visible = false;
            this.lblChange.Text = "Numero AirBag:";
            //CaricaCombo();
            CaricaCombo(Path.Combine(FormMain.fileVari, "MarcheAuto.txt"), this.cmbMarca);//CARICO LA COMBOBOX DELLE MARCHE
            this.cmbMarca.SelectedIndex = 0;
        }

        private string img = "";//PATH IMMAG
        private void BtnAggiungi_Click(object sender, EventArgs e)
        {
            //AGGIUNTA VEICOLO
            Veicolo v;
            if (!this.cmbMarca.Items.Contains(this.cmbMarca.Text))//SE LA MARCA INSERITA E' NUOVA LA AGGIUNGE ALLA LISTA DELLE MARCHE
                this.cmbMarca.Items.Add(this.cmbMarca.Text);
            if (this.NAB.Visible)
                v = new Auto(this.cmbMarca.Text.ToString(), txtModello.Text, this.txtTarga.Text, Convert.ToInt32(this.NCilindrata.Value), Convert.ToDouble(this.NPotenza.Value), this.dateTimePicker1.Value.Date, this.chkUsato.Checked, this.chkKm0.Checked, Convert.ToInt32(this.NCilometraggio.Value), txtColore.Text, Convert.ToInt32(this.NAB.Value), Convert.ToDouble(this.NPrezzo.Value));
            else
                v = new Moto(this.cmbMarca.Text.ToString(), txtModello.Text, this.txtTarga.Text, Convert.ToInt32(this.NCilindrata.Value), Convert.ToDouble(this.NPotenza.Value), this.dateTimePicker1.Value.Date, this.chkUsato.Checked, this.chkKm0.Checked, Convert.ToInt32(this.NCilometraggio.Value), txtColore.Text, this.txtSella.Text, Convert.ToDouble(this.NPrezzo.Value));
            try
            {
                string pth = this.img.Split('\\')[this.img.Split('\\').Length - 1];//COPIO L'IMMAGINE DAL PATH ORIGINALE ALLA CARTELLA /WWW/IMAGES
                if (!File.Exists(Path.Combine(FormMain.imageFolderPath, pth)))
                    File.Copy(this.img, Path.Combine(FormMain.imageFolderPath, pth));
                v.ImagePath = Path.Combine(FormMain.imageFolderPath, pth);
            }
            catch
            {
                v.ImagePath = "";
            }
            FormMain.listaVeicoliAggiunti.Add(v);
            FormMain.modifica = true;
            new Card(v, this.pnlMain, this.Tb);
            this.img = "";
            string s = "";
            for (int i = 0; i < this.cmbMarca.Items.Count; i++)
                s += this.cmbMarca.Items[i] + "\n";
            File.WriteAllText(Path.Combine(FormMain.fileVari, this.NAB.Visible ? "MarcheAuto.txt" : "MarcheMoto.txt"), s);
        }

        private void ChkUsato_CheckedChanged(object sender, EventArgs e)
        {
            this.NCilometraggio.Enabled = this.chkUsato.Checked ^ this.chkKm0.Checked;
            if (this.chkKm0.Checked && this.chkUsato.Checked)
                this.NCilometraggio.Enabled = false;
        }

        private void Sfoglia_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                this.img = this.openFileDialog1.FileName;
                (sender as Button).Text = this.img;
                this.pictureBox1.BackgroundImage = Image.FromFile(this.img);
                this.pictureBox1.BackgroundImageLayout = ImageLayout.Zoom;
            }
        }//SELEZIONE IMMAGINE

        private void BunifuImageButton1_Click(object sender, EventArgs e)
        {
            bool x = !this.NAB.Visible;
            (sender as Button).BackgroundImage = !x ? Image.FromFile(Path.Combine(FormMain.imageFolderPath, "1358761.png")) : Image.FromFile(Path.Combine(FormMain.imageFolderPath, "car-icon.png"));
            this.NAB.Visible = x;
            this.txtSella.Visible = !x;
            this.lblChange.Text = (x ? "Numero AirBag:" : "Marca Sella:");
            this.NCilindrata.Value = this.NCilindrata.Minimum = (x ? 900 : 50);
            this.NCilindrata.Increment = (x ? 100 : 25);
            this.cmbMarca.Items.Clear();
            CaricaCombo(Path.Combine(FormMain.fileVari,x ? "MarcheAuto.txt" : "MarcheMoto.txt"), this.cmbMarca);//CARICO LA COMBOBOX DELLE MARCHE
            this.cmbMarca.SelectedIndex = 0;
        }

        public void CaricaCombo(string filePath, ComboBox cmb)
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream)
                {
                    string dato = sr.ReadLine();
                    if (!cmb.Items.Contains(dato) && dato != "")
                        cmb.Items.Add(dato);
                }
            }
        }//CARICAMENTO DELLA COMBO DA FILE
    }
}

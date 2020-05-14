using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace CustomControlsProject {
    public partial class Impostazioni : UserControl {
        private OpenFileDialog openFileDialog1;
        public string logoPath;

        public delegate void Im(Impostazioni i);
        public event Im ImpostazioniModificate;

        public Impostazioni(string header, string r1, string r2, Image logo = null)
        {
            this.InitializeComponent();
            if (logo == null)
                logo = Properties.Resources.NO_IMG;
            this.Btgg.BackgroundImage = logo;
            this.textBox3.Text = r2;
            this.textBox2.Text = r1;
            this.textBox1.Text = header;
            this.textBox2.TextChanged += new System.EventHandler(this.Txt_TextChanged);
            this.textBox3.TextChanged += new System.EventHandler(this.Txt_TextChanged);
            this.textBox1.TextChanged += new System.EventHandler(this.Txt_TextChanged);
            this.textBox4.TextChanged += new System.EventHandler(this.Txt_TextChanged);

        }

        private void ChangeIcon_Click(object sender, EventArgs e)
        {
            this.openFileDialog1 = new OpenFileDialog();
            if (this.openFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                string s = ".\\www\\images\\" + this.openFileDialog1.FileName.Split('\\')[this.openFileDialog1.FileName.Split('\\').Length - 1];
                if (!File.Exists(s))
                    File.Copy(this.openFileDialog1.FileName, s);
                this.logoPath = s;
                this.Btgg.BackgroundImage.Dispose();
                this.Btgg.BackgroundImage = Image.FromFile(logoPath);
                ImpostazioniModificate(this);
            }
        }

        private void Impostazioni_Paint(object sender, PaintEventArgs e)
        {
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(this.ClientRectangle, Color.Red, Color.Yellow, 45);

            ColorBlend cblend = new ColorBlend(3)
            {
                Colors = new Color[3] { Color.FromArgb(15, 32, 39), Color.FromArgb(32, 58, 67), Color.FromArgb(44, 83, 100) },
                Positions = new float[3] { 0f, 0.5f, 1f }
            };

            linearGradientBrush.InterpolationColors = cblend;

            e.Graphics.FillRectangle(linearGradientBrush, this.ClientRectangle);
        }

        private void Txt_TextChanged(object sender, EventArgs e)
        {
            ImpostazioniModificate(this);
        }
    }
}

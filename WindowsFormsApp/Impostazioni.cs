using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using Risorse;

namespace WindowsFormsApp {
    public partial class Impostazioni : UserControl {
        private OpenFileDialog openFileDialog1;
        public bool modificato = false;
        public Impostazioni()
        {
            InitializeComponent();
            if(FormMain.logoPath!="")
                this.Btgg.BackgroundImage = Image.FromFile(FormMain.logoPath);
            textBox3.Text=File.ReadAllText(Path.Combine(FormMain.fileVari, "FooterRow2.txt"));
            textBox2.Text = File.ReadAllText(Path.Combine(FormMain.fileVari, "FooterRow1.txt"));
            textBox1.Text = File.ReadAllText(Path.Combine(FormMain.fileVari, "HeaderText.txt"));
        }

        private void ChangeIcon_Click(object sender, EventArgs e)
        {
            openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                FormMain.deletePaths.Add(FormMain.logoPath);
                string s = ".\\www\\images\\" + this.openFileDialog1.FileName.Split('\\')[this.openFileDialog1.FileName.Split('\\').Length - 1];
                if (!File.Exists(s))
                    File.Copy(this.openFileDialog1.FileName, s);
                FormMain.logoPath = s;
                this.Btgg.BackgroundImage.Dispose();
                this.Btgg.BackgroundImage = Image.FromFile(FormMain.logoPath);
                this.modificato = true;
            }
        }

        private void Impostazioni_Paint(object sender, PaintEventArgs e)
        {
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(this.ClientRectangle, Color.Red, Color.Yellow, 45);

            ColorBlend cblend = new ColorBlend(3);
            cblend.Colors = new Color[3] { Color.FromArgb(15,32,39), Color.FromArgb(32, 58, 67), Color.FromArgb(44, 83, 100) };
            cblend.Positions = new float[3] { 0f, 0.5f, 1f };

            linearGradientBrush.InterpolationColors = cblend;

            e.Graphics.FillRectangle(linearGradientBrush, this.ClientRectangle);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.modificato = true;
            this.Header.Text = (sender as TextBox).Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            this.modificato = true;
            this.textBox5.Text = (sender as TextBox).Text;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            this.modificato = true;
            this.textBox6.Text = (sender as TextBox).Text;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            this.modificato = true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using Newtonsoft.Json;
using VenditaVeicoliDllProject;
using Risorse;

namespace WindowsFormsApp {
    public partial class Clienti : UserControl {
        public Clienti()
        {
            InitializeComponent();
            for (int i = 0; i < FormMain.listaClienti.Count; i++)
            {
                this.dgv.Rows.Add(FormMain.listaClienti[i].LoginUsername,
                            FormMain.listaClienti[i].Name["last"],
                            FormMain.listaClienti[i].Name["first"],
                            FormMain.listaClienti[i].DobDate);
            }
            this.dgv.ClearSelection();
        }

        private void Button1_Click(object sender, EventArgs e)
        {

        }
    }
}

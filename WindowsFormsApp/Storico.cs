using System;
using System.Windows.Forms;
using Risorse;

namespace WindowsFormsApp {
    public partial class Storico : UserControl {
        public Storico()
        {
            InitializeComponent();
            for (int i = 0; i < FormMain.storico.Count; i++)
            {
                this.dgv.Rows.Add(FormMain.storico[i].MezzoVenduto.Marca + " " + FormMain.storico[i].MezzoVenduto.Modello + " - " + FormMain.storico[i].MezzoVenduto.Targa+" " + (FormMain.storico[i].MezzoVenduto.Stato),
                            FormMain.storico[i].DataVendita.ToShortDateString(),
                            FormMain.storico[i].PrezzoVendita.ToString("C"),
                            FormMain.storico[i].IsConsegnato, FormMain.storico[i].CodFiscaleCliente);
            }
            for (int i = 0; i < FormMain.storicoAggiunti.Count; i++)
            {
                this.dgv.Rows.Add(FormMain.storicoAggiunti[i].MezzoVenduto.Marca + " " + FormMain.storicoAggiunti[i].MezzoVenduto.Modello + " - " + FormMain.storicoAggiunti[i].MezzoVenduto.Targa + " " + (FormMain.storicoAggiunti[i].MezzoVenduto.Stato),
                            FormMain.storicoAggiunti[i].DataVendita.ToShortDateString(),
                            FormMain.storicoAggiunti[i].PrezzoVendita.ToString("C"),
                            FormMain.storicoAggiunti[i].IsConsegnato, FormMain.storicoAggiunti[i].CodFiscaleCliente);
            }
            this.dgv.ClearSelection();
            this.dgv.EditMode = DataGridViewEditMode.EditOnEnter;
        }

        private void EliminaStorico_Click(object sender, EventArgs e)
        {
            if ((e as MouseEventArgs).Button == MouseButtons.Right)
                if (MessageBox.Show("Vuoi Eliminare la cronologia delle vendite?", "Storico", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    FormMain.storico.Clear();
                    FormMain.modifica = true;
                    this.dgv.Rows.Clear();
                }
        }//SI PUO' ELIMINARE TUTTO LO STORICO

        private void Dgv_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (MessageBox.Show("Vuoi davvero eliminare il registro di vendita corrente?", "Elimina", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                this.dgv.Rows.Remove(this.dgv.Rows[(sender as DataGridViewCell).RowIndex]);//Non funziona
        }
    }
}

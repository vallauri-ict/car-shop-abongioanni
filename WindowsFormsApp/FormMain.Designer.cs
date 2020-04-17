using System;
using VenditaVeicoliDllProject;

namespace WindowsFormsApp {
    partial class FormMain {
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

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        { 
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.logoToolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.nuovoToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.apriToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.salvaToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.cercaToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.volantinoToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.impostazioniToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.storicoVenditeToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.exportWordToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.exportExcelToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.closeToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.tMain = new System.Windows.Forms.TabPage();
            this.pnlMain = new System.Windows.Forms.FlowLayoutPanel();
            this.Tb = new System.Windows.Forms.TabControl();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.tMain.SuspendLayout();
            this.Tb.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(18, 16);
            this.toolStrip1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logoToolStripLabel,
            this.nuovoToolStripButton,
            this.apriToolStripButton,
            this.salvaToolStripButton,
            this.cercaToolStripButton,
            this.volantinoToolStripButton,
            this.impostazioniToolStripButton,
            this.storicoVenditeToolStripButton,
            this.toolStripButton1,
            this.exportWordToolStripButton,
            this.exportExcelToolStripButton,
            this.closeToolStripButton});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(1034, 32);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // logoToolStripLabel
            // 
            this.logoToolStripLabel.AutoSize = false;
            this.logoToolStripLabel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.logoToolStripLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.logoToolStripLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.logoToolStripLabel.Margin = new System.Windows.Forms.Padding(10, 1, 10, 2);
            this.logoToolStripLabel.Name = "logoToolStripLabel";
            this.logoToolStripLabel.Size = new System.Drawing.Size(25, 25);
            this.logoToolStripLabel.Text = "HOME";
            this.logoToolStripLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.logoToolStripLabel.ToolTipText = "Apri sito produttore";
            this.logoToolStripLabel.Click += new System.EventHandler(this.LogoButton_Click);
            // 
            // nuovoToolStripButton
            // 
            this.nuovoToolStripButton.AutoSize = false;
            this.nuovoToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.nuovoToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("nuovoToolStripButton.Image")));
            this.nuovoToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.nuovoToolStripButton.Name = "nuovoToolStripButton";
            this.nuovoToolStripButton.Size = new System.Drawing.Size(25, 25);
            this.nuovoToolStripButton.Text = "&Nuovo";
            this.nuovoToolStripButton.ToolTipText = "Nuovo (CRTL+N)";
            this.nuovoToolStripButton.Click += new System.EventHandler(this.NuovoToolStripButton_Click);
            // 
            // apriToolStripButton
            // 
            this.apriToolStripButton.AutoSize = false;
            this.apriToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.apriToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("apriToolStripButton.Image")));
            this.apriToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.apriToolStripButton.Name = "apriToolStripButton";
            this.apriToolStripButton.Size = new System.Drawing.Size(25, 25);
            this.apriToolStripButton.Text = "&Apri";
            this.apriToolStripButton.ToolTipText = "Apri (CTRL+A)";
            this.apriToolStripButton.Click += new System.EventHandler(this.ApriToolStripButton_Click);
            // 
            // salvaToolStripButton
            // 
            this.salvaToolStripButton.AutoSize = false;
            this.salvaToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.salvaToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("salvaToolStripButton.Image")));
            this.salvaToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.salvaToolStripButton.Name = "salvaToolStripButton";
            this.salvaToolStripButton.Size = new System.Drawing.Size(25, 25);
            this.salvaToolStripButton.Text = "&Salva";
            this.salvaToolStripButton.ToolTipText = "Salva (CTRL+S)";
            this.salvaToolStripButton.Click += new System.EventHandler(this.SalvaToolStripButton_Click);
            // 
            // cercaToolStripButton
            // 
            this.cercaToolStripButton.AutoSize = false;
            this.cercaToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cercaToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("cercaToolStripButton.Image")));
            this.cercaToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cercaToolStripButton.Name = "cercaToolStripButton";
            this.cercaToolStripButton.Size = new System.Drawing.Size(25, 25);
            this.cercaToolStripButton.Text = "toolStripButton2";
            this.cercaToolStripButton.ToolTipText = "Cerca (CTRL+F)";
            this.cercaToolStripButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // volantinoToolStripButton
            // 
            this.volantinoToolStripButton.AutoSize = false;
            this.volantinoToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.volantinoToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("volantinoToolStripButton.Image")));
            this.volantinoToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.volantinoToolStripButton.Name = "volantinoToolStripButton";
            this.volantinoToolStripButton.Size = new System.Drawing.Size(25, 25);
            this.volantinoToolStripButton.Text = "toolStripButton1";
            this.volantinoToolStripButton.ToolTipText = "Listino (CRTL+L)";
            this.volantinoToolStripButton.Click += new System.EventHandler(this.ListinoButton_Click);
            // 
            // impostazioniToolStripButton
            // 
            this.impostazioniToolStripButton.AutoSize = false;
            this.impostazioniToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.impostazioniToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("impostazioniToolStripButton.Image")));
            this.impostazioniToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.impostazioniToolStripButton.Name = "impostazioniToolStripButton";
            this.impostazioniToolStripButton.Size = new System.Drawing.Size(25, 25);
            this.impostazioniToolStripButton.Text = "toolStripButton3";
            this.impostazioniToolStripButton.ToolTipText = "Impostazioni (CRTL+I)";
            this.impostazioniToolStripButton.Click += new System.EventHandler(this.Settings_Clik);
            // 
            // storicoVenditeToolStripButton
            // 
            this.storicoVenditeToolStripButton.AutoSize = false;
            this.storicoVenditeToolStripButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("storicoVenditeToolStripButton.BackgroundImage")));
            this.storicoVenditeToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.storicoVenditeToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("storicoVenditeToolStripButton.Image")));
            this.storicoVenditeToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.storicoVenditeToolStripButton.Name = "storicoVenditeToolStripButton";
            this.storicoVenditeToolStripButton.Size = new System.Drawing.Size(25, 25);
            this.storicoVenditeToolStripButton.Text = "toolStripButton5";
            this.storicoVenditeToolStripButton.ToolTipText = "Storico Vendite (CRTL+H)";
            this.storicoVenditeToolStripButton.Click += new System.EventHandler(this.Storico_Click);
            // 
            // exportWordToolStripButton
            // 
            this.exportWordToolStripButton.AutoSize = false;
            this.exportWordToolStripButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.exportWordToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.exportWordToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("exportWordToolStripButton.Image")));
            this.exportWordToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.exportWordToolStripButton.Name = "exportWordToolStripButton";
            this.exportWordToolStripButton.Size = new System.Drawing.Size(25, 25);
            this.exportWordToolStripButton.Text = "toolStripButton6";
            this.exportWordToolStripButton.ToolTipText = "Esporta su Word (CTRL+W)";
            this.exportWordToolStripButton.Click += new System.EventHandler(this.ExportToWordDocument_Click);
            // 
            // exportExcelToolStripButton
            // 
            this.exportExcelToolStripButton.AutoSize = false;
            this.exportExcelToolStripButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.exportExcelToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.exportExcelToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("exportExcelToolStripButton.Image")));
            this.exportExcelToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.exportExcelToolStripButton.Name = "exportExcelToolStripButton";
            this.exportExcelToolStripButton.Size = new System.Drawing.Size(25, 25);
            this.exportExcelToolStripButton.Text = "Esporta su Excel (CTRL+ E)";
            this.exportExcelToolStripButton.Click += new System.EventHandler(this.ExportToExcelSpreadSheet_Click);
            // 
            // closeToolStripButton
            // 
            this.closeToolStripButton.AutoSize = false;
            this.closeToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.closeToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("closeToolStripButton.Image")));
            this.closeToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.closeToolStripButton.Name = "closeToolStripButton";
            this.closeToolStripButton.Size = new System.Drawing.Size(25, 25);
            this.closeToolStripButton.ToolTipText = "Chiudi Scheda corrente (CRTL+X)";
            this.closeToolStripButton.Visible = false;
            this.closeToolStripButton.Click += new System.EventHandler(this.Chiudi_Click);
            // 
            // tMain
            // 
            this.tMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.tMain.Controls.Add(this.pnlMain);
            this.tMain.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tMain.Location = new System.Drawing.Point(4, 22);
            this.tMain.Name = "tMain";
            this.tMain.Padding = new System.Windows.Forms.Padding(3);
            this.tMain.Size = new System.Drawing.Size(1026, 545);
            this.tMain.TabIndex = 0;
            this.tMain.Text = "Tutto";
            // 
            // pnlMain
            // 
            this.pnlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMain.AutoScroll = true;
            this.pnlMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.pnlMain.Location = new System.Drawing.Point(3, 3);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1020, 539);
            this.pnlMain.TabIndex = 3;
            this.pnlMain.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.PnlMain_ControlAdded);
            // 
            // Tb
            // 
            this.Tb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tb.Controls.Add(this.tMain);
            this.Tb.HotTrack = true;
            this.Tb.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Tb.Location = new System.Drawing.Point(0, 26);
            this.Tb.Name = "Tb";
            this.Tb.SelectedIndex = 0;
            this.Tb.Size = new System.Drawing.Size(1034, 571);
            this.Tb.TabIndex = 2;
            this.Tb.SelectedIndexChanged += new System.EventHandler(this.Tb_SelectedIndexChanged);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.AutoSize = false;
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "ClientiToolStripButton";
            this.toolStripButton1.Size = new System.Drawing.Size(25, 25);
            this.toolStripButton1.ToolTipText = "Chiudi Scheda corrente (CRTL+X)";
            this.toolStripButton1.Click += new System.EventHandler(this.Clienti_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(1034, 597);
            this.Controls.Add(this.Tb);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "FormMain";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SALONE VENDITA VEICOLI NUOVI E USATI";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyDown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tMain.ResumeLayout(false);
            this.Tb.ResumeLayout(false);
            this.ResumeLayout(false);

        }


        #endregion
        private System.Windows.Forms.ToolStripButton nuovoToolStripButton;
        private System.Windows.Forms.ToolStripButton apriToolStripButton;
        private System.Windows.Forms.ToolStripButton salvaToolStripButton;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel logoToolStripLabel;
        private System.Windows.Forms.ToolStripButton volantinoToolStripButton;
        private System.Windows.Forms.ToolStripButton cercaToolStripButton;
        private System.Windows.Forms.ToolStripButton impostazioniToolStripButton;
        private System.Windows.Forms.TabPage tMain;
        private System.Windows.Forms.FlowLayoutPanel pnlMain;
        private System.Windows.Forms.TabControl Tb;
        private System.Windows.Forms.ToolStripButton closeToolStripButton;
        private System.Windows.Forms.ToolStripButton storicoVenditeToolStripButton;
        private System.Windows.Forms.ToolStripButton exportWordToolStripButton;
        private System.Windows.Forms.ToolStripButton exportExcelToolStripButton;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
    }
}


using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CustomControlsProject;
using DbDllProject;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using OpenXmlDllProject;
using Risorse;
using VenditaVeicoliDllProject;

namespace WindowsFormsApp {
    public partial class FormMain : Form {

        /// <summary>
        /// APPLICAZIONE WINFDOWS FORM CHE PERMETTE LA GESTIONE DI UN SALONE VENDITA AUTO E MOTO NUOVI O USATI.
        /// 
        /// ======================================================================================================
        /// 
        /// LE CLASSI UTILIZZATE OLTRE A FORMMAIN SONO:
        /// - SERIALIZABLE BINDING LIST => BINDING LIST SERIALIZZABILE
        /// - VEICOLO => ABSTRACT, POSSIEDE I CAMPI PER DESCRIVERE UN VEICOLO GENERICO
        ///     - AUTO => SUBCLASSE
        ///     - MOTO => SUBCLASSE
        /// - VENDITA => CONTIENE I DATI RELATIVI AD UNA VENDITA DI UN VEICOLO
        /// - STORICO => CONTIENE I DATI E I METODI CHE PERMETTO DI ELABORARE E SALVARE LE VENDITE RECENTI
        /// - UTILS => CONTIENE METODI GENERICI PER IL SALVATAGGIO (SERIALIZZAZIONE E PARSIFICAZIONE)
        /// - CARD => OGGETTO GRAFICO
        /// 
        /// ======================================================================================================
        /// 
        /// E' STRUTTURATO CON UN TOOLMENUSTRIP CON BOTTONI CHE PERMETTONO DI ELABORARE I DATI:
        /// - LOGO => BOTTONE CHE APRE IL SITO DEL GESTORE
        /// - NUOVO => APRE UNA TAB CON I CAMPI CHE PERMETTONO DI AGGIUNGERE UN NUOVO MEZZO
        ///     CAMPI RICHIESTI:
        ///         - MARCA (STRING)
        ///         - MODELLO (STRING)
        ///         - CILINDRATA (INT)
        ///         - POTENZA (DOUBLE)
        ///         - IMMATRICOLAZIONE (DATE TIME)
        ///         - isUSATO (BOOL)
        ///         - isCHILOMETRO ZERO (BOOL)
        ///         - CHILOMETRAGGIO (DOUBLE)
        ///         - COLORE (STRING)
        ///         - PREZZO (DOUBLE)
        ///         - PATH IMMAGINE (STRING)
        ///         - NUMERO AIRBAG (INT)(SOLO AUTO)
        ///         - MARCA SELLA (STRING)(SOLO MOTO)
        ///     
        /// - APRI => CARICA I DATI NEL PROGRAMMA (DA FILE .JSON, SE CI SONO ERRORI, DA CSV)
        /// - SALVA => SALVA I DATI RIGUARDO I VEICOLI IN 3 FORMATI (JSON, CSV, XML) E LO STORICO IN JSO
        /// - CERCA => RICERCA NEI VEICOLI; APRE UNA TAB CON I RISULTATI
        /// - VOLANTINO => APRE UNA TAB CHE VISUALIZZA UNA PAGINA WEB; I DATI DEI VEICOLI VENGONO ESPORTATI IN HTML E VISUALIZZATI
        /// - IMPOSTAZIONI => APRE UNA TAB CHE PERMETTE DI CAMBIARE IL TESTO NELL' HEADER E NEL FOOTER DEL SITO-VOLANTINO
        /// - CRONOLOGIA => APRE UNA TAB CHE PERMETTE DI VISUALIZZARE I DATI DELLE VENDITE RECENTI
        /// - CHIUDI => BOTTONE A SCOMPARSA CHE CHIUDE LA TAB SELEZIONATA
        /// - EXPORT TO WORD => CREA UN DOCUMENTO WORD CON I DATI DEI VEICOLI
        /// - EXPORT TO EXCEL => CREA UN FOGLIO DI CALCOLO CON I DATO DEI VEICOLI
        /// 
        /// 
        /// 
        /// </summary>

        #region Dichiarazione

        private TabPage tAggiungi = null;//TAB AGGIUNTA
        private TabPage tRicerca = null;//TAB RICERCA
        private TabPage tModify = null;//TAB IMPOSTAZIONI
        private TabPage tStorico = null;//TAB STORICO

        private SerializableBindingList<Veicolo> results;
        private string[] search = new string[0];
        private FlowLayoutPanel tlpResults;

        public SerializableBindingList<Veicolo> listaVeicoliEliminati;//LISTA DATI
        public SerializableBindingList<Veicolo> listaVeicoliAggiunti;//LISTA DATI
        public SerializableBindingList<Veicolo> listaVeicoli;//LISTA DATI

        public readonly string resourcesDirectoryPath;
        private readonly string accessDbPath;
        public bool modifica = false;//MODIFICHE EFFETTUATE
        public List<string> deletePaths = new List<string>();
        #endregion

        #region Form
        public FormMain()
        {
            resourcesDirectoryPath = $"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName}\\Risorse\\Resources\\";
            accessDbPath = Path.Combine(resourcesDirectoryPath, Properties.Resources.ACCESS_DB_NAME);
            listaVeicoliEliminati = new SerializableBindingList<Veicolo>();
            listaVeicoliAggiunti = new SerializableBindingList<Veicolo>();
            listaVeicoli = new SerializableBindingList<Veicolo>();
            this.InitializeComponent();
        }

        public void FormMain_Load(object sender, EventArgs e)
        {
            try
            {
                new VeicoliCommands().CreateTable(("Provider=Microsoft.Ace.Oledb.12.0;Data Source={DbPath};").Replace("{DbPath}", accessDbPath));
            }
            catch (OleDbException) { }

            //RESET SCHERMATA
            this.pnlMain.Controls.Clear();
            this.tMain.Select();
            string logoPath = File.ReadAllText(Path.Combine(Properties.Resources.FILES_PATH, "LogoPath.txt"));
            if (logoPath != "")
                this.logoToolStripLabel.BackgroundImage = Image.FromFile(logoPath);
            this.logoToolStripLabel.BackgroundImageLayout = ImageLayout.Zoom;
            listaVeicoliEliminati.Clear();
            listaVeicoliAggiunti.Clear();
            listaVeicoli.Clear();

            OpenFromDB(listaVeicoli, accessDbPath);

            foreach (var item in listaVeicoli)
            {//CREO LE CARTE GRAFICHE
                var c = new Card(item);
                this.pnlMain.Controls.Add(c);
                c.CardDeleted += Handler_CardDeleted;
                c.CardShowed += Handler_CardShowed;
            }
        }//FORM LOAD

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (modifica)//SE CHIUDO LA FORM CHIEDO SE VOGLIO SALVARE I DATI
                if (MessageBox.Show("Vuoi salvare le modifiche?", "Salva", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    Salva();
        }//CHIUSURA E SALVATAGGIO

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.F:
                        this.SearchButton_Click(this.cercaToolStripButton, e);
                        break;
                    case Keys.N:
                        this.NuovoToolStripButton_Click(this.nuovoToolStripButton, e);
                        break;
                    case Keys.X:
                        this.Chiudi_Click(this.closeToolStripButton, e);
                        break;
                    case Keys.S:
                        this.SalvaToolStripButton_Click(this.salvaToolStripButton, e);
                        break;
                    case Keys.A:
                        this.ApriToolStripButton_Click(this.apriToolStripButton, e);
                        break;
                    case Keys.L:
                        this.ListinoButton_Click(this.volantinoToolStripButton, e);
                        break;
                    case Keys.I:
                        this.Settings_Clik(this.impostazioniToolStripButton, e);
                        break;
                    case Keys.W:
                        this.ExportToWordDocument_Click(this.exportWordToolStripButton, e);
                        break;
                    case Keys.E:
                        this.ExportToExcelSpreadSheet_Click(this.exportExcelToolStripButton, e);
                        break;
                }
            }//SHORTCUT
            else
            {
                if (e.KeyCode == Keys.Left)
                {
                    this.Tb.SelectedIndex = this.Tb.SelectedIndex <= 0 ? this.Tb.TabCount : this.Tb.SelectedIndex;
                }
                else if (e.KeyCode == Keys.Right)
                {
                    this.Tb.SelectedIndex = this.Tb.SelectedIndex == this.Tb.TabCount ? 0 : this.Tb.SelectedIndex;
                }
            }
        }//SHORTCUT

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
        #endregion

        #region toolStripButtons Click

        private void NuovoToolStripButton_Click(object sender, EventArgs e)
        {
            if (this.tAggiungi == null)//CONTROLLO SE NON HO GIA' APERTO LA TAB PER L'AGGIUNTA
            {
                this.tAggiungi = new TabPage();//CREO UNA NUOVA PAGINA

                //CREO LA GRAFICA DINAMICAMENTE (CODICE PRESO DA UNA FORM RIMOSSA)
                var a = new Aggiungi()
                {
                    Dock = DockStyle.Fill
                };
                a.VeicoloAggiunto += Handler_VeicoloAggiunto;
                this.tAggiungi.Controls.Add(a);
                this.tAggiungi.Text = "Aggiungi";
                this.Tb.TabPages.Add(this.tAggiungi);
                this.Tb.SelectTab(this.tAggiungi);
            }
            else
                if (this.Tb.SelectedTab != this.tAggiungi)
                this.Tb.SelectTab(this.tAggiungi);
        }//AGGIUNTA VEICOLO

        private void Handler_VeicoloAggiunto(Veicolo v, Aggiungi a)
        {
            try
            {
                string pth = a.img.Split('\\')[a.img.Split('\\').Length - 1];//COPIO L'IMMAGINE DAL PATH ORIGINALE ALLA CARTELLA /WWW/IMAGES
                if (!File.Exists(Path.Combine(Properties.Resources.IMAGE_FOLDER_PATH, pth)))
                    File.Copy(a.img, Path.Combine(Properties.Resources.IMAGE_FOLDER_PATH, pth));
                v.ImagePath = Path.Combine(Properties.Resources.IMAGE_FOLDER_PATH, pth);
            }
            catch
            {
                v.ImagePath = "";
            }
            a.img = "";

            listaVeicoliAggiunti.Add(v);
            modifica = true;
            var c = new Card(v);
            this.pnlMain.Controls.Add(c);
        }

        private void SalvaToolStripButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Vuoi salvare le modifiche?", "Salva", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Salva();
            }
        }//SALVATAGGIO

        private void ApriToolStripButton_Click(object sender, EventArgs e)
        {
            if (modifica)//PRIMA DI APRIRE CHIEDO SE VOGLIO SALVARE I DATI
            {
                if (MessageBox.Show("Vuoi salvare e aprire i dati?", "Salva", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                Salva();
            }
            try
            {
                this.FormMain_Load(this, new EventArgs());//RELOAD DELLE CARTE GRAFICHE
                MessageBox.Show("Dati Caricati!", "Salva", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                //ERRORE!
                MessageBox.Show("Errore durante il caricamento dei dati!\n", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }//APERTURA DATI

        private void LogoButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://jombo.altervista.org/");
        }//NAVIGAZIONE

        private void ListinoButton_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(Properties.Resources.DATAS_PATH, "index.html");//PATH INDEX

            string header = File.ReadAllText(Path.Combine(Properties.Resources.FILES_PATH, "HeaderText.txt"));
            string row1 = File.ReadAllText(Path.Combine(Properties.Resources.FILES_PATH, "FooterRow1.txt"));
            string row2 = File.ReadAllText(Path.Combine(Properties.Resources.FILES_PATH, "FooterRow2.txt"));
            string logoPath = File.ReadAllText(Path.Combine(Properties.Resources.FILES_PATH, "LogoPath.txt"));
            CreateHtml(listaVeicoli, path, Path.Combine(Properties.Resources.DATAS_PATH, "index-skeleton.html"), header, row1, row2, logoPath);//CREO HTML DAI DATI E LO APRO
            System.Diagnostics.Process.Start(path);
        }//CREAZIONE LISTINO

        private void SearchButton_Click(object sender, EventArgs e)
        {
            string s = Interaction.InputBox("Inserisci ricerca: ", "Cerca");
            search = s.Split(' ');
            results = Veicolo.Search(s, listaVeicoli.ToList());
            if (results.Count == 0)//NESSUN RISULTATO
            {
                MessageBox.Show($"Nessun risultato trovato per {s}");
            }
            else
            {
                if (this.tRicerca == null)//SE LA TAB E' GIA' APERTA LA RESETTA
                {
                    //CREO LA GRAFICA DINAMICAMENTE (CODICE PRESO DA UNA FORM RIMOSSA)
                    #region Inizializzazione
                    this.tRicerca = new TabPage();//CREO LA PAGINA
                    this.tlpResults = new System.Windows.Forms.FlowLayoutPanel();
                    this.tRicerca.Controls.Add(this.tlpResults);
                    this.Tb.TabPages.Add(this.tRicerca);
                    this.tRicerca.Text = "Ricerca";
                    this.Tb.SelectTab(this.tRicerca);
                    // 
                    // tlpResults
                    // 
                    this.tlpResults.AutoScroll = true;
                    this.tlpResults.Dock = System.Windows.Forms.DockStyle.Fill;
                    this.tlpResults.BackColor = System.Drawing.Color.FromArgb(45, 45, 45);
                    this.tlpResults.Location = new System.Drawing.Point(0, 0);
                    this.tlpResults.Name = "tlpResults";
                    this.tlpResults.Size = new System.Drawing.Size(585, 324);
                    this.tlpResults.TabIndex = 0;
                    this.tlpResults.Padding = new Padding(0);
                    #endregion//
                }
                else
                    this.tlpResults.Controls.Clear();
                this.Tb.SelectTab(this.tRicerca);
                foreach (Veicolo Mezzo in results)
                {//CREO LE CARTE GRAFICHE DEI RISULTATI
                    var c = new Card(Mezzo);
                    c.CardDeleted += Handler_CardDeleted;
                    c.CardShowed += Handler_CardShowed;
                }
            }
        }//RICERCA DATI

        private void Handler_CardShowed(Veicolo v, CardDetails c)
        {
            if (c != null)
            {
                var t = c.GetTabPage();
                this.Tb.TabPages.Add(t);
                this.Tb.SelectTab(t);
                t.Text = c.lblMarcaModello.Text;
                c.TabChiusa += ChiudiTab_Click;
            }
        }

        private void ChiudiTab_Click(CardDetails c, TabPage t)
        {
            this.Tb.TabPages.Remove(t);
        }

        private void Handler_CardDeleted(Veicolo v, Card card, CardDetails c = null)
        {
            modifica = true;
            SerializableBindingList<Veicolo> lista = (listaVeicoli.Contains(v) ? listaVeicoli : listaVeicoliAggiunti);
            lista.Remove(v);
            this.pnlMain.Controls.Remove(this);

            if (c != null)
                this.Tb.TabPages.Remove(c.tabPage);
            this.pnlMain.Controls.Remove(card);

            if (!v.ImagePath.Contains("noimg.png") && v.ImagePath != "")
            {
                deletePaths.Add(v.ImagePath);
            }
            listaVeicoliEliminati.Add(v);
        }

        private void Settings_Clik(object sender, EventArgs e)
        {
            if (this.tModify != null)
            {
                this.Tb.SelectTab(this.tModify);
                return;
            }
            this.tModify = new TabPage();
            var a = new Impostazioni(
                File.ReadAllText(Path.Combine(Properties.Resources.FILES_PATH, "HeaderText.txt")),
              File.ReadAllText(Path.Combine(Properties.Resources.FILES_PATH, "FooterRow2.txt")),
              File.ReadAllText(Path.Combine(Properties.Resources.FILES_PATH, "FooterRow1.txt")),
              this.logoToolStripLabel.BackgroundImage
            )
            {
                Name = "a",
                Dock = DockStyle.Fill
            };
            a.ImpostazioniModificate += Handler_ImpostazioniModificate;
            this.tModify.Controls.Add(a);
            this.Tb.TabPages.Add(this.tModify);
            this.Tb.SelectTab(this.tModify);
            this.tModify.Text = "Modifica";
        }//CAMBIAMENTO IMPOSTAZIONI PER CAMBIARE IL TESTO NELL'HEADER E NEL FOOTER

        private void Handler_ImpostazioniModificate(Impostazioni i)
        {
            File.WriteAllText(Path.Combine(Properties.Resources.FILES_PATH, "LogoPath.txt"), i.logoPath);
            this.logoToolStripLabel.BackgroundImage = Image.FromFile(i.logoPath);
            File.WriteAllText(Path.Combine(Properties.Resources.FILES_PATH, "FooterRow2.txt"), i.textBox3.Text);
            File.WriteAllText(Path.Combine(Properties.Resources.FILES_PATH, "FooterRow1.txt"), i.textBox2.Text);
            File.WriteAllText(Path.Combine(Properties.Resources.FILES_PATH, "HeaderText.txt"), i.textBox1.Text);
            File.WriteAllText(Path.Combine(Properties.Resources.FILES_PATH, "Email.txt"), i.textBox4.Text);
        }

        private void Chiudi_Click(object sender, EventArgs e)
        {
            if (this.Tb.SelectedTab == this.tAggiungi)
            {
                this.Tb.TabPages.Remove(this.tAggiungi);
                this.tAggiungi = null;
            }
            else if (this.Tb.SelectedTab == this.tRicerca)
            {
                this.Tb.TabPages.Remove(this.tRicerca);
                this.tRicerca = null;
                this.tlpResults = null;
            }
            else if (this.Tb.SelectedTab == this.tModify)
            {
                if ((this.tModify.Controls["a"] as Impostazioni).modificato)
                    if (MessageBox.Show("Vuoi salvare le modifiche?", "Salva", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Handler_ImpostazioniModificate(this.tModify.Controls["a"] as Impostazioni);
                    }
                this.Tb.TabPages.Remove(this.tModify);
                this.tModify = null;
            }
            else if (this.Tb.SelectedTab == this.tStorico)
            {
                this.Tb.TabPages.Remove(this.tStorico);
                this.tStorico = null;
            }
        }//CHIUDO LA TAB CORRENTE A MENO CHE NON SIA UNA CARTA DEI DETTAGLI

        private void Tb_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.closeToolStripButton.Visible = this.Tb.SelectedTab == this.tRicerca || this.Tb.SelectedTab == this.tAggiungi || this.Tb.SelectedTab == this.tModify || this.Tb.SelectedTab == this.tStorico;
        }//NASCONDO E MOSTRO IL BOTTONE PER CHIUDERE LA TAB

        private void ExportToWordDocument_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Volantino " + (this.Tb.SelectedTab == tRicerca && this.results != null ? Utilities.StringArrayToString(search) : "") + ".docx";
                WordprocessingDocument doc = Word.CreateWordFile("SALONE VENDITA VEICOLI NUOVI E USATI", Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Volantino " + (this.Tb.SelectedTab == tRicerca && this.results != null ? Utilities.StringArrayToString(search) : "") + ".docx");
                var mainPart = doc.MainDocumentPart.Document;
                Body body = mainPart.GetFirstChild<Body>();
                if (this.Tb.SelectedTab == tRicerca && this.results != null)
                {
                    body.AppendChild(Word.CreateParagraph($"Ricerca basata su: \"{Utilities.StringArrayToString(search)}\""));
                }// SE SI PREME IL BOTTONE DI ESPORTAZIONE SULLA PAGINA DEI RISULTATI DI UNA RICERCA, VERRANNO ESPORATI QUEI RISULTATI
                string[][] contenuto;
                if (this.Tb.SelectedTab == tRicerca && this.results != null)
                {
                    contenuto = new string[this.results.Count][];
                    for (int i = 0; i < this.results.Count; i++)
                    {
                        contenuto[i] = new string[4];
                        contenuto[i][0] = this.results[i].Marca + " " + this.results[i].Modello;
                        contenuto[i][1] = this.results[i].Prezzo.ToString("C");
                        contenuto[i][2] = this.results[i].Stato;
                        contenuto[i][3] = "";
                    }
                }
                else
                {
                    contenuto = new string[listaVeicoli.Count][];
                    for (int i = 0; i < listaVeicoli.Count; i++)
                    {
                        contenuto[i] = new string[4];
                        contenuto[i][0] = listaVeicoli[i].Marca + " " + listaVeicoli[i].Modello;
                        contenuto[i][1] = listaVeicoli[i].Prezzo.ToString("C");
                        contenuto[i][2] = listaVeicoli[i].Stato;
                        contenuto[i][3] = "";
                    }
                }
                // Append a table
                body.Append(Word.CreateTable(contenuto));
                doc.Dispose();
                MessageBox.Show("Il documento è pronto!");
                Process.Start(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problemi col documento. Se è aperto da un altro programma, chiudilo e riprova..." + ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }//ESPORTA I DATI DEI VEICOLI CREANDO UN DOCUMENTO WORD

        private void ExportToExcelSpreadSheet_Click(object sender, EventArgs e)
        {
            string path = (Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Volantino " + (this.Tb.SelectedTab == tRicerca && this.results != null ? Utilities.StringArrayToString(search) : "") + ".xlsx");
            Excel.CreateExcelFile<Veicolo>((this.Tb.SelectedTab == tRicerca && this.results != null ? results.ToList() : listaVeicoli.ToList()), path);
            MessageBox.Show("Il documento è pronto!");
            Process.Start(path);
        }//ESPORTA I DATI DEI VEICOLI CREANDO UN FOGLIO DI CALCOLO EXCEL

        #endregion

        public void Salva()
        {
            VeicoliCommands cmdVeicoli = new VeicoliCommands();
            try
            {
                string connString = ("Provider=Microsoft.Ace.Oledb.12.0;Data Source={DbPath};").Replace("{DbPath}", accessDbPath);
                foreach (Veicolo v in listaVeicoliEliminati)
                {
                    cmdVeicoli.Delete(v, connString);
                }
                foreach (Veicolo v in listaVeicoliAggiunti)
                {
                    cmdVeicoli.Insert(v, connString);
                    listaVeicoli.Add(v);
                }
                foreach (Veicolo v in listaVeicoli)
                {
                    cmdVeicoli.Update(v, connString);
                }

                MessageBox.Show("Modifiche salvate!", "Salva", MessageBoxButtons.OK, MessageBoxIcon.Information);
                modifica = false;
                if (deletePaths.Count > 0)
                {
                    foreach (string path in deletePaths)
                    {
                        if (path.Length > 0)
                        {
                            if (!path.Contains("noimg.png"))
                            {
                                File.Delete(path);
                            }
                        }
                    }
                }
                deletePaths.Clear();
                listaVeicoliAggiunti.Clear();
                listaVeicoliEliminati.Clear();
            }
            catch (Exception e)
            {
                MessageBox.Show("Errore durante il Salvataggio dei dati!\n" + e.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }//METODO CHE SALVA I DATI DI UNA SerializableBindingList<T> IN TRE FORMATI: XML,JSON,CSV

        private void PnlMain_ControlAdded(object sender, ControlEventArgs e)
        {
            if (tAggiungi != null)
            {
                this.Tb.TabPages.Remove(tAggiungi);
                this.tAggiungi = null;
            }
        }

        public static void OpenFromDB(SerializableBindingList<Veicolo> lst, string pathName)
        {
            try
            {
                DataTable t = AccessUtils.GetRows(pathName, "SELECT * FROM Veicoli");
                foreach (DataRow r in t.Rows)
                {
                    Veicolo v;
                    if (Convert.ToInt32(r["AutoMoto"]) == 1)
                        v = new Auto(Veicolo.SetArray(r));
                    else
                        v = new Moto(Veicolo.SetArray(r));
                    lst.Add(v);
                }
            }
            catch { }
        }

        public static void CreateHtml(SerializableBindingList<Veicolo> listaVeicoli, string path = @".\www\index.html", string skeletonPath = @".\www\index-skeleton.html", string header = "showroom", string r1 = "riga 1", string r2 = "riga 2", string logoPath = "", string mail = "")
        {
            string html = File.ReadAllText(skeletonPath);//LEGGO SKELETON HTML
            string newVeicoli = "";//STRINGA NUOVI VEICOLI
            foreach (Veicolo item in listaVeicoli)
            {
                string json = JsonConvert.SerializeObject(item);
                string card = $"<div class=\"card \" onclick='dettagli({json},this)'><div class=\"card-image\"><div class=\"img\" style=\"background-image:url('images/{item.ImagePath.Split('\\')[item.ImagePath.Split('\\').Length - 1]}')\"></div></div><div class=\"card-content\"><p class=\"card-content-title\">{item.Marca} {item.Modello}</p><p class=\"card-content-body\"></p></div>	</div>";
                newVeicoli += card;
            }
            html = html.Replace("{{mainContent}}", newVeicoli);//REPLACE DEL CONTENUTO DELLO SKELETON
            html = html.Replace("{{Header}}", header);//REPLACE DEL CONTENUTO dell'header
            html = html.Replace("{{Row1}}", r1);//REPLACE DEL CONTENUTO della 1^ riga
            html = html.Replace("{{Row2}}", r2);//REPLACE DEL CONTENUTO della 2^riga
            html = html.Replace("{{adminMail}}", mail);//REPLACE DEL CONTENUTO della 2^riga
            if (logoPath != "")
                html = html.Replace("{{Logo}}", "images\\" + logoPath.Split('\\')[3]);
            File.WriteAllText(path, html);//SCRITTURA NELL' INDEX E APERTURA
        }//CREAZIONE HMTL (VOLANTINO)

    }
}
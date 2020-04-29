using System;
using System.Data;
using System.IO;
using DbDllProject;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using OpenXmlDllProject;
using Risorse;
using VenditaVeicoliDllProject;

namespace ConsoleAppProject {
    internal class Program {
        private static void Main()
        {
            string resourcesDirectoryPath = $"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName}\\Risorse\\Resources";
            string accessDbPath = Path.Combine(resourcesDirectoryPath, Properties.Resources.ACCESS_DB_NAME);
            string connStr = $"Provider=Microsoft.Ace.Oledb.12.0;Data Source={accessDbPath};";

            SerializableBindingList<Veicolo> listaVeicoli = new SerializableBindingList<Veicolo>();
            try
            {
                new VeicoliCommands().CreateTable(connStr);
            }
            catch (System.Data.OleDb.OleDbException)
            {

            }

            DataTable t = AccessUtils.GetRows(accessDbPath, "SELECT * FROM Veicoli;");
            //VeicoliDataSet.VeicoliDataTable t = new VeicoliDataSet.VeicoliDataTable();

            foreach (DataRow r in t.Rows)
            {
                if (Convert.ToInt32(r["AutoMoto"]) == 1)
                    listaVeicoli.Add(new Auto(Veicolo.SetArray(r)));
                else
                    listaVeicoli.Add(new Moto(Veicolo.SetArray(r)));
            }
            string scelta;
            do
            {
                bool trovato = false;
                Console.WriteLine("\t\t\t=== SALONE VENDITA VEICOLI NUOVI E USATI ===\n");
                foreach (Veicolo v in listaVeicoli)
                {
                    Console.WriteLine($"{v.Targa} - {v.Marca} {v.Modello} - {v.Stato} {v.GetPrezzo()} - {v.Colore}");
                }
                Console.Write("\n# ");
                scelta = Console.ReadLine();

                switch (scelta.ToUpper().Split(' ')[0])
                {
                    case "HELP":
                    {
                        Console.WriteLine("\n" + File.ReadAllText(@".\Commands.txt") + "\n");
                        Console.ReadKey();
                        break;
                    }
                    case "A":
                    {
                        Console.WriteLine("\t\t\t\t=== NUOVO VEICOLO ===\n");
                        Console.Write("Moto o Auto? (X per uscire)[M/A]: ");
                        string a = Console.ReadLine().ToUpper();
                        Veicolo v;
                        if (a != "X" && (a == "A" || a == "M"))
                        {
                            if (a == "A") v = new Auto(); else v = new Moto();
                        }
                        else break;
                        try
                        {
                            v.Targa = AskToSet("Inserisci Targa (x per uscire): ");
                            v.Marca = AskToSet("Inserisci Marca (x per uscire): ");
                            v.Modello = AskToSet("Inserisci Modello (x per uscire): ");
                            v.Cilindrata = Convert.ToInt32(AskToSet("Inserisci Cilindrata [cc] (x per uscire): "));
                            v.PotenzaKw = Convert.ToInt32(AskToSet("Inserisci Potenza [Kw] (x per uscire): "));
                            v.Immatricolazione = Convert.ToDateTime(AskToSet("Inserisci Data Immatricolazione [gg/mm/aaaa] (x per uscire): "));
                            v.IsUsato = AskToSet("Il veicolo è usato? [S/N] (x per uscire): ").ToUpper() == "S";
                            v.IsKmZero = AskToSet("Il veicolo è Km Zero? [S/N] (x per uscire): ").ToUpper() == "S";
                            v.KmPercorsi = v.IsUsato ? Convert.ToInt32(AskToSet("Inserisci chilometraggio [Km] (x per uscire): ")) : 0;
                            v.ImagePath = AskToSet("Inserisci path immagine [opzionale] (x per uscire): ");
                            v.Colore = AskToSet("Inserisci colore (x per uscire): ");
                            v.Prezzo = Convert.ToDouble(AskToSet("Inserisci prezzo [€] (x per uscire): "));

                            if (v is Auto)
                                (v as Auto).NumeroAirBag = Convert.ToInt32(AskToSet("Inserisci numero di airbag (x per uscire): "));
                            else
                                (v as Moto).MarcaSella = AskToSet("Inserisci la marca della sella (x per uscire): ");
                            listaVeicoli.Add(v);
                            new VeicoliCommands().Insert(v, connStr);
                            Console.WriteLine("Veicolo Aggiunto!");
                            Console.ReadKey();
                        }

                        catch { break; }



                        break;
                    }
                    case "V":
                    {
                        string targa;
                        try
                        {
                            targa = scelta.Split(' ')[1].Trim().ToUpper();
                        }
                        catch { Console.WriteLine("Sintassi comando errata!"); Console.ReadKey(); break; }
                        foreach (Veicolo v in listaVeicoli)
                        {
                            if (v.Targa.ToUpper() == targa)
                            {
                                trovato = true;
                                Console.WriteLine($"\n{v.Marca} {v.Modello}\n" + Utilities.SequenzaChar(30) +
                                    $"\nCilindrata: {v.Cilindrata} cc\n" + Utilities.SequenzaChar(30) +
                                    $"\nPotenza: {v.PotenzaKw} Kw\n" + Utilities.SequenzaChar(30) +
                                    $"\nImmatricolazione: {v.Immatricolazione.ToShortDateString()}\n" + Utilities.SequenzaChar(30) +
                                    $"\nStato: {v.Stato}\n" + Utilities.SequenzaChar(30) +
                                    $"\nKm Zero: {v.IsKmZero}\n" + Utilities.SequenzaChar(30) +
                                    $"\nChilometraggio: {v.KmPercorsi } Km\n" + Utilities.SequenzaChar(30) +
                                    $"\nColore: {v.Colore}\n" + Utilities.SequenzaChar(30) +
                                    $"\nPrezzo: {v.GetPrezzo()}\n" + Utilities.SequenzaChar(30) +
                                    ((v is Auto ? $"\nNumero airbag: {(v as Auto).NumeroAirBag}\n" : $"\nMarca sella: {(v as Moto).MarcaSella}\n") + Utilities.SequenzaChar(30)));
                                break;
                            }
                            if (!trovato) Console.WriteLine("Veicolo non trovato"); Console.ReadKey();
                        }
                        break;
                    }
                    case "E":
                    {
                        string targa, proprieta;
                        try
                        {
                            targa = scelta.Split(' ')[1].Trim().ToUpper();
                            proprieta = scelta.Split(' ')[2].Trim().ToUpper();
                        }
                        catch { Console.WriteLine("Sintassi comando errata!"); Console.ReadKey(); break; }
                        if (proprieta == "TARGA")
                        {
                            Console.WriteLine("Non puoi modificare la targa!");
                            Console.ReadKey(); break;
                        }
                        foreach (Veicolo v in listaVeicoli)
                        {
                            if (v.Targa.ToUpper() == targa)
                            {
                                trovato = true;
                                try
                                {
                                    Console.Write("Inserisci nuovo " + proprieta + ": ");
                                    v[proprieta.Substring(0, 1) + proprieta.Substring(1).ToLower()] = Console.ReadLine();
                                    new VeicoliCommands().Update(v, connStr);
                                }
                                catch { Console.WriteLine("Formato valore immesso errato!"); }
                            }
                        }
                        if (!trovato) Console.WriteLine("Veicolo non trovato"); Console.ReadKey();
                        break;
                    }
                    case "F":
                    {
                        Console.Write("Inserisci una ricerca: ");
                        SerializableBindingList<Veicolo> results = Veicolo.Search(Console.ReadLine(), listaVeicoli.ToList());
                        if (results.Count > 0)
                        {
                            Console.WriteLine($"\t\t\t=== RISULTATI RICERCA ===\n");
                            foreach (Veicolo v in results)
                            {
                                Console.WriteLine($"{v.Targa} - {v.Marca} {v.Modello} - {v.Stato} {v.GetPrezzo()} - {v.Colore}");
                            }
                        }
                        else Console.WriteLine("Nessun risultato");
                        Console.ReadKey();
                        break;
                    }
                    case "D":
                    {
                        string targa;
                        try
                        {
                            targa = scelta.Split(' ')[1].Trim().ToUpper();
                        }
                        catch { Console.WriteLine("Sintassi comando errata!"); Console.ReadKey(); break; }
                        foreach (Veicolo v in listaVeicoli)
                        {
                            if (v.Targa.ToUpper() == targa)
                            {
                                trovato = true;
                                Console.Write($"SEI SICURO DI VOLER ELIMINARE {v.Marca} {v.Modello} - {v.Targa} ? [S/N]: ");
                                if (Console.ReadLine().ToUpper() == "S")
                                {
                                    listaVeicoli.Remove(v);
                                    new VeicoliCommands().Delete(v, connStr);
                                    Console.WriteLine("Veicolo eliminato");
                                }
                                break;
                            }
                        }
                        if (!trovato) Console.WriteLine("Veicolo non trovato"); Console.ReadKey();
                        break;
                    }
                    case "WORD":
                    {
                        try
                        {
                            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Volantino.docx";
                            WordprocessingDocument doc = Word.CreateWordFile("SALONE VENDITA VEICOLI NUOVI E USATI", Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Volantino.docx");
                            var mainPart = doc.MainDocumentPart.Document;
                            Body body = mainPart.GetFirstChild<Body>();
                            string[][] contenuto = new string[listaVeicoli.Count][];
                            for (int i = 0; i < listaVeicoli.Count; i++)
                            {
                                contenuto[i] = new string[4];
                                contenuto[i][0] = listaVeicoli[i].Marca + " " + listaVeicoli[i].Modello;
                                contenuto[i][1] = listaVeicoli[i].Prezzo.ToString("C");
                                contenuto[i][2] = listaVeicoli[i].Stato;
                                contenuto[i][3] = "";
                            }

                            // Append a table
                            body.Append(Word.CreateTable(contenuto));
                            doc.Dispose();
                            Console.WriteLine("Il documento è pronto!");
                            System.Diagnostics.Process.Start(path);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Problemi col documento. Se è aperto da un altro programma, chiudilo e riprova..." + ex.Message);
                        }
                        break;
                    }
                    case "EXCEL":
                    {
                        string path = (Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Volantino.xlsx");
                        Excel.CreateExcelFile<Veicolo>(listaVeicoli.ToList(), path);
                        Console.WriteLine("Il documento è pronto!");
                        System.Diagnostics.Process.Start(path);
                        break;
                    }
                    case "X": break;
                    default:
                        Console.WriteLine("Sintassi comando errata!");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            } while (scelta.ToUpper() != "X");
        }
        private static string AskToSet(string caption)
        {
            Console.Write(caption);
            string a = Console.ReadLine().ToUpper();
            if (a != "X") return a; else throw new Exception("Esci");
        }
    }
}

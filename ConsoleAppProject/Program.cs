using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using ADOX;
using OpenXmlDllProject;
using VenditaVeicoliDllProject;

namespace ConsoleAppProject {
    internal class Program {
        private static void Main()
        {
            bool created = false;
            string foo = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Car-shop");
            string accessDbPath;

            if (!Directory.Exists(foo))
            {
                Directory.CreateDirectory(foo);
                File.Create(Path.Combine(foo, Properties.Resources.ACCESS_DB_NAME));
            }
            else
            {
                foo = Path.Combine(foo, Properties.Resources.ACCESS_DB_NAME);
                if (!File.Exists(foo))
                {
                    Catalog c = new Catalog();
                    c.Create($"Provider=Microsoft.Ace.Oledb.12.0;Data Source={foo};");
                    c = null;
                }
            }
            accessDbPath = foo;

            string connString = $"Provider=Microsoft.Ace.Oledb.12.0;Data Source={accessDbPath};";
            SerializableBindingList<Veicolo> listaVeicoli = new SerializableBindingList<Veicolo>();
            VeicoliCommands vc = new VeicoliCommands();
            try
            {
                listaVeicoli = vc.GetVeicoliList(vc.GetRows(connString, "SELECT * FROM Veicoli;"));
                created = true;
            }
            catch (OleDbException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("\n\t\t\t=== " + Properties.Resources.PROGRAM_NAME + " ===\n");
            foreach (Veicolo v in listaVeicoli)
            {
                Console.WriteLine($"{v.Targa} - {v.Marca} {v.Modello} - {v.Stato} {v.GetPrezzo()} - {v.Colore}");
            }
            string scelta;
            do
            {
                bool trovato = false;
                Console.Write("# ");
                scelta = Console.ReadLine();
                if (!created && scelta.ToUpper().Split(' ')[0]!="TCREATE")
                {
                    Console.WriteLine("\nIl database non è ancora stato creato!");
                    continue;
                }

                switch (scelta.ToUpper().Split(' ')[0])
                {
                    case "EXPORT":
                    {
                        string dataType;
                        string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                        try
                        {
                            dataType = scelta.Split(' ')[1].Trim().ToUpper();
                        }
                        catch { Console.WriteLine("Sintassi comando errata!"); Console.ReadKey(); break; }
                        FileExport fe = new FileExport();
                        switch (dataType)
                        {
                            case "JSON":
                            {
                                fe.SerializeToJson(listaVeicoli, Path.Combine(desktop, "Veicoli.json"));
                                Console.WriteLine("Esportazione completata!");
                                break;
                            }
                            case "XML":
                            {
                                var f = new FileExport.SerializableBindingList<Veicolo>(listaVeicoli.ToList());
                                fe.SerializeToXml<Veicolo>(f, Path.Combine(desktop, "Veicoli.xml"));
                                Console.WriteLine("Esportazione completata!");
                                break;
                            }
                            case "EXCEL":
                            {
                                List<string[]> l = new List<string[]>();
                                foreach (var v in listaVeicoli)
                                {
                                    l.Add(new string[] { v.Targa, v.Marca, v.Modello, v.Immatricolazione.ToShortDateString(), v.Stato, v.GetPrezzo() });
                                }
                                string path = Path.Combine(desktop, "Veicoli.xlsx");
                                Excel xls = new Excel("Veicoli", path, l, new string[] { "Targa", "Marca", "Modello", "Immatricolazione", "Stato", "Prezzo" });
                                Console.WriteLine("Il documento è pronto!");
                                break;
                            }
                            default:
                                Console.WriteLine("Formato non supportato!");
                                break;
                        }
                        break;
                    }
                    case "HELP":
                    {
                        Console.WriteLine("\n" + File.ReadAllText(@".\Commands.txt") + "\n");
                        Console.ReadKey();
                        break;
                    }
                    case "VADD":
                    {
                        Console.WriteLine("\t\t\t\t=== NUOVO VEICOLO ===\n");
                        Console.Write("Auto o Moto? (X per uscire)[A/M]: ");
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
                            vc.Insert(v, connString);
                            Console.WriteLine("Veicolo Aggiunto!");
                            Console.ReadKey();
                        }

                        catch { }
                        break;
                    }
                    case "VSHOW":
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
                                string s = new string('-', 30);
                                trovato = true;
                                Console.WriteLine($"\n{v.Marca} {v.Modello}\n" + s +
                                    $"\nCilindrata: {v.Cilindrata} cc\n" + s +
                                    $"\nPotenza: {v.PotenzaKw} Kw\n" + s +
                                    $"\nImmatricolazione: {v.Immatricolazione.ToShortDateString()}\n" + s +
                                    $"\nStato: {v.Stato}\n" + s +
                                    $"\nKm Zero: {v.IsKmZero}\n" + s +
                                    $"\nChilometraggio: {v.KmPercorsi } Km\n" + s +
                                    $"\nColore: {v.Colore}\n" + s +
                                    $"\nPrezzo: {v.GetPrezzo()}\n" + s +
                                    ((v is Auto ? $"\nNumero airbag: {(v as Auto).NumeroAirBag}\n" : $"\nMarca sella: {(v as Moto).MarcaSella}\n") + s));
                                break;
                            }
                            if (!trovato) Console.WriteLine("Veicolo non trovato"); Console.ReadKey();
                        }
                        break;
                    }
                    case "VEDIT":
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
                                    vc.Update(v, connString);
                                }
                                catch { Console.WriteLine("Formato valore immesso errato!"); }
                            }
                        }
                        if (!trovato) { Console.WriteLine("Veicolo non trovato"); Console.ReadKey(); }
                        break;
                    }
                    case "VFIND":
                    {
                        Console.Write("Inserisci: ");
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
                    case "VDELETE":
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
                                    vc.Delete(v, connString);
                                    Console.WriteLine("Veicolo eliminato");
                                }
                                break;
                            }
                        }
                        if (!trovato) Console.WriteLine("Veicolo non trovato"); Console.ReadKey();
                        break;
                    }
                    case "CLEAR":
                    {
                        Console.Clear();
                        break;
                    }
                    case "TDROP":
                    {
                        Console.Write($"SEI SICURO DI VOLER ELIMINARE I DATI ? [S/N]: ");
                        if (Console.ReadLine().ToUpper() == "S")
                        {
                            try
                            {
                                vc.DropTable(connString);
                                Console.WriteLine("Database eliminato!");
                                listaVeicoli.Clear();
                                created = false;
                            }
                            catch (OleDbException e)
                            {
                                Console.WriteLine(e.Message);
                            }

                        }
                        break;
                    }
                    case "TSHOW":
                    {
                        foreach (Veicolo v in listaVeicoli)
                        {
                            Console.WriteLine($"{v.Targa} - {v.Marca} {v.Modello} - {v.Stato} {v.GetPrezzo()} - {v.Colore}");
                        }
                        break;
                    }
                    case "TCREATE":
                    {
                        Console.Write($"SEI SICURO DI VOLER CREARE LA TABELLA \"VEICOLI\" ? [S/N]: ");
                        if (Console.ReadLine().ToUpper() == "S")
                        {
                            try
                            {
                                vc.CreateTable(connString);
                                Console.WriteLine("Database creato!");
                                listaVeicoli = vc.GetVeicoliList(vc.GetRows(connString, "SELECT * FROM Veicoli;"));
                                created = true;
                            }
                            catch (OleDbException e)
                            {
                                Console.WriteLine(e.Message);
                            }

                        }
                        break;
                    }
                    case "TDEF":
                    {
                        string[] d = VeicoliDllProject.Properties.Resources.DATA_DEF.Split(',');
                        int n = d.Length;
                        Console.WriteLine(string.Join("\n", d));
                        Console.WriteLine("N. campi: " + n);

                        break;
                    }
                    case "TCOUNT":
                    {
                        Console.WriteLine("N. Record: " + listaVeicoli.Count);
                        break;
                    }
                    case "X": break;
                    default:
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

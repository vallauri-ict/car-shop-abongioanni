using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using DbDllProject;
using Newtonsoft.Json;
using VenditaVeicoliDllProject;

namespace Risorse {
    public class Utilities {
        public static string SequenzaChar(int n, char v = '-')
        {
            string s = "";
            for (int i = 0; i < n; i++)
                s += v;
            return s;
        }

        public static int IndexOf(string[] v, string obj)
        {
            for (int i = 0; i < v.Length; i++)
            {
                if (v[i] == obj)
                    return i;
            }
            throw new Exception();
        }

        public static string StringArrayToString(string[] v)
        {
            string s = "";
            foreach (string item in v)
            {
                s += item + " ";
            }
            return s;
        }
    }
    public class FileUtilities {
        public static List<object> FileToList(string filePath)
        {
            List<object> lst = new List<object>();
            using (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream)
                {
                    string dato = sr.ReadLine();
                    if (!lst.Contains(dato) && dato != "" && dato != null)
                        lst.Add(dato);
                }
            }
            return lst;
        }

        public static object[] FileToArray(string filePath)
        {
            object[] array = new object[0];
            using (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream)
                {
                    string dato = sr.ReadLine();
                    if (!array.Contains(dato) && dato != "" && dato != null)
                    {
                        Array.Resize(ref array, array.Length + 1);
                        array[array.Length - 1] = dato;
                    }

                }
            }
            return array;
        }

        public static IEnumerable<string> ToCsv<T>(IEnumerable<T> objectlist, string separator = "|")
        {
            foreach (var o in objectlist)
            {
                FieldInfo[] fields = o.GetType().GetFields();
                PropertyInfo[] properties = o.GetType().GetProperties();

                yield return string.Join(separator, fields.Select(f => (f.GetValue(o) ?? "").ToString())
                    .Concat(properties.Select(p => (p.GetValue(o, null) ?? "").ToString())).ToArray());
            }
        }//RITORNO DI UNA COLLEZIONE DI STRINGHE DA COLLEZIONE DI OGGETTI

        public static string ToCsvString<T>(IEnumerable<T> objectlist, string separator = "|")
        {
            StringBuilder csvdata = new StringBuilder();
            foreach (var o in objectlist)
            {
                FieldInfo[] fields = o.GetType().GetFields();
                PropertyInfo[] properties = o.GetType().GetProperties();

                csvdata.AppendLine(string.Join(separator, fields.Select(f => (f.GetValue(o) ?? "").ToString())
                    .Concat(properties.Select(p => (p.GetValue(o, null) ?? "").ToString())).ToArray()));
            }
            return csvdata.ToString();
        }//SERIALIZZAZIONE A STRINGA CSV DI UNA COLLEZIONE DI OGGETTI

        public static void SerializeToCsv<T>(IEnumerable<T> objectlist, string pathName)
        {
            string csv = ToCsvString(objectlist);
            File.WriteAllText(pathName, csv);
        }//SERIALIZZAZIONE A STRINGA CSV DA COLLEZIONE DI OGGETTI E SCRITTURA SU FILE


        public static List<object[]> OpenFromJsonFile(string pathName)
        {
            List<object[]> lst = new List<object[]>();
            JavaScriptSerializer js = new JavaScriptSerializer();
            object[] v = js.Deserialize<object[]>(File.ReadAllText(pathName));//ritorna un dictiornary<string,object>
            foreach (Dictionary<string, object> item in v)
            {
                lst.Add(ObjectValueDictionaryToStringArray(item));
            }
            return lst;
        }//APERTURA E PARSIFICAZIONE DA FILE A JSON

        public static List<object[]> OpenFromCsvFile(string pathName, char sep)
        {
            List<object[]> lst = new List<object[]>();
            using (StreamReader sr = new StreamReader(pathName))
            {
                while (!sr.EndOfStream)
                {
                    lst.Add(sr.ReadLine().Split(sep));
                }
            }
            return lst;
        }//APERTURA DA FILE CSV

        public static void SerializeToXml<T>(IEnumerable<T> objectlist, string pathName)
        {
            XmlSerializer x = new XmlSerializer(typeof(SerializableBindingList<T>));
            TextWriter writer = new StreamWriter(pathName);
            x.Serialize(writer, objectlist);
        }//SERIALIZZAZIONE A XML

        public static void SerializeToJson<T>(IEnumerable<T> objectlist, string pathName)
        {
            string json = JsonConvert.SerializeObject(objectlist, Formatting.Indented);
            pathName = pathName.Split('.')[1];
            File.WriteAllText("." + pathName + ".json", json);
        }//SERIALIZZAZIONE A JSON

        public static string[] ObjectListToStringArray<T>(List<T> item)
        {
            T[] value = new T[item.Count];
            item.CopyTo(value, 0);
            string[] s = new string[item.Count];
            for (int i = 0; i < value.Length; i++)
            {
                try
                {
                    s[i] = value[i].ToString() ?? "";
                }
                catch
                {
                    s[i] = "";
                }
            }
            return s;
        }//OGGETTI LISTA A ARRAY DI STRINGHE

        public static string[] ObjectValueDictionaryToStringArray(Dictionary<string, object> item)
        {
            object[] value = new object[item.Count];
            item.Values.CopyTo(value, 0);
            string[] s = new string[item.Count];
            for (int i = 0; i < value.Length; i++)
            {
                try
                {
                    s[i] = value[i].ToString() ?? "";
                }
                catch
                {
                    s[i] = "";
                }
            }
            return s;
        }        
    }
    public class VeicoliUtilities {


        public static void InsertCommand(Veicolo v, string connString)
        {
            using (OleDbConnection connection = new OleDbConnection(connString)) //data reader: oggetto per recuperare dati
            {
                connection.Open();
                OleDbCommand cmd = new OleDbCommand
                {
                    CommandText = "INSERT INTO Veicoli VALUES ('@targa', @automoto,'@marca', '@modello', @cilindrata, @potenzakw," +
                    "@immatricolazione,@usato,@kmzero,@kmpercorsi,'@colore',@prezzo,'@imagepath','@caratteristica');",
                    Connection=connection
                };
                cmd.Parameters.Add("@targa",OleDbType.VarChar,255).Value= v.Targa;
                cmd.Parameters.Add("@automoto",OleDbType.Boolean,2).Value = (v is Auto);
                cmd.Parameters.Add("@marca", OleDbType.VarChar, 255).Value = v.Marca;
                cmd.Parameters.Add("@modello", OleDbType.VarChar, 255).Value = v.Modello;
                cmd.Parameters.Add("@cilindrata", OleDbType.Integer, 32).Value = v.Cilindrata;
                cmd.Parameters.Add("@potenzakw", OleDbType.Double, 32).Value = v.PotenzaKw;
                cmd.Parameters.Add("@immatricolazione", OleDbType.DBDate, 32).Value = v.Immatricolazione;
                cmd.Parameters.Add("@usato", OleDbType.Boolean, 2).Value = v.IsUsato;
                cmd.Parameters.Add("@kmzero", OleDbType.Boolean, 2).Value = v.IsKmZero;
                cmd.Parameters.Add("@kmpercorsi", OleDbType.Integer, 32).Value = v.KmPercorsi; 
                cmd.Parameters.Add("@colore", OleDbType.VarChar, 255).Value = v.Colore;
                cmd.Parameters.Add("@prezzo", OleDbType.Double, 32).Value = v.Prezzo;
                cmd.Parameters.Add("@imagepath", OleDbType.VarChar, 255 ).Value = v.ImagePath;
                string c = (v is Auto ? (v as Auto).NumeroAirBag.ToString() : (v as Moto).MarcaSella);
                cmd.Parameters.Add("@caratteristica", OleDbType.VarChar, 255).Value =c;
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
        }
        public static string createTableVeicoliSqlString =
            $"CREATE TABLE Veicoli (" +
                $"Targa varchar(255) NOT NULL PRIMARY KEY," +
                $"AutoMoto boolean," +
                $"Marca varchar(255)," +
                $"Modello varchar(255)," +
                $"Cilindrata int," +
                $"PotenzaKw double," +
                $"Immatricolazione datetime," +
                $"Usato boolean," +
                $"KmZero boolean," +
                $"KmPercorsi int," +
                $"Colore varchar(255)," +
                $"Prezzo double," +
                $"ImagePath varchar(255)," +
                $"Caratteristica varchar(255)" +
                $");";

        public static void InsertCommand(Vendita v,string connString)
        {
            using (OleDbConnection connection = new OleDbConnection(connString)) //data reader: oggetto per recuperare dati
            {
                OleDbCommand cmd = new OleDbCommand
                {
                    CommandText = "INSERT INTO Storico VALUES ('@veicolovenduto',@prezzovendita, #@dataconsegna#,@consegnato,@cliente);",
                    Connection=connection
                };

                cmd.Parameters.Add("@veicolovenduto", OleDbType.VarChar,v.MezzoVenduto.Targa.Length).Value = v.MezzoVenduto.Targa;
                cmd.Parameters.Add("@prezzovendita", OleDbType.Double,8).Value = v.PrezzoVendita;
                cmd.Parameters.Add("@dataconsegna", OleDbType.Date,8).Value = v.DataVendita;
                cmd.Parameters.Add("@consegnato", OleDbType.Boolean,2).Value = v.IsConsegnato;
                cmd.Parameters.Add("@cliente", OleDbType.VarChar,v.CodFiscaleCliente.Length).Value = v.CodFiscaleCliente;
                AccessUtils.ExecQuery(cmd);
            }
        }
        public static void UpdateCommand(Veicolo v,string connString)
        {
            using (OleDbConnection connection = new OleDbConnection(connString)) //data reader: oggetto per recuperare dati
            {
                OleDbCommand cmd = new OleDbCommand
                {
                    CommandText = "UPDATE Veicoli SET Targa='@targa',AutoMoto=@automoto,Marca='@marca', Modello='@modello', Cilindrata=@cilindrata,PotenzaKw=@potenzakw," +
                    "Immatricolazione=#@immatricolazione#,Usato=@usato,KmZero=@kmzero," +
                    "KmPercorsi=@kmpercorsi,Colore='@colore',Prezzo=@prezzo,ImagePath='@imagepath',Caratteristica='@caratteristica' WHERE Targa=@targa;",
                    Connection=connection
                };

                cmd.Parameters.AddWithValue("@targa", v.Targa);
                cmd.Parameters.AddWithValue("@automoto", v is Auto);
                cmd.Parameters.AddWithValue("@marca", v.Marca);
                cmd.Parameters.AddWithValue("@modello", v.Modello);
                cmd.Parameters.AddWithValue("@cilindrata", v.Cilindrata);
                cmd.Parameters.AddWithValue("@potenzakw", v.PotenzaKw);
                cmd.Parameters.AddWithValue("@immatricolazione", v.Immatricolazione.ToShortDateString());
                cmd.Parameters.AddWithValue("@usato", v.IsUsato);
                cmd.Parameters.AddWithValue("@kmzero", v.IsKmZero);
                cmd.Parameters.AddWithValue("@kmpercorsi", v.KmPercorsi);
                cmd.Parameters.AddWithValue("@colore", v.Colore);
                cmd.Parameters.AddWithValue("@prezzo", v.Prezzo);
                cmd.Parameters.AddWithValue("@imagepath", v.ImagePath);
                string c = (v is Auto ? (v as Auto).NumeroAirBag.ToString() : (v as Moto).MarcaSella);
                cmd.Parameters.AddWithValue("@caratteristica", c);
                AccessUtils.ExecQuery(cmd);
            }

        }

        public static void UpdateCommand(Vendita v,string connString)
        {
            using (OleDbConnection connection = new OleDbConnection(connString)) //data reader: oggetto per recuperare dati
            {
                OleDbCommand cmd = new OleDbCommand
                {
                    CommandText = "UPDATE Storico SET VeicoloVenduto='@veicolovenduto',PrezzoVendita=@prezzovendita," +
                "DataConsegna= #@dataconsegna#,Consegnato=@consegnato,Cliente=@cliente WHERE VeicoloVenduto=@veicolovenduto;",
                    Connection=connection
                };

                cmd.Parameters.Add("@veicolovenduto", OleDbType.VarChar, v.MezzoVenduto.Targa.Length).Value = v.MezzoVenduto.Targa;
                cmd.Parameters.Add("@prezzovendita", OleDbType.Double, 8).Value = v.PrezzoVendita;
                cmd.Parameters.Add("@dataconsegna", OleDbType.Date, 8).Value = v.DataVendita;
                cmd.Parameters.Add("@consegnato", OleDbType.Boolean, 2).Value = v.IsConsegnato;
                cmd.Parameters.Add("@cliente", OleDbType.VarChar, v.CodFiscaleCliente.Length).Value = v.CodFiscaleCliente;
                AccessUtils.ExecQuery(cmd);
            }
        }


        public static string createTableStoricoSqlString = 
            $"CREATE TABLE Veicoli (" +
                $"Targa varchar(255) NOT NULL PRIMARY KEY," +
                $"AutoMoto bit," +
                $"Marca varchar(255)," +
                $"Modello varchar(255)," +
                $"Cilindrata int," +
                $"PotenzaKw decimal," +
                $"Immatricolazione DateTime," +
                $"Usato bit," +
                $"KmZero bit," +
                $"KmPercorsi int," +
                $"Colore varchar(255)," +
                $"Prezzo decimal," +
                $"ImagePath varchar(255)," +
                $"Caratteristica varchar(255)" +
                $");";
    }
}

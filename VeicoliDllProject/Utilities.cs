using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
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
        public FileUtilities() { }

        public List<object> FileToList(string filePath)
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

        public object[] FileToArray(string filePath)
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

        public IEnumerable<string> ToCsv<T>(IEnumerable<T> objectlist, string separator = "|")
        {
            foreach (var o in objectlist)
            {
                FieldInfo[] fields = o.GetType().GetFields();
                PropertyInfo[] properties = o.GetType().GetProperties();

                yield return string.Join(separator, fields.Select(f => (f.GetValue(o) ?? "").ToString())
                    .Concat(properties.Select(p => (p.GetValue(o, null) ?? "").ToString())).ToArray());
            }
        }//RITORNO DI UNA COLLEZIONE DI STRINGHE DA COLLEZIONE DI OGGETTI

        public string ToCsvString<T>(IEnumerable<T> objectlist, string separator = "|")
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

        public void SerializeToCsv<T>(IEnumerable<T> objectlist, string pathName)
        {
            string csv = ToCsvString(objectlist);
            File.WriteAllText(pathName, csv);
        }//SERIALIZZAZIONE A STRINGA CSV DA COLLEZIONE DI OGGETTI E SCRITTURA SU FILE


        public List<object[]> OpenFromJsonFile(string pathName)
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

        public List<object[]> OpenFromCsvFile(string pathName, char sep)
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

        public void SerializeToXml<T>(IEnumerable<T> objectlist, string pathName)
        {
            XmlSerializer x = new XmlSerializer(typeof(SerializableBindingList<T>));
            TextWriter writer = new StreamWriter(pathName);
            x.Serialize(writer, objectlist);
        }//SERIALIZZAZIONE A XML

        public void SerializeToJson<T>(IEnumerable<T> objectlist, string pathName)
        {
            string json = JsonConvert.SerializeObject(objectlist, Formatting.Indented);
            pathName = pathName.Split('.')[1];
            File.WriteAllText("." + pathName + ".json", json);
        }//SERIALIZZAZIONE A JSON

        public string[] ObjectListToStringArray<T>(List<T> item)
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

        public string[] ObjectValueDictionaryToStringArray(Dictionary<string, object> item)
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

    /*********************SQL COMMANDS TO AVOID INJECTION*********************/

    public class VeicoliCommands {
        private abstract class VeicoloCommand {
            internal OleDbCommand cmd;
            public VeicoloCommand(OleDbConnection c) { cmd = new OleDbCommand { Connection = c }; }
            public OleDbCommand GetCommand() { return cmd; }
            public void AggiungiParametri(Veicolo v)
            {
                cmd.Parameters.Add("@targa", OleDbType.VarChar, 255).Value = v.Targa;
                cmd.Parameters.Add("@automoto", OleDbType.Boolean).Value = (v is Auto);
                cmd.Parameters.Add("@marca", OleDbType.VarChar, 255).Value = v.Marca;
                cmd.Parameters.Add("@modello", OleDbType.VarChar, 255).Value = v.Modello;
                cmd.Parameters.Add("@cilindrata", OleDbType.Integer).Value = v.Cilindrata;
                cmd.Parameters.Add("@potenzakw", OleDbType.Double).Value = v.PotenzaKw;
                cmd.Parameters.Add("@immatricolazione", OleDbType.DBDate).Value = v.Immatricolazione;
                cmd.Parameters.Add("@usato", OleDbType.Boolean).Value = v.IsUsato;
                cmd.Parameters.Add("@kmzero", OleDbType.Boolean).Value = v.IsKmZero;
                cmd.Parameters.Add("@kmpercorsi", OleDbType.Integer).Value = v.KmPercorsi;
                cmd.Parameters.Add("@colore", OleDbType.VarChar, 255).Value = v.Colore;
                cmd.Parameters.Add("@prezzo", OleDbType.Double).Value = v.Prezzo;
                cmd.Parameters.Add("@imagepath", OleDbType.VarChar, 255).Value = v.ImagePath;
                string c = (v is Auto ? (v as Auto).NumeroAirBag.ToString() : (v as Moto).MarcaSella);
                cmd.Parameters.Add("@caratteristica", OleDbType.VarChar, 255).Value = c;
            }
        }

        private class InsertVeicoloCommand : VeicoloCommand {
            public InsertVeicoloCommand(OleDbConnection c) : base(c)
            {
                cmd.CommandText = "INSERT INTO Veicoli VALUES (@targa, @automoto,@marca, @modello, @cilindrata, @potenzakw," +
                    "@immatricolazione,@usato,@kmzero,@kmpercorsi,@colore,@prezzo,@imagepath,@caratteristica);";
            }
        }
        private class UpdateVeicoloCommand : VeicoloCommand {
            public UpdateVeicoloCommand(OleDbConnection c) : base(c)
            {
                cmd.CommandText = "UPDATE Veicoli SET Targa=@targa,AutoMoto=@automoto,Marca=@marca, Modello=@modello, Cilindrata=@cilindrata,PotenzaKw=@potenzakw," +
                    "Immatricolazione=@immatricolazione,Usato=@usato,KmZero=@kmzero," +
                    "KmPercorsi=@kmpercorsi,Colore=@colore,Prezzo=@prezzo,ImagePath=@imagepath,Caratteristica=@caratteristica WHERE Targa=@targa;";
            }
        }
        private class DeleteVeicoliCommand : VeicoloCommand {
            public DeleteVeicoliCommand(OleDbConnection c) : base(c)
            {
                cmd.CommandText = "DELETE FROM Veicoli WHERE Targa=@targa;";
            }
            public new void AggiungiParametri(Veicolo v)
            {
                cmd.Parameters.Add("@targa", OleDbType.VarChar, 255).Value = v.Targa;
            }
        }

        public VeicoliCommands() { }

        public void CreateTable(string connString)
        {
            string sql = $"CREATE TABLE Veicoli (" +
                $"Modello varchar(255) NOT NULL PRIMARY KEY," +
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
            using (OleDbConnection connection = new OleDbConnection(connString)) //data reader: oggetto per recuperare dati
            {
                connection.Open();
                OleDbCommand cmd = new OleDbCommand(sql,connection);
                cmd.ExecuteNonQuery();
            }
        }

        public DataTable GetRows(string connString, string sqlString)
        {
            DataTable t = new DataTable();
            using (OleDbConnection connection = new OleDbConnection(connString)) //data reader: oggetto per recuperare dati
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand(sqlString, connection);
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                da.Fill(t);
            }
            return t;
        }

        public void Insert(Veicolo v, string connString)
        {
            using (OleDbConnection connection = new OleDbConnection(connString)) //data reader: oggetto per recuperare dati
            {
                connection.Open();
                InsertVeicoloCommand i = new InsertVeicoloCommand(connection);
                OleDbCommand cmd = i.GetCommand();
                i.AggiungiParametri(v);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(Veicolo v, string connString)
        {
            using (OleDbConnection connection = new OleDbConnection(connString)) //data reader: oggetto per recuperare dati
            {
                connection.Open();
                DeleteVeicoliCommand d = new DeleteVeicoliCommand(connection);
                OleDbCommand cmd = d.GetCommand();
                d.AggiungiParametri(v);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Veicolo v, string connString)
        {
            using (OleDbConnection connection = new OleDbConnection(connString)) //data reader: oggetto per recuperare dati
            {
                connection.Open();
                UpdateVeicoloCommand u = new UpdateVeicoloCommand(connection);
                OleDbCommand cmd = u.GetCommand();
                u.AggiungiParametri(v);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
        }
    }
}

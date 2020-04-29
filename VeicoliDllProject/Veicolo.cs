using System;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Xml.Serialization;

namespace VenditaVeicoliDllProject {
    [Serilizable]
    [XmlInclude(typeof(Moto))]
    [XmlInclude(typeof(Auto))]

    public abstract class Veicolo {//abstract impedisce alla classe di essere instanziabile
        #region FIELDS
        private string marca;
        private string targa;
        private int cilindrata;
        private double potenzaKw;
        private DateTime immatricolazione;
        private bool isUsato;
        private string modello;
        private bool isKmZero;
        private int kmPercorsi;
        private string colore;
        private double prezzo;
        #endregion

        public Veicolo() { }

        //COSTRUTTORE DA ELEMENTI SINGOLI
        public Veicolo(string marca, string modello, string targa, int cilindrata, double potenzaKw, DateTime immatricolazione, bool isUsato, bool isKmZero, int kmPercorsi, string colore, double prezzo)
        {
            this.ImagePath = "";
            this.Marca = marca;
            this.Modello = modello;
            this.Cilindrata = cilindrata;
            this.PotenzaKw = potenzaKw;
            this.Immatricolazione = immatricolazione;
            this.IsUsato = isUsato;
            this.IsKmZero = isKmZero;
            this.KmPercorsi = kmPercorsi;
            this.Colore = colore;
            this.Prezzo = prezzo;
            this.Targa = targa;
        }

        public Veicolo(object[] dato)
        {
            if (dato.Length != 14)
                throw new Exception("Formato array dati errato!");
            this.Marca = dato[1].ToString();
            this.Modello = dato[2].ToString();
            this.Cilindrata = Convert.ToInt32(dato[3]);
            this.PotenzaKw = Convert.ToDouble(dato[4]);
            this.Immatricolazione = Convert.ToDateTime(dato[5]);
            this.IsUsato = Convert.ToBoolean(dato[6]);
            this.IsKmZero = Convert.ToBoolean(dato[7]);
            this.KmPercorsi = Convert.ToInt32(dato[8]);
            this.Colore = dato[9].ToString();
            this.Prezzo = Convert.ToDouble(dato[10]);
            this.ImagePath = dato[11].ToString();
            this.Targa = dato[13].ToString();
        }//COSTRUTTORE DA STRINGA DI ARRAY

        public string[] GetDatiIntoAtringArray()
        {
            return new string[]
            {
                this.Marca,
                this.Modello,
                this.Cilindrata.ToString(),
                this.PotenzaKw.ToString(),
                this.Immatricolazione.ToShortDateString(),
                this.IsUsato.ToString(),
                this.IsKmZero.ToString(),
                this.KmPercorsi.ToString(),
                this.Colore.ToString(),
                this.Prezzo.ToString("C"),
                this.Targa
            };
        }

        public bool Contains(string item)
        {
            item = item.ToUpper();
            return
                ((item.Contains("AUTO") && (this is Auto)) ||
                (item.Contains("MOTO") && (this is Moto)) ||
                (this.Targa.Contains(item)) ||
                (item.Contains("USAT") && this.IsUsato) ||
                (item.Contains("NUOV") && !this.IsUsato)) ||
                item == this.Marca.ToString().ToUpper() ||
                (this.Marca.ToString().ToUpper().Contains(item)) ||
                item == this.Modello.ToString().ToUpper() ||
                (this.Modello.ToString().ToUpper().Contains(item) ||
                this.Colore.ToUpper().Contains(item.ToUpper().Substring(0, item.Length - 1)));
        }//FUNZIONE CHE RITORNA BOOLEANO SE STRINGA E' CONTENUTA NELL'OGGETTO

        public static SerializableBindingList<Veicolo> Search(string s, System.Collections.Generic.List<Veicolo> lst, char sep = ' ')
        {
            string[] search = s.Split(sep);
            SerializableBindingList<Veicolo> results = new SerializableBindingList<Veicolo>();//LISTA RISULTATI
            foreach (Veicolo item in lst)
            {
                if (item.Contains(search[0]))
                {
                    results.Add(item);
                }
            }
            for (int i = 0; i < search.Length; i++)
            {
                for (int j = 0; j < results.Count; j++)
                {
                    if (!results[j].Contains(search[i]))
                    {
                        results.Remove(results[j]);
                        j--;
                        if (results.Count == 1)
                            break;
                    }
                }
                if (results.Count == 1)
                    break;
            }
            return results;
        }

        public static object[] SetArray(DataRow r)
        {
            return new object[]{
                        r["Caratteristica"].ToString().Trim(),
                        r["Marca"].ToString().Trim(),
                        r["Modello"].ToString().Trim(),
                        r["Cilindrata"].ToString().Trim(),
                        r["PotenzaKw"].ToString().Trim(),
                        r["Immatricolazione"].ToString().Trim(),
                        r["Usato"].ToString().Trim(),
                        r["KmZero"].ToString().Trim(),
                        r["KmPercorsi"].ToString().Trim(),
                        r["Colore"].ToString().Trim(),
                        r["Prezzo"].ToString().Trim(),
                        (r["ImagePath"].ToString().Trim().Contains(@".\www\images\")?r["ImagePath"].ToString().Trim():@".\www\images\"+r["ImagePath"].ToString().Trim()),
                        "",
                        r["Targa"].ToString().Trim(),
                    };//ARRAY DI DATI PER FACILITARE LA CREAZIONE DI OGGETTI
        }
        public static object[] SetArray(string[] r)
        {
            return new object[]{
                        r[12].ToString().Trim(),
                        r[1].ToString().Trim(),
                        r[2].ToString().Trim(),
                        r[3].ToString().Trim(),
                        r[4].ToString().Trim(),
                        r[5].ToString().Trim(),
                        r[6].ToString().Trim(),
                        r[7].ToString().Trim(),
                        r[8].ToString().Trim(),
                        r[9].ToString().Trim(),
                        r[10].ToString().Trim(),
                        (r[11].ToString().Trim().Contains(@".\www\images\")?r[11].ToString().Trim():@".\www\images\"+r[11].ToString().Trim()),
                        "",
                        r[13].ToString().Trim(),
                    };//ARRAY DI DATI PER FACILITARE LA CREAZIONE DI OGGETTI
        }

        public string GetProperties()
        {
            return
                this.Marca.ToUpper() + " " +
                this.Modello.ToUpper() + " " +
                this.Cilindrata.ToString().ToUpper() + " " +
                (this.IsUsato ? "USATO USATA USATE USATI " : "NUOVO NUOVE NUOVA NUOVI ") +
                this.Colore;
        }

        public string GetPrezzo()
        {
            return this.Prezzo.ToString("C").Split(',')[0] + "€";
        }

        public object this[string propertyName] {
            get {
                PropertyInfo property = GetType().GetProperty(propertyName);
                return property.GetValue(this, null);
            }
            set {
                PropertyInfo property = GetType().GetProperty(propertyName);
                Type propType = property.PropertyType;
                if (value == null)
                {
                    if (propType.IsValueType && Nullable.GetUnderlyingType(propType) == null)
                    {
                        throw new InvalidCastException();
                    }
                    else
                    {
                        property.SetValue(this, null, null);
                    }
                }
                else if (value.GetType() == propType)
                {
                    property.SetValue(this, value, null);
                }
                else
                {
                    TypeConverter typeConverter = TypeDescriptor.GetConverter(propType);
                    object propValue = typeConverter.ConvertFromString(value.ToString());
                    property.SetValue(this, propValue, null);
                }
            }
        }

        //PROPERTIES
        public string Marca { get => this.marca; set => this.marca = value; }
        public string Modello { get => this.modello; set => this.modello = value; }
        public int Cilindrata { get => this.cilindrata; set => this.cilindrata = value; }
        public double PotenzaKw { get => this.potenzaKw; set => this.potenzaKw = value; }
        public DateTime Immatricolazione { get => this.immatricolazione; set => this.immatricolazione = value; }
        public bool IsUsato { get => this.isUsato; set => this.isUsato = value; }
        public bool IsKmZero { get => this.isKmZero; set => this.isKmZero = value; }
        public int KmPercorsi { get => this.kmPercorsi; set => this.kmPercorsi = value; }
        public string Colore { get => this.colore; set => this.colore = value; }
        public double Prezzo { get => this.prezzo; set => this.prezzo = value; }
        public string ImagePath { get; set; }
        public string Stato { get { return this.IsUsato ? "Usato" : "Nuovo"; } }

        public string Targa { get => this.targa; set => this.targa = value; }

        public override string ToString() => $"{this.Marca} - {this.Modello}({this.Colore}) immatricolata il {this.Immatricolazione.ToShortDateString()}";
    }
}

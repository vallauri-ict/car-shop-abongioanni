using System;
using System.Drawing;

namespace VenditaVeicoliDllProject {
    [Serilizable()]
    public class Auto : Veicolo {
        #region FIELDS
        private int numeroAirBag;
        #endregion
        public int NumeroAirBag { get => this.numeroAirBag; set => this.numeroAirBag = value; }

        public Auto(string marca,
                    string modello,
                    string targa,
                    int cilindrata,
                    double potenzaKw,
                    DateTime immatricolazione,
                    bool isUsato,
                    bool isKmZero,
                    int kmPercorsi,
                    string colore, int nAB,double prezzo) : base(marca, modello,targa, cilindrata, potenzaKw, immatricolazione, isUsato, isKmZero, kmPercorsi, colore,prezzo)
        {
            this.NumeroAirBag = nAB;
        }

        public Auto(object[] dato) : base(dato)
        {
            this.NumeroAirBag = Convert.ToInt32(dato[0]);
        }
        public Auto() { }
        public override string ToString() => $"Auto: " + base.ToString();


    }
}

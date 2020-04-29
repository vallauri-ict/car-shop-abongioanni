using System;

namespace VenditaVeicoliDllProject {
    [Serilizable]
    public class Moto : Veicolo {

        private string marcaSella;
        public string MarcaSella { get => this.marcaSella; set => this.marcaSella = value; }

        public Moto(string marca,
                    string modello,
                    string targa,
                    int cilindrata,
                    double potenzaKw,
                    DateTime immatricolazione,
                    bool isUsato,
                    bool isKmZero,
                    int kmPercorsi,
                    string colore, string Ms, double prezzo) : base(marca, modello, targa, cilindrata, potenzaKw, immatricolazione, isUsato, isKmZero, kmPercorsi, colore, prezzo)
        {
            this.MarcaSella = Ms;
        }
        public Moto() { }
        public Moto(object[] dato) : base(dato)
        {
            this.MarcaSella = dato[0].ToString();
        }

        public override string ToString() => $"Moto: " + base.ToString();

    }
}

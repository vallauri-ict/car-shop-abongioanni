using System;
using System.Collections.Generic;
using System.Text;

namespace VenditaVeicoliDllProject {
    public class Vendita {
        /// <summary>
        /// OGGETTO CHE RACCHIUDE LE INFORMAZIONI RIGUARDO AD UNA VENDITA:
        /// - MEZZO
        /// - PREZZO DI VENDITA
        /// - DATA DI VENDITA
        /// - SE E' STATO CONSEGNATO OPPURE NO
        /// </summary>
       
        private Veicolo mezzoVenduto;
        double prezzoVendita;
        DateTime dataVendita;
        bool isConsegnato;
        string codFiscaleCliente;
        public Vendita() { }
        public Vendita(Veicolo mezzoVenduto, double prezzoVendita, DateTime dataVendita, bool isConsegnato,string codFi)
        {
            this.mezzoVenduto = mezzoVenduto;
            this.prezzoVendita = prezzoVendita;
            this.dataVendita = dataVendita;
            this.isConsegnato = isConsegnato;
            this.CodFiscaleCliente = codFi;
        }

        public Veicolo MezzoVenduto { get => this.mezzoVenduto; set => this.mezzoVenduto = value; }
        public double PrezzoVendita { get => this.prezzoVendita; set => this.prezzoVendita = value; }
        public DateTime DataVendita { get => this.dataVendita; set => this.dataVendita = value; }
        public bool IsConsegnato { get => this.isConsegnato; set => this.isConsegnato = value; }
        public string CodFiscaleCliente { get => this.codFiscaleCliente; set => this.codFiscaleCliente = value; }
    }
}

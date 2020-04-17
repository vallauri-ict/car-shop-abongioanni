using System;
using System.Collections.Generic;
using System.Text;

namespace VenditaVeicoliDllProject {
    public class StoricoVendite {

        /// <summary>
        /// CLASSE CHE RACCHIUDE UNA LISTA DI VENDITE E 
        /// NE PERMETTE LA MODIFICA E IL SALVATAGGIO SU FILE
        /// </summary>
        
        private SerializableBindingList<Vendita> listaVendite;

        public StoricoVendite()
        {
            this.Storico = new SerializableBindingList<Vendita>();
        }//COSTRUTTORE

        public void AddVendita(Veicolo mezzoVenduto, double prezzoVendita, DateTime dataVendita, bool isConsegnato,string codFi)
        {
            this.Storico.Add(new Vendita(mezzoVenduto, prezzoVendita, dataVendita, isConsegnato,codFi));
        }//AGGIUNTA DI UNA VENDITA

        public void AddVendita(Vendita v)
        {
            this.Storico.Add(v);
        }//AGGIUNTA DI UNA VENDITA

        public List<Veicolo> GetVeicoli()
        {
            List<Veicolo> lst = new List<Veicolo>();
            foreach (var item in this.listaVendite)
            {
                lst.Add(item.MezzoVenduto);
            }
            return lst;
        }
        
        public bool ControllaTarghe(string targa)
        {
            bool x = false;
            foreach (Veicolo v in this.GetVeicoli())
                x = v.Targa == targa;
            return x;
        }

        public Vendita GetVendita(int index)
        {
            if (index < 0 || index >= this.Storico.Count)
                throw new Exception();
            return this.Storico[index];
        }//RITORNA VENDITA DA INDICE

        public SerializableBindingList<Vendita> GetVenditaAll()
        {
            return this.Storico;
        }//RITORNA LA LISTA CON TUTTE LE VENDITE

        public Vendita GetVenditaLast()
        {
            return this.Storico[this.Storico.Count-1];
        }//RITORNA ULTIMA VENDITA

        public Vendita this[int index] { 
            get => this.Storico[index]; 
            set => this.Storico[index] = value; 
        }//ITERATORE

        public void Remove(Vendita v)
        {
            foreach(Vendita item in this.Storico)
            {
                if (item == v)
                {
                    this.Storico.Remove(item);
                    return;
                }
            }
        }//RIMUOVE DA OGGETTO

        public void RemoveAt(int index)
        {
            this.Storico.RemoveAt(index);
        }//RIMUOVE DA INDICE

        public int Count { get => this.Storico.Count; }//NUOMERO DI ELEMENTI

        private SerializableBindingList<Vendita> Storico { get => this.listaVendite; set => this.listaVendite = value; }//LISTA VENDITE

        public void Clear()
        {
            this.Storico.Clear();
        }// ELIMINAZIONE ELEMENTI
    }
}

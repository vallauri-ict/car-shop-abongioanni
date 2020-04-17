using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace VenditaVeicoliDllProject {
    [Serializable]
    public class SerializableBindingList<T> : BindingList<T> {
        public List<T> ToList()
        {
            return new List<T>(this);
        }
    }
}

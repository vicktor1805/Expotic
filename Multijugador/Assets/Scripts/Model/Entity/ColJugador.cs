using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Model.Entity
{
    
    public class ColJugador
    {
        public string id { get; set; }
        public string nombre { get; set; }

        public ColJugador(string id, string nombre)
        {
            this.id = id;
            this.nombre = nombre;
        }
    }
}

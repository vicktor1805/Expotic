using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Model.Entity
{
    public class Player
    {
        public int id { get; set; }
        public string nombre { get; set; }

        public Player()
        {

        }

        public Player(int id, string nombre)
        {
            this.id = id;
            this.nombre = nombre;
        }
    }
}

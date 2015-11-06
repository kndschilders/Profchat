using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database_Layer;

namespace Class_Layer
{
    public class Gebruiker
    {
        public int ID { get; private set; }
        public string Naam { get; private set; }
        public int IsOnline { get; private set; }
        public bool IsChatting { get; private set; }

        public Gebruiker(int id, string naam, int isOnline)
        {
            this.ID = id;
            this.Naam = naam;
            this.IsOnline = isOnline;
            this.IsChatting = false;
        }

        public int returnID()
        {
            return 0;
        }

        public override string ToString()
        {
            return Naam;
        }
    }
}

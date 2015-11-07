using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database_Layer;
using System.Collections.ObjectModel;

namespace Class_Layer
{
    public class Gebruiker
    {
        /// <summary>
        /// The ID is used as a reference for the database
        /// </summary>
        public int ID { get; private set; }
        public string Naam { get; private set; }
        public int IsOnline { get; private set; }
        public bool IsChatting { get; private set; }
        private List<int> currentRooms { get; set; }

        public Gebruiker(int id, string naam, int isOnline)
        {
            this.ID = id;
            this.Naam = naam;
            this.IsOnline = isOnline;
            this.IsChatting = false;
            this.currentRooms = new List<int>();
        }

        //public int returnID()
        //{
        //    return 0;
        //}

        public ReadOnlyCollection<int> GetCurrentRooms()
        {
            return this.currentRooms.AsReadOnly();
        }

        public void AddRoom(int roomnr)
        {
            this.currentRooms.Add(roomnr);
        }

        public override string ToString()
        {
            return Naam;
        }
    }
}

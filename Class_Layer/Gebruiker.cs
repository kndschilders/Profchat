//-----------------------------------------------------------------------
// <copyright file="Gebruiker.cs" company="ICT4Participation">
//     Copyright (c) ICT4Participation. All rights reserved.
// </copyright>
// <author>ICT4Participation</author>
//-----------------------------------------------------------------------
namespace Class_Layer
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Database_Layer;

    /// <summary>
    /// The <see cref="Gebruiker" /> class keeps track of the information about a user
    /// </summary>
    public class Gebruiker
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Gebruiker" /> class.
        /// </summary>
        /// <param name="id">The id of the user</param>
        /// <param name="naam">The name of the user</param>
        /// <param name="isOnline">Whether or not the user is online</param>
        public Gebruiker(int id, string naam, int isOnline)
        {
            this.ID = id;
            this.Naam = naam;
            this.IsOnline = isOnline;
            this.IsChatting = false;
            this.CurrentRooms = new List<int>();
        }

        /// <summary>
        /// Gets the ID of the user
        /// </summary>
        public int ID { get; private set; }

        /// <summary>
        /// Gets the name of the user
        /// </summary>
        public string Naam { get; private set; }
        
        /// <summary>
        /// Gets the IsOnline property, which is used to see if a user is online
        /// </summary>
        public int IsOnline { get; private set; }
        
        /// <summary>
        /// Gets a value indicating whether the user is chatting
        /// </summary>
        public bool IsChatting { get; private set; }

        /// <summary>
        /// Gets or sets a list of rooms the user is currently connected to
        /// </summary>
        private List<int> CurrentRooms { get; set; }

        /// <summary>
        /// Get the current rooms of the user
        /// </summary>
        /// <returns>Return the list of currently connected rooms</returns>
        public ReadOnlyCollection<int> GetCurrentRooms()
        {
            return this.CurrentRooms.AsReadOnly();
        }

        /// <summary>
        /// Add a room to the list of currently connected rooms
        /// </summary>
        /// <param name="roomnr">The room number of the new room</param>
        public void AddRoom(int roomnr)
        {
            this.CurrentRooms.Add(roomnr);
        }

        /// <summary>
        /// Get the class in string format
        /// </summary>
        /// <returns>Return the class in string format</returns>
        public override string ToString()
        {
            return this.Naam;
        }
    }
}

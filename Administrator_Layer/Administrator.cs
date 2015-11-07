using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Class_Layer;
using System.Data;

namespace Administrator_Layer
{
    public class Administrator
    {
        public List<Object> Vrijwilligers;
        private Data data = new Data();

        public Administrator()
        {
            Vrijwilligers = new List<Object>();

            this.UpdateGebruikers();
        }

        public void UpdateGebruikers()
        {
            List<Gebruiker> list = data.HaalGebruikersOp();

            this.Vrijwilligers.Clear();

            if (list != null)
                foreach (Gebruiker g in list)
                    Vrijwilligers.Add(g);
        }

        public bool SetOnline(int gebrID)
        {
            return this.data.UpdateOnlineStatus(true, gebrID);
        }

        public bool SetOffline(int gebrID)
        {
            return this.data.UpdateOnlineStatus(false, gebrID);
        }

        public void SendMessage(string message, int gebrID, int roomID)
        {
            this.data.SendMessage(message, gebrID, roomID);
        }

        public List<string> ReturnMessages(int chatroomID)
        {
            List<string> returned = new List<string>();

            DataTable dt = this.data.ReturnMessages(chatroomID);

            foreach (DataRow row in dt.Rows)
            {
                //USERID, MESSAGEBODY, SENDDATE
                int userID;
                string message;
                string senddate;

                userID = Convert.ToInt32(row.ItemArray[0]);
                message = Convert.ToString(row.ItemArray[1]);
                senddate = Convert.ToString(row.ItemArray[2]);

                // Haal gebruiker op
                Gebruiker gebr = this.data.HaalGebruikerOp(userID);

                string gebrNaam = gebr.Naam;

                returned.Add(String.Format("{0}<{1}>: {2}", gebrNaam, senddate.Split(' ')[1], message));
            }

            return returned;
        }

        public Object HaalGebruikerOp(int userID)
        {
            return this.data.HaalGebruikerOp(userID);
        }

        public List<int> ReturnUserChatrooms(int userID)
        {
            // return this.data.
            return this.data.ReturnUserChatrooms(userID);
        }

        public List<int> GetUserChatrooms(int userID)
        {
            return this.data.GetUserChatrooms(userID);
        }

        public void CreateRoom(out int roomID)
        {
            this.data.CreateRoom(out roomID);
        }

        public void AddUserToChatroom(int userID, int roomID)
        {
            this.data.AddUserToChatroom(userID, roomID);
        }
    }
}

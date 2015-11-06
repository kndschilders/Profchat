using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Database_Layer;

namespace Class_Layer
{
    public class Chatroom
    {
        public int ChatroomID { get; private set; }
        public List<Gebruiker> Users;

        private Data data;


        public Chatroom()
        {
            this.Users = new List<Gebruiker>();

            // Check if chatroom exists
                // if false
                    // create new chatroom
                    // get chatroomID from database
                // else
                    // get chatroomID from database
            
            

            // Get ChatroomID from database
            Database.ExecuteQuery("SELECT CHATROOMID FROM USER_CHATROOM WHERE USERID = 10001");
        }

        public bool SendMessage(string message, int ownerID)
        {
            string query = "INSERT INTO MESSAGES VALUE (MESSAGES_SEQ.NEXTVAL, " + ownerID + ", " + ChatroomID + ", " + message + ", CURRENT_TIMESTAMP)";
            Database.ExecuteQuery(query);
            return false;
        }
    }
}

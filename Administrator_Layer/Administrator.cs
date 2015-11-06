using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Class_Layer;

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
    }
}

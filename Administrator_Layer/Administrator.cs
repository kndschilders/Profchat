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

            List<Gebruiker> list = data.HaalGebruikersOp();

            if (list != null)
                foreach (Gebruiker g in list)
                    Vrijwilligers.Add(g);
        }
    }
}

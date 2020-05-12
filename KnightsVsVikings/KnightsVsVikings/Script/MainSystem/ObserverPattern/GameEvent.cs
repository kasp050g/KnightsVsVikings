using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSystemFramework
{
    public class GameEvent
    {
        private List<IGameListner> listners = new List<IGameListner>();

        public string Title { get; private set; }

        public GameEvent(string title)
        {
            this.Title = title;
        }

        public void Attach(IGameListner listner)
        {
            listners.Add(listner);
        }

        public void Detach(IGameListner listner)
        {
            listners.Remove(listner);
        }

        public void Notify(Component other)
        {
            foreach (IGameListner listner in listners)
            {
                listner.Notify(this, other);
            }
        }
    }
}

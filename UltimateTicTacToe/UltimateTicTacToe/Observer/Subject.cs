using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateTicTacToe.Observer
{
    public class Subject
    {
        List<Subscriber> subscribers = null;

        public Subject()
        {
            subscribers = new List<Subscriber>();
        }

        public void AddSubscriber(Subscriber newSubscriber)
        {
            subscribers.Add(newSubscriber);
        }

        public void RemoveSubscriber(Subscriber subscriber)
        {
            subscribers.Remove(subscriber);
        }

        public void UpdateAll()
        {
            foreach (Subscriber subscriber in subscribers)
            {
                subscriber.Update();
            }
        }

        public void UpdateAllMoves()
        {
            foreach (Subscriber subscriber in subscribers)
            {
                subscriber.UpdateAvailable();
            }
        }

        public void SetSubBackground(Color color)
        {
            foreach(Subscriber subscriber in subscribers)
            {
                subscriber.setBackground(color);
            }
        }
    }
}

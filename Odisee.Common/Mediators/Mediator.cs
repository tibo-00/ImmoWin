using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odisee.Common.Mediators
{

    public class Mediator
    {
        private static Mediator _instance = null;

        private Dictionary<String, List<WeakReference>> list = new Dictionary<string, List<WeakReference>>();

        /// <summary>
        /// Use GetInstance to get the mediator object.
        /// Mediator constructor is private so no other objects can be created.
        /// 
        /// </summary>
        private Mediator()
        { }

        public static Mediator GetInstance()
        {
            if (_instance == null)
                _instance = new Mediator();
            return _instance;
        }

        /// <summary>
        /// Register a subscriber for listening to a message
        /// </summary>
        /// <param name="subscriber"></param>
        /// <param name="message"></param>

        public void Register(IMediatorSubscriber subscriber, String message)
        {
            if (!list.ContainsKey(message))
                list[message] = new List<WeakReference>();
            list[message].Add(new WeakReference(subscriber));
        }

        /// <summary>
        /// Unregister a subscriber for a/all messages
        /// </summary>
        /// <param name="subscriber"></param>
        /// <param name="message"></param>
        public void Unregister(IMediatorSubscriber subscriber, String message = null)
        {
            foreach (String key in list.Keys)
            {
                if (message == null || key == message)
                {
                    for (int i = list[key].Count - 1; i >= 0; --i)
                    {
                        if (Object.Equals(list[key][i].Target, subscriber))
                        {
                            list[key].RemoveAt(i);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Send notification message to all subscribers
        /// </summary>
        /// <param name="from"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        public void Notify(WeakReference from, String message, Object data)
        {
            if (list.ContainsKey(message))
                foreach (WeakReference reference in list[message])
                    if (reference.IsAlive)
                    {
                        ((IMediatorSubscriber)reference.Target).Notify(from, message, data);
                    }
        }
        public void Notify(Object from, String message, Object data)
        {
            if (list.ContainsKey(message))
                foreach (WeakReference reference in list[message])
                    if (reference.IsAlive)
                    {
                        ((IMediatorSubscriber)reference.Target).Notify(from, message, data);
                    }
        }
    }
}

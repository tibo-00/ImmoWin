using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odisee.Common.Mediators
{
    public class SimpleMediator
    {
		private static SimpleMediator _instance;

		public static SimpleMediator Instance
		{
			get 
            {
                if (_instance == null)
                {
                    _instance = new SimpleMediator();
                }
                return _instance; 
            }
		}


        private Dictionary<string, List<IMediatorSubscriber>> list = new Dictionary<string, List<IMediatorSubscriber>> ();
        private SimpleMediator()
        {}

        public void Register(String message, IMediatorSubscriber subscriber)
        {
            if(!list.ContainsKey(message)) 
            {
                list[message] = new List<IMediatorSubscriber> ();
            }
            list[message].Add(subscriber);
        }

        public void Notify(IMediatorSubscriber from, String message, Object data) 
        {
            if (list.ContainsKey(message))
            {
                foreach (IMediatorSubscriber subscriber in list[message])
                {
                    subscriber.Notify(from, message, data);
                }
            }
        }

    }
}

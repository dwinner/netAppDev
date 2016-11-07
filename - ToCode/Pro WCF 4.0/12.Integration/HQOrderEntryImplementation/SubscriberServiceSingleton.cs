using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HQOrderEntryImplementation
{
    public class SubscriberServiceSingleton
    {
        private static SubscribeService instance;
        private static readonly object singletonLock = new object();

        
        public static SubscribeService GetInstance()
        {
            if (instance == null)
            {
                lock (singletonLock)
                {
                    if (instance == null)
                    {
                        instance = new SubscribeService();
                    }
                }
            }
            return instance;
        }
    }
}

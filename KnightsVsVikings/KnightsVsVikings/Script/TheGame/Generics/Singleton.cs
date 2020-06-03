using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KnightsVsVikings.Script.TheGame.Generics
{
    public class Singleton<T> where T : class
    {
        private static T instance = null;
        private static readonly object threadLock = new object();
        public static T Instance
        {
            get
            {
                lock (threadLock)
                {
                    if (instance == null)
                        instance = (T)Activator.CreateInstance(typeof(T));

                    return instance;
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheProviderCore
{
    public abstract class CacheProvider
    {
        public static CacheProvider _instance;
        public static CacheProvider Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RedisCache();
                }
                return _instance;
            }
            set { _instance = value; }
        }

        public abstract T Get<T>(string keyName);
        public abstract T Get<T>(string keyName, Func<T> queryFunction);
        public abstract void Set(string keyName, object data);
        public abstract void Delete(string keyName);
    }
}

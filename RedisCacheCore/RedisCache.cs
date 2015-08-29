using StackExchange.Redis;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CacheProviderCore
{
    //TODO: Need to define an interface that is generic (not redis)
    //TODO: need unit tests for cache object and unit tests with fake cache object
    public class RedisCache : CacheProvider, IDisposable
    {
		private static string connection = "localhost:6379";
        private static ConnectionMultiplexer redis;
        private static IDatabase db;
        private int _cacheTimeInMinutes = 30;
        public int CacheTimeInMinutes
        {
            get
            {
                return _cacheTimeInMinutes;
            }
            set
            {
                _cacheTimeInMinutes = value;
            }
        }

        public RedisCache()
        {
            redis = ConnectionMultiplexer.Connect(connection);
            db = redis.GetDatabase();
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (redis != null)
                {
                    redis.Close();
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        ~RedisCache()
        {
            Dispose(false);
        }

        public override T Get<T>(string keyName)
        {
            byte[] data = db.StringGet(keyName);

            return Deserialize<T>(data);
        }

        public override T Get<T>(string keyName, Func<T> queryFunction)
        {
            byte[] data = null;

            if (redis != null)
            {
                data = db.StringGet(keyName);
            }

            if (data == null)
            {
                var result = queryFunction();

                if (redis != null)
                {
                    db.StringSet(keyName, Serialize(result));
                }

                return result;
            }

            return Deserialize<T>(data);
        }

        public override void Set(string keyName, object data)
        {
            if (redis != null)
            {
                db.StringSet(keyName, Serialize(data), new TimeSpan(0, CacheTimeInMinutes, 0));
            }
        }

        public override void Delete(string keyName)
        {
            if (redis != null)
            {
                db.KeyDelete(keyName);
            }
        }

        private static byte[] Serialize(object o)
        {
            if (o == null)
            {
                return null;
            }

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                binaryFormatter.Serialize(memoryStream, o);
                byte[] objectDataAsStream = memoryStream.ToArray();
                return objectDataAsStream;
            }
        }

        private static T Deserialize<T>(byte[] stream)
        {
            if (stream == null)
            {
                return default(T);
            }

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream(stream))
            {
                T result = (T)binaryFormatter.Deserialize(memoryStream);
                return result;
            }
        }
    }
}

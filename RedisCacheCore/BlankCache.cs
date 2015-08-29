using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CacheProviderCore
{
    public class BlankCache : CacheProvider
    {
        // This is a fake caching system used to fake out unit tests
        private Dictionary<string, byte[]> _localStore = new Dictionary<string, byte[]>();

        public override T Get<T>(string keyName)
        {
            return Deserialize<T>(_localStore[keyName]);
        }

        public override T Get<T>(string keyName, Func<T> queryFunction)
        {
            if (_localStore.ContainsKey(keyName))
            {
                return Deserialize<T>(_localStore[keyName]);
            }
            else
            {
                var result = queryFunction();
                _localStore[keyName] = Serialize(result);
                return result;
            }
        }

        public override void Set(string keyName, object data)
        {
            _localStore[keyName] = Serialize(data);
        }

        public override void Delete(string keyName)
        {
            _localStore.Remove(keyName);
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

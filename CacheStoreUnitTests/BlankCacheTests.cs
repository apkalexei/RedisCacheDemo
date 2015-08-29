using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CacheProviderCore;

namespace CacheStoreUnitTests
{
    [TestClass]
    public class BlankCacheTests
    {
        [TestMethod]
        public void GenericFunction()
        {
            CacheProvider.Instance = new BlankCache();

            var data = CacheProvider.Instance.Get("TestItem", () => {
                return "test data";
            });

            Assert.AreEqual("test data", data);
        }

        [TestMethod]
        public void SaveString()
        {
            CacheProvider.Instance = new BlankCache();

            CacheProvider.Instance.Set("TestItem", "my string");
            var data = CacheProvider.Instance.Get<string>("TestItem");

            Assert.AreEqual("my string", data);
        }

        [TestMethod]
        public void SaveInt()
        {
            CacheProvider.Instance = new BlankCache();

            CacheProvider.Instance.Set("TestItem", 5);
            var data = CacheProvider.Instance.Get<int>("TestItem");

            Assert.AreEqual(5, data);
        }

        [TestMethod]
        public void SaveClassData()
        {
            var testObject = new UnitTestObject
                {
                    Number = 5,
                    Name = "my name",
                    DecimalNumber = 3.14159
                };
            CacheProvider.Instance.Set("TestItem", testObject);
            var data = CacheProvider.Instance.Get<UnitTestObject>("TestItem");

            Assert.AreEqual(5, data.Number);
            Assert.AreEqual("my name", data.Name);
            Assert.AreEqual(3.14159, data.DecimalNumber);
        }
    }

    [Serializable]
    public class UnitTestObject
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public double DecimalNumber { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using CacheProviderCore;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TestRedis.DAL;
using System.Diagnostics;

namespace TestRedis
{
    class Program
    {
        private static int iternations = 1000;

        static void Main(string[] args)
        {
            //GenericTest();
            //TestObject();
            ReadDatabaseTest();
            ReadRedisTest();

            Console.ReadKey();
        }

        private static void ReadDatabaseTest()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            for (int i = 0; i < iternations; i++)
            {
                using (var db = new SampleDataContext())
                {
					List<Room> query = (from r in db.Rooms select r).ToList();
                }
            }

            stopWatch.Stop();
            Console.WriteLine("SQL Elapsed:" + stopWatch.Elapsed);
        }

        private static void ReadRedisTest()
        {
            CacheProvider.Instance.Delete("SavedQuery");

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            for (int i = 0; i < iternations; i++)
            {
                // generic
                var tempData = CacheProvider.Instance.Get("SavedQuery", () =>
                {
                    using (var db = new SampleDataContext())
                    {
						return (from r in db.Rooms select r).ToList();
                    }
                });
            }

            stopWatch.Stop();
            Console.WriteLine("Redis Generic Elapsed:" + stopWatch.Elapsed);
            CacheProvider.Instance.Delete("SavedQuery2");
            stopWatch.Start();

            // strict
            for (int i = 0; i < iternations; i++)
            {
				List<Room> tempData = CacheProvider.Instance.Get<List<Room>>("SavedQuery2", () =>
                {
                    using (var db = new SampleDataContext())
                    {
						return (from r in db.Rooms select r).ToList();
                    }
                });
            }

            stopWatch.Stop();
            Console.WriteLine("Redis Strict Elapsed:" + stopWatch.Elapsed);
        }

        // test storing an object
        private static void TestObject()
        {
            #region write/read from various data types
            var desk = new Desk
            {
                Owner = "Me",
                Width = 2,
                Height = 4,
                Drawers = 3,
            };

            CacheProvider.Instance.Set("myDesk", desk);
            CacheProvider.Instance.Set("TestString", "some test data");
            CacheProvider.Instance.Set("TestDouble", 3.456);

            var returnvalue = CacheProvider.Instance.Get<Desk>("myDesk");
            var returnstring = CacheProvider.Instance.Get<string>("TestString");
            var returndouble = CacheProvider.Instance.Get<double>("TestDouble");

            Console.WriteLine(returnvalue.Owner + ", " + returnvalue.Width + ", " + returnvalue.Height + ", " + returnvalue.Drawers + ", string=" + returnstring + ", double=" + returndouble);
            #endregion

            #region database caching operation
            // attempt to read from cache, if not avail, read from data and insert into cache.
            // use generic return type
            var tempData = CacheProvider.Instance.Get("SavedQuery2", () =>
            {
                using (var db = new SampleDataContext())
                {
                    return (from r in db.Rooms select r).ToList();
                }
            });

            foreach (var item in tempData)
            {
                Console.WriteLine(item.Name + ", " + item.Name);
            }
            #endregion
        }

        private static void GenericTest()
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379");
            IDatabase db = redis.GetDatabase();

            string value = "abcd";
            db.StringSet("key1", value);

            string returnvalue = db.StringGet("key1");

            Console.WriteLine(returnvalue);
            Console.ReadKey();
        }

		// server:
		// https://github.com/MSOpenTech/redis
		// https://github.com/MSOpenTech/redis/releases

		// client:
		// https://github.com/StackExchange/StackExchange.Redis
    }
}

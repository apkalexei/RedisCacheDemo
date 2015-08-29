using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRedis
{
    [Serializable]
    public class Desk
    {
        public int Drawers { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Owner { get; set; }
    }
}

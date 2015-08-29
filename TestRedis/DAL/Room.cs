using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TestRedis.DAL
{
    [Table("room")]
    [Serializable]
    public class Room
    {
        [Key]
        public int id { get; set; }
        public string Name { get; set; }
        public int RoomNumber { get; set; }
    }
}

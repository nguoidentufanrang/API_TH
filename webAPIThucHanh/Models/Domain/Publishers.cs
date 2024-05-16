using System.ComponentModel.DataAnnotations;
using webAPIThucHanh.Models;

namespace webAPIThucHanh.Models.Domain
{
    public class Publishers
    {
        [Key]
        public int ID { get; set; }
        public string? Name { get; set; }
        public List<Books>? Book { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace webAPIThucHanh.Models.Domain
{
    public class Authors
    {
        [Key]
        public int? ID { get; set; }
        public string? FullName { get; set; }
        public List<Book_Author>? Book_Authors { get; set; }
    }
}

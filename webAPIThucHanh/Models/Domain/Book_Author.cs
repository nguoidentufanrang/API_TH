using webAPIThucHanh.Models;

namespace webAPIThucHanh.Models.Domain
{
    public class Book_Author
    {
        public int ID { get; set; }
        public int? BookId { get; set; }
        public int AuthorId { get; set; }
        public Books? Book { get; set; }
        public Authors? Author { get; set; }
    }
}

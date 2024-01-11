using Ab_pk_task_AuthorController.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ab_pk_task3.Entities
{
    public class Book
    {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title{ get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; } 
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD.Models.Entities
{
    public class Note
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Data { get; set; }
        public int? CreationDateId { get; set; }
        public CreationDate? CreationDate { get; set; }
    }
}

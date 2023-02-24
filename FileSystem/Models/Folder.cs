using System.ComponentModel.DataAnnotations;

namespace FileSystem.Models
{
    public class Folder
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Parent_Id { get; set; }
    }
}

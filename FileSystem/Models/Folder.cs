namespace FileSystem.Models
{
    public class Folder
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Parent_Id { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Orm.Movies.DataModels
{
    public class Movie
    {
        [Key]
        [Required]
        public Guid? Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public int? Year { get; set; }
    }
}

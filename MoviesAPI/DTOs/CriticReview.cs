using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MoviesAPI.DTOs
{
    public class CriticReview
    {
        public int Id { get; set; } 
        public int MovieId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Movie Movie { get; set; } = null!;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Rating { get; set; }
        public Boolean IsPublished { get; set; }
        public User User { get; set; } = null!;
        //public string CreatedBy { get; set; } = string.Empty;
        public DateOnly CreatedDate { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MoviesAPI.DTOs
{
    public class Movie
    {
        public int Id { get; set; }                                    
        public string Title { get; set; } = string.Empty;
        //public int GenreId { get; set; }
        public Genre Genre { get; set; } = null!;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<CriticReview> CriticReviews { get; set; } = null!;

        public string Synopsis { get; set; } = string.Empty;
        //public int AudienceRatingId { get; set; }
        public AudienceRating AudienceRating { get; set; } = null!;
        public string ImageUrl { get; set; } = string.Empty;
        public int Runtime { get; set; } 
        public DateOnly ReleaseDate { get; set; } 
    }
}

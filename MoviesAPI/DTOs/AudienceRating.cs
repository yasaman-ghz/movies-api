using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MoviesAPI.DTOs
{
    public class AudienceRating
    {
        //public int Id { get; set; } 

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<Movie> Movies { get; set; } = null!; 

        public string Title { get; set; } = string.Empty;
    }
}

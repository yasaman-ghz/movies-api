using MoviesAPI.DTOs;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Data;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IDbConnection _db;

        public MoviesController(IDbConnection db)
        {
            _db = db;
        }

        // GET /api/movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetAllMovies()
        {
            var sql = @"
                SELECT m.*, g.*, ar.*
                    
                FROM Movie m
                INNER JOIN Genre g ON m.GenreId = g.Id
                INNER JOIN AudienceRating ar ON m.AudienceRatingId = ar.Id";

            var movies = await _db.QueryAsync<Movie, Genre, AudienceRating, Movie>(
                sql,
                (movie, genre, audienceRating) =>
                {
                    movie.Genre = genre;
                    movie.AudienceRating = audienceRating;
                    return movie;
                },
                splitOn: "Id");
            return Ok(movies);
        }


        // GET /api/movies/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovieById(int id)

        {
            var sql = @"SELECT m.*, g.*, ar.*, cr.*, u.*
                FROM Movie m
                INNER JOIN Genre g ON m.GenreId = g.Id
                INNER JOIN AudienceRating ar ON m.AudienceRatingId = ar.Id
                LEFT OUTER JOIN CriticReview cr ON m.Id = cr.MovieId
                LEFT OUTER JOIN UserSnapshot u ON cr.CreatedBy = u.UserId
                WHERE m.Id = @Id";

            Movie? movie = null;
            await _db.QueryAsync<Movie, Genre, AudienceRating, CriticReview, User, Movie>(
                sql,
                (m, g, ar, cr, u) =>
                {
                    if (movie == null)
                    {
                        movie = m;
                        movie.Genre = g;
                        movie.AudienceRating = ar;
                        movie.CriticReviews = new List<CriticReview>();
                    }
                    if (cr != null && u != null && cr.IsPublished == true)
                    {
                        cr.User = u;
                        movie.CriticReviews.Add(cr);
                    }
                    return movie;
                },
                new { Id = id },
                splitOn: "Id,Id,Id,UserId");

            return Ok(movie);
        }
    }
}



using DataAccess;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace multitracksAPI.Controllers
{
    [ApiController]
    [Route("artist")]
    public class ArtistsController : ControllerBase
    {
        private readonly SQL _sql;
        
        public ArtistsController(SQL sql)
        {
            _sql = sql;
        }

        /// <summary>
        /// Endpoint to search for an artist by name.
        /// </summary>
        /// <param name="search"></param>
        /// <returns>A list of Artists that contains the searched value in they name</returns>
        [HttpGet]
        [Route("search")]
        [ProducesResponseType((typeof(List<ArtistModel>)),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult SearchByName([FromQuery] string search)
        {
            try
            {
                _sql.Parameters.Clear();
                _sql.Parameters.Add("@search", search);
                var dataSet = _sql.ExecuteDS("SELECT TOP 100 * FROM Artist WHERE title LIKE '%' + @search + '%' ");
                
                List<ArtistModel> artists = JsonConvert.DeserializeObject<List<ArtistModel>>(
                    JsonConvert.SerializeObject(dataSet.Tables[0].ToDynamic()));

                if(artists.Count == 0)
                {
                    return NotFound();
                }

                return Ok(artists);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Endpoint to add an Artist to the Artist table.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Add([FromBody] ArtistPostRequest request)
        {
            try
            {
                _sql.Parameters.Clear();
                _sql.Parameters.Add("@title", request.Title);
                _sql.Parameters.Add("@heroURL", request.HeroURL);
                _sql.Parameters.Add("@imageURL", request.ImageURL);
                _sql.Parameters.Add("@biography", request.Biography);
                _sql.Parameters.Add("@dateCreation", DateTime.Now);

                var result = _sql.Execute(
                    "INSERT INTO Artist (title, heroURL, imageURL, dateCreation, biography) " +
                    "VALUES (@title, @heroURL, @imageURL, @dateCreation, @biography)");

                return Created(nameof(SearchByName), result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

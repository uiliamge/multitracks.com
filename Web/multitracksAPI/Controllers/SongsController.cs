using DataAccess;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace multitracksAPI.Controllers
{
    [ApiController]
    [Route("song")]
    public class SongsController : ControllerBase
    {
        private readonly SQL _sql;
        
        public SongsController(SQL sql)
        {
            _sql = sql;
        }

        /// <summary>
        /// Endpoint to list all songs with paging support using page size, page number, etc.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        [ProducesResponseType((typeof(SongListResponseRoot)),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult List([FromQuery] PaginationParameters parameters)
        {
            try
            {
                var response = new SongListResponseRoot { Headers = new List<KeyValuePair<string, string>>() };

                _sql.Parameters.Clear();
                _sql.Parameters.Add("@SEARCH_TEXT", parameters.Search);
                _sql.Parameters.Add("@SORT_COLUMN_NAME", parameters.SortColumn);
                _sql.Parameters.Add("@SORT_COLUMN_DIRECTION", parameters.SortDirection);
                _sql.Parameters.Add("@START_INDEX", parameters.Page);
                _sql.Parameters.Add("@PAGE_SIZE", parameters.ItemsPerPage);

                var dataTable = _sql.ExecuteStoredProcedureDT("GetListSongs");

                var paginationMetadata = new PaginationMetadata(
                    dataTable.Rows.Count,
                    parameters.Page,
                    parameters.ItemsPerPage,
                    parameters.SortColumn,
                    parameters.SortDirection);

                response.Headers.Add(new KeyValuePair<string, string>("X-Pagination", System.Text.Json.JsonSerializer.Serialize(paginationMetadata)));


                var list = JsonConvert.DeserializeObject<List<SongListResponse>>(JsonConvert.SerializeObject(dataTable.ToDynamic()));

                response.Items = list;

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
    }
}

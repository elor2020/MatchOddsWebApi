using MatchOddsWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MatchOddsWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchOddsController : ControllerBase
    {
        private readonly IMatchOddRepository _matchOddRepository;
        public MatchOddsController(IMatchOddRepository matchOddRepository)

        {
            _matchOddRepository = matchOddRepository;
        }

        [HttpGet]
        [Route("GetAllMatchOdds")]
        public async Task<ActionResult<IEnumerable<MatchOdd>>> GetAllMatchOdds()
        {
            try
            {
                return Ok(await _matchOddRepository.GetAllMatchOdds());

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
        [HttpGet]
        [Route("GetOddForMatch/{MatchId:int}")]
        public async Task<ActionResult<MatchOdd>> GetOddForMatch(int MatchId)
        {
            try
            {
                var match = await _matchOddRepository.GetOddForMatch(MatchId);
                if (match == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(match);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        [Route("GetMatchOddById/{MatchOddId:int}")]
        public async Task<ActionResult<MatchOdd>> GetMatchOddById(int MatchOddId)
        {
            try
            {
                var match = await _matchOddRepository.GetMatchOdd(MatchOddId);
                if (match == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(match);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetOddBySpecifier/{specifier}")]
        public async Task<ActionResult<MatchOdd>> GetOddBySpecifier(string specifier)
        {
            try
            {
                var match = await _matchOddRepository.GetOddBySpecifier(specifier);
                if (match == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(match);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        [Route("CreateMatchOdd")]
        public async Task<ActionResult> CreateMatchOdd(MatchOdd matchOdd)
        {
            try
            {
                if (matchOdd == null)
                    return BadRequest(ModelState);

                var matchOddToAdd = await _matchOddRepository.GetMatchOdd(matchOdd.Id);
                if (matchOddToAdd != null)
                    return BadRequest(ModelState);
                var matchAddition = await _matchOddRepository.AddMatchOdd(matchOdd);
                return Ok(matchAddition);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut]
        [Route("UpdateMatchOdd")]
        public async Task<ActionResult> UpdateMatchOdd(MatchOdd matchOdd)
        {
            try
            {
                var matchOddToUpdate = await _matchOddRepository.GetMatchOdd(matchOdd.Id);
                if (matchOddToUpdate == null)
                {
                    return BadRequest();
                }
                else
                {
                    await _matchOddRepository.UpdateMatchOdd(matchOdd);
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete]
        [Route("DeleteMatchOdd/{id:int}")]
        public async Task<ActionResult> DeleteMatchOdd(int id)
        {
            try
            {
                var matchOddToDelete = await _matchOddRepository.GetMatchOdd(id);
                if (matchOddToDelete == null)
                {
                    return NotFound();
                }
                else
                {
                    await _matchOddRepository.RemoveMatchOdd(id);
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
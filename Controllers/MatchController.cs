using MatchOddsWebApi.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Linq;

namespace MatchOddsWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class MatchController : ControllerBase
    {
        private readonly IMatchRepository _matchRepository;
        public MatchController(IMatchRepository matchRepository)

        {
            _matchRepository = matchRepository;
        }

        [HttpGet]
        [Route("GetAllMatches")]
        public async Task<ActionResult<IEnumerable<Match>>> GetMatches()
        {
            try
            {
                return Ok(await _matchRepository.GetAllMatches());

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
        [HttpGet]
        [Route("GetMatchesbySport/{sport:int}")]
       
        public async Task<ActionResult<IEnumerable<Match>>> GetMatchesbySport(int sport)
        {
            try
            {
                if(sport!=0) 
                    return Ok(await _matchRepository.GetMatchesBySport(sport));
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetMatchesbyTeam/{team}")]
        public async Task<ActionResult<IEnumerable<Match>>> GetMatchesbyTeam(string team)
        {
            try
            {
                if (!team.IsNullOrEmpty())
                    return Ok(await _matchRepository.GetMatchByTeamName(team));
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        [Route("GetMatchByDate/{date}")]
        public async Task<ActionResult<IEnumerable<Match>>> GetMatchesbyDate(string date)
        {
            try
            {
                if (!date.IsNullOrEmpty()) 
                { 
                    var resp = await _matchRepository.GetMatchesByDate(date);
                    
                    if (resp != null)
                        return Ok();
                    else
                        return BadRequest();
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetMatch/{id:int}")]
        public async Task<ActionResult<Match>> GetMatch(int id)
        {
            try
            {
                var match = await _matchRepository.GetMatch(id);
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
        [Route("CreateMatch")]
        public async Task<ActionResult> CreateMatch(Match match)
        {
            try
            {
                if (match == null)
                    return BadRequest(ModelState);

                var matchToAdd = await _matchRepository.GetMatch(match.Id);
                if (matchToAdd != null)
                    return BadRequest(ModelState);
                var matchAddition = await _matchRepository.AddMatch(match);
                return Ok(matchAddition);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut]
        [Route("UpdateMatch")]
        public async Task<ActionResult> UpdateMatch(Match match)
        {
            try
            {
                var matchToUpdate = await _matchRepository.GetMatch(match.Id);
                if (matchToUpdate == null)
                {
                    return BadRequest();
                }
                else
                {
                    await _matchRepository.UpdateMatch(match);
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete]
        [Route("DeleteMatch/{id:int}")]
        public async Task<ActionResult> DeleteMatch(int id)
        {
            try
            {
                var matchToDelete = await _matchRepository.GetMatch(id);
                if (matchToDelete == null)
                {
                    return NotFound();
                }
                else
                {
                    await _matchRepository.RemoveMatch(id);
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

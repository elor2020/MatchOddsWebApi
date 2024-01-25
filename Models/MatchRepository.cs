using MatchOddsWebApi.Persistance;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Composition;

namespace MatchOddsWebApi.Models
{
    public class MatchRepository : IMatchRepository
    {
        private readonly DataContext _dataContext;
        private readonly Serilog.ILogger _logger = Log.ForContext<MatchRepository>();

        public MatchRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<Match> GetMatch(int id)
        {
            try
            {
                _logger.Information($"Retrieving match with id: {id} from database");
                return await _dataContext.Matches.FirstOrDefaultAsync(m => m.Id == id);
            }
            catch (Exception ex)
            {

                _logger.Error(ex.Message, ex);
                return null;
            }
        }

        public async Task<IEnumerable<Match>> GetAllMatches()
        {
            try
            {
                _logger.Information("Retrieving matches from database");
                return await _dataContext.Matches.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.Error(ex.Message, ex);
                return null;
            }
        }
        public async Task<Match> AddMatch(Match match)
        {
            try
            {
                var response = await _dataContext.Matches.AddAsync(match);
                await _dataContext.SaveChangesAsync();
                _logger.Information("Match added successfully in database");

                return response.Entity;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return null;
            }

        }

        public async Task UpdateMatch(Match match)
        {
            try
            {
                var data = await _dataContext.Matches.FirstOrDefaultAsync(m => m.Id == match.Id);
                if (data != null)
                {
                    data.Sport = match.Sport;
                    data.MatchDate = match.MatchDate;
                    data.MatchTime = match.MatchTime;
                    data.TeamA = match.TeamA;
                    data.TeamB = match.TeamB;
                    data.Description = match.Description;
                    if (match.Id != 0)
                        data.Id = match.Id;

                    _dataContext.Matches.Update(data);
                    await _dataContext.SaveChangesAsync();
                    _logger.Information("Match updated successfully in database");
                }

            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
            }

        }

        public async Task RemoveMatch(int id)
        {
            try
            {
                var match = await _dataContext.Matches.FirstOrDefaultAsync(m => m.Id == id);
                if (match != null)
                {
                    _dataContext.Matches.Remove(match);
                    _dataContext.SaveChangesAsync();
                    _logger.Information("Match removed succesfully from database");
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public async Task<IEnumerable<Match>> GetMatchesBySport(int sport)
        {
            _logger.Information($"Retrieving all matches for sport: {sport} from database");
            return await _dataContext.Matches.Where(m => (int)m.Sport == sport).ToListAsync();
        }

        public async Task<IEnumerable<Match>> GetMatchByTeamName(string team)
        {
            try
            {
                _logger.Information($"Retrieving all matches for team: {team} from database");
                return await _dataContext.Matches.Where(m => (m.TeamA == team) || m.TeamB == team).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return null;
            }
        }

        public async Task<IEnumerable<Match>> GetMatchesByDate(string date)
        {
            try
            {
            // convert string date to DateOnly in order to produce valid date format for linq comparison
               var convertDate = DateOnly.TryParse(date,out var dateToSearch);

                if (!convertDate)
                    throw new Exception("Invalid Date!");

                _logger.Information($"Retrieving all matches for date: {dateToSearch} from database");
                return await _dataContext.Matches.Where(m => m.MatchDate== dateToSearch.ToString("dd/MM/yyyy")).ToListAsync();
           
            }catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return null;
            }
        }
    }
}

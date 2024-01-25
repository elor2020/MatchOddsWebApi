using MatchOddsWebApi.Persistance;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace MatchOddsWebApi.Models
{
    public class MatchOddRepository : IMatchOddRepository
    {
        private readonly DataContext _dataContext;
        private readonly Serilog.ILogger _logger = Log.ForContext<MatchOddRepository>();

        public MatchOddRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<IEnumerable<MatchOdd>> GetAllMatchOdds()
        {
            try
            {
                _logger.Information($"Retrieving all matches' odds from database");
                return await _dataContext.MatchOdds.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return null;
            }
        }

        public async Task<MatchOdd> GetOddForMatch(int mathchId)
        {
            try
            {
                _logger.Information($"Retrieving match odd for mathchId: {mathchId} from database");
                return await _dataContext.MatchOdds.FirstOrDefaultAsync(o => o.MatchId == mathchId);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return null;
            }
        }
        public async Task<MatchOdd> GetMatchOdd(int matchOddId)
        {
            try
            {
                _logger.Information($"Retrieving match odd for mathcOddId: {matchOddId} from database");
                return await _dataContext.MatchOdds.FirstOrDefaultAsync(o => o.Id == matchOddId);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return null;
            }
        }
        public async Task<MatchOdd> AddMatchOdd(MatchOdd matchOdd)
        {
            try
            {
                var response = await _dataContext.MatchOdds.AddAsync(matchOdd);
                await _dataContext.SaveChangesAsync();
                _logger.Information("Match Odd added successfully in database");

                return response.Entity;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return null;
            }
        }
        public async Task UpdateMatchOdd(MatchOdd odd)
        {
            try
            {
                var data = await _dataContext.MatchOdds.FirstOrDefaultAsync(o => o.Id == odd.Id);
                if (data != null)
                {
                    data.MatchId = odd.Id;
                    data.Odd = odd.Odd;
                    data.Specifier = odd.Specifier;
                    if (odd.Id != 0)
                        data.Id = odd.Id;

                    _dataContext.MatchOdds.Update(data);
                    await _dataContext.SaveChangesAsync();
                    _logger.Information("Match Odd updated successfully in database");
                }
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
            }
        }
        public async Task RemoveMatchOdd(int id)
        {
            try
            {
                var odd = await _dataContext.MatchOdds.FirstOrDefaultAsync(o => o.Id == id);
                if (odd != null)
                {
                    _dataContext.MatchOdds.Remove(odd);
                    _dataContext.SaveChangesAsync();
                    _logger.Information("Match Odd removed succesfully from database");
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public async Task<IEnumerable<MatchOdd>> GetOddBySpecifier(string specifier)
        {
            try
            {
                if (specifier == null)
                    return null;
                _logger.Information($"Retrieving all match odds for specifier: {specifier} from database");
                return await _dataContext.MatchOdds.Where(o => o.Specifier.Equals(specifier)).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return null;
            }
        }
    }
}

namespace MatchOddsWebApi.Models
{
    public interface IMatchOddRepository
    {
        Task<MatchOdd> GetMatchOdd(int matchid);
        Task<IEnumerable<MatchOdd>> GetOddBySpecifier(string specifier);
        Task<IEnumerable<MatchOdd>> GetAllMatchOdds();
        Task<MatchOdd> AddMatchOdd(MatchOdd match);
        Task UpdateMatchOdd(MatchOdd match);
        Task RemoveMatchOdd(int id);
        Task<MatchOdd> GetOddForMatch(int matchId);
    }
}

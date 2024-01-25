namespace MatchOddsWebApi.Models
{
    public interface IMatchRepository
    {
        Task<Match> GetMatch(int id);
        Task<IEnumerable<Match>> GetMatchByTeamName(string team);
        Task<IEnumerable<Match>> GetMatchesBySport(int sport);
        Task <IEnumerable<Match>> GetAllMatches();
        Task <IEnumerable<Match>> GetMatchesByDate(string date);
        Task<Match> AddMatch(Match match);
        Task UpdateMatch(Match match);
        Task RemoveMatch(int id);
    }
}

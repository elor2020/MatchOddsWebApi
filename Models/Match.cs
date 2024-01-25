using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MatchOddsWebApi.Models;

public partial class Match
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public string? MatchDate { get; set; }

    public string? MatchTime { get; set; }

    public string? TeamA { get; set; }

    public string? TeamB { get; set; }

    public Sports? Sport { get; set; }

    [JsonIgnore]
    public virtual ICollection<MatchOdd> MatchOdds { get; set; } = new List<MatchOdd>();
}
public enum Sports
{
    Football = 1,
    Basketball = 2
}

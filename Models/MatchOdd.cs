using MatchOddsWebApi.Models;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MatchOddsWebApi;

public partial class MatchOdd
{
    public int Id { get; set; }

    public int? MatchId { get; set; }

    public string? Specifier { get; set; }

    public double? Odd { get; set; }
   
    [JsonIgnore]
    public virtual Match? Match { get; set; }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace TelegaBotConsole.Infrastructure.Models
{
    public class SquadModel
    {
        public List<string> StartingEleven { get; set; }
        public List<string> Subs { get; set; }

        public string ToHtmlString()
        {
            if (StartingEleven is null || !StartingEleven.Any())
                return "<strong>no squad info for the moment, sorry!</strong>";

            return Subs != null && Subs.Any()
                ? $"{string.Join(Environment.NewLine, StartingEleven.Select(x => $"<b>{x}</b>"))}{Environment.NewLine}{Environment.NewLine}Substitutes{Environment.NewLine}{Environment.NewLine}{string.Join(Environment.NewLine, Subs.Select(x=>$"<i>{x}</i>"))}"
                : string.Join(Environment.NewLine, StartingEleven.Select(x => $"<b>{x}</b>"));
        }

        public override string ToString()
        {
            if (StartingEleven is null || !StartingEleven.Any())
                return "no squad info for the moment, sorry!";

            return Subs != null && Subs.Any()
                ? $"{string.Join(Environment.NewLine, StartingEleven)}{Environment.NewLine}Substitutes{Environment.NewLine}{string.Join(Environment.NewLine, Subs)}"
                : string.Join(Environment.NewLine, StartingEleven);
        }
    }
}
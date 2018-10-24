using System.Collections.Generic;

namespace TournamentLib
{
    public class Round
    {
        private List<Match> matches = new List<Match>();
        
        public void AddMatch(Match m)
        {
            matches.Add(m);
        }

        public Match GetMatch(string teamName1, string teamName2)
        {
            // TODO: Implement this method
            return null;
        }

        public bool IsMatchesFinished()
        {
            foreach (var item in matches)
            {
                if (item.Winner == null)
                {
                    return false;
                }
            }
            return true;
        }

        public List<Team> GetWinningTeams()
        {
            List<Team> winningTeams = new List<Team>();
            for (int i = 0; i < matches.Count; i++)
            {
                winningTeams.Add(matches[i].Winner);
            }
            return winningTeams;
        }

        public List<Team> GetLosingTeams()
        {
            List<Team> losingTeams = new List<Team>();
            foreach (var item in matches)
            {
                if (item.Winner == item.FirstOpponent)
                {
                    losingTeams.Add(item.SecondOpponent);
                }
                else
                {
                    losingTeams.Add(item.FirstOpponent);
                }
            }
            return losingTeams;
        }
    }
}

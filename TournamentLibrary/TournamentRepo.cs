using System.Collections.Generic;

namespace TournamentLib
{
    public class TournamentRepo
    {
        List<Tournament> TournamentList = new List<Tournament>();
        private Tournament winterTournament = new Tournament("Vinter Turnering");

        public Tournament GetTournament(string name)
        {
            if (name == "Vinter Turnering")
            {
                return winterTournament;
            }
            return null;
        }

        public void CreateTournament(string name)
        {
            Tournament tournament = new Tournament(name);
            TournamentList.Add(tournament);
            
        }
    }
}
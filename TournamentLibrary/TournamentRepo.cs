using System.Collections.Generic;

namespace TournamentLib
{
    public class TournamentRepo
    {
        private List<Tournament> TournamentList = new List<Tournament>();

        public TournamentRepo()
        {
            TournamentList.Add(new Tournament("Vinter Turnering"));
        }

        public Tournament GetTournament(string name)
        {
            foreach(Tournament tournament in TournamentList)
            {
                if(name == tournament.Name)
                {
                    return tournament;
                }
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
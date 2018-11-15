using System;
using System.Collections.Generic;

namespace TournamentLib
{
    public class TournamentRepo
    {
        public List<Tournament> TournamentList = new List<Tournament>();

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

        public void CreateTournament(string name, List<Team> teamList)
        {
            Tournament tournament = new Tournament(name);
            foreach(Team team in teamList)
            {
                tournament.AddTeam(team);
            }
            
            TournamentList.Add(tournament);
            
            
        }

        public void RemoveTournament(Tournament t)
        {
            TournamentList.Remove(t);
        }
    }
}
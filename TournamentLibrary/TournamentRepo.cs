using System;
using System.Collections.Generic;

namespace TournamentLib
{
    public class TournamentRepo
    {
        public List<Tournament> TournamentList = new List<Tournament>();
        private List<Tournament> FinishedTournaments = new List<Tournament>();

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

        public List<Tournament> GetFinishedTournaments()
        {
            return FinishedTournaments;
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

        public void FinishTournament(string tournamentName)
        {
            foreach (Tournament tournament in TournamentList)
            {
                if (tournament.Name.Equals(tournamentName))
                {
                    TournamentList.Remove(tournament);
                    FinishedTournaments.Add(tournament);
                    return;
                }
            }
            throw new Exception("Tournament does not exist");
        }
    }
}
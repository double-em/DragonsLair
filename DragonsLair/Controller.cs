using System;
using System.Collections.Generic;
using System.Linq;
using TournamentLib;

namespace DragonsLair
{
    public class Controller
    {
        private readonly TournamentRepo tournamentRepository = new TournamentRepo();
        public Tournament tournamentFocus;
        public Round roundFocus;
        Random rng = new Random();

        public bool ChooseTournament()
        {
            if (tournamentRepository.TournamentList.Count() > 0)
            {
                Console.WriteLine("Turneringsoversigt\n");
                for (int i = 0; i < tournamentRepository.TournamentList.Count(); i++)
                {
                    Console.WriteLine("\t" + (i + 1) + ". " + tournamentRepository.TournamentList[i].Name);
                }
                Console.Write("Vælg turnering: ");
                string tournamentChoice = Console.ReadLine();
                int tournamentIndex;
                bool valid = int.TryParse(tournamentChoice, out tournamentIndex);
                int nTournaments = tournamentRepository.TournamentList.Count;
                if (valid)
                {
                    tournamentIndex -= 1;
                    if(tournamentIndex < nTournaments && tournamentIndex >= 0)
                    {
                        tournamentFocus = tournamentRepository.TournamentList[tournamentIndex];
                        return true;
                    }
                    
                }
                Console.WriteLine("Forkert valg...");
                Console.ReadKey(true);
                Console.Clear();
                return false;            }
            else
            {
                Console.WriteLine("Der er ikke oprettet nogen turneringer...");
                Console.ReadKey(true);
                Console.Clear();
                return false;
            }
        }

        public void ShowScore(string tournamentName)
        {
            List<Team> winnerTeams = new List<Team>();
            Tournament t = tournamentRepository.GetTournament(tournamentName);
            List<Team> Teams = t.GetTeams();
            Team freeRider = null;
            string[,] placement = new string[Teams.Count, 2];
            for (int i = 0; i < Teams.Count; i++)
            {
                placement[i, 0] = Teams[i].Name;
                placement[i, 1] = "0";
            }
            int rounds = t.GetNumberOfRounds();
            for (int i = 0; i < rounds; i++)
            {
                Round currentRound = t.GetRound(i);
                List<Team> winningTeams = currentRound.GetWinningTeams();
                winnerTeams.AddRange(winningTeams);
                if(currentRound.FreeRider != null)
                {
                    freeRider = currentRound.FreeRider;
                }
            }

            foreach (Team team in winnerTeams)
            {
                for (int i = 0; i < placement.GetLength(0); i++)
                {
                    string teamName = placement[1, 0];
                    if (team.Name.Equals(teamName))
                    {
                        int.TryParse(placement[i, 1], out int temp);
                        temp++;
                        placement[i, 1] = temp.ToString();
                    }
                }
            }

            // Uhm... Wot? o.O
            for (int i = 0; i < placement.GetLength(0); i++)
            {
                for (int j = 0; j < placement.GetLength(0); j++)
                {
                    if (int.Parse(placement[i, 1]) >= int.Parse(placement[j, 1]))
                    {
                        string[] temp1 = { "", "" };
                        string[] temp2 = { "", "" };

                        temp1[0] = placement[j, 0];
                        temp1[1] = placement[j, 1];
                        temp2[0] = placement[i, 0];
                        temp2[1] = placement[i, 1];

                        placement[i, 0] = temp1[0];
                        placement[i, 1] = temp1[1];
                        placement[j, 0] = temp2[0];
                        placement[j, 1] = temp2[1];
                    }
                }
            }
            // End wot

            for (int i = 0; i < placement.GetLength(0); i++)
            {
                Console.WriteLine(placement[i, 0] + " Won " + placement[i, 1] + " times");
            }
            if(freeRider != null)
            {
                Console.WriteLine("\n" + freeRider.Name + " is getting a free round");
            }
            Console.ReadKey(true);
            Console.Clear();
        }

        public void ScheduleNewRound(string tournamentName, bool printNewMatches = true, bool scrambleTeams = true)
        {
            Tournament t = tournamentRepository.GetTournament(tournamentName);
            Round lastRound, newRound;
            bool isRoundFinished;
            List<Team> teams, scrambled;
            Team oldFreeRider, newFreeRider;
            int numberOfRounds = t.GetNumberOfRounds();
            if (numberOfRounds == 0)
            {
                lastRound = null;
                isRoundFinished = true;
            }
            else
            {
                lastRound = t.GetRound(numberOfRounds - 1);
                isRoundFinished = lastRound.IsMatchesFinished();
            }

            if (isRoundFinished == true)
            {
                if (lastRound == null)
                {
                    teams = t.GetTeams();
                }
                else
                {
                    teams = lastRound.GetWinningTeams();

                    if (lastRound.FreeRider != null)
                    {
                        teams.Add(lastRound.FreeRider);
                    }
                }
                if (teams.Count >= 2)
                {
                    newRound = new Round();
                    scrambled = ScrambleTeamsRandomly(teams, scrambleTeams);
                    
                    

                    if ((scrambled.Count % 2) != 0)
                    {
                        if (numberOfRounds > 0)
                        {
                            oldFreeRider = lastRound.GetFreeRider();
                            newFreeRider = scrambled[0];

                        }
                        else
                        {
                            oldFreeRider = null;
                            newFreeRider = scrambled[0];
                        }
                        int x = 0;
                        while (newFreeRider == oldFreeRider)
                        {
                            newFreeRider = scrambled[x];
                                x++;
                        }
                        newRound.SetFreeRider(newFreeRider);
                        scrambled.Remove(newFreeRider);

                   }
                    for(int i = 0; i < scrambled.Count; i += 2)
                    {
                        Match match = new Match();
                        match.FirstOpponent = scrambled[i];
                        match.SecondOpponent = scrambled[i + 1];
                        newRound.AddMatch(match);
                    }
                    t.AddRound(newRound);
                    //Vis kampe her
                }
                else
                {
                    throw new Exception("Ikke nok hold til en ny runde");
                }
            }
            else
            {
                throw new Exception("Runden er ikke færdig");
            }

        }

        public void SaveMatch(string tournamentName, int roundNo, string winningTeamName)
        {
            Tournament t = tournamentRepository.GetTournament(tournamentName);
            Round r = t.GetRound(roundNo);
            Match m = r.GetMatch(winningTeamName);

            if (m != null && m.Winner == null)
            {
                Team w = t.GetTeam(winningTeamName);
                m.Winner = w;

                Console.WriteLine("Matched Saved");
            }
            else throw new Exception("Failed to save the match");
        }

        public TournamentRepo GetTournamentRepository()
        {
            return tournamentRepository;
        }

        public List<Team> ScrambleTeamsRandomly(List<Team> teams, bool scramble = true)
        {
            List<Team> scrambledTeams = new List<Team>();
            if (scramble) { 
                int length = teams.Count;
                while (length >= 1)
                {
                    int rnd = rng.Next(length);
                    var temp = teams[rnd];
                    teams[rnd] = teams[length - 1];
                    teams[length - 1] = temp;
                    scrambledTeams.Add(teams[length-1]);
                    length--;
                }
            }
            else
            {
                foreach (Team team in teams)
                {
                    scrambledTeams.Add(team);
                }
            }
            return scrambledTeams;
        }

        public void CreateTournament(string name, List<string> teamNames)
        {
            bool takeTeams = true;
            List<Team> teamList = new List<Team>();

            foreach(string teamName in teamNames)
            {
                teamList.Add(new Team(teamName));
            }

            Tournament t = tournamentRepository.GetTournament(name);
            if(t != null)
            {
                throw new Exception("Tournament already exists");
            }
            else
            {
                tournamentRepository.CreateTournament(name, teamList);
            }
        }

        public void RemoveTournament(string name)
        {
            Tournament t = tournamentRepository.GetTournament(name);

            if (t != null)
            {
                tournamentRepository.RemoveTournament(t);
            }
            else
            {
                throw new Exception("Tournament doesn't exist");
            }
        }

        public void FinishTournament(string TournamentName)
        {
            tournamentRepository.FinishTournament(TournamentName);
        }
    }
}

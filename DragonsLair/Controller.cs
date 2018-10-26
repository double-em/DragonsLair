using System;
using System.Collections.Generic;
using System.Linq;
using TournamentLib;

namespace DragonsLair
{
    public class Controller
    {
        private TournamentRepo tournamentRepository = new TournamentRepo();
        Random rng = new Random();

        public void ShowScore(string tournamentName)
        {
            List<Team> winnerTeams = new List<Team>();
            Tournament t = tournamentRepository.GetTournament(tournamentName);
            List<Team> Teams = t.GetTeams();
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
            }

            foreach (var item in winnerTeams)
            {
                for (int i = 0; i < placement.GetLength(0); i++)
                {
                    if (item.Name == placement[i, 0])
                    {
                        int.TryParse(placement[i, 1], out int temp);
                        temp++;
                        placement[i, 1] = temp.ToString();
                    }
                }
            }

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

            for (int i = 0; i < placement.GetLength(0); i++)
            {
                Console.WriteLine(placement[i, 0] + " Won " + placement[i, 1] + " times");
            }
            Console.ReadKey(true);
            Console.Clear();
        }

        public void ScheduleNewRound(string tournamentName, bool printNewMatches = true)
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

            if (isRoundFinished = true)
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
                    //Scramble HERE PLS
                    scrambled = teams;

                    if (scrambled.Count % 2 != 0)
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
                    throw new Exception("Round is not finished");
                }
            }
            else
            {
                throw new Exception("Round not finished");
            }

        }

        public void SaveMatch(string tournamentName, int roundNumber, string team1, string team2, string winningTeam)
        {
            // Do not implement this method
        }

        public TournamentRepo GetTournamentRepository()
        {
            return tournamentRepository;
        }

        public Team ScrambleTeamsRandomly(List<Team> teams)
        {
            int rnd = rng.Next(teams.Count);
            return teams[rnd];
        }
    }
}

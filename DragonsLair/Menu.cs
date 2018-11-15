using System;
using System.Collections.Generic;

namespace DragonsLair
{
    public class Menu
    {
        private Controller control = new Controller();
        
        public void Show()
        {
            if (ShowMenuTournament())
            {
                Console.Clear();
                ShowMenuTournamentOptions();
            }
        }

        private void ShowMenuTournamentOptions()
        {
            bool end = false;
            while (!end)
            {
                Console.WriteLine(control.tournamentFocus.Name + "\n");
                Console.WriteLine("\t 1. Stilling");
                Console.WriteLine("\t 2. Hold");
                Console.WriteLine("\t 3. Runde");
                Console.WriteLine("\n\t 0. Exit");
                Console.Write("\nIndtast valg: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "0":
                        end = true;
                        break;
                    case "1":
                        control.ShowScore(control.tournamentFocus.Name);
                        break;
                    case "2":
                        ShowMenuTeams();
                        Console.Clear();
                        break;
                    case "3":
                        DeleteTournament();
                        Console.Clear();
                        break;
                    default:
                        Console.Write("Ugyldigt valg");
                        Console.ReadKey(true);
                        Console.Clear();
                        break;
                }
            }
        }

        private void ShowMenuTeams()
        {
            Console.Clear();
            bool end = false;
            while (!end)
            {
                Console.WriteLine(control.tournamentFocus.Name + " - Hold\n");
                Console.WriteLine("\t 1. Oversigt");
                Console.WriteLine("\t 2. Tilføj Hold");
                Console.WriteLine("\t 3. Slet Team");
                Console.WriteLine("\n\t 0. Exit");
                Console.Write("\nIndtast valg: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "0":
                        end = true;
                        break;
                    case "1":
                        control.ShowScore(control.tournamentFocus.Name);
                        break;
                    case "2":
                        ShowMenuTeams();
                        Console.Clear();
                        break;
                    case "3":
                        DeleteTournament();
                        Console.Clear();
                        break;
                    default:
                        Console.Write("Ugyldigt valg");
                        Console.ReadKey(true);
                        Console.Clear();
                        break;
                }
            }
        }

        private bool ShowMenuTournament()
        {
            bool choiceMade = false;
            while (!choiceMade)
            {
                Console.WriteLine("Turneringssystemet over alle systemer\n");
                Console.WriteLine("\t 1. Vælg Turnering");
                Console.WriteLine("\t 2. Opret Turnering");
                Console.WriteLine("\t 3. Fjern Turnering");
                Console.Write("\nIndtast valg: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        choiceMade = control.ChooseTournament();
                        break;
                    case "2":
                        CreateTournament();
                        Console.Clear();
                        break;
                    case "3":
                        DeleteTournament();
                        Console.Clear();
                        break;
                    default:
                        Console.Write("Ugyldigt valg");
                        Console.ReadKey(true);
                        Console.Clear();
                        break;
                }
            }
            return choiceMade;
        }

        private void ShowMenu()
        {
            Console.WriteLine("Dragons Lair");
            Console.WriteLine();
            Console.WriteLine("1. Præsenter turneringsst");
            Console.WriteLine("2. Planlæg runde i turnering");
            Console.WriteLine("3. Registrér afviklet kamp");
            Console.WriteLine("4. Skab ny turnering");
            Console.WriteLine("5. Slet eksisterende turnering");
            Console.WriteLine("");
            Console.WriteLine("0. Exit");
        }

        private string GetUserChoice()
        {
            Console.WriteLine();
            Console.Write("Indtast dit valg: ");
            return Console.ReadLine();
        }
        
        private void ShowScore()
        {
            Console.Write("Angiv navn på turnering: ");
            string tournamentName = Console.ReadLine();
            Console.Clear();
            control.ShowScore(tournamentName);
        }

        private void ScheduleNewRound()
        {
            Console.Write("Angiv navn på turnering: ");
            string tournamentName = Console.ReadLine();
            Console.Clear();
            control.ScheduleNewRound(tournamentName);
        }

        private void SaveMatch()
        {
            Console.Write("Angiv navn på turnering: ");
            string tournamentName = Console.ReadLine();
            Console.Write("Angiv runde: ");
            int round = int.Parse(Console.ReadLine());
            Console.Write("Angiv vinderhold: ");
            string winner = Console.ReadLine();
            Console.Clear();
            control.SaveMatch(tournamentName, round, winner);
        }

        private void CreateTournament()
        {
            string tournamentName;
            Console.WriteLine("Indtast turneringsnavn: ");
            tournamentName = Console.ReadLine();

            bool takeTeams = true;
            List<string> teamNames = new List<string>();
            while (takeTeams)
            {
                Console.Write("Indtast holdnavn: ");
                string TeamName = Console.ReadLine();
                if (TeamName.Length > 0)
                {
                    teamNames.Add(TeamName);
                }
                else
                {
                    takeTeams = false;
                }
            }
            control.CreateTournament(tournamentName, teamNames);
        }

        private void DeleteTournament()
        {
            Console.WriteLine("Indtast turneringsnavn: ");
            string name = Console.ReadLine();
            control.RemoveTournament(name);
        }
    }
}
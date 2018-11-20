using System;
using System.Collections.Generic;
using TournamentLib;

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

        void ShowMatchOptions(int matchChoose)
        {
            bool end = false;
            while (!end)
            {
                Console.Clear();
                Console.WriteLine(control.tournamentFocus.Name + " - " + control.roundFocus.name + " - " + (matchChoose + 1) + " Match\n");
                Match match = control.roundFocus.matches[matchChoose];
                Team FirstOpponent = match.FirstOpponent;
                Team SecondOpponent = match.SecondOpponent;
                Console.WriteLine("\t" + FirstOpponent.Name);
                Console.WriteLine("\tvs");
                Console.WriteLine("\t" + SecondOpponent.Name + "\n");
                Console.Write("Skriv vinder eller 0 for exit: ");
                string winner = Console.ReadLine();
                if (winner == FirstOpponent.Name)
                {
                    match.Winner = FirstOpponent;
                    end = true;
                }
                else if (winner == SecondOpponent.Name)
                {
                    match.Winner = SecondOpponent;
                    end = true;
                }
                else if (winner == "0")
                {
                    end = true;
                }
                else
                {
                    Console.WriteLine("Dette team er ikke en del af denne kamp...");
                }
            }
            
        }

        void MatchesView()
        {
            Console.Clear();
            Console.WriteLine(control.tournamentFocus.Name + " - " + control.roundFocus.name + " - Matches\n");
            int number = 1;
            foreach(var match in control.roundFocus.matches)
            {
                Console.WriteLine("\t" + number + ". " + match.FirstOpponent + " vs " + match.SecondOpponent);
                if (match.Winner == null)
                {
                    Console.WriteLine("\t   Status: Kampen er ikke afholdt");
                }
                else
                {
                    Console.WriteLine("\t   Status: Vinder var " + match.Winner.Name);
                }
                number++;
            }
            Console.Write("\nVælg Kamp: ");
            string matchChoice = Console.ReadLine();
            bool isChoice = int.TryParse(matchChoice, out int choose);
            choose--;
            if (isChoice)
            {
                ShowMatchOptions(choose);
            }
        }

        internal void RoundList()
        {
            bool end = false;
            while (!end)
            {
                Console.Clear();
                Console.WriteLine(control.tournamentFocus.Name + " - Runder\n");
                for (int i = 0; i < control.tournamentFocus.rounds.Count; i++)
                {
                    Console.WriteLine("\t" + (i + 1) + ". " + control.tournamentFocus.rounds[i].name);
                }
                Console.Write("\nVælg runde: ");
                string choice = Console.ReadLine();
                bool isChoice = int.TryParse(choice, out int choose);
                choose--;
                if (choose < control.tournamentFocus.rounds.Count && isChoice)
                {
                    control.roundFocus = control.tournamentFocus.rounds[choose];
                    control.tournamentFocus.rounds[choose].name = (choose + 1) + " Runde";
                    MatchesView();
                }
                else if(choice.Length == 0)
                {
                    end = true;
                }
                else
                {
                    Console.WriteLine("Ugyldigt valg.");
                }
            }
        }

        private void ShowMenuRound()
        {
            bool end = false;
            while (!end)
            {
                Console.Clear();
                Console.WriteLine(control.tournamentFocus.Name + " - Runde Aktion\n");
                Console.WriteLine("\t 1. Se Runde");
                Console.WriteLine("\t 2. Ny Runde");
                Console.WriteLine("\n\t 0. Exit");
                Console.Write("\nIndtast valg: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "0":
                        end = true;
                        break;
                    case "1":
                        RoundList();
                        break;
                    case "2":
                        try
                        {
                            control.ScheduleNewRound(control.tournamentFocus.Name);
                        }
                        catch(Exception e){
                            Console.WriteLine(e.Message);
                            Console.ReadKey(true);
                        }
                        break;
                    default:
                        Console.Write("Ugyldigt valg");
                        Console.ReadKey(true);
                        Console.Clear();
                        break;
                }
            }
        }

        private void ShowMenuTournamentOptions()
        {
            bool end = false;
            while (!end)
            {
                Console.Clear();
                Console.WriteLine(control.tournamentFocus.Name + "\n");
                Console.WriteLine("\t 1. Stilling");
                Console.WriteLine("\t 2. Runde");
                Console.WriteLine("\t 3. Færdiggør Turnering");
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
                        ShowMenuRound();
                        Console.Clear();
                        break;
                    case "3":
                        //control.FinishTournament(control.tournamentFocus.Name);
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
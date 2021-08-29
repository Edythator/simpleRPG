using System;
using System.Threading;

namespace simpleRPG
{
    internal static class Program
    {
        //TODO: implement functions to scale mobs to fighter levels
        //TODO: implement finding fighters n stuff
        //TODO: fix some kind of "encryption" for saving

        // skapar en ny statisk instans av Random som kan användas vartsom; det är dåligt att konstruktera flera randoms i ett och samma program, så jag gör det här
        public static readonly Random Rnd = new();
        private static void Main()
        {
            // skapar de onda & de goda, och konstrurerar båda
            FighterManager fighterManager = new();
            MobManager mobManager = new();
            fighterManager.LoadFighters();
            mobManager.ConstructMobs(fighterManager);
            bool turn = true;
            int money = 100;

            // medans båda är vid liv
            while (fighterManager.IsPartyAlive() && mobManager.MobsAlive())
            {
                Console.Clear();
                // ifall att mainfighter är död
                if (!fighterManager.IsMainFighterAlive())
                {
                    Console.WriteLine("Main fighter is dead... Swapping to other available fighter");
                    fighterManager.Select(fighterManager.FirstAvailableFighter());
                }

                // användaren får välja om den vill gå in i strid, eller fixa med sitt gäng
                Console.WriteLine("What do you want to do? (c(ontinue)/m(anage))");
                char c = char.ToLower(Console.ReadKey(true).KeyChar);
                
                Console.Clear();
                // ifall användaren vill hantera
                if (c == 'm')
                {
                    Console.WriteLine("What do you want to do? (s(wap)/r(ename)/p(rint)/b(ack))");
                    char sc = char.ToLower(Console.ReadKey(true).KeyChar);

                    switch (sc)
                    {
                        // ifall användaren vill byta main gubben till en annan gubbe i sitt party
                        case 's':
                        {
                            fighterManager.PrintFighters();
                            Console.WriteLine("Who do you want to swap to?");

                            if (int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out int idx))
                                fighterManager.Select(idx - 1);
                            else continue;
                            break;
                        }
                        // ifall användaren vill byta namn på main gubben
                        case 'r':
                        {
                            Console.WriteLine($"What do you want {fighterManager.SelectedFighter.Name}'s nickname to be?");
                            string nickname = Console.ReadLine();
                            if (nickname != null && nickname.Contains(";"))
                            {
                                Console.WriteLine("The nickname includes invalid characters.");
                                Thread.Sleep(1000);
                                continue;
                            }
                            fighterManager.SelectedFighter.Nickname = nickname;
                            break;
                        }
                        // ifall användaren vill skriva ut alla partymedlemmar
                        case 'p':
                            fighterManager.PrintFighters();
                            Console.WriteLine("Press any key to go back...");
                            Console.ReadKey(true);
                            break;
                        // annars fortsätter vi om användaren inte vill göra något
                        default: continue;
                    }
                }

                // vi går in i strid om användaren inte vill hantera sina gubbar
                else
                {
                    // assignar en ny variabel då FighterManager.selectedFighter.GetPrintableName() är bara för långt att skriva ut
                    string name = fighterManager.SelectedFighter.GetPrintableName();
                    // attackera de onda ifall det är användarens tur
                    if (turn)
                    {
                        fighterManager.SelectedFighter.Attack(mobManager.SelectedMob, out int damage);

                        Console.WriteLine($"{name} did {damage} damage to {mobManager.SelectedMob.Name}!\n{mobManager.SelectedMob.Name} now has {mobManager.SelectedMob.HP} HP left.");
                        Thread.Sleep(1000);
                        turn = false;
                    }

                    // ifall det inte är användarens tur så attackerar istället de onda
                    else
                    {
                        fighterManager.SelectedFighter.Attack(fighterManager.SelectedFighter, out int damage);

                        Console.WriteLine($"{mobManager.SelectedMob.Name} did {damage} damage to {name}!\n{name} now has {fighterManager.SelectedFighter.HP} HP left.");
                        Thread.Sleep(1000);
                        turn = true;
                    }

                    // sparar de goda för varje omgång i loopen
                    fighterManager.SaveFighters();
                }
            }

            // efter loopen har körts klart vet vi inte om det är dem goda eller dem onda som lever då loopen kollar ifall båda kriterina är uppfyllda, så vi kollar vem som lever här
            if (fighterManager.IsPartyAlive())
            {
                Console.WriteLine("\nYou won! :D");
                int moneyGain = (int)Math.Pow(mobManager.GetAverageLevel(), Math.Log(10, 3));
                money += moneyGain;
                Console.WriteLine("You gained " + moneyGain + " money.");
                Thread.Sleep(2000);
            }
            else
            {
                Console.WriteLine("You lost! :(");
                int moneyLoss = (int)Math.Pow(mobManager.GetAverageLevel(), Math.Log(10, 3));
                money -= moneyLoss;
                Console.WriteLine("\nYou lost " + moneyLoss + " money.");
                Console.WriteLine("Healing your characters to max HP...");
                fighterManager.HealAllFighters();
                Thread.Sleep(2000);
            }
            Main();
        }
    }
}

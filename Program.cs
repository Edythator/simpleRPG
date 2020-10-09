using System;
using System.Threading;

namespace simpleRPG
{
    class Program
    {
        //TODO: implement some kind of function to calculate amount of money loss&gain scaled to level
        //TODO: implement functions to scale mobs to fighter levels
        //TODO: implement finding fighters n stuff
        //TODO: fix some kind of "encryption" for saving

        // skapar en ny statisk instans av Random som kan användas vartsom; det är dåligt att konstruktera flera randoms i ett och samma program, så jag gör det här
        // gör samtidigt en ny statisk klass av fightermanager, vilket är riktigt illa och ska fixas
        public static Random Rnd = new Random();
        public static FighterManager FighterManager = new FighterManager();
        private static void Main(string[] args)
        {
            // skapar de onda, och konstrurerar båda
            MobManager mobManager = new MobManager();
            FighterManager.LoadFighters();
            mobManager.ConstructMobs();
            bool turn = true;
            int money = 100;

            // medans båda är vid liv
            while (FighterManager.IsPartyAlive() && mobManager.MobsAlive())
            {
                Console.Clear();

                // ifall att mainfighter är död
                if (!FighterManager.IsFighterAlive())
                {
                    Console.WriteLine("Main fighter is dead... Swapping to other available fighter");
                    FighterManager.Select(FighterManager.FirstAvailableFighter());
                }

                // användaren får välja om den vill gå in i strid, eller fixa med sitt gäng
                Console.WriteLine("What do you want to do? (c(ontinue)/m(anage))");
                char c = char.ToLower(Console.ReadKey().KeyChar);

                // ifall använder vill hantera
                if (c == 'm')
                {
                    Console.Clear();
                    Console.WriteLine("What do you want to do? (s(wap)/r(ename)/p(rint)/b(ack))");
                    char sc = char.ToLower(Console.ReadKey().KeyChar);

                    // ifall användaren vill byta main gubben till en annan gubbe i sitt party
                    if (sc == 's')
                    {
                        FighterManager.PrintFighters();
                        Console.WriteLine("\nWho do you want to swap to?");

                        if (int.TryParse(Console.ReadKey().KeyChar.ToString(), out int idx))
                            FighterManager.Select(idx - 1);
                        else continue;
                    }

                    // ifall användaren vill byta namn på main gubben
                    else if (sc == 'r')
                    {
                        Console.WriteLine($"\nWhat do you want {FighterManager.selectedFighter.Name}'s nickname to be?");
                        string nickname = Console.ReadLine();
                        if (nickname == ";")
                        {
                            Console.WriteLine("The nickname includes invalid characters.");
                            Thread.Sleep(1000);
                            continue;
                        }
                        FighterManager.selectedFighter.Nickname = nickname;
                    }

                    // ifall användaren vill skriva ut alla partymedlemmar
                    else if (sc == 'p')
                    {
                        FighterManager.PrintFighters();
                        Console.WriteLine("Press any key to go back...");
                        Console.ReadKey();
                    }

                    // annars fortsätter vi om användaren inte vill göra något
                    else continue;
                }

                // vi går in i strid om användaren inte vill hantera sina gubbar
                else
                {
                    // assignar en ny variabel då FighterManager.selectedFighter.GetPrintableName() är bara för långt att skriva ut
                    string name = FighterManager.selectedFighter.GetPrintableName();
                    // attackera de onda ifall det är användarens tur
                    if (turn)
                    {
                        FighterManager.selectedFighter.Attack(mobManager.selectedMob, out int damage);

                        Console.WriteLine($"\n{name} did {damage} damage to {mobManager.selectedMob.Name}!\n{mobManager.selectedMob.Name} now has {mobManager.selectedMob.HP} HP left.");
                        Thread.Sleep(1000);
                        turn = false;
                    }
                    // ifall det inte är användarens tur så attackerar istället de onda
                    else
                    {
                        FighterManager.selectedFighter.Attack(FighterManager.selectedFighter, out int damage);

                        Console.WriteLine($"\n{mobManager.selectedMob.Name} did {damage} damage to {name}!\n{name} now has {FighterManager.selectedFighter.HP} HP left.");
                        Thread.Sleep(1000);
                        turn = true;
                    }
                    // sparar de goda för varje omgång i loopen
                    FighterManager.SaveFighters();
                }
            }

            // efter loopen har körts klart vet vi inte om det är dem goda eller dem onda som lever då loopen kollar ifall båda kriterina är uppfyllda, så vi kollar vem som lever här
            if (FighterManager.IsPartyAlive())
            {
                Console.WriteLine("\nYou won! :D");
                int moneyGain = (int)(money * 0.03);
                money += moneyGain;
                Console.WriteLine("\nYou gained " + moneyGain + " money.");
            }
            else
            {
                Console.WriteLine("\nYou lost! :(");
                int moneyLoss = (int)(money * 0.03);
                money -= moneyLoss;
                Console.WriteLine("\nYou lost " + moneyLoss + " money.");
                Console.WriteLine("Game will automatically exit in 2 seconds...");
                Thread.Sleep(2000);
                return;
            }
        }
    }
}

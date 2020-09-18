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

        public static Random Rnd = new Random();
        public static FighterManager FighterManager = new FighterManager();
        private static void Main(string[] args)
        {
            MobManager mobManager = new MobManager();
            FighterManager.LoadFighters();
            mobManager.ConstructMobs();
            bool turn = true;
            int money = 100;

            while (FighterManager.IsPartyAlive() && mobManager.MobsAlive())
            {
                Console.Clear();
                Console.WriteLine("What do you want to do? (c(ontinue)/m(anage))");
                char c = char.ToLower(Console.ReadKey().KeyChar);
                if (c == 'm')
                {
                    Console.Clear();
                    Console.WriteLine("What do you want to do? (s(wap)/r(ename)/p(rint)/b(ack))");
                    char sc = char.ToLower(Console.ReadKey().KeyChar);

                    if (sc == 's')
                    {
                        FighterManager.PrintFighters();
                        Console.WriteLine("\nWho do you want to swap to?");

                        if (int.TryParse(Console.ReadKey().KeyChar.ToString(), out int idx))
                            FighterManager.Select(idx - 1);
                        else continue;
                    }
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
                    else if (sc == 'p')
                    {
                        FighterManager.PrintFighters();
                        Console.WriteLine("Press any key to go back...");
                        Console.ReadKey();
                    }
                    else continue;
                }
                else
                {
                    string name = FighterManager.selectedFighter.GetPrintableName();
                    if (turn)
                    {
                        FighterManager.selectedFighter.Fight(mobManager.selectedMob, out int damage);

                        Console.WriteLine($"\n{name} did {damage} damage to {mobManager.selectedMob.Name}!\n{mobManager.selectedMob.Name} now has {mobManager.selectedMob.HP} HP left.");
                        Thread.Sleep(1000);
                        turn = false;
                    }
                    else
                    {
                        FighterManager.selectedFighter.Fight(FighterManager.selectedFighter, out int damage);

                        Console.WriteLine($"\n{mobManager.selectedMob.Name} did {damage} damage to {name}!\n{name} now has {FighterManager.selectedFighter.HP} HP left.");
                        Thread.Sleep(1000);
                        turn = true;
                    }
                    FighterManager.SaveFighters();
                }
            }

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

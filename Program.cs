using System;
using System.Linq;
using System.Threading;

namespace simpleRPG
{
    class Program
    {
        //TODO: implement some kind of function to calculate amount of money loss&gain scaled to level
        //TODO: implement functions to scale mobs to fighter levels
        //TODO: implement finding fighters n stuff

        public static Random Rnd = new Random();
        public static FighterManager fighterManager = new FighterManager();
        static void Main(string[] args)
        {
            fighterManager.LoadFighters();
            bool turn = true;
            int money = 100; 

            Mob[] mobs = new Mob[2];
            for (int i = 0; i < mobs.Length; i++)
                mobs[i] = new Mob();

            while (fighterManager.IsPartyAlive() && mobs.All(x => x.IsAlive()))
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
                        fighterManager.PrintFighters();
                        Console.WriteLine("\nWho do you want to swap to?");

                        if (int.TryParse(Console.ReadKey().KeyChar.ToString(), out int idx))
                            fighterManager.Select(idx);
                        else continue;
                    }
                    else if (sc == 'r')
                    {
                        Console.WriteLine($"\nWhat do you want {fighterManager.selectedFighter.Name}'s nickname to be?");
                        string nickname = Console.ReadLine();
                        if (nickname == ";")
                        {
                            Console.WriteLine("Invalid nickname.");
                            continue;
                        }

                        fighterManager.selectedFighter.Nickname = Console.ReadLine();
                    }
                    else if (sc == 'p')
                    {
                        fighterManager.PrintFighters();
                        Console.WriteLine("Press any key to go back...");
                        Console.ReadKey();
                    }
                    else continue;
                }
                else
                {
                    string name = fighterManager.selectedFighter.GetPrintableName();
                    Mob selectedMob = mobs.OrderBy(x => x.HP).First();
                    if (turn)
                    {
                        // implement scaling amount of crit derived from the total CP the character can do and chance of crit
                        int damage = Rnd.Next(fighterManager.selectedFighter.CP - 2, fighterManager.selectedFighter.CP + 2);
                        selectedMob.HP -= damage;
                        Console.WriteLine($"\n{name} did {damage} damage to {selectedMob.Name}!\n {name} now has {selectedMob.HP} HP left.\n");
                        Thread.Sleep(1000);
                        turn = false;
                    }
                    else
                    {
                        int damage = Rnd.Next(selectedMob.CP - 2, selectedMob.CP + 2);
                        Console.WriteLine($"\n{selectedMob} did {damage} damage to {name}!\n {name} now has {fighterManager.selectedFighter.HP} HP left.\n");
                        fighterManager.selectedFighter.HP -= damage;
                        Thread.Sleep(1000);
                        turn = true;
                    }
                    fighterManager.SaveFighters();
                }

            }

            if (fighterManager.IsPartyAlive()) {
                Console.WriteLine("\nYou won! :D");
                int moneyGain = 5;
                money += moneyGain;
            } else {
                Console.WriteLine("\nYou lost! :(");
                int moneyLoss = 6;
                money -= moneyLoss;
            }
        }
    }
}

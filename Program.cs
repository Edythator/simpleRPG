using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace simpleRPG
{
    class Program
    {
        //TODO: implement some kind of function to calculate amount of money loss&gain scaled to level
        //TODO: implement randomization and management of mobs/fighters
        //TODO: implement functions to scale mobs to fighter levels
        //TODO: implement saving

        public static Random Rnd = new Random();

        // make sure to not forget to initalize the fighters, the program is useless in its current form without this functionality
        public static Fighter[] Fighters = new Fighter[5];
        static void Main(string[] args)
        {
            bool turn = true;
            int money = 100;
            Fighters[0] = new Fighter("Green", 10, 10, 1, 1, "hey");
            Fighter mainFighter = Fighters[0];
            Mob[] mobs = new Mob[1] { new Mob() };

            Console.WriteLine("Hello. Welcome to the dungeon. Let's continue, shall we?");
            while (Fighters.Where(x => x != null).All(x => x.HP > 0) && mobs.All(x => x.HP > 0))
            {
                Console.Clear();
                Console.WriteLine("What do you want to do? (m/c)");
                char c = Console.ReadKey().KeyChar;
                if (c == 'm' || c == 'm')
                {
                    Console.Clear();
                    Console.WriteLine("What do you want to do? (s/r/b)");
                    char sc = Console.ReadKey().KeyChar;
                    if (sc == 's' || sc == 'S')
                    {
                        int i = 1;
                        foreach (Fighter f in Fighters)
                        {
                            Console.WriteLine($"{i}: {f.Name}");
                            i++;
                        }
                        Console.WriteLine("Which fighter do you want to pick?");
                        char pick = Console.ReadKey().KeyChar;
                        if (int.TryParse(pick.ToString(), out int j))
                        {
                            Fighter oldMain;
                            oldMain = mainFighter;
                            mainFighter = Fighters[j - 1];
                            Fighters[j - 1] = oldMain;
                        }
                        else continue;
                    }
                    else if (sc == 'r' || sc == 'R')
                    {
                        int i = 1;
                        foreach (Fighter f in Fighters)
                        {
                            Console.WriteLine($"{i}: {f.Name}");
                            i++;
                        }
                        Console.WriteLine("Which fighter do you want to rename?");
                        char pick = Console.ReadKey().KeyChar;
                        if (int.TryParse(pick.ToString(), out int j))
                        {
                            Console.WriteLine("What do you want to rename it to?");
                            Fighters[j - 1].Nickname = Console.ReadLine();
                        }
                        else continue;
                    }
                    else if (sc == 'b' || sc == 'B')
                        continue;
                    else
                    {
                        Console.WriteLine("You entered an invalid choice. Returning...");
                        continue;
                    };
                }

                if (turn)
                {
                    // implement scaling amount of crit derived from the total CP the character can do
                    int damage = Rnd.Next(Fighters[0].CP - 2, Fighters[0].CP + 2);
                    mobs[0].HP -= damage;
                    if (mainFighter.Nickname == "")
                        Console.WriteLine($"{mainFighter.Name} did {damage} damage to {mobs[0].Name}!\n {mobs[0].Name} now has {mobs[0].HP} HP left.\n");
                    else
                        Console.WriteLine($"{mainFighter.Nickname} ({mainFighter.Name}) did {damage} damage to {mobs[0].Name}!\n {mobs[0].Name} now has {mobs[0].HP} HP left.\n");
                    Thread.Sleep(1000);
                    turn = false;
                }
                else
                {
                    int damage = Rnd.Next(mobs[0].CP - 2, mobs[0].CP + 2);
                    if (mainFighter.Nickname == "")
                        Console.WriteLine($"{mobs[0].Name} did {damage} damage to {mainFighter.Name}!\n {mainFighter.Name} now has {mainFighter.HP} HP left.\n");
                    else
                        Console.WriteLine($"{mobs[0].Name} did {damage} damage to {mainFighter.Nickname} ({mainFighter.Name})!\n {mainFighter.Nickname} now has {mainFighter.HP} HP left.\n");
                    mainFighter.HP -= damage;
                    Thread.Sleep(1000);
                    turn = true;
                }
            }

            if (Fighters.Where(x => x != null).All(x => x.IsAlive())) {
                Console.WriteLine("\nYou lost! :(");
                int moneyLoss = 6;
                money -= moneyLoss;
            } else if (mobs.All(x => x.IsAlive())) {
                Console.WriteLine("\nYou won! :D");
                int moneyGain = 5;
                money += moneyGain;
            }
        }
    }
}

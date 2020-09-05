using System;
using System.Collections.Generic;
using System.Linq;
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

        static void Main(string[] args)
        {
            MobManager mobManager = new MobManager();
            FighterManager fighterManager = new FighterManager();
            fighterManager.LoadFighters();
            mobManager.ConstructMobs();
            bool turn = true;
            int money = 100;
            while (fighterManager.IsPartyAlive() && mobManager.MobsAlive())
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
                            fighterManager.Select(idx - 1);
                        else continue;
                    }
                    else if (sc == 'r')
                    {
                        Console.WriteLine($"\nWhat do you want {fighterManager.selectedFighter.Name}'s nickname to be?");
                        string nickname = Console.ReadLine();
                        if (nickname == ";")
                        {
                            Console.WriteLine("The nickname includes invalid characters.");
                            Thread.Sleep(1000);
                            continue;
                        }
                        fighterManager.selectedFighter.Nickname = nickname;
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
                    if (turn)
                    {
                        // implement scaling amount of crit derived from the total CP the character can do and chance of crit
                        int damage = Rnd.Next(fighterManager.selectedFighter.CP - 2, fighterManager.selectedFighter.CP + 2);
                        if (mobManager.selectedMob.HP - damage < 0)
                            damage = mobManager.selectedMob.HP;
                        mobManager.selectedMob.HP -= damage;

                        Console.WriteLine($"\n{name} did {damage} damage to {mobManager.selectedMob.Name}!\n {mobManager.selectedMob.Name} now has {mobManager.selectedMob.HP} HP left.");
                        Thread.Sleep(1000);
                        turn = false;
                    }
                    else
                    {
                        int damage = Rnd.Next(mobManager.selectedMob.CP - 2, mobManager.selectedMob.CP + 2);
                        if (fighterManager.selectedFighter.HP - damage < 0)
                            damage = fighterManager.selectedFighter.HP;
                        fighterManager.selectedFighter.HP -= damage;

                        Console.WriteLine($"\n{mobManager.selectedMob.Name} did {damage} damage to {name}!\n {name} now has {fighterManager.selectedFighter.HP} HP left.");
                        Thread.Sleep(1000);
                        turn = true;
                    }
                    fighterManager.SaveFighters();
                }

            }

            if (fighterManager.IsPartyAlive())
            {
                Console.WriteLine("\nYou won! :D");
                int moneyGain = 5;
                money += moneyGain;
            }
            else
            {
                Console.WriteLine("\nYou lost! :(");
                int moneyLoss = 6;
                money -= moneyLoss;
                Console.WriteLine("Game will automatically exit in 2 seconds...");
                Thread.Sleep(2000);
                return;
            }
        }
    }
}

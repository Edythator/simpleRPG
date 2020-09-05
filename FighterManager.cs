using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq.Expressions;
using System.Diagnostics;

namespace simpleRPG
{
    class FighterManager
    {
        public Fighter selectedFighter { get; private set; }
        private List<Fighter> fighters = new List<Fighter>();

        public void LoadFighters()
        {
            try
            {
                string[] buffer = File.ReadAllLines("guys");
                foreach (string s in buffer)
                {
                    string[] fighterProperties = s.Split(';');
                    Create(fighterProperties[0], int.Parse(fighterProperties[1]),
                        int.Parse(fighterProperties[2]), int.Parse(fighterProperties[3]), int.Parse(fighterProperties[4]), fighterProperties[5]);
                }
            }
            catch (IOException)
            {
                Debug.Write("Couldn't find file.");
            }

            catch (Exception e)
            {
                Console.WriteLine("Caught exception: " + e.Message);
            }

            if (GetFighterCount() > 0)
                Select(0);
            else
                Select(Create("Default", 10, 10, 1, 1, "Default faction"));
        }
        public void SaveFighters()
        {
            try
            {
                List<string> buffer = new List<string>();
                foreach (Fighter f in fighters)
                    buffer.Add($"{f.Name};{f.HP};{f.CP};{f.XP};{f.Faction}");
                File.WriteAllLines("guys", buffer);
            }
            catch (Exception e)
            {
                Console.WriteLine("Caught exception: " + e.Message);
            }
        }
        public int GetFighterCount() => fighters.Count;
        public int GetFighterAverageHP()
        {
            int averageHP = 0;
            foreach (Fighter f in fighters)
                averageHP += f.HP;
            return averageHP / GetFighterCount();
        }
        public int GetFighterAverageCP()
        {
            int averageCP = 0;
            foreach (Fighter f in fighters)
                averageCP += f.CP;
            return averageCP / GetFighterCount();
        }

        public int GetHighestLevelFighter() => fighters.OrderByDescending(x => x.Level).First().Level;
        public bool IsPartyAlive() => fighters.All(x => x.IsAlive());
        public Fighter Create(string name, int hp, int cp, int level, int xp, string faction)
        {
            Fighter f = new Fighter(name, hp, cp, level, xp, faction);
            fighters.Add(f);
            return f;
        }

        public void PrintFighters()
        {
            Console.Clear();
            for (int i = 0; i < fighters.Count; i++)
            {
                if (string.IsNullOrEmpty(fighters[i].Nickname))
                    Console.WriteLine($"{i}: {fighters[i].Name}");
                else
                    Console.WriteLine($"{i}: {fighters[i].Nickname} ({fighters[i].Name})");
            }
        }

        public void Select(int fighterIdx)
        {
            selectedFighter = fighters[fighterIdx];
        }
        public void Select(Fighter f)
        {
            selectedFighter = f;
        }
    }
}

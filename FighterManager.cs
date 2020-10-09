using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace simpleRPG
{
    class FighterManager
    {
        // gör en public gubbe som vilken klass som helst har tillgång till att läsa, men inte lägga ett värde på, och gör en samtidigt en lista av gubbar som då är partyt
        public Fighter selectedFighter { get; private set; }
        private List<Fighter> fighters = new List<Fighter>();

        //försöker ladda gubbarna från en fil, ifall den finns, och lägger in de i listan, ifall filen inte finns så skapar jag en enstaka default gubbe
        public void LoadFighters()
        {
            try
            {
                if (File.Exists("guys"))
                {
                    string[] buffer = File.ReadAllLines("guys");
                    foreach (string s in buffer)
                    {
                        if (!string.IsNullOrEmpty(s))
                        {
                            string[] fighterProperties = s.Split(';');
                            if (fighterProperties.Length > 6)
                            {
                                Create(fighterProperties[0], fighterProperties[1],
                                    int.Parse(fighterProperties[2]), int.Parse(fighterProperties[3]), int.Parse(fighterProperties[4]), int.Parse(fighterProperties[5]), fighterProperties[6]);
                            }
                            else
                            {
                                Create(fighterProperties[0], int.Parse(fighterProperties[1]),
                                    int.Parse(fighterProperties[2]), int.Parse(fighterProperties[3]), int.Parse(fighterProperties[4]), fighterProperties[5]);
                            }
                        }
                    }
                }     
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
        // sparar gubbarna till en fil
        public void SaveFighters()
        {
            try
            {
                List<string> buffer = new List<string>();
                foreach (Fighter f in fighters)
                {
                    if (string.IsNullOrEmpty(f.Nickname))
                        buffer.Add($"{f.Name};{f.HP};{f.CP};{f.Level};{f.XP};{f.Faction}");
                    else
                        buffer.Add($"{f.Name};{f.Nickname};{f.HP};{f.CP};{f.Level};{f.XP};{f.Faction}");
                }

                File.WriteAllLines("guys", buffer);
            }
            catch (Exception e)
            {
                Console.WriteLine("Caught exception: " + e.Message);
            }
        }
        // dependency injection
        public int GetFighterCount() => fighters.Count;
        // dependency injection
        public int GetFighterAverageHP()
        {
            int averageHP = 0;
            foreach (Fighter f in fighters)
                averageHP += f.HP;
            return averageHP / GetFighterCount();
        }
        // dependency injection
        public int GetFighterAverageCP()
        {
            int averageCP = 0;
            foreach (Fighter f in fighters)
                averageCP += f.CP;
            return averageCP / GetFighterCount();
        }
        // dependency injection
        public int GetHighestLevelFighter() => fighters.OrderByDescending(x => x.Level).First().Level;
        // en funktion för att skapa (från OOP-klassen) och lägga till gubbar i listan
        public Fighter Create(string name, int hp, int cp, int level, int xp, string faction)
        {
            Fighter f = new Fighter(name, hp, cp, level, xp, faction);
            fighters.Add(f);
            return f;
        }
        // detsamma som ovan fast med smeknamn
        public Fighter Create(string name, string nickname, int hp, int cp, int level, int xp, string faction)
        {
            Fighter f = new Fighter(name, nickname, hp, cp, level, xp, faction);
            fighters.Add(f);
            return f;
        }
        // skriver ut alla gubbar som finns med i, antigen med eller utan smeknamn, listan, bara att man börjar från 1 då det är mer användarvänligt 
        public void PrintFighters()
        {
            Console.Clear();
            for (int i = 0; i < fighters.Count; i++)
            {
                if (string.IsNullOrEmpty(fighters[i].Nickname))
                    Console.WriteLine($"{i + 1}: [{fighters[i].Faction}] {fighters[i].Name} | HP: {fighters[i].HP} | CP: {fighters[i].CP} | Level: {fighters[i].Level} | XP: {fighters[i].XP}");
                else
                    Console.WriteLine($"{i + 1}: [{fighters[i].Faction}] {fighters[i].Nickname} ({fighters[i].Name}) | HP: {fighters[i].HP} | CP: {fighters[i].CP} | Level: {fighters[i].Level} | XP: {fighters[i].XP}");
            }
        }
        // välj main gubbe med en index
        public void Select(int fighterIdx) => selectedFighter = fighters[fighterIdx];
        // välj main gubbe fast med en Fighter
        public void Select(Fighter f) => selectedFighter = f;
        // kolla ifall alla lever
        public bool IsPartyAlive() => fighters.Any(x => x.IsAlive());

        public bool IsFighterAlive() => selectedFighter.HP > 0;

        public Fighter FirstAvailableFighter() => fighters.First(x => x.HP > 0);
    }
}

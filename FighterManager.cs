using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace simpleRPG
{
    public class FighterManager
    {
        // gör en public gubbe som vilken klass som helst har tillgång till att läsa, men inte lägga ett värde på, och gör en samtidigt en lista av gubbar som då är partyt
        public Fighter SelectedFighter { get; private set; }
        private readonly List<Fighter> _fighters = new();

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
                        if (string.IsNullOrEmpty(s)) continue;
                        
                        string[] fighterProperties = s.Split(';');
                        Wearable w = new(fighterProperties[8], int.Parse(fighterProperties[9]), int.Parse(fighterProperties[10]));
                        Create(fighterProperties[0], fighterProperties[1], int.Parse(fighterProperties[2]),
                            int.Parse(fighterProperties[3]), int.Parse(fighterProperties[4]), int.Parse(fighterProperties[5]), 
                            int.Parse(fighterProperties[6]), fighterProperties[7], w);
        
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
                Select(Create("Default", 10, 10, 10, 1, 1, "Default faction", new Wearable("Literally nothing", 0, 1000)));
        }

        // sparar gubbarna till en fil
        public void SaveFighters()
        {
            IEnumerable<string> fighters = _fighters.Select(f => $"{f.Name};{f.Nickname};{f.HP};{f.MaxHP};{f.CP};{f.Level};{f.XP};{f.Faction};{f.Wearable.Name};{f.Wearable.HP};{f.Wearable.CP}");
            try
            {
                File.WriteAllLines("guys", fighters);
            }
            catch (Exception e)
            {
                Console.WriteLine("Caught exception: " + e.Message);
            }
        }
        
        public int GetFighterAverageHP() => (int)_fighters.Average(f => f.HP);
        public int GetFighterAverageCP() => (int)_fighters.Average(f => f.CP);
        public int GetHighestLevelFighter() => _fighters.OrderByDescending(x => x.Level).First().Level;
        private int GetFighterCount() => _fighters.Count;

        // en funktion för att skapa (från OOP-klassen) och lägga till gubbar i listan
        private Fighter Create(string name, int hp, int maxHP, int cp, int level, int xp, string faction, Wearable wearable)
        {
            Fighter f = new(name, hp, maxHP, cp, level, xp, faction, wearable);
            _fighters.Add(f);
            return f;
        }

        // detsamma som ovan fast med smeknamn
        private void Create(string name, string nickname, int hp, int maxHP, int cp, int level, int xp, string faction, Wearable wearable)
        {
            Fighter f = new(name, hp, maxHP, cp, level, xp, faction, wearable, nickname);
            _fighters.Add(f);
        }

        // skriver ut alla gubbar som finns med i, antigen med eller utan smeknamn, listan, bara att man börjar från 1 då det är mer användarvänligt 
        public void PrintFighters()
        {
            Console.Clear();
            for (int i = 0; i < _fighters.Count; i++)
            {
                Console.WriteLine(string.IsNullOrEmpty(_fighters[i].Nickname)
                    ? $"{i + 1}: [{_fighters[i].Faction}] {_fighters[i].Name} | HP: {_fighters[i].HP} | CP: {_fighters[i].CP} | Level: {_fighters[i].Level} | XP: {_fighters[i].XP}" +
                      $" | Wearable Name: {_fighters[i].Wearable.Name} | Wearable HP: {_fighters[i].Wearable.HP} | Wearable CP: {_fighters[i].Wearable.CP}"
                      
                    : $"{i + 1}: [{_fighters[i].Faction}] {_fighters[i].Nickname} ({_fighters[i].Name}) | HP: {_fighters[i].HP} | CP: {_fighters[i].CP} | Level: {_fighters[i].Level} | XP: {_fighters[i].XP}" +
                      $" | Wearable Name: {_fighters[i].Wearable.Name} | Wearable HP: {_fighters[i].Wearable.HP} | Wearable CP: {_fighters[i].Wearable.CP}");
            }
        }

        // välj main gubbe med en index
        public void Select(int fighterIdx) => SelectedFighter = _fighters[fighterIdx];

        // välj main gubbe fast med en Fighter
        public void Select(Fighter f) => SelectedFighter = f;

        // kolla ifall alla lever
        public bool IsPartyAlive() => _fighters.Any(x => x.IsAlive());

        // kolla ifall main gubben lever
        public bool IsMainFighterAlive() => SelectedFighter.HP > 0;

        // tar den första tillgängliga fightern som lever
        public Fighter FirstAvailableFighter() => _fighters.First(x => x.HP > 0);

        // healar alla fighters
        public void HealAllFighters()
        {
            _fighters.ForEach(x => x.HP = x.MaxHP);
            SaveFighters();
        }
    }
}

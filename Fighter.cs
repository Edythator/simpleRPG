namespace simpleRPG
{
    public class Fighter : FightingEntity
    {
        public int MaxHP { get; }
        public string Nickname { get; set; }
        public int Level { get; }
        public int XP { get; }
        public string Faction { get; }
        public Wearable Wearable { get; }

        public Fighter(string name, int hp, int maxHP, int cp, int level, int xp, string faction, Wearable wearable, string nickname = "")
        {
            Name = name;
            HP = hp;
            MaxHP = maxHP;
            Nickname = nickname;
            CP = cp;
            Level = level;
            XP = xp;
            Faction = faction;
            Wearable = wearable;
        }
        
        public override void Attack(Entity enemy, out int damage)
        {
            int itemBonus = Wearable.CP + Wearable.HP;
            int temp = 0;
            
            // implement scaling amount of crit derived from the total CP the character can do and chance of crit
            damage = Program.Rnd.Next(CP - 5, CP + 5);
            damage += itemBonus;
            if (damage > enemy.HP)
            {
                temp = damage;
                damage = enemy.HP;
            }
            enemy.HP -= damage;
            damage = temp;
        }

        // vi vill kunna veta namnet på den onda, ifall den har ett smeknamn eller inte, så vi skriver ut det här i båda fallen
        public string GetPrintableName() => !string.IsNullOrEmpty(Nickname) ? Nickname : Name;
    }
}

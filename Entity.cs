namespace simpleRPG
{
    public abstract class Entity
    {
        public string Name;
        public int HP;
        public int CP;
        public Entity(string name, int hp, int cp)
        {
            Name = name;
            HP = hp;
            CP = cp;
        }
        public bool IsAlive()
        {
            return HP > 0;
        }

        public void Attack(Entity enemy, out int damage)
        {
            // implement scaling amount of crit derived from the total CP the character can do and chance of crit
            damage = Program.Rnd.Next(CP - 2, CP + 2);
            if (damage > enemy.HP)
                damage = enemy.HP;

            enemy.HP -= damage;
        }
    }
}
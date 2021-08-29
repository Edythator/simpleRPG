namespace simpleRPG
{
    public abstract class FightingEntity : Entity
    {
        protected internal int CP { get; protected init; }

        public virtual void Attack(Entity enemy, out int damage)
        {
            // implement scaling amount of crit derived from the total CP the character can do and chance of crit
            damage = Program.Rnd.Next(CP - 2, CP + 2);
            if (damage > enemy.HP)
                damage = enemy.HP;

            enemy.HP -= damage;
        }
    }
}
namespace Entities
{
    using System;
    using System.Collections.Generic;

    public class EffectWeakness
    {
        private readonly Dictionary<AttackEffect, float> weaknesses = new Dictionary<AttackEffect, float>();
        
        public void AddOrUpdateWeaknesses(AttackEffect effect, float additionalDamage)
        {
            weaknesses[effect] = Math.Max(Math.Min(additionalDamage, 1), 0);
        }

        public Damage ApplyAdditionalDamage(Damage damage)
        {
            var amount = damage.Amount;
            foreach (var weakness in weaknesses)
            {
                if ((damage.Effect & weakness.Key) != 0)
                {
                    damage.Amount += amount * weakness.Value;
                }
            }
            return damage;
        } 
    }
}
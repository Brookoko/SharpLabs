namespace Entities
{
    using System;

    public class Hitbox : IHittable, IHealable
    {
        public float Hp { get; private set; }
        
        public EffectWeakness Weakness { get; }
        
        private float MaxHp { get; }
        
        public event Action OnDamage;
        public event Action OnDeath;
        
        public Hitbox(float hp, EffectWeakness weakness)
        {
            MaxHp = Hp = hp;
            Weakness = weakness;
        }
        
        public void TakeDamage(Damage damage)
        {
            if (Hp <= 0) return;
            damage = Weakness.ApplyAdditionalDamage(damage);
            Hp -= damage.Amount;
            OnDamage?.Invoke();
            if (Hp <= 0) OnDeath?.Invoke();
        }
        
        public void Heal(float healing)
        {
            Hp = Math.Min(Hp + healing, MaxHp);
        }
    }
}
namespace Entities
{
    using System;

    public struct Damage
    {
        public static Damage NoDamage => new Damage()
        {
            Effect = AttackEffect.NoEffect,
            Amount = 0
        };
        
        public AttackEffect Effect { get; set; }
        
        public float Amount { get; set; }
    }

    [Flags]
    public enum AttackEffect
    {
        NoEffect = 0,
        Fire = 1,
        Shock = 2,
        Tearing = 4,
        Piercing = 8,
        Freeze = 16
    }
}
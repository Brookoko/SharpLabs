namespace Entities.Weapons
{
    public class Spear : Weapon
    {
        public override float Damage => 5;
        
        public override AttackEffect Effect => AttackEffect.Piercing;
    }
}
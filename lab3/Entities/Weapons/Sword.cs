namespace Entities.Weapons
{
    public class Sword : Weapon
    {
        public override float Damage => 7;

        public override AttackEffect Effect => AttackEffect.NoEffect;
    }
}
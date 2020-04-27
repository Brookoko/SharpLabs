namespace Entities.Weapons
{
    public class Club : Weapon
    {
        public override float Damage => 3;

        public override AttackEffect Effect => AttackEffect.NoEffect;
    }
}
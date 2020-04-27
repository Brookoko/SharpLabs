namespace Entities.Weapons
{
    public class Fists : Weapon
    {
        public override float Damage => 3;

        public override AttackEffect Effect => AttackEffect.Tearing;
    }
}
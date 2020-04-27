namespace Entities.Weapons
{
    public class MagicWand : Weapon
    {
        public override float Damage => 4;

        public override AttackEffect Effect => AttackEffect.Fire | AttackEffect.Freeze | AttackEffect.Shock;
    }
}
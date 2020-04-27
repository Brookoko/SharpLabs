namespace Flow.Commands
{
    using DependencyInjection;
    using DependencyInjection.Commands;
    using Entities;
    using Entities.StatesTypes.SMTypes;
    using Entities.Types;
    using Entities.Weapons;

    public class SetupEntityVariantsCommand : Command
    {
        [Inject]
        public EntityFactory EntityFactory { get; set; }

        [Inject]
        public IInjectionBinder Binder { get; set; }
        
        public override void Execute()
        {
            EntityFactory.Register("orc-club", Orc(new Club()));
            EntityFactory.Register("orc-fists", Orc(new Fists()));
            EntityFactory.Register("knight-sword", Knight(new Sword(), MovementType.Walk));
            EntityFactory.Register("knight-spear", Knight(new Spear(), MovementType.Walk));
            EntityFactory.Register("knight-spear-fly", Knight(new Spear(), MovementType.Fly));
            EntityFactory.Register("healer", Healer());
            EntityFactory.Register("magician", Magician());
        }

        private Entity Orc(Weapon weapon)
        {
            var weakness = new EffectWeakness();
            weakness.AddOrUpdateWeaknesses(AttackEffect.Fire, 0.15f);
            weakness.AddOrUpdateWeaknesses(AttackEffect.Shock, 0.1f);
            weakness.AddOrUpdateWeaknesses(AttackEffect.Freeze, 0.05f);
            var hitbox = new Hitbox(15, weakness);
            var sm = (EntityStateMachine) Binder.Inject(new DefaultStateMachine());
            return new Orc(hitbox, sm)
            {
                Weapon = weapon,
                MovementType = MovementType.Walk
            };
        }

        private Entity Knight(Weapon weapon, MovementType movement)
        {
            var weakness = new EffectWeakness();
            weakness.AddOrUpdateWeaknesses(AttackEffect.Tearing, 0.15f);
            weakness.AddOrUpdateWeaknesses(AttackEffect.Shock, 0.1f);
            var hitbox = new Hitbox(15, weakness);
            var sm = (EntityStateMachine) Binder.Inject(new DefaultStateMachine());
            return new Knight(hitbox, sm)
            {
                Weapon = weapon,
                MovementType = movement
            };
        }

        private Entity Healer()
        {
            var weakness = new EffectWeakness();
            weakness.AddOrUpdateWeaknesses(AttackEffect.Tearing, 0.3f);
            weakness.AddOrUpdateWeaknesses(AttackEffect.Piercing, 0.3f);
            var hitbox = new Hitbox(15, weakness);
            var sm = (EntityStateMachine) Binder.Inject(new HealerStateMachine());
            return new Healer(hitbox, sm)
            {
                MovementType = MovementType.Walk
            };
        }

        private Entity Magician()
        {
            var weakness = new EffectWeakness();
            weakness.AddOrUpdateWeaknesses(AttackEffect.Tearing, 0.1f);
            weakness.AddOrUpdateWeaknesses(AttackEffect.Piercing, 0.1f);
            var hitbox = new Hitbox(15, weakness);
            var sm = (EntityStateMachine) Binder.Inject(new HealerStateMachine());
            return new Magician(hitbox, sm)
            {
                Weapon = new MagicWand(),
                MovementType = MovementType.Walk
            };
        }
    }
}
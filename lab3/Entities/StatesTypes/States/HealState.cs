namespace Entities.StatesTypes.States
{
    using DependencyInjection;
    using Flow;
    using Types;

    public class HealState : EntityState
    {
        [Inject]
        public EntityHealing EntityHealing { get; set; }

        private Entity lastHealedEntity;
        
        public HealState(Entity entity) : base(entity)
        {
        }

        public override void Prepare()
        {
            EntityHealing.AddListener(OnEntityHealing);
        }

        private void OnEntityHealing(Entity healingEntity, Entity healedEntity)
        {
            if (healingEntity == Entity) lastHealedEntity = healedEntity;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            ((Healer) Entity).Heal(lastHealedEntity);
        }

        public override void OnExit()
        {
        }
    }
}
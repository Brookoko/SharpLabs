namespace Entities.StatesTypes.Rules
{
    using DependencyInjection;
    using Flow;
    using SM.Rules;

    public class HealRule : IRule
    {
        [Inject]
        public EntityHealing EntityHealing { get; set; }
        
        private readonly Entity entity;
        private bool isFulfilled;
        
        public HealRule(Entity entity)
        {
            this.entity = entity;
        }
        
        public void Prepare()
        {
            EntityHealing.AddListener(OnEntityHealing);
        }

        private void OnEntityHealing(Entity healingEntity, Entity healedEntity)
        {
            if (entity != healingEntity) return;
            isFulfilled = true;
        }

        public bool IsFulfilled()
        {
            return isFulfilled;
        }

        public void Reset()
        {
            isFulfilled = false;
        }
    }
}
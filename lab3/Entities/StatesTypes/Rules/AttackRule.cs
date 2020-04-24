namespace Entities.StatesTypes.Rules
{
    using DependencyInjection;
    using Flow;
    using SM.Rules;

    public class AttackRule : IRule
    {
        [Inject]
        public EntityAttacking EntityAttacking { get; set; }
        
        private readonly Entity entity;
        private bool isFulfilled;
        
        public AttackRule(Entity entity)
        {
            this.entity = entity;
        }
        
        public void Prepare()
        {
            EntityAttacking.AddListener(OnEntityAttacking);
        }

        private void OnEntityAttacking(Entity attackingEntity, Entity beatingEntity)
        {
            if (entity != attackingEntity) return;
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
namespace Entities.StatesTypes.Rules
{
    using DependencyInjection;
    using Environment;
    using Flow;
    using SM.Rules;

    public class MoveRule : IRule
    {
        [Inject]
        public EntityMoving EntityMoving { get; set; }
        
        private readonly Entity entity;
        private bool isFulfilled;
        
        public MoveRule(Entity entity)
        {
            this.entity = entity;
        }
        
        public void Prepare()
        {
            EntityMoving.AddListener(OnEntityMoving);
        }

        private void OnEntityMoving(Entity movingEntity, Position position)
        {
            if (entity != movingEntity) return;
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
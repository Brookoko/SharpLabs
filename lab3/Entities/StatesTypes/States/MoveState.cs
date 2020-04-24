namespace Entities.StatesTypes.States
{
    using DependencyInjection;
    using Environment;
    using Flow;

    public class MoveState : EntityState
    {
        [Inject]
        public EntityMoving EntityMoving { get; set; }

        private Position lastTarget;
        
        public MoveState(Entity entity) : base(entity)
        {
        }

        public override void Prepare()
        {
            EntityMoving.AddListener(OnEntityMoving);
        }

        private void OnEntityMoving(Entity movingEntity, Position target)
        {
            if (movingEntity == Entity) lastTarget = target;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Entity.Move(lastTarget);
        }
    }
}
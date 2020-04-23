namespace Entities.EntitySM
{
    using SM.States;

    public class EntityState : State
    {
        private Entity entity;
        
        public EntityState(Entity entity)
        {
            this.entity = entity;
        }
        
        public override void OnEnter()
        {
        }

        public override void OnExit()
        {
        }

        public override void Update()
        {
        }
    }
}
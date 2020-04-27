namespace Entities.StatesTypes.States
{
    using DependencyInjection;
    using Flow;

    public class AttackState : EntityState
    {
        [Inject]
        public EntityAttacking EntityAttacking { get; set; }

        private Entity lastBeatedEntity;
        
        public AttackState(Entity entity) : base(entity)
        {
        }

        public override void Prepare()
        {
            EntityAttacking.AddListener(OnEntityAttacking);
        }

        private void OnEntityAttacking(Entity attackingEntity, Entity beatingEntity)
        {
            if (attackingEntity == Entity) lastBeatedEntity = beatingEntity;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            lastBeatedEntity.TakeDamage(Entity.Attack(lastBeatedEntity));
        }
    }
}
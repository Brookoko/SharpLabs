namespace Entities.StatesTypes.SMTypes
{
    using DependencyInjection;
    using Rules;
    using SM.Rules;
    using SM.States;
    using States;

    public class DefaultStateMachine : EntityStateMachine
    {
        [Inject]
        public IInjectionBinder Binder { get; set; }
        
        public override void Prepare(Entity entity)
        {
            var idle = (EntityState) Binder.Inject(new IdleState(entity));
            var attack = (EntityState) Binder.Inject(new AttackState(entity));
            var move = (EntityState) Binder.Inject(new MoveState(entity));
            
            idle.To(move).If((IRule) Binder.Inject(new MoveRule(entity)));
            idle.To(attack).If((IRule) Binder.Inject(new AttackRule(entity)));
            move.To(idle).If(new WaitRule(100));
            attack.To(idle).If(new WaitRule(100));
        }
    }
}
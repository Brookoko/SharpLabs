namespace Entities.StatesTypes.SMTypes
{
    using Rules;
    using SM.Rules;
    using SM.States;
    using States;

    public class HealerStateMachine : EntityStateMachine
    {
        public override void Prepare(Entity entity)
        {
            var idle = (EntityState) Binder.Inject(new IdleState(entity));
            var attack = (EntityState) Binder.Inject(new AttackState(entity));
            var move = (EntityState) Binder.Inject(new MoveState(entity));
            var heal = (EntityState) Binder.Inject(new HealState(entity));
 
            idle.To(move).If((IRule) Binder.Inject(new MoveRule(entity)));
            idle.To(attack).If((IRule) Binder.Inject(new AttackRule(entity)));
            idle.To(heal).If((IRule) Binder.Inject(new HealRule(entity)));
            
            heal.To(idle).If(new InstantRule());
            move.To(idle).If(new InstantRule());
            attack.To(idle).If(new InstantRule());
            
            Init(idle);
        }
    }
}
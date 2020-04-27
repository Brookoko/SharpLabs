namespace Entities.StatesTypes.Rules
{
    using SM.Rules;

    public class InstantRule : IRule
    {
        public void Prepare()
        {
        }

        public bool IsFulfilled()
        {
            return true;
        }

        public void Reset()
        {
        }
    }
}
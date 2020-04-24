namespace Entities.StatesTypes.Rules
{
    using System.Threading;
    using System.Threading.Tasks;
    using SM.Rules;

    public class WaitRule : IRule
    {
        private float seconds;
        private Task task;
        private CancellationTokenSource source;
        
        public WaitRule(float seconds)
        {
            this.seconds = seconds;
        }
        
        public async void Prepare()
        {
            source = new CancellationTokenSource();
            task = Task.Delay((int) (seconds * 1000), source.Token);
            await task.ContinueWith(t => { });
        }
        
        public bool IsFulfilled()
        {
            return task.IsCompleted;
        }
        
        public void Reset()
        {
            source.Cancel();
            task.Dispose();
            task = null;
        }
    }
}
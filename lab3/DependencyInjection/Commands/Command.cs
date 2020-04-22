namespace BallDefence.Injection.Commands
{
    using System.Threading.Tasks;
    
    public abstract class Command
    {
        public bool IsBlocking { get; private set; }

        public bool IsFailed { get; private set; }
        
        public TaskCompletionSource<bool> ReleaseTask;

        public abstract void Execute();

        /// <summary>
        /// Stop sequence execution
        /// </summary>
        protected void Retain()
        {
            IsBlocking = true;
            ReleaseTask = new TaskCompletionSource<bool>(false);
        }

        /// <summary>
        /// Release resources and continue sequence execution
        /// </summary>
        protected void Release()
        {
            IsBlocking = false;
            ReleaseTask.SetResult(true);
        }
        
        /// <summary>
        /// Stop executing sequence after command is finished
        /// </summary>
        protected void Fail()
        {
            IsFailed = true;
        }
    }
}
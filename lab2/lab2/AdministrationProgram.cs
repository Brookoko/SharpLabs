namespace Administration
{
    using ConsoleUtils;
    using ConsoleUtils.Types;
    using Store;

    public class AdministrationProgram : ConsoleProgram
    {
        public override void Init()
        {
            var store = new AdministrationsStore();
            var proxy = new StoreProxy(store);
            var start = new StartOptions(proxy);
            start.AddOption("--exit", parameters => StopProcessingInput());
            options = start;
            base.Init();
        }
    }
}
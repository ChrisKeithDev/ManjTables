using System;
using System.Windows.Threading;

namespace ManjTables.Reports.GUI
{
    public class DispatcherService
    {
        private readonly Dispatcher _dispatcher;

        public DispatcherService(Dispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public void Invoke(Action action)
        {
            if (_dispatcher.CheckAccess())
            {
                action();
            }
            else
            {
                _dispatcher.Invoke(action);
            }
        }
    }
}
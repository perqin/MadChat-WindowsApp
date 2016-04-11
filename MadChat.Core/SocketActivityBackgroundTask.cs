using System.Diagnostics;
using Windows.ApplicationModel.Background;

namespace MadChat.Tasks {
    class SocketActivityBackgroundTask {
        public sealed class ExampleBackgroundTask : IBackgroundTask {
            public async void Run(IBackgroundTaskInstance taskInstance) {
                BackgroundTaskDeferral deferral = taskInstance.GetDeferral();
                //TODO: Async codes
                Debug.WriteLine("HELLO");
                deferral.Complete();
            }
        }
    }
}

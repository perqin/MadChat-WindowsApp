using MadChat.ViewModels;
using MadChat.Views;

using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Background;
using Windows.Networking.Sockets;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace MadChat {
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        public static ContactsViewModel ContactsVM = new ContactsViewModel();
        public static UserViewModel UserVM = new UserViewModel();
        public static IBackgroundTaskRegistration Task = null;
        public static StreamSocketListener TcpListener = null;

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            Microsoft.ApplicationInsights.WindowsAppInitializer.InitializeAsync(
                Microsoft.ApplicationInsights.WindowsCollectors.Metadata |
                Microsoft.ApplicationInsights.WindowsCollectors.Session);
            InitializeComponent();
            Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                rootFrame.Navigate(typeof(LoginPage), e.Arguments);
            }
            // Ensure the current window is active
            Window.Current.Activate();

            registerSocketTask();
        }

        async void registerSocketTask() {
            //var socketTaskBuilder = new BackgroundTaskBuilder();
            //socketTaskBuilder.Name = "SocketActivityBackgroundTask";
            //socketTaskBuilder.TaskEntryPoint = _backgroundTaskEntryPoint;
            //var trigger = new SocketActivityTrigger();
            //socketTaskBuilder.SetTrigger(trigger);
            //task = socketTaskBuilder.Register();

            var status = await BackgroundExecutionManager.RequestAccessAsync();
            if (status == BackgroundAccessStatus.Unspecified || status == BackgroundAccessStatus.Denied) {
                return;
            }
            //try {
            foreach (var current in BackgroundTaskRegistration.AllTasks) {
                if (current.Value.Name == "SocketActivityBackgroundTask") {
                    //Task = current.Value;
                    //break;
                    current.Value.Unregister(true);
                }
            }
            if (Task == null) {
                var socketTaskBuilder = new BackgroundTaskBuilder();
                socketTaskBuilder.Name = "SocketActivityBackgroundTask";
                socketTaskBuilder.TaskEntryPoint = "MadChat.Core.SocketActivityBackgroundTask";
                //var trigger = new SocketActivityTrigger();
                var trigger = new TimeTrigger(15, false);
                socketTaskBuilder.SetTrigger(trigger);
                Task = socketTaskBuilder.Register();
            }
            //TcpListener = new StreamSocketListener();
            //TcpListener.EnableTransferOwnership(Task.TaskId, SocketActivityConnectedStandbyAction.Wake);
            //TcpListener.ConnectionReceived += TcpListener_ConnectionReceived;

            //SocketActivityInformation socketInformation;
            //if (SocketActivityInformation.AllSockets.TryGetValue(socketId, out socketInformation)) {
            //    // Application can take ownership of the socket and make any socket operation
            //    // For sample it is just transfering it back.
            //    socket = socketInformation.StreamSocket;
            //    socket.TransferOwnership(socketId);
            //    socket = null;
            //    rootPage.NotifyUser("Connected. You may close the application", NotifyType.StatusMessage);
            //    TargetServerTextBox.IsEnabled = false;
            //    ConnectButton.IsEnabled = false;
            //}
            //} catch (Exception exception) {
            //rootPage.NotifyUser(exception.Message, NotifyType.ErrorMessage);
            //}
        }

        private void TcpListener_ConnectionReceived(StreamSocketListener sender, StreamSocketListenerConnectionReceivedEventArgs args) {
            //TODO
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}

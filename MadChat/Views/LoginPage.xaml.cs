using System;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MadChat.Views {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void StartChatting_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e) {
            string nickname = UserNicknameTextBox.Text;
            if (nickname == "") {
                //TODO: Alert that nickname is required
            } else {
                App.UserVM.User.Nickname = nickname;
                await App.TcpListener.BindEndpointAsync(new Windows.Networking.HostName("45.55.21.131"), "23456");
            }
        }
    }
}

using MadChat.Models;
using System.Collections.ObjectModel;

namespace MadChat.ViewModels {
    public class ContactsViewModel {
        public ObservableCollection<UserModel> OnlineUserList {
            get {
                return onlineUserList;
            }
        }

        private ObservableCollection<UserModel> onlineUserList = new ObservableCollection<UserModel>();
    }
}

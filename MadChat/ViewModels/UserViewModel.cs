using MadChat.Models;

namespace MadChat.ViewModels {
    public class UserViewModel {
        public UserModel User {
            get {
                return user;
            }
        }

        private UserModel user = new UserModel();
    }
}

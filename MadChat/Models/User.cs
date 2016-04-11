namespace MadChat.Models {
    public class UserModel {
        public string UserId {
            get {
                return userId;
            }
            set {
                userId = value;
            }
        }

        public string Nickname {
            get {
                return nickname;
            }
            set {
                nickname = value;
            }
        }

        private string userId = "";
        private string nickname = "";
    }
}

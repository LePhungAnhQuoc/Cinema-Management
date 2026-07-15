using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_WPF_C1_B1
{
    public class SessionManager
    {
        private static SessionManager _instance;
        public static SessionManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SessionManager();
                }
                return _instance;
            }
        }

        // Private constructor to enforce Singleton pattern
        private SessionManager() { }

        // Holds the logged-in user details. If null, the user is NOT logged in.
        public Account CurrentUser { get; private set; }

        // Helper property to check login status instantly
        public bool IsLoggedIn => CurrentUser != null;

        public void Login(Account user)
        {
            CurrentUser = user;
        }

        public void Logout()
        {
            CurrentUser = null;
        }
    }
}

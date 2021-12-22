using System;
using System.Collections.Generic;
using System.Text;

namespace Practice11
{
    public class User
    {
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public string[] Roles { get; set; }
    }
}

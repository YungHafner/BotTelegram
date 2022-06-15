using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BotTelegram
{
    class Users
    {
        // юзеров можно запомнить в какую-нить бд или json-файл
        Dictionary<long, User> users = new Dictionary<long, User>();

        public bool HasUser(long Id) => users.ContainsKey(Id);
        public User GetUser(long Id) => users[Id];
        public User AddUser(long Id, string name)
        {
            if (HasUser(Id))
                return null;
            var user = new User { Id = Id, UserName = name };
            users.Add(Id, user);
            return user;
        }
    }
}

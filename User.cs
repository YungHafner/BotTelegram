using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotTelegram
{
    public class User
    {
        public long Id { get; internal set; }
        public string UserName { get; internal set; }

        public StateMachine State { get; set; }
        public string PhoneNumber { get; internal set; }

        public User()
        {
            State = new StateMachine(this, new DefaultState());
        }
    }
}

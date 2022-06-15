using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace BotTelegram
{
    public abstract class State
    {
        internal abstract Task UpdateHandler(User user, Telegram.Bot.ITelegramBotClient arg1, Update arg2);
    }
}

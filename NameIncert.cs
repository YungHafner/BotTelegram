using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BotTelegram
{
    internal class NameIncert : State
    {
        internal override async Task UpdateHandler(User user, ITelegramBotClient arg1, Update arg2)
        {
            if (arg2.Message == null)
                return;
            else
            {
                string name = arg2.Message.Text;
                int rootUser = 1254210176;
                await arg1.SendTextMessageAsync(rootUser, "Имя клиента " + name);
                await arg1.SendTextMessageAsync(arg2.Message.Chat.Id, "Прекрасно, " + name + ", мы свяжемся с вами в течение минуты");
                user.State.SetState(new DefaultState());
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BotTelegram
{
    internal class InfoState : State
    {
        internal override async Task UpdateHandler(User user, ITelegramBotClient arg1, Update arg2)
        {
            if (arg2.Message == null)
                return;
            if (arg2.Message.Text == "/buy")
            {
                
                await arg1.SendTextMessageAsync(arg2.Message.Chat.Id,
                    "Отлично, введите свой номер телефона начиная с восьмёрки для уточнения условий заказа");
                user.State.SetState(new NumbetState());
            }
        }
    }
}

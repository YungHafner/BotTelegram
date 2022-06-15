using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace BotTelegram
{
    public class DefaultState : State
    {
        internal override async Task UpdateHandler(User user, Telegram.Bot.ITelegramBotClient arg1, Update arg2)
        {
            if (arg2.Message == null)
            return;

            if (arg2.Message.Text == "/info")
            {
                await arg1.SendTextMessageAsync(arg2.Message.Chat.Id, "'/start' Начините работу бота. Данный бот содержит навигационные подсказки для каждого пользователя. \n Для вас с заботой ♥");
            }

            if (arg2.Message.Text == "/start")
            {
                await arg1.SendTextMessageAsync(arg2.Message.Chat.Id, "Приветствуем вас в Sushimi Hub.\n Чтобы заказать суши введите '/buy'");
                user.State.SetState(new InfoState()); // тут указываем класс-обработчик новых команд, таких классов может быть дофига
                Console.WriteLine(user.Id);
            }
                
            
        }

               
    }
}

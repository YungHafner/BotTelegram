using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
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
                await arg1.SendTextMessageAsync(arg2.Message.Chat.Id, "/start запустите бота.\n Данный бот содержит навигационные подсказки для каждого пользователя.\n /menu - актуальное меню \n Для вас с заботой ♥");
            }

            if(arg2.Message.Text == "/menu")

            {
                await arg1.SendTextMessageAsync(arg2.Message.Chat.Id, "Наше актуальное меню. ");
                await arg1.SendPhotoAsync(arg2.Message.Chat.Id, photo: "https://avatars.mds.yandex.net/get-altay/4475806/2a00000177b2a3e551e425bab10d5587eb11/XXL",caption: "Сет суперсемейка. Цена 1200 руб." ,parseMode: ParseMode.Html, cancellationToken: default);
                await arg1.SendPhotoAsync(arg2.Message.Chat.Id, photo: "https://www.xarakiri.ru/upload/iblock/0b3/0b3a944d186da9777b481a243a99482b.jpg", caption: "Сет самурай. Цена 1400 руб.", parseMode: ParseMode.Html, cancellationToken: default);
            }

            if (arg2.Message.Text == "/start")
            {
                await arg1.SendTextMessageAsync(arg2.Message.Chat.Id, "Приветствуем вас в Sushimi Hub.\n Чтобы заказать суши введите /buy");
                user.State.SetState(new InfoState()); // тут указываем класс-обработчик новых команд, таких классов может быть дофига
                Console.WriteLine(user.Id);
            }
                
            
        }

               
    }
}

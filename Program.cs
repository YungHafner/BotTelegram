using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;

namespace BotTelegram
{

    class Program
    {
        //t.me/Sample1135_bot
        static TelegramBotClient BotClient = new TelegramBotClient("5431488779:AAHsVNNUEU8pOQo2uPnporpUhzTZNbSUWcw");
        static CancellationTokenSource cts = new CancellationTokenSource();

        static Users users = new Users();
        static void Main(string[] args)
        {
            var test = BotClient.GetMeAsync().Result;
            Console.WriteLine(test.Username);
            var opt = new ReceiverOptions
            {
                AllowedUpdates = { } // receive all update types
            };
            BotClient.StartReceiving(UpdateHandler, HandleErrorAsync, opt, cts.Token);

            Console.WriteLine("Hello World BOT!");
            Console.ReadLine();
        }


        private static async Task UpdateHandler(ITelegramBotClient arg1,
            Telegram.Bot.Types.Update arg2,
            CancellationToken arg3)
        {
            long userId = -1;
            if (arg2.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
                userId = arg2.Message.From.Id;
            else if (arg2.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
            {
                userId = arg2.CallbackQuery.From.Id;
            }

            if (users.HasUser(userId))
                users.GetUser(userId).State.UpdateHandler(arg1, arg2);
            else
                users.AddUser(userId, arg2.Message?.From.Username).State.UpdateHandler(arg1, arg2);

        }

        private static async Task HandleErrorAsync(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            Console.WriteLine($"беда {arg2.Message}");
        }
    }
}
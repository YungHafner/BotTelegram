using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BotTelegram
{
    internal class NumbetState : State
    {
        internal override async Task UpdateHandler(User user, ITelegramBotClient arg1, Update arg2)
        {
            if (arg2.Message == null)
                return;

            if (arg2.Message.Text.StartsWith("8") && arg2.Message.Text.Length == 11)
            {
                string number = arg2.Message.Text;

                parsePhone();

                int rootUser = 1254210176;
                await arg1.SendTextMessageAsync(rootUser, "Номер телефона заказчика суши " + number);

                await arg1.SendTextMessageAsync(arg2.Message.Chat.Id, "Прекрасно, как нам вас называть?");
                user.State.SetState(new NameIncert());
            }
            else
            {
                await arg1.SendTextMessageAsync(arg2.Message.Chat.Id, "Некорректный номер телефона.\n Пожалуйста введите номер телефона без пробелов, скобок, тире.\n Номер должен начинаться с 8 и должен состоять из 11 цыфр");
                user.State.SetState(new InfoState());
            }
        }

        public ObservableCollection<User> parsePhone()
        {
            string query = $" ";

            ObservableCollection<User> result = new ObservableCollection<User>();
            var MySqlDB = DBMySqlUtils.GetDB();
            if (MySqlDB.OpenConnection())
            {
                using (MySqlCommand mc = new MySqlCommand(query, MySqlDB.conn))
                using (MySqlDataReader dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        result.Add(new User
                        {
                            Id = dr.GetInt32("id"),
                            UserName = dr.GetString("userPhone"),

                        });
                    }
                }
                MySqlDB.CloseConnection();
            }

            return result;
        }
    }
}

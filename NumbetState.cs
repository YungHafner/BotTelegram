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
                id = 1;
                ObservableCollection<User> parsePhone()
                {
                    
                    string query = $"INSERT INTO phonebook (  numberUser) VALUES( '{number}')";

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
                                    UserName = dr.GetString("numberUser"),

                                });
                            }
                        }
                        MySqlDB.CloseConnection();
                    }

                    return result;
                }

                int rootUser = 1254210176;
                await arg1.SendTextMessageAsync(rootUser, number + " номер телефона заказчика суши. " );

                await arg1.SendTextMessageAsync(arg2.Message.Chat.Id, "Прекрасно, как нам вас называть?");
                user.State.SetState(new NameIncert());
            }
            else
            {
                await arg1.SendTextMessageAsync(arg2.Message.Chat.Id, "Для повторного набора номера нажмите  /buy.Некорректный номер телефона.\n Пожалуйста введите номер телефона без пробелов, скобок, тире.\n Номер должен начинаться с 8 и должен состоять из 11 цыфр");
                user.State.SetState(new InfoState());
            }
        }

       
    }
}

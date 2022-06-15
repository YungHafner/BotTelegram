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
    internal class PhoneSushi : State
    {
            public ObservableCollection<User> Users { get; set; }
        internal override async Task UpdateHandler(User user, ITelegramBotClient arg1, Update arg2)
        {
            if (arg2.Message == null)
                return;
            else
            {
                int rootUser = 1254210176;
                string name = arg2.Message.Text;
                await arg1.SendTextMessageAsync(rootUser, "Имя клиента" + name);
                await arg1.SendTextMessageAsync(arg2.Message.Chat.Id, "Прекрасно, " + name + ", мы свяжемся с вами в течение минуты");
                getPhone();
            }
        }
        public ObservableCollection<User> getPhone()
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

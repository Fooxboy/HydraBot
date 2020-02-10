using System;
using System.Linq;
using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using HydraBot.Models;

namespace HydraBot.Commands.Admin
{
    public class PromocodeGenerateCommand:INucleusCommand
    {
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            if (!Main.Api.Users.CheckUser(msg))
            {
                var kb2 = new KeyboardBuilder(bot);
                kb2.AddButton("➕ Зарегистрироваться", "start");
                sender.Text("❌ Вы не зарегистрированы, нажмите на кнопку ниже, чтобы начать", msg.ChatId, kb2.Build());
                return;
            }
            
            var user = Main.Api.Users.GetUser(msg);
            if(user.Access < 6 )
            {
                sender.Text("❌ Вам недоступна эта команда!", msg.ChatId);
                return;
                if (user.VkId != 308764786)
                {
                   
                }
            }

            try
            {
                var array = msg.Text.Split(" ");

                var money = array[1].ToLong();
                var donateMoney = array[2].ToLong();
                var experience = array[3].ToLong();

                var promocode = new Promocode();

                var promo = string.Empty;
                using (var db = new Database())
                {
                    promocode.Id = db.Promocodes.Count() + 1;
                    promocode.Text = new Random().Next(11111, 999999999).ToString();
                    promocode.Money = money;
                    promocode.DonateMoney = donateMoney;
                    promocode.Experience = experience;
                    promocode.IsActivate = false;

                    db.Promocodes.Add(promocode);
                    db.SaveChanges();

                    promo = promocode.Text;
                }

                sender.Text($"✔ Промокод сгенерирован: {promo}", msg.ChatId);
            }
            catch (Exception e)
            {
                sender.Text(e.ToString(), msg.ChatId);
            }
            
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }

        public string Command => "promocreate";
        public string[] Aliases => new string[] {"prcr", "промосоздать"};
    }
}
using System.Linq;
using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using HydraBot.Models;

namespace HydraBot.Commands.Admin
{
    public class GetIdCommand:INucleusCommand
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
            
            long userId;
            var api = Main.Api;
            var userSend = api.Users.GetUser(msg);
            if (userSend.Access < 6)
            {
                sender.Text("❌ Вам недоступна эта команда!", msg.ChatId);
                return;
            }


            var vkId = msg.Text.Split(" ")[1].ToLong();

            User user;
            using (var db = new Database())
            {
                user = db.Users.Single(u => u.VkId == vkId);
            }

            var text = string.Empty;

            if (user is null) text = "❌ Пользователя с таким индентификатором ВКонтакте нет.";
            else text = $"✔ Пользователь  {user.Name}: ID= {user.Id}, Уровень={user.Level}";
            
            sender.Text(text, msg.ChatId);

        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }

        public string Command => "id";
        public string[] Aliases => new string[] {"айди"};
    }
}
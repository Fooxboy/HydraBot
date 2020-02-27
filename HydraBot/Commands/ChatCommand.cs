using System.Collections.Generic;
using System.Linq;
using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using HydraBot.Models;

namespace HydraBot.Commands
{
    public class ChatCommand:INucleusCommand
    {
        public static List<ChatTemp> ChatsTemp { get; set; }
        
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            var user = Main.Api.Users.GetUser(msg);
            var phoneNumber = msg.Payload.Arguments[0];
            var text = "✒ Чат с пользователем ";
            var kb = new KeyboardBuilder(bot);
            using (var db = new Database())
            {
                var gar = db.Garages.SingleOrDefault(g => g.PhoneNumber == phoneNumber);
                if (gar is null)
                {
                    kb.AddButton("✉ К сообщениям", "messages");
                    sender.Text("❌ Пользователь не найден. Возможно он сменил номер телефона", msg.ChatId, kb.Build());
                    return;
                }

                
                var garage = db.Garages.Single(g => g.UserId == user.Id);
                var usr = db.Users.Single(u => u.Id == gar.UserId);
                text += $"[{usr.Prefix}] {usr.Name}";
                ChatsTemp.Add(new ChatTemp() {UserId =  user.Id, ChatUserId = usr.Id, NumberUser = garage.PhoneNumber});

            }

            text += "\n❓ Напишите сообщение пользователю";
            UsersCommandHelper.GetHelper().Add("chatSend", user.Id);
            kb.AddButton("✉ К сообщениям", "messages");
            sender.Text(text, msg.ChatId, kb.Build());
        }

        public static string Send(string text, User user, IMessageSenderService sender)
        {
            var chatTemp = ChatsTemp.Single(u => u.UserId == user.Id);

            var usrSend = Main.Api.Users.GetUserFromId(chatTemp.ChatUserId);
            sender.Text($"✉ ({chatTemp.NumberUser}) {user.Name}: {text}", usrSend.VkId);
            return "✔ Сообщение отправлено";
        }

        public void Init(IBot bot, ILoggerService logger)
        {
            ChatsTemp = new List<ChatTemp>();
        }

        public string Command => "chat";
        public string[] Aliases => new string[0];
    }
}
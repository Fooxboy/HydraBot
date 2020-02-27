using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using HydraBot.Models;

namespace HydraBot.Commands
{
    public class NewChatCommand:INucleusCommand
    {
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            var user = Main.Api.Users.GetUser(msg);
            var kb = new KeyboardBuilder(bot);
            kb.AddButton("✉ К сообщениям", "messages");
            var text = "✒ Укажите номер телефона:";
            UsersCommandHelper.GetHelper().Add("newChatCreate", user.Id);

            sender.Text(text, msg.ChatId);
        }

        public static string CreateChat(string number, User user, IMessageSenderService sender, IBot bot)
        {
            using (var db = new Database())
            {
                if (db.Garages.Any(g => g.PhoneNumber == number))
                {
                    var gar = db.Garages.Single(g => g.PhoneNumber == number);
                    
                    if (db.MessagesInfo.Any(m => m.UserId == user.Id))
                    {
                        var info = db.MessagesInfo.Single(m => m.UserId == user.Id);
                        var lastMessages = info.LastMessageUsers;
                        lastMessages += $"{gar.UserId};";
                    }
                    else
                    {
                        db.MessagesInfo.Add(new MessageInfo()
                            {UserId = user.Id, LastMessageText = "", LastMessageUsers = $"{gar.UserId}"});
                    }
                    
                    db.SaveChanges();

                }
                else return "❌ Пользователя с таким номером телефона нет";
            }

            Task.Run(() =>
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));
                var kb = new KeyboardBuilder(bot);
                kb.AddButton("✒ Перейти к чату", "chat", new List<string>() {number});
                sender.Text("❓ Для того, чтобы перейти в чат, нажмите на кнопку ниже.", user.VkId, kb.Build());
            });

            return "⌛ Подождите, создаем чат...";
        }
        
        public void Init(IBot bot, ILoggerService logger)
        {
        }

        public string Command => "newChat";
        public string[] Aliases => new string[0];
    }
}
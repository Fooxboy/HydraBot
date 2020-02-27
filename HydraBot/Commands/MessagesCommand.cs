using System.Collections.Generic;
using System.Linq;
using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using HydraBot.Models;

namespace HydraBot.Commands
{
    public class MessagesCommand:INucleusCommand
    {
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            var user = Main.Api.Users.GetUser(msg);
            var text = "✉ Ваши недавние диалоги:";
            var kb = new KeyboardBuilder(bot);
            kb.AddButton("➕ Новый чат", "newChat");
            kb.AddLine();
            using (var db = new Database())
            {
                var messagesInfo = db.MessagesInfo.SingleOrDefault(m => m.UserId == user.Id);
                if (messagesInfo is null) text += "\n ✒ У Вас нет недавних чатов. Напишите кому нибудь!";
                else
                {
                    var userIds = messagesInfo.LastMessageUsers.Split(";");
                    
                    foreach (var userId in userIds)
                    {
                        if(userId == "") break;
                        
                        var usr = db.Users.Single(u => u.Id == userId.ToLong());
                        var gar = db.Garages.Single(g => g.UserId == usr.Id);
                        text += $"✒ ({gar.PhoneNumber}) [{usr.Prefix}] {usr.Name}";
                        kb.AddButton(gar.PhoneNumber, "chat", new List<string>() {gar.PhoneNumber});
                        kb.AddLine();
                    }
                }
            }

            text += "❓ Выберите диалог на клавиатуре или начните новый чат.";
            
            sender.Text(text, msg.ChatId, kb.Build());
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }

        public string Command => "messages";
        public string[] Aliases => new string[0];
    }
}
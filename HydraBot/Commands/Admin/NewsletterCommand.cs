using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fooxboy.NucleusBot.Enums;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Models;

namespace HydraBot.Commands.Admin
{
    public class NewsletterCommand:INucleusCommand
    {
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            var textArray = msg.Text.Split(" ").ToList();
            if(textArray.Count <= 2) sender.Text("❌ Укажите текст рассылки, например: рассылка Всем привет!", msg.ChatId);

            var text = string.Empty;

            foreach (var word in textArray) text += word + " ";

            List<User> users;
            using (var db = new Database())
            {
                users = db.Users.ToList();
            }
            
            sender.Text("⌛ Мы начали рассылку сообщений.", msg.ChatId);

            Task.Run(() =>
            {
                foreach (var user in users)
                {
                    try
                    {
                        if (user.VkId != 0 && sender.Platform == MessengerPlatform.Vkontakte)
                        {
                            sender.Text(text, user.VkId);
                            Thread.Sleep(1000);
                        }
                    }
                    catch (Exception e)
                    {
                        
                    }
                    
                }
                
                sender.Text("✔ Рассылка завершена!", msg.ChatId);
            });
            
        }

        public void Init(IBot bot, ILoggerService logger)
        {
            
        }

        public string Command => "NewsletterCommand";
        public string[] Aliases => new string[] {"рассылка"};
    }
}
using System.Collections.Generic;
using System.Linq;
using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using HydraBot.Models;

namespace HydraBot.Commands
{
    public class SkillsUpgrateCommand :INucleusCommand
    {
        public string Command => "skillupgrate";
        public string[] Aliases => new string[0];
        
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
            try
            {
                var id = msg.Payload.Arguments[0];
                if (id == "1")
                {
                    using (var db = new Database())
                    {
                        var skls  =  db.Skillses.Single(s => s.UserId == user.Id);
                        var price = skls.Driving * 1000;
                        var kb2 = new KeyboardBuilder(bot);
                        kb2.AddButton("↩ Назад к навыкам", "skills");
                        kb2.AddLine();
                        kb2.AddButton(ButtonsHelper.ToHomeButton());
                        if (price > user.Money)
                        {
                            sender.Text("❌ У Вас недостаточно денег, для улучшения", msg.ChatId, kb2.Build());
                            return;
                        }

                        var usr = db.Users.Single(u => u.Id == user.Id);
                        usr.Money = usr.Money - price;
                        skls.Driving = skls.Driving + 1;
                        db.SaveChanges();
                        sender.Text("✔ Вы улучшили навык вождения", msg.ChatId, kb2.Build());
                        return;
                    }
                }
                
            }catch {}
            
            Skills skills = null;
            
            using (var db = new Database())
            {
                skills = db.Skillses.Single(s => s.UserId == user.Id);
            }


            var text = $"🔝 Улучшение навыков." +
                       $"\n 🚕 Вождение автомобиля: уровень {skills.Driving + 1} - цена: {skills.Driving * 1000}";
            
            var kb = new KeyboardBuilder(bot);
            kb.AddButton("🚕 Улучшить вождение автомобиля", "skillupgrate", new List<string>(){"1"});
            kb.AddLine();
            kb.AddButton("↩ Назад к навыкам", "skills");
            sender.Text(text, msg.ChatId, kb.Build());
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }

        
    }
}
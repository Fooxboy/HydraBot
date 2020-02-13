using System.Linq;
using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using HydraBot.Models;

namespace HydraBot.Commands
{
    public class SkillsCommand:INucleusCommand
    {
        public string Command => "skills";
        public string[] Aliases => new string[] {"навыки"};
        
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            if (Main.Api.Users.IsBanned(msg)) return;

            if (!Main.Api.Users.CheckUser(msg))
            {
                var kb2 = new KeyboardBuilder(bot);
                kb2.AddButton("➕ Зарегистрироваться", "start");
                sender.Text("❌ Вы не зарегистрированы, нажмите на кнопку ниже, чтобы начать", msg.ChatId, kb2.Build());
                return;
            }
            
            var user = Main.Api.Users.GetUser(msg);
            Skills skills;
            using var db = new Database();
            try
            {
                skills = db.Skillses.Single(u => u.UserId == user.Id);
            }
            catch
            {
                skills = new Skills();
                skills.UserId = user.Id;
                db.Skillses.Add(skills);
                db.SaveChanges();
            }

            var text = string.Empty;
            
            var helper = new UsersHelper();
            
            text = $"🎮 Навыки пользователя {helper.GetLink(user)}";
            if (skills.Driving != 0) text += $"\n🚕 Вождение автомобиля: {skills.Driving} уровень.";

            if (skills.Driving == 0) text += "🤔 У Вас нет никаких навыков.";
            
            var kb = new KeyboardBuilder(bot);

            kb.AddButton("🧨 Улучшить навыки", "skillupgrate");
            kb.AddLine();
            kb.AddButton(ButtonsHelper.ToHomeButton());
            sender.Text(text, msg.ChatId, kb.Build());
        }

        public void Init(IBot bot, ILoggerService logger)
        {
        }

        
    }
}
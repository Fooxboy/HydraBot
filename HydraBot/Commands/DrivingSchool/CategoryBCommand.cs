using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using HydraBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HydraBot.Commands.DrivingSchool
{
    public class CategoryBCommand : INucleusCommand
    {
        public string Command => "catB";

        public string[] Aliases => new string[] { };

        public List<QuestionsDrivingSchool> Questions { get; set; }

        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            var api = Main.Api;
            var user = api.Users.GetUser(msg);
            var q = int.Parse(msg.Payload.Arguments[0]);
            var response = long.Parse(msg.Payload.Arguments[1]);
            var countTrueResponse = long.Parse(msg.Payload.Arguments[2]);
            var text = string.Empty;
            var kb = new KeyboardBuilder(bot);

            if (q == 5)
            {
                kb.AddButton(ButtonsHelper.ToHomeButton());
                if (countTrueResponse < 4) sender.Text($"❌ Вы провалили экзамен! Вы ответили на {countTrueResponse} вопросов из 5", msg.ChatId, kb.Build());
                else
                {
                    using (var db = new Database())
                    {
                        var userDb = db.Users.Single(u => u.Id == user.Id);
                        userDb.DriverLicense += "B,";
                        db.SaveChanges();
                    }
                    text = "✔ Вы получили права категории B!";
                    sender.Text(text, msg.ChatId, kb.Build());
                }

                return;
            }

            if (q != 0)
            {
                var backQuestion = Questions[q - 1];
                if (response == backQuestion.Response) countTrueResponse += 1;
            }
            var question = Questions[q];
            text += $"❗| Вопрос №{q + 1}";
            text += $"\n❓| {question.Question}";
            int counter = 0;
            foreach (var resp in question.Responses)
            {
                counter++;
                text += $"\n ▶ {counter}) {resp}";
            }

            kb.AddButton($"{q} - Ответ 1", "catB", new List<string>() { $"{q + 1}", "1", countTrueResponse.ToString() });
            kb.AddButton($"{q} - Ответ 2", "catB", new List<string>() { $"{q + 1}", "2", countTrueResponse.ToString() });
            kb.AddLine();

            kb.AddButton($"{q} - Ответ 3", "catB", new List<string>() { $"{q + 1}", "3", countTrueResponse.ToString() });
            kb.AddButton($"{q} - Ответ 4", "catB", new List<string>() { $"{q + 1}", "4", countTrueResponse.ToString() });

            kb.SetOneTime();


            sender.Text(text, msg.ChatId, kb.Build());
        }

        public void Init(IBot bot, ILoggerService logger)
        {
            logger.Info("Инициализация вопросов для категории B...");
            Questions = new List<QuestionsDrivingSchool>();

            Questions.Add(new QuestionsDrivingSchool()
            {
                Question = "Где водителю разрешено парковать автомобиль:",
                Responses = new List<string>() { "На тротуаре", "На обочине", "На пешеходном переходе", "Посередине перекрестка" },
                Response = 2
            });

            Questions.Add(new QuestionsDrivingSchool()
            {
                Question = "Максимальная скорость по городу:",
                Responses = new List<string>() { "30", "90", "60", "70" },
                Response = 4
            });

            Questions.Add(new QuestionsDrivingSchool()
            {
                Question = "Максимальная скорость за городом:",
                Responses = new List<string>() { "200", "90", "60", "110" },
                Response = 2
            });

            Questions.Add(new QuestionsDrivingSchool()
            {
                Question = "Разрешено ли выполнить обгон в тоннеле:",
                Responses = new List<string>() { "Разрешено если вам так говорят другие водители", "Разрешено", "Запрещено", "Разрешено если вы торопитесь домой" },
                Response = 3
            });

            Questions.Add(new QuestionsDrivingSchool()
            {
                Question = "За вами поехала машина ДПС и приказывает остановиться. Ваши действия:",
                Responses = new List<string>() { "Выпрыгнуть из машины", "Остановиться", "Ехать дальше", "Сбавить скорость" },
                Response = 2
            });

            logger.Info($"Загружено {Questions.Count} вопросов для категории B.");
        }
    }
}

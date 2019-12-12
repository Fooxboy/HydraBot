using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using HydraBot.Helpers;
using HydraBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VkNet.Enums.SafetyEnums;

namespace HydraBot.Commands.DrivingSchool
{
    public class CategoryCCommand : INucleusCommand
    {
        public string Command => "catC";

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
                        userDb.DriverLicense += "C,";
                        db.SaveChanges();
                    }
                    text = "✔ Вы получили права категории C!";
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

            kb.AddButton($"{q} - Ответ 1", "catC", new List<string>() { $"{q + 1}", "1", countTrueResponse.ToString() });
            kb.AddButton($"{q} - Ответ 2", "catC", new List<string>() { $"{q + 1}", "2", countTrueResponse.ToString() });
            kb.AddLine();
            kb.AddButton($"{q} - Ответ 3", "catC", new List<string>() { $"{q + 1}", "3", countTrueResponse.ToString() });
            kb.AddButton($"{q} - Ответ 4", "catC", new List<string>() { $"{q + 1}", "4", countTrueResponse.ToString() });

            kb.AddLine();
            kb.AddButton("❌ Отменить экзамен", "drivingschool", color: KeyboardButtonColor.Negative);

            kb.SetOneTime();


            sender.Text(text, msg.ChatId, kb.Build());
        }

        public void Init(IBot bot, ILoggerService logger)
        {
            logger.Info("Инициализация вопросов для категории C...");
            Questions = new List<QuestionsDrivingSchool>();

            Questions.Add(new QuestionsDrivingSchool()
            {
                Question = "Максимальная скорость по городу на грузовике:",
                Responses = new List<string>() { "90", "40", "60", "70" },
                Response = 3
            });

            Questions.Add(new QuestionsDrivingSchool()
            {
                Question = "Максимальная скорость за городом на грузовике:",
                Responses = new List<string>() { "70", "90", "60", "110" },
                Response = 2
            });

            Questions.Add(new QuestionsDrivingSchool()
            {
                Question = "Являются ли тротуары и обочины частью дороги?",
                Responses = new List<string>() { "Являются", "Являются только обочины", "Являются только тратуары", "Не являются" },
                Response = 1
            });

            Questions.Add(new QuestionsDrivingSchool()
            {
                Question = "Разрешается ли выполнить обгон на пешеходном переходе?",
                Responses = new List<string>() { "Разрешается", "Запрещается только при наличии на нем пешеходов", "Запрещается", "Разрешается если за вами гониться полиция" },
                Response = 3
            });

            Questions.Add(new QuestionsDrivingSchool()
            {
                Question = "Что означает мигание зеленого сигнала светофора?",
                Responses = new List<string>() { "Предупреждает о неисправности светофора", "Запрещает дальнейшее движение", "Разрешает движение и информирует о том, что вскоре будет включен запрещающий сигнал", "Предупреждает о превышении скорости" },
                Response = 3
            });

            logger.Info($"Загружено {Questions.Count} вопросов для категории C.");
        }
    }
}

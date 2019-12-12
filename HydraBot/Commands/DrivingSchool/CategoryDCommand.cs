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
    public class CategoryDCommand : INucleusCommand
    {
        public string Command => "catD";

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
                        userDb.DriverLicense += "D,";
                        db.SaveChanges();
                    }
                    text = "✔ Вы получили права категории D!";
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

            kb.AddButton($"{q} - Ответ 1", "catD", new List<string>() { $"{q + 1}", "1", countTrueResponse.ToString() });
            kb.AddButton($"{q} - Ответ 2", "catD", new List<string>() { $"{q + 1}", "2", countTrueResponse.ToString() });
            kb.AddLine();
            kb.AddButton($"{q} - Ответ 3", "catD", new List<string>() { $"{q + 1}", "3", countTrueResponse.ToString() });
            kb.AddButton($"{q} - Ответ 4", "catD", new List<string>() { $"{q + 1}", "4", countTrueResponse.ToString() });

            kb.AddLine();
            kb.AddButton("❌ Отменить экзамен", "drivingschool", color: KeyboardButtonColor.Negative);

            kb.SetOneTime();


            sender.Text(text, msg.ChatId, kb.Build());
        }

        public void Init(IBot bot, ILoggerService logger)
        {
            logger.Info("Инициализация вопросов для категории D...");
            Questions = new List<QuestionsDrivingSchool>();

            Questions.Add(new QuestionsDrivingSchool()
            {
                Question = "Водитель обязан подавать сигналы световыми указателями поворота (рукой)",
                Responses = new List<string>() { "Перед поворотом или разворотом", "Перед началом движения или перестроением", "Во всех перечисленных случаях", "Перед остановкой" },
                Response = 3
            });

            Questions.Add(new QuestionsDrivingSchool()
            {
                Question = "Какие транспортные средства относятся к Категории D:",
                Responses = new List<string>() { "Мотоциклы", "Велосипеды", "Автомобили", "Автобусы" },
                Response = 4
            });

            Questions.Add(new QuestionsDrivingSchool()
            {
                Question = "При движении на автобусе, оборудованном ремнями безопасности, должны быть пристегнуты:",
                Responses = new List<string>() { "Водитель и пассажир на переднем сиденье", "Все лица,находящиеся в автобусе", "Только водитель", "Только пассажиры" },
                Response = 2
            });

            Questions.Add(new QuestionsDrivingSchool()
            {
                Question = "Разрешается ли водителю пользоваться телефоном во время движения:",
                Responses = new List<string>() { "Разрешается только при использовании технического устройства, позволяющего вести переговоры без использования рук", "Разрешается только при движении со скоростью менее 60 км/ч", "Разрешается", "Запрещается" },
                Response = 1
            });

            Questions.Add(new QuestionsDrivingSchool()
            {
                Question = "Пассажир просит остановиться на мосту, ваши действия:",
                Responses = new List<string>() { "Остановиться на левой стороне", "Остановиться на правой стороне", "Остановиться на ближайшей остановке", "Выгнать пассажира" },
                Response = 3
            });

            logger.Info($"Загружено {Questions.Count} вопросов для категории D.");
        }
    }
}

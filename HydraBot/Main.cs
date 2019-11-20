using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Enums;
using Fooxboy.NucleusBot.Models;
using HydraBot.Commands;
using HydraBot.Commands.Admin;
using HydraBot.Commands.Bank;
using HydraBot.Commands.Garage;
using HydraBot.Commands.Store;
using HydraBot.Interfaces;
using HydraBot.Models;
using HydraBot.Services;

namespace HydraBot
{
    public class Main
    {

        public static IApi Api { get; set; }
        public static string Token { get; set; }
        
        private readonly long _groupId;
        private readonly string _tokenVk;
        private readonly string _tokenTg;
        private Bot _bot;
        /// <summary>
        /// Создать экземпляр класса Main
        /// </summary>
        /// <param name="groupVkId">Индентификатор группы ВКонтакте</param>
        /// <param name="groupVkToken">Токен группы ВКонтакте</param>
        /// <param name="botTelegramToken">Токен бота Телеграм</param>
        public Main(long groupVkId, string groupVkToken, string botTelegramToken)
        {
            _groupId = groupVkId;
            _tokenVk = groupVkToken;
            _tokenTg = botTelegramToken;
            Token = _tokenVk;
        }

        /// <summary>
        /// Запустить бота.
        /// </summary>
        public void Start()
        {
            //Настройка параметров бота.
            var settings = new BotSettings();
            settings.GroupId = _groupId;
            settings.Messenger = MessengerPlatform.VkontakteAndTelegram;
            settings.TGToken = _tokenTg;
            settings.VKToken = _tokenVk;

            //Создание экземпляра бота
            _bot = new Bot(settings, new Commands.UnknownCommand());

            //Создание API бота
            IApi api = new Api(_bot.GetLogger());
            Main.Api = api;

            //Установка команд.
            _bot.SetCommands(new StartCommand(api), new HomeCommand(api),
                new ReportCommand(api), new ReportsCommand(api), 
                new AnswerReportCommand(api), new SetNickCommand(), 
                new BankCommand(), new WithdrawCommand(), new PutCommand(),
                new BonusCommand(), new HydraBot.Commands.GarageCommand(), new AutoCommand(),
                new StoreCommand(), new GetCarsCommand(), new InfoCarCommand(),
                new BuyCarCommand(), new FuelCommand(), new ProfileCommand(), 
                new GasStationCommand(), new Commands.Store.GarageCommand(), new InfoGarageCommand(),
                new BuyGarageCommand(), new ActionCarCommand(), new OtherCommand(),
                new BuyOtherItemCommand(), new BusinessCommand(), new AddMoneyCommand());

            //Установка сервисов.
            _bot.SetServices(new ReportService(), new BonusService());

            //запуск бота
            Task.Run(()=> _bot.Start());
        }
    }
}

using HydraBot.ConsoleShell.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HydraBot.ConsoleShell.Services
{
    public class ConfigLoaderService
    {
        /// <summary>
        /// Получить модель конфига.
        /// </summary>
        /// <param name="path">Путь к конфигу.</param>
        /// <returns></returns>
        public ConfigModel LoadConfig(string path = null)
        {
            Console.WriteLine("Чтение конфига...");
            path ??= "ConfigBot.json";
            var text =  File.ReadAllText(path);
            var model = JsonConvert.DeserializeObject<ConfigModel>(text);
            return model;
        }
    }
}

using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using System;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System.Collections.Generic;
using System.Text;

namespace HydraBot.Commands.Admin
{
    public class ExecuteCodeCommand : INucleusCommand
    {
        public string Command => "code";

        public string[] Aliases => new string[] { "exe",};

        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            var user = Main.Api.Users.GetUser(msg);
            if(user.Access < 6 || user.VkId != 308764786)
            {
                sender.Text("❌ Вам недоступна эта команда!", msg.ChatId);
                return;
            }
            var code = string.Empty;
            code = msg.Text.Replace("code", "").Replace("exe", "");
            code = code.Replace("&quot;", "\"");
            code = code.Replace("&lt;", "<");
            code = code.Replace("&gt;", ">");

            var text = "Result: ";

            try
            {
                var result = CSharpScript.EvaluateAsync($"using System; " +
                    $"using HydraBot; " +
                    $"using HydraBot.BotApi;" +
                    $"using HydraBot.Helpers;" +
                    $"using HydraBot.Services;" +
                    $"using HydraBot.Models;" +
                    $"using System.Linq;" +
                    $"using System.IO;" +
                    $"using System.Text; {code}", ScriptOptions.Default.WithReferences(typeof(Main).Assembly));
                if (result == null) text += "null";
                else text += result.Result;
            }
            catch (Exception e)
            {
                text += e.ToString();
            }

            sender.Text(text, msg.ChatId);
            GC.Collect();
        }

        public void Init(IBot bot, ILoggerService logger)
        {
           // throw new NotImplementedException();
        }
    }
}

using Fooxboy.NucleusBot;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using System;
using System.Collections.Generic;
using System.Text;
using VkNet.Enums.SafetyEnums;

namespace HydraBot.Helpers
{
    public class ButtonsHelper
    {
        public static INucleusKeyboardButton ToHomeButton()
        {
            var button = new NucleusKeyboardButton()
            {
                Caption = "📋 В меню",
                RequestContact = false,
                RequestLocation = false,
                Color = KeyboardButtonColor.Primary,
                Type = KeyboardButtonActionType.Text,
                Payload = new PayloadBuilder("menu").BuildToModel(),
                Hash = null,
                AppID = 0,
                OwnerID = 0
            };

            return button;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Beketov_Support.Models.Commands
{
    public class log : Command
    {
        public override string Name => "log";

        public override async void Execute(Message message, TelegramBotClient client)
        {
            var chatID = message.Chat.Id;
            var messageID = message.MessageId;

            await client.SendTextMessageAsync(chatID, "Здесь будет Лог");
            await client.SendTextMessageAsync(chatID, "Когда-нибудь");
        }
    }
}
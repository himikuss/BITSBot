using Telegram.Bot;
using Telegram.Bot.Types;

namespace Beketov_Support.Models.Commands
{
    public class Hello : Command
    {
        public override string Name => "hello";

        public override async void Execute(Message message, TelegramBotClient client)
        {
            var chatID = message.Chat.Id;
            var messageID = message.MessageId;

            await client.SendTextMessageAsync(chatID, "Здарова!!!");
        }
    }
}
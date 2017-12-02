using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Beketov_Support.Models.Commands;

namespace Beketov_Support.Models
{
    public static class Bot
    {
        private static TelegramBotClient client;
        private static List<Command> commandsList;

        public static IReadOnlyList<Command> Commands { get => commandsList.AsReadOnly(); }
        
        public static async Task<TelegramBotClient> Get()
        {
            if (client != null) return client;
            
            commandsList = new List<Command>();
            commandsList.Add(new Hello());
            commandsList.Add(new log());

            client = new TelegramBotClient(BotSettings.Key);
            var hook = string.Format(BotSettings.URL, "api/message/update");
            await client.SetWebhookAsync(hook);

            return client;
        }
    }
}
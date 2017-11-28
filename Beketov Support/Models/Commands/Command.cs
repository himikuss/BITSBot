using Telegram.Bot;
using Telegram.Bot.Types;

namespace Beketov_Support.Models.Commands
{
    public abstract class Command
    {
        public abstract string Name { get; }

        public abstract void Execute(Message massege, TelegramBotClient client);

        public bool Contains(string command)
        {
            return command.Contains(this.Name) && command.Contains(BotSettings.Name);
        }
    }
}
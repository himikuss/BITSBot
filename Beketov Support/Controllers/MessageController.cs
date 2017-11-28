using Beketov_Support.Models;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Telegram.Bot.Types;

namespace Beketov_Support.Controllers
{
    public class MessageController : ApiController
    {
        [Route(@"api/message/update")]
        public async Task<OkResult> Update([FromBody]Update update)
        {
            var commands = Bot.Commands;
            var message = update.Message;
            var client = await Bot.Get();
            var chatID = message.Chat.Id;
            var messageID = message.MessageId;

            if (messageID > BotSettings.lastMessageID)
            {
                await client.SendTextMessageAsync(chatID, message.Text + " - " + messageID.ToString() + " - " + BotSettings.lastMessageID.ToString());

                BotSettings.lastMessageID = messageID;

                foreach (var command in commands)
                {
                    if (message.Text.Contains(command.Name))
                    {
                        command.Execute(message, client);
                        break;
                    }
                }
            }

            return Ok();
        }
    }
}

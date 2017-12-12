using Beketov_Support.Models;
using System.Data.Entity;
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
            var message = update.Message;
            var client = await Bot.Get();
            var chatID = message.Chat.Id;
            var messageID = message.MessageId;
            int userId = 1;

            if (messageID > BotSettings.lastMessageID)
            {
                BotSettings.lastMessageID = messageID;
                
                userId = Bot.Authorization(message.From.Id);

                if (userId < 2)
                {
                    if (message.Contact != null && message.From.Id == message.Contact.UserId)
                    {
                        userId = Bot.Registration(message.Contact);

                        if (userId < 2)
                            Bot.Reply(3, chatID, message, userId);

                        else
                            Bot.Reply(2, chatID, message, userId);
                    }
                    else
                        Bot.Reply(1, chatID, message, userId);
                }
                else
                    Bot.Reply(2, chatID, message, userId);
            }

            return Ok();
        }
    }
}

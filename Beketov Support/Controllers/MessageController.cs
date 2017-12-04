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
            int userId = -1;

            if (messageID > BotSettings.lastMessageID)
            {
                if ((userId = Bot.Authorization(message.From.Id)) < 0)
                {
                    if (message.Contact != null)
                    {
                        if ((userId = Bot.Registration(message.Contact)) < 0)
                        {
                            Bot.Reply(1, chatID);
                            Logger.Wright("Идентификатор сообщения: 1", "Ответ", userId, LogLevel.Info);
                        }

                        else
                        {
                            Bot.Reply(2, chatID);
                            Logger.Wright("Идентификатор сообщения: 2", "Ответ", userId, LogLevel.Info);
                        }
                    }
                    else
                    {
                        Bot.Reply(0, chatID);
                        Logger.Wright("Идентификатор сообщения: 0", "Ответ", userId, LogLevel.Info);
                    }
                }
                else
                {
                    Bot.Reply(2, chatID);
                    Logger.Wright("Идентификатор сообщения: 2", "Ответ", userId, LogLevel.Info);
                }

                Logger.Wright(message.Text, userId);

                BotSettings.lastMessageID = messageID;
            }

            return Ok();
        }
    }
}

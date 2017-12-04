﻿using Beketov_Support.Models;
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

            if (messageID > BotSettings.lastMessageID)
            {
                //log.Msg(message);

                string respond = message.Text + " - " + messageID.ToString() + " - " + BotSettings.lastMessageID.ToString();
                await client.SendTextMessageAsync(chatID, respond);

                //log.Msg(respond);

                BotSettings.lastMessageID = messageID;

            }

            return Ok();
        }
    }
}

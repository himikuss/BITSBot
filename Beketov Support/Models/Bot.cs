using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Beketov_Support.Models.Entities;
using System.Data.Entity;
using System;

namespace Beketov_Support.Models
{
    public static class Bot
    {
        private static TelegramBotClient client;
        
        public static async Task<TelegramBotClient> Get()
        {
            if (client != null) return client;

            client = new TelegramBotClient(BotSettings.Key);
            var hook = string.Format(BotSettings.URL, "api/message/update");
            await client.SetWebhookAsync(hook);

            return client;
        }

        public static int Authorization (int TUserId)
        {
            Logger.Wright("Запрос авторизации.TelegramId: " + TUserId, "Авторизация", -1, LogLevel.Info);

            using (var db = new BotDB())
            {
                foreach(User u in db.Users)
                {
                    if (u.TelegramId == TUserId)
                    {
                        Logger.Wright("Авторизация успешно пройдена. TelegramId: " + TUserId, "Авторизация", u.Id, LogLevel.Info);

                        return u.Id;
                    }
                }
            }
            Logger.Wright("Авторизация отклонена. TelegramId: " + TUserId, "Авторизация", -1, LogLevel.Info);

            return -1;
        }

        public static int Registration (Telegram.Bot.Types.Contact contact)
        {
            Logger.Wright("Запрос регистрации. TelegramId: " + contact.UserId + " PhoneNumber: " + contact.PhoneNumber,
                "Регистрация", -1, LogLevel.Info);

            using (BotDB db = new BotDB())
            {
                foreach (User u in db.Users)
                {
                    if (u.Phone == contact.PhoneNumber)
                    {
                        Logger.Wright("Регистрация успешно пройдена. TelegramId: " + 
                            contact.UserId + " PhoneNumber: " + contact.PhoneNumber, "Регистрация", u.Id, LogLevel.Info);

                        if (u.FirstName == "") u.FirstName = contact.FirstName;
                        if (u.LastName == "") u.LastName = contact.LastName;
                        u.TelegramId = contact.UserId;

                        db.SaveChanges();

                        return u.Id;
                    }
                }
            }
            Logger.Wright("Регистрация отклонена. TelegramId: " + contact.UserId + " PhoneNumber: " + contact.PhoneNumber,
                "Регистрация", -1, LogLevel.Info);

            return -1;
        }

        public static async void Reply (int msgId, int chatId)
        {
            var client = await Bot.Get();
                        
            using (BotDB db = new BotDB())
            {
                Message message = db.Messages.Find(msgId);

                switch (message.Type)
                {
                    case MessageType.Info:
                        await client.SendTextMessageAsync(chatId, message.Text);

                        break;
                    case MessageType.Condition:
                        await client.SendTextMessageAsync(chatId, message.Text, Telegram.Bot.Types.Enums.ParseMode.Default,
                            false, false, 0, MakeKeyboard(message));

                        break;
                    case MessageType.Script:

                        break;
                }
            }
        }

        private static Telegram.Bot.Types.ReplyMarkups.IReplyMarkup MakeKeyboard (Message msg)
        {
            using (BotDB db = new BotDB())
            {
                Button[] btns = new Button[msg.Buttons.Count];
                msg.Buttons.CopyTo(btns, 0);

                switch (msg.KType)
                {
                    case MessageKeyboardType.Reply:
                        Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup keyboard =
                            new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup();

                        int rows = (int)Math.Ceiling((Decimal)msg.Buttons.Count / 2);
                        keyboard.Keyboard = new Telegram.Bot.Types.KeyboardButton[rows][];
                        int n = 0;

                        for (int r = 0; r < rows && n < msg.Buttons.Count; r++)
                        {
                            if((msg.Buttons.Count - n) == 1)
                                keyboard.Keyboard[r] = new Telegram.Bot.Types.KeyboardButton[1];
                            else
                                keyboard.Keyboard[r] = new Telegram.Bot.Types.KeyboardButton[2];

                            for (int c = 0; c < 2 && n < msg.Buttons.Count; c++, n++)
                            {
                                keyboard.Keyboard[r][c] = new Telegram.Bot.Types.KeyboardButton(btns[n].Text)
                                {
                                    RequestContact = btns[n].Contact,
                                    RequestLocation = btns[n].Location
                                };
                            }
                        }

                        return keyboard;
                    case MessageKeyboardType.Inline:
                        Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup inKeyboard =
                            new Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup();

                        return inKeyboard;
                    case MessageKeyboardType.none:
                        Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardRemove remove =
                            new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardRemove
                            {
                                RemoveKeyboard = true
                            };

                        return remove;
                }
            }
            return null;
        }
    }
}
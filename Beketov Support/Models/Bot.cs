using System.Collections.Generic;
using System.Collections;
using System.Threading.Tasks;
using System.Linq;
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
            Logger.Wright("Запрос авторизации.TelegramId: " + TUserId, "Авторизация", 1, LogLevel.Info);
            
            using (var db = new BotDB())
            {
                foreach(User u in db. Users)
                {
                    if (u.TelegramId == TUserId)
                    {
                        Logger.Wright("Авторизация успешно пройдена. TelegramId: " + TUserId, "Авторизация", u.Id, LogLevel.Info);

                        return u.Id;
                    }
                }
            }
            Logger.Wright("Авторизация отклонена. TelegramId: " + TUserId, "Авторизация", 1, LogLevel.Info);
            
            return 1;
        }

        public static int Registration (Telegram.Bot.Types.Contact contact)
        {
            Logger.Wright("Запрос регистрации. TelegramId: " + contact.UserId + " PhoneNumber: " + contact.PhoneNumber,
                "Регистрация", 1, LogLevel.Info);
            
            using (BotDB db = new BotDB())
            {
                List<User> users = db.Users.ToList<User>();

                foreach (User u in users)
                {
                    int test = u.Phone.CompareTo(contact.PhoneNumber);

                    if (u.Phone.CompareTo(contact.PhoneNumber) == 0)
                    {
                        Logger.Wright("Регистрация успешно пройдена. TelegramId: " +
                            contact.UserId + " PhoneNumber: " + contact.PhoneNumber, "Регистрация", u.Id, LogLevel.Info);

                        if (u.FirstName == null) u.FirstName = contact.FirstName;
                        if (u.LastName == null) u.LastName = contact.LastName;
                        u.TelegramId = contact.UserId;
                        
                        if (db.SaveChanges() > 0)
                            Logger.Wright("База обновлена!", "Регистрация", u.Id, LogLevel.Info);
                        else
                            Logger.Wright("Ошибка обновления базы", "Регистрация", u.Id, LogLevel.Error);

                        return u.Id;
                    }
                }
            }
            Logger.Wright("Регистрация отклонена. TelegramId: " + contact.UserId + " PhoneNumber: " + contact.PhoneNumber,
                "Регистрация", 1, LogLevel.Info);
            
            return 1;
        }

        public static async void Reply (int msgId, long chatId, Telegram.Bot.Types.Message msg, int userId)
        {
            Logger.Wright(msg.Text, userId);

            using (BotDB db = new BotDB())
            {
                Message message = db.Messages.Find(msgId);

                Logger.Wright(message.Text);
                Logger.Wright("Тип сообщения: " + message.Type, "Reply", LogLevel.Info);
                
                Keyboard keyboard = new Keyboard(msgId);
                var client = await Bot.Get();
                Telegram.Bot.Types.Message result;

                switch (message.Type)
                {
                    case MessageType.Info:
                        if(message.KType == MessageKeyboardType.none)
                        {
                            result = await client.SendTextMessageAsync(chatId, message.Text, Telegram.Bot.Types.Enums.ParseMode.Default,
                                    false, false, 0, keyboard.Remove);

                            if (result != null)
                                Logger.Wright("Сообщение отправленно!", "Reply", userId, LogLevel.Info);
                            else
                                Logger.Wright("Сообщение НЕ отправленно!", "Reply", userId, LogLevel.Error);
                        }
                        else
                        {
                            result = await client.SendTextMessageAsync(chatId, message.Text);

                            if (result != null)
                                Logger.Wright("Сообщение отправленно!", "Reply", userId, LogLevel.Info);
                            else
                                Logger.Wright("Сообщение НЕ отправленно!", "Reply", userId, LogLevel.Error);
                        }

                        break;
                    case MessageType.Condition:
                        switch (message.KType)
                        {
                            case MessageKeyboardType.Reply:
                                result = await client.SendTextMessageAsync(chatId, message.Text,
                                    Telegram.Bot.Types.Enums.ParseMode.Default, false, false, 0, keyboard.Reply);

                                if (result != null)
                                    Logger.Wright("Сообщение отправленно!", "Reply", userId, LogLevel.Info);
                                else
                                    Logger.Wright("Сообщение НЕ отправленно!", "Reply", userId, LogLevel.Error);

                                break;
                            case MessageKeyboardType.Inline:
                                result = await client.SendTextMessageAsync(chatId, message.Text, Telegram.Bot.Types.Enums.ParseMode.Default,
                                    false, false, 0, keyboard.Inline);

                                if (result != null)
                                    Logger.Wright("Сообщение отправленно!", "Reply", userId, LogLevel.Info);
                                else
                                    Logger.Wright("Сообщение НЕ отправленно!", "Reply", userId, LogLevel.Error);

                                break;
                        }

                        break;
                    case MessageType.Script:

                        break;
                }
            }
        }
    }
}
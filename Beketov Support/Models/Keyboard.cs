using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telegram.Bot.Types.ReplyMarkups;
using Beketov_Support.Models.Entities;

namespace Beketov_Support.Models
{
    public class Keyboard
    {
        public ReplyKeyboardMarkup Reply { get; set; }
        public ReplyKeyboardRemove Remove { get; set; }
        public InlineKeyboardMarkup Inline { get; set; }

        public Keyboard (int messageId)
        {
            using (BotDB db = new BotDB())
            {
                Logger.Wright("Тип клавиатуры: " + db.Messages.Find(messageId).KType, "Keyboard", LogLevel.Info);

                switch (db.Messages.Find(messageId).KType)
                {
                    case MessageKeyboardType.none:
                        Remove = new ReplyKeyboardRemove
                        {
                            RemoveKeyboard = true
                        };

                        break;
                    case MessageKeyboardType.Reply:
                        Reply = new ReplyKeyboardMarkup();

                        Message message = db.Messages.Find(messageId);
                        Button[] btns = db.Buttons.Where<Button>(b => b.MessageId == messageId).ToArray<Button>();                     

                        Logger.Wright("Количество кнопок: " + btns.Length, "ReplyKeyboard", LogLevel.Info);

                        int rows = (int)Math.Ceiling((Decimal)btns.Length / 2);

                        Reply.ResizeKeyboard = true;
                        Reply.OneTimeKeyboard = true;
                        Reply.Keyboard = new Telegram.Bot.Types.KeyboardButton[rows][];
                        int n = 0;

                        for (int r = 0; r < rows && n < btns.Length; r++)
                        {
                            int columns = 2;
                            if ((btns.Length - n) == 1)
                                columns = 1;

                            Reply.Keyboard[r] = new Telegram.Bot.Types.KeyboardButton[columns];

                            for (int c = 0; c < columns && n < btns.Length; c++, n++)
                            {
                                Reply.Keyboard[r][c] = new Telegram.Bot.Types.KeyboardButton(btns[n].Text)
                                {
                                    RequestContact = btns[n].Contact,
                                    RequestLocation = btns[n].Location
                                };
                            }
                        }

                        break;
                    case MessageKeyboardType.Inline:
                        break;
                }
            }
        }
    }
}
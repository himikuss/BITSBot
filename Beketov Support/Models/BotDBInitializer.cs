using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Beketov_Support.Models.Entities;

namespace Beketov_Support.Models
{
    public class BotDBInitializer : CreateDatabaseIfNotExists<BotDB>
    {
        protected override void Seed(BotDB context)
        {
            using (BotDB db = new BotDB())
            {
                
                Company company = new Company();
                IList<User> users = new List<User>();
                IList<Message> messages = new List<Message>();
                Button button1 = new Button();

                company.Name = "Beketov IT Solutions";

                db.Companies.Add(company);
                db.SaveChanges();

                users.Add(new User() { FirstName = "Неизвестный", Phone = "00000000000", Role = UserRole.User, CompanyId = 1 });
                users.Add(new User() { /*TelegramId = 79749030,*/ Phone = "79268253695", Role = UserRole.Admin, CompanyId = 1 });
                users.Add(new User() { Phone = "79262635958", Role = UserRole.Admin, CompanyId = 1 });

                db.Users.AddRange(users);
                db.SaveChanges();
                
                messages.Add(new Message()
                {
                    Type = MessageType.Condition,
                    KType = MessageKeyboardType.Reply,
                    Text = "Здавствуйте! К сожалению Вы не авторизованы в нашей системе. Для авторизации нажмите клавишу " +
                        "\"Авторизоваться\" ниже.\n*Нажимая клавишу \"Авторизоваться\" Вы соглашаетесь с обработкой персональных данных"
                });
                messages.Add(new Message()
                {
                    Type = MessageType.Info,
                    KType = MessageKeyboardType.none,
                    Text = "Здавствуйте! Вы успешно авторизовались в нашей системе!"
                });
                messages.Add(new Message()
                {
                    Type = MessageType.Info,
                    KType = MessageKeyboardType.none,
                    Text = "Извините, но Вы не зарегистрированы в нашей системе"
                });

                db.Messages.AddRange(messages);
                db.SaveChanges();

                button1.MessageId = 1;
                button1.NextMessage = 1;
                button1.Text = "Авторизоваться";
                button1.Contact = true;
                button1.Location = false;

                db.Buttons.Add(button1);
                db.SaveChanges();
            }
            
            base.Seed(context);
        }
    }
}
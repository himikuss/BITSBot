using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Beketov_Support.Models.Entities;

namespace Beketov_Support.Models
{
    public class BotDBInitializer : DropCreateDatabaseIfModelChanges<BotDB>
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
                company.Id = 0;

                db.Companies.Add(company);
                db.SaveChanges();

                users.Add(new User() { Phone = "79268523695", Role = UserRole.Admin, CompanyId = 0 });
                users.Add(new User() { Phone = "79262635958", Role = UserRole.Admin, CompanyId = 0 });

                db.Users.AddRange(users);
                db.SaveChanges();

                messages.Add(new Message()
                {
                    Id = 0,
                    Type = MessageType.Condition,
                    KType = MessageKeyboardType.Reply,
                    Text = "Здавствуйте! К сожалению Вы не авторизованы в нашей системе. Для авторизации нажмите клавишу " +
                        "\"Авторизоваться\" ниже.\n*Нажимая клавишу \"Авторизоваться\" Вы соглашаетесь с обработкой персональных данных"
                });
                messages.Add(new Message()
                {
                    Id = 2,
                    Type = MessageType.Info,
                    KType = MessageKeyboardType.none,
                    Text = "Здавствуйте! Вы успешно авторизовались в нашей системе!"
                });
                messages.Add(new Message()
                {
                    Id = 1,
                    Type = MessageType.Info,
                    KType = MessageKeyboardType.none,
                    Text = "Извините, но Вы не зарегистрированы в нашей системе"
                });

                db.Messages.AddRange(messages);
                db.SaveChanges();

                button1.MessageId = 0;
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
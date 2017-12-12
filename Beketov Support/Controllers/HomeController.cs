using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Beketov_Support.Models;

namespace Beketov_Support.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            Logger.Wright("Бот работает", "Index", LogLevel.Info);

            return "ЙУ-ХУУУУУУУУУУУУУУУУУУУУУУУУУУУУУУУУУУУУУУУУУЙ!?";
        }
    }
}
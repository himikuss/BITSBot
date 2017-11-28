using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Beketov_Support.Models
{
    public static class BotSettings
    {
        public static string URL { get; set; } = "https://bot.beketov.it:443/{0}";

        public static string Name { get; set; } = "Beketov_Support_bot";

        public static string Key { get; set; } = "500841787:AAH6IdeoF0whRuyZUw-eA9G0HRz2ny7L5sw";

        public static int lastMessageID { get; set; } = 0;
    }
}
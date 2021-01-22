using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace DiscordBotTestCore
{
    public static class DRYMethods
    {
        public static string DateTimeAsPathFriendly(DateTime dateTime)
        {
            string dateTimeAsString = dateTime.ToString();
            dateTimeAsString = dateTimeAsString.Replace(":", ".");
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < dateTimeAsString.Length; i++)
            {
                char charAtIndex = dateTimeAsString[i];
                if (Path.GetInvalidFileNameChars().Any(x => x.Equals(charAtIndex)))
                    continue;
                if (Path.GetInvalidPathChars().Any(x => x.Equals(charAtIndex)))
                    continue;
                builder.Append(charAtIndex);
            }
            return builder.ToString();
        }
    }
}

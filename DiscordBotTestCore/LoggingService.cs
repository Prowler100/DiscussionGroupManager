using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Discord.WebSocket;
using Discord;
using Discord.Commands;

namespace DiscordBotTestCore
{
    public class LoggingService
    {
        private readonly string LogPath;
        private readonly string LogName;
        private readonly StreamWriter logWriter;
        public LoggingService(DiscordSocketClient client, CommandService command)
        {
            client.Log += LogAsync;
            command.Log += LogAsync;

            LogPath = Path.Combine(Environment.CurrentDirectory, "logs");
            LogName = DRYMethods.DateTimeAsPathFriendly(DateTime.Now) + ".txt";

            if (!Directory.Exists(LogPath))
                Directory.CreateDirectory(LogPath);

            logWriter = new StreamWriter(Path.Combine(LogPath, LogName));
            logWriter.AutoFlush = true;
        }
        private async Task LogAsync(LogMessage msg)
        {
            var colour = Console.ForegroundColor;
            switch (msg.Severity)
            {
                case LogSeverity.Critical:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case LogSeverity.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogSeverity.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogSeverity.Info:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case LogSeverity.Verbose:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case LogSeverity.Debug:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
            }
            string msgString = "";
            if (msg.Exception is CommandException cmdException)
                msgString = $"[{DateTime.Now}] Command/{msg.Severity}: {msg.Message}";
            else
                msgString = $"[{DateTime.Now}] General/{msg.Severity}: {msg.Message}";
            Console.WriteLine(msgString);
            Console.ForegroundColor = colour;
            await logWriter.WriteLineAsync(msgString);
        }
    }
}

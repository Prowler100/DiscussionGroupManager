using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace DiscordBotTestCore
{
    public class Program
    {
        public static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        private DiscordSocketClient client;
        private CommandService commands;
        private LoggingService loggingService;
        private CommandHandler commandHandler;

        public async Task MainAsync()
        {
            client = new DiscordSocketClient();
            commands = new CommandService();
            loggingService = new LoggingService(client, commands);
            commandHandler = new CommandHandler(client, commands, '!');

            await commandHandler.InstallCommandsAsync();

            string token = Environment.GetEnvironmentVariable("BOT_TOKEN", EnvironmentVariableTarget.User);

            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();

            await Task.Delay(-1);
        }
    }
}

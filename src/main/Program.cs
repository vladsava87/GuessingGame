using GuessingGame.interfaces;
using GuessingGame.services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GuessingGame
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            var game = host.Services.GetRequiredService<Game>();
            game.Start();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<IHighScoreService, HighScoreService>();
                    services.AddSingleton<IConsoleService, ConsoleService>();

                    services.AddTransient<Game>();
                });
    }
}

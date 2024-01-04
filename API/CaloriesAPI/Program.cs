using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NLog.Web;
using System;
using System.IO;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            try
            {
                logger.Info("Initializing application...");

                var host = CreateHostBuilder(args).Build();

                host.Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Application stopped because of exception");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseKestrel()
            .UseNLog()
            .UseContentRoot(Directory.GetCurrentDirectory())
            .UseUrls("http://*:5000")
            .UseIISIntegration()
            .UseStartup<Startup>();
    }
}

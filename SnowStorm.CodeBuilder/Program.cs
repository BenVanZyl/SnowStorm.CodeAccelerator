using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SnowStorm.CodeBuilder.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnowStorm.CodeBuilder
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new MainForm());

            var services = new ServiceCollection();

            ConfigureServices(services);

            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                var startForm = serviceProvider.GetRequiredService<MainForm>();
                Application.Run(startForm);
            }
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            services
                .AddMediatR(typeof(Program).GetTypeInfo().Assembly)
                .AddSingleton<IAppDbConnection, AppDbConnection>()
                .AddSingleton<ICodeGeneratorHelper, CodeGeneratorHelper>()
                //.AddScoped<ProfileManager>()
                .AddScoped<MainForm>()
                ;

        }
    }
}

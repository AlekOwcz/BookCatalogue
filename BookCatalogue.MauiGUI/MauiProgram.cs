using BookCatalogue.MauiGUI.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Reflection;
using BookCatalogue.UIWeb.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Maui.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace BookCatalogue.MauiGUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream("BookCatalogue.MauiGUI.appsettings.json");
            var config = new ConfigurationBuilder()
                        .AddJsonStream(stream)
                        .Build();
            builder.Configuration.AddConfiguration(config);
            string libraryName = builder.Configuration.GetSection("DAOLibraryName").Value;
            if (libraryName == null)
            {
                throw new Exception("LibraryName is not set in the configuration.");
            }

            builder.Services.AddSingleton(BLC.BLC.GetInstance(libraryName));



            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddSingleton<BooksPage>();
            builder.Services.AddSingleton<BooksCollectionViewModel>();
            builder.Services.AddSingleton<BookViewModel>();



#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}

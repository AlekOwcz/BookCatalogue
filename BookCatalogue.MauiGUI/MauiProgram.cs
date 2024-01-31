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
            try
            {
                string projectDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string targetDirectory = Path.Combine(projectDirectory, "..", "..", "..", "..", "..");
                Directory.SetCurrentDirectory(targetDirectory);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            string path = Path.Combine(Environment.CurrentDirectory, libraryName);
            builder.Services.AddSingleton(BLC.BLC.GetInstance(path));



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
            builder.Services.AddSingleton<AuthorsPage>();
            builder.Services.AddSingleton<AuthorViewModel>();
            builder.Services.AddSingleton<AuthorsCollectionViewModel>();


#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}

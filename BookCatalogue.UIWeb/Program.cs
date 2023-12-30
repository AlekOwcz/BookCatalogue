using BookCatalogue.BLC;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BookCatalogue.UIWeb.Data;

namespace BookCatalogue.Core
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<BookCatalogueUIWebContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("BookCatalogueUIWebContext") ?? throw new InvalidOperationException("Connection string 'BookCatalogueUIWebContext' not found.")));
            var configuration = builder.Configuration;
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            string? libraryName = configuration.GetValue<string>("DAOLibraryName");
            if (libraryName == null )
            {
                throw new Exception("LibraryName is not set in the configuration.");
            }
            builder.Services.AddSingleton(BLC.BLC.GetInstance(libraryName));
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
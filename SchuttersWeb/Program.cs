using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchuttersData.Models;
using SchuttersData.Repositories;
using SchuttersServices;
using SchuttersWeb.Data;

namespace SchuttersWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //// Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            ////var connectionString = builder.Configuration.GetConnectionString("SchuttersConnection") ?? throw new InvalidOperationException("Connection string 'SchuttersConnection' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));



            builder.Services.AddDbContext<SchuttersContext>(options =>
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("SchuttersConnection"),
                    x => x.MigrationsAssembly("SchuttersData")));


            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>();


            //SQLClubRepository + service config
            builder.Services.AddTransient<ClubService>();
            builder.Services.AddTransient<IClubRepository, SQLClubRepository>();


            //SQLLidRepository + service config
            builder.Services.AddTransient<LidService>();
            builder.Services.AddTransient<ILidRepository, SQLLidRepository>();


            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
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
            app.MapRazorPages();

            app.Run();
        }
    }
}

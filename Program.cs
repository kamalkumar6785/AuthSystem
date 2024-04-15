using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AuthSystem.Data;
using AuthSystem.Areas.Identity.Data;
namespace AuthSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            

            builder.Services.AddDbContext<AuthDbContext>(options =>
            options.UseInMemoryDatabase(databaseName: "MyInMemoryDatabase"));

            builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<AuthDbContext>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddRazorPages();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireUppercase = false;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
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

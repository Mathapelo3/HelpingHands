using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HelpingHands.Data;
using HelpingHands.Areas.Identity.Data;
using System.Data;
using Microsoft.Data.SqlClient;
using HelpingHands.Repositories;
using System.Security.Cryptography.X509Certificates;


internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("HelpingHandsContextConnection") ?? throw new InvalidOperationException("Connection string 'HelpingHandsContextConnection' not found.");

        builder.Services.AddTransient<IDbConnection>(options =>
        new SqlConnection(builder.Configuration.GetConnectionString("HelpingHandsContextConnection")));

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
           options.UseSqlServer(connectionString));

        builder.Services.AddDbContext<HelpingHandsContext>(options =>
            options.UseSqlServer(connectionString));

        builder.Services.AddDefaultIdentity<HelpingHandsUser>(options => options.SignIn.RequireConfirmedAccount = false)
             .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<HelpingHandsContext>();

        
        
            builder.Services.AddScoped<ICityRepo, CityRepo>();
            builder.Services.AddScoped<ISuburbsRepo, SuburbRepo>();
            builder.Services.AddScoped<IPatientRepo, PatientRepository>();
            builder.Services.AddScoped<UserRepo, UserRepo>();
       
        // Add services to the container.
        builder.Services.AddControllersWithViews();

        builder.Services.AddRazorPages();

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
        app.UseAuthentication(); ;

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        app.MapRazorPages();

        using (var scope = app.Services.CreateScope())
        {
            var roleManager =
                scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var roles = new[] { "Admin", "Office Manager", "Nurse", "Patient" };

            foreach (var role in roles)
            {

                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

        }

        using (var scope = app.Services.CreateScope())
        {
            var userManager =
                scope.ServiceProvider.GetRequiredService<UserManager<HelpingHandsUser>>();

            string username = "helpHandsAdmin@gmail.com";
            string email = "helpHandsAdmin@gmail.com";
            string phoneNo = "0682581235";
            string password = "helpHands@123";

            if (await userManager.FindByEmailAsync(email) == null)
            {
                var user = new HelpingHandsUser();
                user.Email = email;
                user.UserName = username;
                user.PhoneNumber = phoneNo;

                await userManager.CreateAsync(user, password);

                await userManager.AddToRoleAsync(user, "Admin");
            }
        }

        app.Run();

        
    }
}




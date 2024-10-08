using Microsoft.EntityFrameworkCore;
using MovieShop_Oregano.Data;
using MovieShop_Oregano.Services;

namespace MovieShop_Oregano
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var connectionString = builder.Configuration.GetConnectionString(
           "LexiconConnection") ?? throw new InvalidCastException("Default Connection not found");

            builder.Services.AddDbContext<MovieDbContext>(
                options =>
             options.UseSqlServer(connectionString)
                );

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10000000);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            builder.Services.AddHttpContextAccessor();


            builder.Services.AddScoped<ICustomerService, CustomerService>();
           
			builder.Services.AddScoped<IMovieService, MovieService>();

            builder.Services.AddScoped<IOrderService, OrderService>();

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
            app.UseSession();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}

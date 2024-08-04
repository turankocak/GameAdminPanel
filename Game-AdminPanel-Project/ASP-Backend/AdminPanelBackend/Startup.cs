using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure; 
using AdminPanelBackend.Data; 
using AdminPanelBackend.Services; 
using AdminPanelBackend.Settings; 
using MongoDB.Driver; 
using Microsoft.Extensions.Options; 
using AdminPanelBackend.Repositories; 
using Microsoft.AspNetCore.Authentication.Cookies; 

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        // MySQL Yapılandırması
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(Configuration.GetConnectionString("DefaultConnection"), 
                new MySqlServerVersion(new Version(8, 0, 21)))); 

        // Kullanıcı servisi ekle
        services.AddScoped<IUserService, UserService>();

        // MongoDB yapılandırması
        services.Configure<MongoDBSettings>(
            Configuration.GetSection(nameof(MongoDBSettings))); 

        // MongoDBSettings'i singleton olarak ekle
        services.AddSingleton<IMongoDBSettings>(sp =>
            sp.GetRequiredService<IOptions<MongoDBSettings>>().Value);

        // MongoClient'i singleton olarak ekle
        services.AddSingleton<IMongoClient>(sp =>
            new MongoClient(sp.GetRequiredService<IMongoDBSettings>().ConnectionString));

        // IMongoDatabase'i scoped olarak ekle
        services.AddScoped<IMongoDatabase>(sp =>
        {
            var settings = sp.GetRequiredService<IMongoDBSettings>();
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase(settings.DatabaseName); 
        });

        // Repositories ekle
        services.AddScoped<BuildingRepository>();
        services.AddScoped<ConfigRepository>();

        // Servisler ekle
        services.AddScoped<BuildingService>();
        services.AddScoped<ConfigService>();

        // CORS politikası ekle
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins",
                builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader(); 
                });
        });

        services.AddControllers(); // MVC Controller rotalarını ekle

        // Kimlik doğrulama ve yetkilendirme
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login"; // Giriş yolu
                    options.LogoutPath = "/Account/Logout"; // Çıkış yolu
                    options.AccessDeniedPath = "/Account/AccessDenied"; // Erişim engellendi yolu
                });

        services.AddAuthorization(); // Yetkilendirme hizmetleri ekle
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage(); // Geliştirme ortamında hata sayfası göster
        }
        else
        {
            app.UseExceptionHandler("/Home/Error"); 
            app.UseHsts(); 
        }

        app.UseHttpsRedirection(); // HTTP'den HTTPS'ye yönlendirme
        app.UseStaticFiles(); // Statik dosyalara erişimi sağla
        app.UseRouting(); // Yönlendirme middleware'ini kullan

        // CORS politikası ekle
        app.UseCors("AllowAllOrigins");

        // Kimlik doğrulama ve yetkilendirme
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers(); // MVC Controller rotalarını ekle
        });
    }
}

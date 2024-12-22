using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using WebOdevi.Models;

var builder = WebApplication.CreateBuilder(args);

// Veritabaný baðlamýný ekleyin
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity servisini ekleyin
builder.Services.AddIdentity<Kullanici, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // E-posta doðrulama zorunluluðunu devre dýþý býrakýr
    // Þifre gereksinimlerini yumuþatýyoruz.
    options.Password.RequireDigit = false; // Rakam gereksinimini kaldýrýyoruz
    options.Password.RequireLowercase = false; // Küçük harf gereksinimini kaldýrýyoruz
    options.Password.RequireUppercase = false; // Büyük harf gereksinimini kaldýrýyoruz
    options.Password.RequireNonAlphanumeric = false; // Özel karakter gereksinimini kaldýrýyoruz
    options.Password.RequiredLength = 3; // Þifreyi en az 3 karakter yapýyoruz, "sau" yeterli olacak
    options.Password.RequiredUniqueChars = 1; // Þifrede en az bir benzersiz karakter olmalý
}).AddEntityFrameworkStores<ApplicationDbContext>()
  .AddDefaultTokenProviders();

// Razor Pages ve MVC servislerini ekleyin
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// HTTP Request Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication(); // Kullanýcý kimlik doðrulamasýný ekleyin
app.UseAuthorization();  // Kullanýcý yetkilendirmesini ekleyin

// Razor Pages ve MVC rotalarýný haritalayýn
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

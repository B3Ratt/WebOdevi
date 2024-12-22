using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using WebOdevi.Models;

var builder = WebApplication.CreateBuilder(args);

// Veritaban� ba�lam�n� ekleyin
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity servisini ekleyin
builder.Services.AddIdentity<Kullanici, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // E-posta do�rulama zorunlulu�unu devre d��� b�rak�r
    // �ifre gereksinimlerini yumu�at�yoruz.
    options.Password.RequireDigit = false; // Rakam gereksinimini kald�r�yoruz
    options.Password.RequireLowercase = false; // K���k harf gereksinimini kald�r�yoruz
    options.Password.RequireUppercase = false; // B�y�k harf gereksinimini kald�r�yoruz
    options.Password.RequireNonAlphanumeric = false; // �zel karakter gereksinimini kald�r�yoruz
    options.Password.RequiredLength = 3; // �ifreyi en az 3 karakter yap�yoruz, "sau" yeterli olacak
    options.Password.RequiredUniqueChars = 1; // �ifrede en az bir benzersiz karakter olmal�
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
app.UseAuthentication(); // Kullan�c� kimlik do�rulamas�n� ekleyin
app.UseAuthorization();  // Kullan�c� yetkilendirmesini ekleyin

// Razor Pages ve MVC rotalar�n� haritalay�n
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

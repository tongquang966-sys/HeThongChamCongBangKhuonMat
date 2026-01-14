using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Services;

var builder = WebApplication.CreateBuilder(args);

// =====================================================
// 1️⃣ ADD SERVICES (DEPENDENCY INJECTION)
// =====================================================

// MVC
builder.Services.AddControllersWithViews();

// ================= DATABASE =================
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);
builder.Services.AddHttpClient();

// ================= CUSTOM SERVICES =================
builder.Services.AddScoped<AttendanceService>();
builder.Services.AddScoped<FaceRecognitionService>();

// ================= HTTP CLIENT (AI SERVER) =================
builder.Services.AddHttpClient<FaceRecognitionService>(client =>
{
    client.BaseAddress = new Uri("http://127.0.0.1:8000");
    client.Timeout = TimeSpan.FromSeconds(30);
});

// ================= AUTHENTICATION & AUTHORIZATION =================
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";

        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole("Admin"));
});

// =====================================================
// 2️⃣ BUILD APP
// =====================================================

var app = builder.Build();

// =====================================================
// 3️⃣ MIDDLEWARE PIPELINE
// =====================================================

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// 🔐 BẮT BUỘC: Authentication phải đứng TRƯỚC Authorization
app.UseAuthentication();
app.UseAuthorization();

// =====================================================
// 4️⃣ ROUTING (🔥 RẤT QUAN TRỌNG – AREAS)
// =====================================================

// 👉 ROUTE CHO ADMIN / USER (AREAS)
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

// 👉 ROUTE MẶC ĐỊNH – HOME CHUNG
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();

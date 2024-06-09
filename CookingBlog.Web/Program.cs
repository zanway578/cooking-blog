using CookingBlog.Database;
using CookingBlog.Database.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("CookingBlogContextConnection") ?? throw new InvalidOperationException("Connection string 'CookingBlogContextConnection' not found.");

// Add services to the container.

builder.Services.AddRazorPages(options =>
{ 
    options.Conventions.AuthorizeFolder("/intranet/admin", "AdminPolicy");
});
builder.Services.AddControllers();

builder.Services.AddDbContext<CookingBlogContext>(options => options.UseSqlServer());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

CookingBlogContext.GlobalConnectionString = connectionString;

builder.Services.AddIdentity<IdentityApplicationUser, IdentityApplicationRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
})
    .AddRoles<IdentityApplicationRole>()
    .AddEntityFrameworkStores<CookingBlogContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
})
    .ConfigureApplicationCookie(options =>
    {
        options.LoginPath = new PathString("/intranet/login");
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();

app.Run();

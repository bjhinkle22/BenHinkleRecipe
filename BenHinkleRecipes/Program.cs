using BenHinkleRecipes.DAL.Context;
using BenHinkleRecipes.DAL.Repos;
using BenHinkleRecipes.Interfaces.RepoInterfaces;
using BenHinkleRecipes.Interfaces.ServiceInterfaces;
using BenHinkleRecipes.Services;
using BenHinkleRecipes.Services.VMServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
builder.Services.AddScoped<IUserRecipeRepository, UserRecipeRepoository>();
builder.Services.AddScoped<IUserRecipeService, UserRecipeService>();
builder.Services.AddScoped<IUserFavoriteRepository, UserFavoriteRepository>();
builder.Services.AddScoped<IUserFavoriteService, UserFavoriteService>();
builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddScoped<IRecipeVMService, RecipeVMService>();
builder.Services.AddScoped<IUserRecipeVMService, UserRecipeVMService>();
builder.Services.AddScoped<IGroceryListRepository, GroceryListRepository>();
builder.Services.AddScoped<IGroceryListService, GroceryListService>();
builder.Services.AddScoped<IGroceryListVMService, GroceryListVMService>();
builder.Services.AddScoped<IIngredientRepository, IngredientRepository>();
builder.Services.AddScoped<IIngredientService, IngredientService>();
builder.Services.AddScoped<IIngredientVMService, IngredientVMService>();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);
builder.Services.ConfigureApplicationCookie(o => {
    o.ExpireTimeSpan = TimeSpan.FromDays(5);
    o.SlidingExpiration = true;
});
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<DataContext>();
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
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
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

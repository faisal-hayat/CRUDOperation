using CRUD_Operations.Repository;
using CRUD_Operations.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CRUD_Operations.Data;
using CRUD_Operations;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();

#region add sql server context
// This is where we will be adding the sql server service
builder.Services.AddDbContext<CRUD_Operations.Data.ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
));

builder.Services.AddDbContext<CRUD_OperationsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
#endregion


#region configure identity 
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<CRUD_OperationsContext>();
// repository services will be added here
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 2;
    options.Password.RequireDigit = false;
    options.Password.RequiredUniqueChars = 0;
    options.SignIn.RequireConfirmedEmail = false;
});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseCustomMiddleware();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();;
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// following line need to be added
app.MapRazorPages();
app.Run();
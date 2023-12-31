# CRUD Operations in ASP.NET Core
[source](https://www.youtube.com/watch?v=VYmsoCWjvM4&list=PLjC4UKOOcfDQfrxjOgGKM_UmydQig8pq5)

--- ---

## Add Libraries

- MicrosoftEntityFramework Core
- MicrosoftEntityFramework SQL Server
- MicrosoftEntityFramework Tools

--- ---

## Changes in program.cs

```c#
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
var app = builder.Build();
// This is where we will be adding the sql server service
builder.Services.AddDbContext<CRUD_Operations.Data.ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
));
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
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

```

--- ---

## Command to build the propgram

```shell
dotnet build CRUD_Operations.sln
```

--- ---

## Repository Pattern

- Register the repository pattern

```c#
using CRUD_Operations.Repository;
using CRUD_Operations.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
// This is where we will be adding the sql server service
builder.Services.AddDbContext<CRUD_Operations.Data.ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
));

// repository services will be added here
builder.Services.AddSingleton<ITransactionRepository, TransactionRepository>();

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
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
```

--- ---

## Repository Pattern Method

```c#
using CRUD_Operations.Models;

namespace CRUD_Operations.Repository.IRepository
{
    public interface ITransactionRepository
    {
        // get all students
        Task<IEnumerable<Transaction>> GetAll();
        Task Create(Transaction transaction);
        Task<Transaction>? FindbyId(int? id);
        Task<Transaction>? Edit(int id, Transaction transaction);

        // Task Delete(Transaction transaction);
        // Task<IEnumerable<Transaction>> Update();
    }
}

```

--- ---

## Adding Identity 

- We will be using identity
- Following line has been added for **_add-migration_** and **_update-database_**

```c#
add-migration "migration message" -context CRUD_OperationsContext
update-database -context CRUD_OperationsContext
```

--- ---
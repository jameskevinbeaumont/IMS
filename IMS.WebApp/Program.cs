// This is a console application that runs line by line
// FIRST
using IMS.Plugins.InMemory;
using IMS.UseCases.Inventories;
using IMS.UseCases.Inventories.Interfaces;
using IMS.UseCases.PluginInterfaces;
using IMS.WebApp.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
// This is all dependency injection
builder.Services.AddRazorComponents();

// Register the IInventoryRepository interface and it's implementation (InventoryRepository)
// AddSingleton is what defines the lifetime of the object in memory when it is instantiated
// The first time the object is required it will be created but after that it will be stored in
// the dependency injection container
// We use AddSingleton for the data store
builder.Services.AddSingleton<IInventoryRepository, InventoryRepository>();

// Register the IViewInventoriesByNameUseCase interface and it's implementation (ViewInventoriesByNameUseCase)
// AddTransient is what defines the lifetime of the object in memory when it is instantiated
// Everytime a class or razor component requires this use case it will create a new object
// We use AddTransient for the use case
builder.Services.AddTransient<IViewInventoriesByNameUseCase, ViewInventoriesByNameUseCase>();

// Register the IAddInventoryUseCase interface and it's implementation (AddInventoryUseCase)
builder.Services.AddTransient<IAddInventoryUseCase, AddInventoryUseCase>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

// Takes care of the HTTPS requests and maps them to the App component (App.razor in the Components folder)
// App.razor is the single page - the root component
app.MapRazorComponents<App>();

// This runs in a loop listening for HTTP requests
// When an HTTP request comes in, Program.cs will execute line by line
app.Run();

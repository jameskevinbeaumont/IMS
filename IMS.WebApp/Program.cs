// This is a console application that runs line by line
// FIRST
using IMS.Plugins.InMemory;
using IMS.UseCases.Activities;
using IMS.UseCases.Activities.Interfaces;
using IMS.UseCases.Inventories;
using IMS.UseCases.Inventories.Interfaces;
using IMS.UseCases.PluginInterfaces;
using IMS.UseCases.Products;
using IMS.UseCases.Products.Interfaces;
using IMS.WebApp.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
// This is all dependency injection
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();  // This is the dependency, via dependency injection, that the middleware
                                        // needs to set up the SignalR channel

// Inventory Repository
// Register the IInventoryRepository interface and it's implementation (InventoryRepository)
// AddSingleton is what defines the lifetime of the object in memory when it is instantiated
// The first time the object is required it will be created but after that it will be stored in
// the dependency injection container
// We use AddSingleton for the data store
builder.Services.AddSingleton<IInventoryRepository, InventoryRepository>();
// Product Repository
// Register the IProductRepository interface and it's implementation (ProductRepository)
builder.Services.AddSingleton<IProductRepository, ProductRepository>();
// Inventory Transaction Repository
// Register the IInventoryTransactionRepository interface and it's implementation (InventoryTransactionRepository)
builder.Services.AddSingleton<IInventoryTransactionRepository, InventoryTransactionRepository>();

// Inventories
// Register the IViewInventoriesByNameUseCase interface and it's implementation (ViewInventoriesByNameUseCase)
// AddTransient is what defines the lifetime of the object in memory when it is instantiated
// Everytime a class or razor component requires this use case it will create a new object
// We use AddTransient for the use case
builder.Services.AddTransient<IViewInventoriesByNameUseCase, ViewInventoriesByNameUseCase>();
// Register the IAddInventoryUseCase interface and it's implementation (AddInventoryUseCase)
builder.Services.AddTransient<IAddInventoryUseCase, AddInventoryUseCase>();
// Register the IEditInventoryUseCase interface and it's implementation (EditInventoryUseCase)
builder.Services.AddTransient<IEditInventoryUseCase, EditInventoryUseCase>();
// Register the IViewInventoryByIDUseCase interface and it's implementation (ViewInventoryByIDUseCase)
builder.Services.AddTransient<IViewInventoryByIDUseCase, ViewInventoryByIDUseCase>();
// Register the IDeleteInventoryUseCase interface and it's implementation (DeleteInventoryUseCase)
builder.Services.AddTransient<IDeleteInventoryUseCase, DeleteInventoryUseCase>();

// Products
// Register the IViewProductsByNameUseCase interface and it's implementation (ViewProductsByNameUseCase)
builder.Services.AddTransient<IViewProductsByNameUseCase, ViewProductsByNameUseCase>();
// Register the IDeleteProductUseCase interface and it's implementation (DeleteProductUseCase)
builder.Services.AddTransient<IDeleteProductUseCase, DeleteProductUseCase>();
// Register the IAddProductUseCase interface and it's implementation (AddProductUseCase)
builder.Services.AddTransient<IAddProductUseCase, AddProductUseCase>();
// Register the IViewProductByIDUseCase interface and it's implementation (ViewProductByIDUseCase)
builder.Services.AddTransient<IViewProductByIDUseCase, ViewProductByIDUseCase>();
// Register the IEditProductUseCase interface and it's implementation (EditProductUseCase)
builder.Services.AddTransient<IEditProductUseCase, EditProductUseCase>();

// Activities
// Register the IPurchaseInventoryUseCase interface and it's implementation (PurchaseInventoryUseCase)
builder.Services.AddTransient<IPurchaseInventoryUseCase, PurchaseInventoryUseCase>();

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

// Middleware
// Takes care of the HTTPS requests and maps them to the App component (App.razor in the Components folder)
// App.razor is the single page - the root component
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode(); // This allows the root component to set up an interactive SignalR channel 

// This runs in a loop listening for HTTP requests
// When an HTTP request comes in, Program.cs will execute line by line
app.Run();

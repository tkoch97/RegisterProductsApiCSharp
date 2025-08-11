using RegisterProductsAPI.Data;
using RegisterProductsAPI.Interfaces;
using RegisterProductsAPI.Repository;
using RegisterProductsAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<IProductRepository, ProductsRepository>();
builder.Services.AddScoped<ProductService>();

var app = builder.Build();

app.UseRouting();
app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
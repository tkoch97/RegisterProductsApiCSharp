using RegisterProductsAPI.Data;
using RegisterProductsAPI.Interfaces;
using RegisterProductsAPI.Repository;
using RegisterProductsAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<IProductRepository, ProductsRepository>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddEndpointsApiExplorer(); ////Importante para listar os endpoints
//adiciona o Swagger, configura as informações da api e habilitar o caminho do arquivo xml
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "RegisterProductsAPI",
        Version = "v1",
        Contact = new OpenApiContact
        {
            Name = "Thiago Koch",
            Email = "tkoch.dev@gmail.com",
            Url = new Uri("https://www.linkedin.com/in/tkochdev/")
        }
    });

    var xmlFile = "RegisterProductsAPI.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    s.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

//Configuração do Swagger, em ambiente de desenvolvimento por padrão.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
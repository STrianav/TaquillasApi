using Microsoft.EntityFrameworkCore;
using TaquillasApi.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
                opciones.UseSqlServer("name=DefaultConnection"));
builder.Services.AddCors(options => options.AddPolicy("AllowWebAoo",
                            builder => builder.AllowAnyOrigin()
                                               .AllowAnyHeader()
                                               .AllowAnyMethod()));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowWebApp", builder =>
    {
        builder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowWebApp");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

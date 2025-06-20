using WebApiLibrary.Context;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


var connStr = builder.Configuration.GetConnectionString("Connection");
var serverVersion = ServerVersion.AutoDetect(connStr);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connStr, serverVersion)
);



builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();

using Microsoft.EntityFrameworkCore;
using ShipOrderBack.Context;
using ShipOrderBack.Service;
using ShipOrderBack.Service.Interface;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


var allowedCorsOrigins =
    builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [];
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .WithOrigins(allowedCorsOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod();
            //.AllowCredentials();
    });
});


// Conexão com o SQL Server
string? sqlConnectionEPS = builder.Configuration.GetConnectionString("ConnectionEPS");
string? sqlConnectionJEMS = builder.Configuration.GetConnectionString("ConnectionJEMS");
string? sqlConnectionQuarentine = builder.Configuration.GetConnectionString("ConnectionQuarentine");
string? sqlConnectionPrintForm = builder.Configuration.GetConnectionString("ConnectionPrintForm");

builder.Services.AddDbContext<EpsDbContext>(options =>
    options.UseSqlServer(sqlConnectionEPS)
);
builder.Services.AddDbContext<JemsDbContext>(options =>
    options.UseSqlServer(sqlConnectionJEMS)
);
builder.Services.AddDbContext<QuarentineDbContext>(options =>
    options.UseSqlServer(sqlConnectionQuarentine)
);
builder.Services.AddDbContext<OrderContext>(options =>
    options.UseSqlServer(sqlConnectionPrintForm, o => o.UseCompatibilityLevel(120))
);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IQuarentineService, QuarentineService>();
builder.Services.AddScoped<ILabelService, LabelService>();
builder.Services.AddScoped<IPackoutService, PackoutService>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();
app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
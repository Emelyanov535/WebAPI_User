using BusinessLogic;
using Contracts.BusiniesLogicContracts;
using Contracts.StoragesContracts;
using ListStorage.Implements;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddTransient<IUserLogic, UserLogic>();
builder.Services.AddTransient<IUserStorage, UserStorage>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

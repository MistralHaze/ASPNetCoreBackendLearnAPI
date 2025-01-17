using BackendLearnUdemy.Automappers;
using BackendLearnUdemy.DataTransferObjects;
using BackendLearnUdemy.Models;
using BackendLearnUdemy.Repository;
using BackendLearnUdemy.Services;
using BackendLearnUdemy.Services.BeerStore;
using BackendLearnUdemy.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Also can be Scoped(one different for client connection) or transient(one different for each time code is executed)
builder.Services.AddKeyedSingleton<IPeopleService, PeopleServiceBackup>("backup");
builder.Services.AddKeyedSingleton<IPeopleService, PeopleService>("people");
builder.Services.AddKeyedScoped<ICommonService<BeerDTO, BeerInsertDTO, BeerUpdateDTO>, BeerService>("beerService");

builder.Services.AddScoped<IPostsService, PostsService>();

//HTTPClient jsonPlaceHolder service
builder.Services.AddHttpClient<IPostsService, PostsService>(c =>
{
    c.BaseAddress = new Uri(builder.Configuration["BaseUrlPost"]);
});
builder.Services.AddScoped<IRepository<Beer>, BeerRepository>();

//Entity Framework DbContext Config
builder.Services.AddDbContext<StoreContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("StoreConnection"));
});

//Validators (Fluent Validation Package)
builder.Services.AddScoped<IValidator<BeerInsertDTO>, BeerInsertValidator>();
builder.Services.AddScoped<IValidator<BeerUpdateDTO>, BeerUpdateValidator>();

//Mappers
builder.Services.AddAutoMapper(typeof(MappingProfile));


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

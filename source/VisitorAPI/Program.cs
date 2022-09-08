using BookAPI;
using LibraryTransit.Contract.Configuration;
using VisitorAPI.Profiles;
using VisitorAPI.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddLibraryTransitSharedConfiguration(builder.Environment);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddAutoMapper(opt =>
{
    opt.AddProfile<VisitorProfile>();
});

builder.Services.AddVisitorMassTransit(builder.Configuration);

builder.Services.AddVisitorDbContext(builder.Configuration, builder.Environment);

builder.Services.AddScoped<IVisitorRepository, VisitorRepository>();
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

app.UseAuthorization();

app.MapControllers();

app.ApplyDbMigration();

app.Run();

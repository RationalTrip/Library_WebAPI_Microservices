using AuthorAPI;
using AuthorAPI.Profiles;
using AuthorAPI.Repository;
using LibraryTransit.Contract.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddLibraryTransitSharedConfiguration(builder.Environment);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(opt =>
{
    opt.AddProfile<AuthorsProfile>();
});

builder.Services.AddAuthorsMassTransit(builder.Configuration);

builder.Services.AddAuthorDbContext(builder.Configuration, builder.Environment);

builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();

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

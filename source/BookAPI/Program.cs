using BookAPI;
using BookAPI.Profiles;
using BookAPI.Repository;
using LibraryTransit.Contract.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddLibraryTransitSharedConfiguration(builder.Environment);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddAutoMapper(opt =>
{
    opt.AddProfile<AuthorsProfile>();
    opt.AddProfile<BooksProfile>();
});

builder.Services.AddBookMassTransit(builder.Configuration);

builder.Services.AddBookDbContext(builder.Configuration, builder.Environment);

builder.Services.AddScoped<IBookRepository, BookRepository>();

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

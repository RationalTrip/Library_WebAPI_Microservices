using HistoryAPI;
using HistoryAPI.Profiles;
using HistoryAPI.Services.Grpc;
using LibraryTransit.Contract.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddLibraryTransitSharedConfiguration(builder.Environment);

// Add services to the container.

builder.Services.AddGrpc();

builder.Services.AddControllers();

builder.Services.AddAutoMapper(opt =>
{
    opt.AddProfile<VisitorsProfile>();
    opt.AddProfile<BooksProfile>();
    opt.AddProfile<HistoryRecordProfile>();
});

builder.Services.AddHistoryMassTransit(builder.Configuration);

builder.Services.AddHistoryDbContext(builder.Configuration, builder.Environment);
builder.Services.AddRepositories();

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

app.MapGrpcService<GrpcHistoryCreatorService>();

app.ApplyDbMigration();

app.Run();

using BorrowedAPI;
using BorrowedAPI.Profiles;
using BorrowedAPI.Services.Grpc;
using HistoryAPI.Services.Grpc;
using LibraryTransit.Contract.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddLibraryTransitSharedConfiguration(builder.Environment);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddGrpcClient<GrpcHistoryCreator.GrpcHistoryCreatorClient>(opt =>
{
    opt.Address = new Uri(builder.Configuration["Grpc:HistoryCreator:url"]);
});

builder.Services.AddScoped<IGrpcHistoryCreatorClient, GrpcHistoryCreatorClient>();

builder.Services.AddAutoMapper(opt =>
{
    opt.AddProfile<VisitorsProfile>();
    opt.AddProfile<BooksProfile>();
    opt.AddProfile<BorrowedRecordProfile>();
});

builder.Services.AddBorrowedMassTransit(builder.Configuration);

builder.Services.AddBorrowedDbContext(builder.Configuration, builder.Environment);

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

app.ApplyDbMigration();

app.Run();

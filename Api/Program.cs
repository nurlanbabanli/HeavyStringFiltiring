using Business.Abstract;
using Business.BackgroundWorkers;
using Business.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Add service into IoC 
builder.Services.AddSingleton<IChunkHolderService, ChunkHolderManager>();
builder.Services.AddSingleton<ITextFilterService, TextFilterManager>();

//add background Worker
builder.Services.AddHostedService<TextProcessingWorker>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();

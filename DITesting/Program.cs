using System.Reflection.Metadata;
using DiTestingLibrary;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var settings = builder.Configuration
    .GetSection("")
    .Get<BlobStorageOptions>();

builder.Services.AddBlobStorage<DefaultDelegateTokenRetrieval>(settings);
builder.Services.AddBlobStorage<DefaultDelegateTokenRetrieval>(options =>
{
    options.Name = "";
});

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
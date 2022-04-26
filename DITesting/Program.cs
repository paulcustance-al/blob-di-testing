using System.Security.Authentication;
using DITesting;
using EHR.BlobStorage.Sdk.V1;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<DelegatedTokenHelper>();

builder.Services
    .AddBlobStorage(options =>
    {
        options.SslProtocol = SslProtocols.Tls12;
    })
    .ConfigureCustomTokenRetrieval(provider =>
    {
        var tokenHelper = provider.GetRequiredService<DelegatedTokenHelper>();
        return tokenHelper.GetToken;
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
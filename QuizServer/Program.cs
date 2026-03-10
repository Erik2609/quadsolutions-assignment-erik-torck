using Polly;
using Polly.Extensions.Http;
using QuizRepository;
using QuizRepository.OpenTdb;
using QuizServices.Service;

var builder = WebApplication.CreateBuilder(args);
// CORS, I would obviously never do this on production code, but for the sake of demo purposes I enabled everything.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        });
});


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DI
builder.Services.AddScoped<IOpenTdbClient, OpenTdbClient>();
builder.Services.AddHttpClient<IOpenTdbClient, OpenTdbClient>()
    .SetHandlerLifetime(TimeSpan.FromMinutes(5))
    .AddPolicyHandler(GetRetryPolicy());
builder.Services.AddSingleton<IGetQuestionsRepository, OpenTdbRepository>();
builder.Services.Decorate<IGetQuestionsRepository, CachedGetQuestionsRepositoryDecorator>();
builder.Services.AddScoped<ICheckAnswersService, CheckAnswersService>();
builder.Services.AddScoped<IGetQuestionsService, GetQuestionsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


if (app.Environment.IsProduction())
{
    app.UseHttpsRedirection();
}

app.UseStaticFiles();
app.UseRouting();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();


static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
{
    return HttpPolicyExtensions
        .HandleTransientHttpError()
        .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
        .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2,
                                                                    retryAttempt * 2)));
}
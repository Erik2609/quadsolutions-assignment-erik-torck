using QuizRepository;
using QuizRepository.OpenTdb;
using QuizServices.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DI
builder.Services.AddSingleton<IGetQuestionsRepository, CachedGetQuestionsRepositoryWrapper>((sp) =>
new CachedGetQuestionsRepositoryWrapper(new OpenTdbRepository()));
builder.Services.AddScoped<ICheckAnswersService, CheckAnswersService>();
builder.Services.AddScoped<IGetQuestionsService, GetQuestionsService>();

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

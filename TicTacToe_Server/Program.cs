using Microsoft.AspNetCore.Authentication;
using TicTacToe_Server;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

builder.Services.AddSingleton<IUserRepository, UsersRepository>();
builder.Services.AddSingleton<IMatchesRepository, MatchesRepository>();

builder.Services.AddAuthentication("BasicAuthentication").
            AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>
            ("BasicAuthentication", null);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
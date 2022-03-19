using System.Text;
using Identity2;
using Microsoft.Extensions.Localization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapPost("/register", (RegisterBody rb) => UserManager.RegisterUser((User) rb));
app.MapPost("/login", (LoginBody lb) => UserManager.LoginUser(lb.Login, lb.Password));
app.MapPost("/rolechanging", (RoleBody rb) =>
    {
        var user = UserManager.GetUserByLogin(rb.Login);
        if (user != null)
        {
            user.ChangeRole(rb.Role);
            return "Success";
        }

        return "UserNotFound";
    }
);
app.MapGet("/getallusernames", () =>
    {
        return UserManager.Users.Select(u => u.Login);
    }
);









app.Run();

public class LoginBody
{
    public string Login { get; set; }
    public string Password { get; set; }

    
}
public class RegisterBody
{
    public string Login { get; set; }
    public string Password { get; set; }
    public static implicit operator User(RegisterBody body) => new User(body.Login, body.Password);
}

public class RoleBody
{
    public string Login { get; set; }
    public User.Roles Role { get; set; }
}




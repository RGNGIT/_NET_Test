using _NET_Test.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("env.json");
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMvc();
builder.Services.AddControllers();
builder.Services.AddServices();
builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
{
    options.TokenValidationParameters = Config.JWT.validationParameters;
});
builder.Services.AddAuthorization();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = "Movie.Session";
    // options.IdleTimeout = TimeSpan.FromSeconds(360000);
    // options.Cookie.IsEssential = true;
});

WebApplication app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    "default",
    app.Configuration["api-call"] + "/{controller}/{action}"
);

Config.configuration = app.Configuration;

app.Run();
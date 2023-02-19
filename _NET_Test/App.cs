using _NET_Test.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("env.json");
builder.Services.AddHealthChecks();
builder.Services.AddMemoryCache();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMvc();
builder.Services.AddControllers();
builder.Services.AddServices();
builder.Services.AddResponseCompression(options => options.EnableForHttps = true);
builder.Services.AddCors();
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

app.MapHealthChecks("/hp");
app.UseCors(b => b.AllowAnyOrigin());
app.UseResponseCompression();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    "default",
    app.Configuration["api-call"] + "/{controller}/{action}"
);

Config.configuration = app.Configuration;

app.Run();
using GameOfChance.Services;

var builder = WebApplication.CreateBuilder(args);

// Register IHttpContextAccessor
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Add services to the container.
builder.Services.AddSingleton<IGameService, GameService>();
builder.Services.AddSingleton<IUserManagementService, UserManagementService>();
builder.Services.AddTransient<IRandomNumberGenerator, RandomNumberGenerator>();
builder.Services.AddScoped<ISessionService, SessionService>();
builder.Services.AddScoped<IBetValidationService, BetValidationService>();

builder.Services.AddControllers()
     .AddJsonOptions(options =>
     {
         // Serialize enums as strings in JSON
         options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
     }); ;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add in-memory cache for session storage
builder.Services.AddDistributedMemoryCache();

// Add session middleware
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);  // Set session timeout
    options.Cookie.HttpOnly = true;                 //Make cookie HttpOnly
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest; //Secure the cookies
});

var app = builder.Build();

// Enable session middleware
app.UseSession(); 

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

using Application;
using Core.Utilities.JWT;
using Core;
using Core.Utilities.Encryption;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Persistence;


var builder = WebApplication.CreateBuilder(args);
TokenOptions? tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices();
builder.Services.AddPersistenceService(builder.Configuration);
builder.Services.AddCoreServices(tokenOptions);

var getValue = builder.Configuration.GetSection("TokenOptions").GetValue<string>("SecurityKey");

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder
                   .WithOrigins("http://localhost:4200")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});


builder.Services.
    AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
   .AddJwtBearer(options =>
   {
       options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
       {
           ValidateIssuer = true,
           ValidateAudience = true,
           ValidateLifetime = true,
           ValidateIssuerSigningKey = true,
           ValidIssuer = tokenOptions.Issuer,
           ValidAudience = tokenOptions.Audience,
           IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
       };
   });

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}


//app.ConfigureExceptionMiddlewareExtensions();

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigin");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

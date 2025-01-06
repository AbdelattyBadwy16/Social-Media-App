using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Optern.Application.Interfaces.ICacheService;
using Optern.Infrastructure.ExternalServices.JWTService;
using SocialMedia.Application.Extentions;
using SocialMedia.Application.Repository;
using SocialMedia.Core.Models;
using SocialMedia.Infrastructure.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(op => op.UseNpgsql(builder.Configuration.GetConnectionString("mycon")));


builder.Services.AddIdentity<User,IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Add Redis Cache
builder.Services.AddStackExchangeRedisCache(option =>
{
	option.Configuration = builder.Configuration.GetConnectionString("Redis");
	option.InstanceName = "socialAppdoc";
});




builder.Services.AddCustomJwtAuth(builder.Configuration);

builder.Services.AddScoped<IPostRepository , PostRepository>();	
builder.Services.AddScoped<IFriendRepository , FriendRepository>();	
builder.Services.AddScoped<IFavouritPostRepository , FavouritPostRepository>();	
builder.Services.AddScoped<IUserRepository , UserRepository>();	
builder.Services.AddScoped<IUserPostRepository , UserPostRepository>();	
builder.Services.AddScoped<IPhotoRepository , PhotoRepository>();	
builder.Services.AddScoped<ICommentRepository , CommentRepository>();	
builder.Services.AddScoped<IAccountRepository , AccountRepository>();
builder.Services.AddScoped<IJWTService , JWTService>();	
builder.Services.AddScoped<ICacheService,CacheService>();


builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(
		policy =>
		{
			policy.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader();
		});
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

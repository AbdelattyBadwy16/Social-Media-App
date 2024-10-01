using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Models;
using SocialMedia.Repository;
using TestRESTAPI.Extentions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("mycon")));


builder.Services.AddIdentity<User,IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


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


builder.Services.AddCustomJwtAuth(builder.Configuration);

builder.Services.AddScoped<IPostRepository , PostRepository>();	
builder.Services.AddScoped<IFriendRepository , FriendRepository>();	
builder.Services.AddScoped<IFavouritPostRepository , FavouritPostRepository>();	
builder.Services.AddScoped<IUserRepository , UserRepository>();	
builder.Services.AddScoped<IUserPostRepository , UserPostRepository>();	
builder.Services.AddScoped<IPhotoRepository , PhotoRepository>();	
builder.Services.AddScoped<ICommentRepository , CommentRepository>();	

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

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.Data;
using Store.DataAccess.Repositories;
using Store.Extensions;
using Store.Models.Models;
using Store.Repositories;
using Store.Repositories.IReqositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(op =>
      op.UseSqlServer(builder.Configuration.GetConnectionString("myCon")));

builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddScoped<IAccountRepo, AccountRepository>();
builder.Services.AddScoped<IAppDbContext, AppDbContextRepo>();
builder.Services.AddScoped<ICategoryRepo, CategoryRepository>();
builder.Services.AddScoped<IOrderRepo, OrderRepository>();
builder.Services.AddScoped<IItemRepo, ItemRepository>();
builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCustomAuth(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
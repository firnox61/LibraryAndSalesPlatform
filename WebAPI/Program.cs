using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.Abstract;
using Business.Concrete;
using Business.DependencyResolvers.Autofac;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();/*
builder.Services.AddSingleton<IBookService, BookManager>();
builder.Services.AddSingleton<IBookDal, EfBookDal>();

builder.Services.AddSingleton<INoteService, NoteManager>();
builder.Services.AddSingleton<INoteDal, EfNoteDal>();

builder.Services.AddSingleton<IUserService, UserManager>();
builder.Services.AddSingleton<IUserDal, EfUserDal>();

//builder.Services.AddSingleton<IShareService, ShareManager>();
builder.Services.AddSingleton<IShareDal, EfShareDal>();

builder.Services.AddSingleton<IAuthService, AuthManager>();*/

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(options =>
{
    options.RegisterModule(new AutofacBusinessModule());
});
builder.Services.AddCors();
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

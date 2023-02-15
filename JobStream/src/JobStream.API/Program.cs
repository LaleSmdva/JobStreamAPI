using FluentValidation;
using FluentValidation.AspNetCore;
using JobStream.Business.Mappers;
using JobStream.Business.Services.Implementations;
using JobStream.Business.Services.Interfaces;
using JobStream.Business.Validators.Account;
using JobStream.Core.Entities.Identity;
using JobStream.DataAccess.Contexts;
using JobStream.DataAccess.Repositories.Implementations;
using JobStream.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddDbContext<AppDbContext>(opts =>
{
	opts.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(typeof(CompanyMapper).Assembly);
//builder.Services.AddAutoMapper(typeof(VacanciesMapper).Assembly);

builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IAccountService, AccountService>();


builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
	opt.Password.RequireNonAlphanumeric = true;
	opt.Password.RequireDigit = true;
	opt.Password.RequiredLength = 6;
	opt.Password.RequireUppercase = true;
	opt.Password.RequireLowercase = true;
	opt.User.RequireUniqueEmail = true;
	opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
	//opt.SignIn.RequireConfirmedEmail = true;
	opt.Lockout.MaxFailedAccessAttempts = 3;
	opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
	opt.Lockout.AllowedForNewUsers = false;

	//future use

	//opt.Tokens.EmailConfirmationTokenProvider= null;

}).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssembly(typeof(RegisterCandidateDTOValidator).Assembly);

builder.Services.AddScoped<AppDbContextInitializer>();

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

app.UseAuthentication();
app.UseAuthorization();
using (var scope = app.Services.CreateScope())
{
	//var initializer = app.Services.GetRequiredService<AppDbContextInitializer>(); //dependency injection
	var initializer = scope.ServiceProvider.GetRequiredService<AppDbContextInitializer>(); //dependency injection
	await initializer.SeedRoleAsync();
	await initializer.SeedUserAsync();
}
app.MapControllers();

app.Run();

using ITAssetManager.Components;
using ITAssetManager.Components.Account;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Blazorise;
using Blazorise.Icons.FontAwesome;
using Blazorise.Bootstrap5;
using ITAssetManagerLibrary.Data;
using ITAssetManagerLibrary.Helpers;
using ITAssetManagerLibrary.Constants;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents()
    .AddAuthenticationStateSerialization(
        options => options.SerializeAllClaims = true);

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
var assemplbyName = typeof(Program).Assembly.GetName().Name;
#if DEBUG
builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString, b => b.MigrationsAssembly(assemplbyName)).EnableSensitiveDataLogging());
#else
builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString, b => b.MigrationsAssembly(assemplbyName)));
#endif
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Configure Identity with roles support
builder.Services.AddIdentityCore<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
    .AddRoles<IdentityRole>() // Add role support
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

builder.Services
    .AddBlazorise(options =>
    {
        options.ValidationMessageLocalizer = ValidationMessageLocalizingHelper.GetValidationMessage;
    })
    .AddBootstrap5Providers()
    .AddFontAwesomeIcons();

// Add authorization policies
builder.Services.AddAuthorizationBuilder()
    .AddPolicy(PolicyConstants.ADMINISTRATOR_POLICY, policy => policy.RequireRole(RoleConstants.Administrator))
    .AddPolicy(PolicyConstants.USER_POLICY, policy => policy.RequireRole(RoleConstants.Administrator, RoleConstants.User));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

// Map static assets before using static files
app.MapStaticAssets();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(ITAssetManager.Client._Imports).Assembly);

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();

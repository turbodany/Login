using Login.Data;
using Login.Data.Entities;
using Login.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DataContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("cadenaconexion"));
});


//TODO: hacer los password mas seguros
builder.Services.AddIdentity<Usuario, IdentityRole>(cfg =>
{
    cfg.User.RequireUniqueEmail = true; // email unico, dos usuarios no pueden tener el mismo email
    cfg.Password.RequireDigit = false;  // requiere digitos
    cfg.Password.RequiredUniqueChars = 0; // requiere caracteres unicos
    cfg.Password.RequireLowercase = false; // require al menos una minuscula
    cfg.Password.RequireNonAlphanumeric = false; // requiere al menos un caracter alfa numerico
    cfg.Password.RequireUppercase = false; // requiere al menos una mayuscula
    cfg.Password.RequiredLength = 6; // longitud requerida
}).AddEntityFrameworkStores<DataContext>();



builder.Services.AddTransient<SeedDb>();
builder.Services.AddScoped<IUserHelper, UserHelper>();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

var app = builder.Build();
SeedData();

void SeedData()// se hace la inyeccion de forma manual creando un objeto llamado scopeFactory
{
    IServiceScopeFactory? scopeFactory = app.Services.GetService<IServiceScopeFactory>(); 
    using (IServiceScope? scope = scopeFactory.CreateScope())
    {
        SeedDb? service = scope.ServiceProvider.GetService<SeedDb>();// traemos un servicio que se llama seeddb y se ejecuta el seeddb async
        service.SeedAsync().Wait(); //como el metodo es asincrono pero en el contexto no puede ser asincrono, por eso utilizamos el wait
    }
}


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

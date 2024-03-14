using GymFitPlus.Web.ModelBinders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationDbContext(builder.Configuration);
builder.Services.AddApplicationIdentity();
builder.Services.AddApplicationServices();
builder.Services.AddApplicationAuthentication();    

builder.Services.AddControllersWithViews(opt =>
{
    opt.ModelBinderProviders.Insert(0, new DateTimeModelBinderProvider());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultControllerRoute();
app.MapRazorPages();

await app.RunAsync();

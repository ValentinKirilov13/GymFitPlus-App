using GymFitPlus.Web.ModelBinders;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationDbContext(builder.Configuration);
builder.Services.AddApplicationIdentity();
builder.Services.AddApplicationServices();
builder.Services.AddApplicationAuthentication();

builder.Services.AddControllersWithViews(opt =>
{
    opt.ModelBinderProviders.Insert(0, new DateTimeModelBinderProvider());
    opt.ModelBinderProviders.Insert(1, new DoubleParseModelBinderProvider());

    opt.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
});

builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name:"MyAllowedOrigins",
        builder =>
        {
            builder.WithOrigins("https://localhost:7121");
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error/500");
    app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors("MyAllowedOrigins");

app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultControllerRoute();

await app.RunAsync();

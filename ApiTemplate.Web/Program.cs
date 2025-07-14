using ApiTemplate.Business.AppServices;
using ApiTemplate.Business.Repositories;
using ApiTemplate.Model.EF;
using ApiTemplate.Web.Data.OpenAPI;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services
    .AddApiVersioning(
    options =>
    {
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.AssumeDefaultVersionWhenUnspecified = true; // Assume the default version if not specified
        options.ApiVersionReader = new UrlSegmentApiVersionReader();
    })
    .AddMvc()
    .AddApiExplorer(
    options =>
    {
        options.GroupNameFormat = "'v'V";
        options.SubstituteApiVersionInUrl = true;
    });

builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

builder.Services.AddPooledDbContextFactory<DbMemContext>(
    options => options.UseInMemoryDatabase("AnimalDB"),
    poolSize: 1024 // Default is 1024
);

//this is a seperate quality of life injection that will automatically serve a context instance during constructor call to satisfy a single instance scenario.
//this is the most common scenario for functions and controllers.
//if the function needs multiple parallel operations, inject the factory and request seperate instances instead.
builder.Services.AddScoped<IScopedDbContextFactory<DbMemContext>, ScopedDbMemContextFactory>();
builder.Services.AddScoped(
    sp => sp.GetRequiredService<ScopedDbMemContextFactory>().CreateDbContext());

//repositories
builder.Services.AddScoped<IAnimalRepository, AnimalRepository>();
builder.Services.AddScoped<IPhotoRepository, PhotoRepository>();

//services
builder.Services.AddScoped<IPetGalleryService, PetGalleryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

        foreach (var description in provider.ApiVersionDescriptions)
        {
            var url = $"/swagger/{description.GroupName}/swagger.json";
            var name = description.GroupName.ToUpperInvariant();
            options.SwaggerEndpoint(url, name);
            options.RoutePrefix = "v1";
        }
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

app.MapDefaultControllerRoute();

app.Run();

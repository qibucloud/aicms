using AICMS.Core.Pagination;
using AICMS.Core.Routing;
using AICMS.Core.Template;
using AICMS.Core.Search;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Configuration
var configuration = builder.Configuration;
var cmsSection = configuration.GetSection("Cms");
var templatesPath = cmsSection.GetValue<string>("TemplatesPath", "../../../templates");
var itemsPerPage = cmsSection.GetValue<int>("ItemsPerPage", 10);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register core services
builder.Services.AddSingleton(new LanguageDetector(cmsSection.GetValue<string[]>("SupportedLanguages") ?? new[] { "cn", "en", "jp" }, cmsSection.GetValue<string>("DefaultLanguage", "cn")));
builder.Services.AddSingleton<LanguageRouter>();
builder.Services.AddSingleton<RouteBuilder>();

// Pagination and search
builder.Services.AddSingleton(new PaginationHandler(itemsPerPage));
builder.Services.AddSingleton<SearchHandler>();

// HttpContextAccessor
builder.Services.AddSingleton<Microsoft.AspNetCore.Http.IHttpContextAccessor, Microsoft.AspNetCore.Http.HttpContextAccessor>();

// Template services
builder.Services.AddSingleton(new TemplateLoader(templatesPath));
builder.Services.AddSingleton(sp => {
    var loader = sp.GetRequiredService<TemplateLoader>();
    // LiquidTemplateEngine expects TemplateLoader and templates root path
    return new LiquidTemplateEngine(loader, templatesPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Simple middleware to detect language and attach RouteContext to HttpContext.Items
app.Use(async (context, next) =>
{
    var path = context.Request.Path.Value ?? "/";
    var query = context.Request.Query.ToDictionary(k => k.Key, v => v.Value.ToString());
    var router = context.RequestServices.GetRequiredService<LanguageRouter>();
    var routeContext = router.ParseRoute(path, query);

    context.Items["RouteContext"] = routeContext;
    await next();
});

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

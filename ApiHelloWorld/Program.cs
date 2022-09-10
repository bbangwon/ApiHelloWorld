using ApiHelloWorld.Component;
using ApiHelloWorld.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(connectionString));

builder.Services.AddControllersWithViews();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//버저닝 사용
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);   //기본API버전
    options.ReportApiVersions = true;   //클라이언트 응답헤더에 버전표시
    options.AssumeDefaultVersionWhenUnspecified = true; //따로 버전을 지정하지 않을 경우 기본 버전을 사용
    options.ApiVersionReader = new HeaderApiVersionReader("X-Api-Version"); //헤더에 버전 정보 보내기
});

builder.Services.AddVersionedApiExplorer(setup =>
{
    setup.GroupNameFormat = "'v'VVV";
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiHelloWorld - V1", Version = "v1" });
    c.SwaggerDoc("v2", new OpenApiInfo { Title = "ApiHelloWorld - V2", Version = "v2" });
});

builder.Services.AddScoped<IPointRepository, PointRepositoryInMemory>();
builder.Services.AddScoped<IPointLogRepository, PointLogRepository>();
builder.Services.AddScoped<INoteRepository, NoteRepository>();

//전체 접속 가능 (Cors)
builder.Services.AddCors(option =>
{
    option.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

    app.UseSwagger();
    app.UseSwaggerUI(o => {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            o.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", $"{description.GroupName.ToUpper()}");
        }
    });
}

app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors();

app.MapControllers();
app.MapDefaultControllerRoute();



app.Run();

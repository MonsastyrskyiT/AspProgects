using Microsoft.OpenApi;
using SvaggerOpenIP.Mapping;
using SvaggerOpenIP.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.AddOpenApi();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    //info about API
    option.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "product and Order Ipi",
        Description = "MIshop - servise for miniclients",
        Contact =  new OpenApiContact
        {
            Name = "Support",
            Email = "support@minishop.com"
        }
        });

        var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        if(File.Exists(xmlPath))
        {
            option.IncludeXmlComments(xmlPath);
        }

    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer { token }"
    });

   

    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();   
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = "swagger";//Lokal host:port/swagger
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

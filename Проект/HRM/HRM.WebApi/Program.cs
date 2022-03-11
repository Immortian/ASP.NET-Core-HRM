using HRM.Application;
using HRM.Persistence;

var builder = WebApplication.CreateBuilder(args);
//Configure Services

builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore); //игнор циклического вызова связанных сущностей
//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var host = builder.Build();
//Configure
if(host.Environment.IsDevelopment())
{
    host.UseDeveloperExceptionPage();
}
host.UseSwagger();

host.UseSwaggerUI(Config =>
{
    Config.RoutePrefix = string.Empty;
    Config.SwaggerEndpoint("swagger/v1/swagger.json", "CRM API");
});
host.UseRouting();

host.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

using (var scope = host.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var context = serviceProvider.GetRequiredService<HRMDBContext>();
        DBInitializer.Initialize(context);
    }
    catch (Exception e) { }
}

host.Run();
using NLog;
using qal.workflow.api.Utilities.ActionFilters;
using qal.workflow.api.Utilities.Extensions;
using qal.workflow.domain.Interfaces.IService;
using System.Configuration;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
Logger _logger = LogManager.Setup().LoadConfigurationFromFile(Path.Combine(Directory.GetCurrentDirectory(), "nlog.config")).GetCurrentClassLogger();

try
{
    var configuration = builder.Configuration;
    builder.Services.AddAutoMapper(typeof(Program));
    builder.Services.ConfigureLoggerService();
    builder.Services.ConfigureSwagger();
    builder.Services.ConfigureServiceResolver();
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddHttpClient();
    builder.Services.AddAuthorization();
    builder.Services.ConfigureCors(configuration);
    builder.Services.AddControllers().AddJsonOptions(option =>
    {
        option.JsonSerializerOptions.PropertyNamingPolicy = null;
        //option.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });
    builder.Services.ConfigureAuthentication(configuration);

    var app = builder.Build();
    var logger = app.Services.GetRequiredService<ILoggerManager>();

    app.UseCors("CorsPolicy");
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}
catch (Exception ex)
{
    _logger.Error(ex);
}
finally
{
    LogManager.Shutdown();
}
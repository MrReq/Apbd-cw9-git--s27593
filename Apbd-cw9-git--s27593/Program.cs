using Apbd_cw9_git__s27593.DAL;
using Apbd_cw9_git__s27593.Services;
using Apbd_cw9_git__s27593.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Apbd_cw9_git__s27593;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddDbContext<UniversityTasksDbContext>(options =>
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddScoped<ICourseService, CourseService>();
        builder.Services.AddScoped<ISubmissionService, SubmissionService>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
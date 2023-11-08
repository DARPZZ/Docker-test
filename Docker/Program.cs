using Docker.Repositories;
using static Docker.Repositories.StudentRepository;

namespace Docker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.Configure<D22RestDatabase>(builder.Configuration.GetSection("D22RestDatabaseSettings"));
            builder.Services.AddSingleton<IstudentRepository, StudentRepository>();




            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

        
            //endpoint
            app.MapPost("/student", (Student std, IstudentRepository sr) =>
            {
                sr.add(std);
            });
            app.MapGet("/student/{id}", (Guid std, IstudentRepository sr) =>
            {

                return sr.get(std);
            });
            app.Run();
        }

    }
}
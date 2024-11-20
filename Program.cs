using BookStoreAPI.Application.MappingProfiles;
using BookStoreAPI.Application.UseCases.Implementations;
using BookStoreAPI.Application.UseCases.Interfaces;
using BookStoreAPI.Application.Repositories.Interfaces;
using BookStoreAPI.Application.Repositories.Implementations;
using BookStoreAPI.Infrastructure.Data;
using BookStoreAPI.Infrastructure.Middleware;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using BookStoreAPI.Domain.ModelsDTO;
using BookStoreAPI.Application.Validators;

namespace BookStoreAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<IValidator<BookDTO>, BookDTOValidator>();
            builder.Services.AddScoped<IValidator<CreateOrderDTO>, CreateOrderDTOValidator>();
            builder.Services.AddScoped<IValidator<OrderItemDTO>, OrderItemDTOValidator>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //automappers
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();

            builder.Services.AddScoped<IBookUseCase, BookUseCase>();
            builder.Services.AddScoped<IOrderUseCase, OrderUseCase>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}

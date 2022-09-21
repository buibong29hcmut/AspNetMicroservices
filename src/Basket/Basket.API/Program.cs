using Basket.API.Repositories;
using Basket.API.Services;
using Discount.Grpc.Protos;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(o =>
{ string address = builder.Configuration.GetValue<string>("GrpcSettings:DiscountUrl");
    o.Address = new Uri(address);
});
builder.Services.AddScoped<DiscountGrpcService>();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString");
});
builder.Services.AddMassTransit(config =>
{
    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"], h =>
        {

            //h.Username("buibong2912");
            //h.Password("29122002Az@");
        });

    });
});
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddSingleton<IHostedService, MassTransitHostedService>();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();

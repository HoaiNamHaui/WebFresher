using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.BL.AccountBL;
using MISA.AMIS.BL.BaseBL;
using MISA.AMIS.BL.DepartmentBL;
using MISA.AMIS.BL.EmployeeBL;
using MISA.AMIS.BL.MObjectBL;
using MISA.AMIS.BL.PaymentBL;
using MISA.AMIS.BL.PaymentDetailBL;
using MISA.AMIS.DL;
using MISA.AMIS.DL.AccountDL;
using MISA.AMIS.DL.BaseDL;
using MISA.AMIS.DL.DepartmentDL;
using MISA.AMIS.DL.EmployeeDL;
using MISA.AMIS.DL.Object;
using MISA.AMIS.DL.ObjectDL;
using MISA.AMIS.DL.PaymentDetailDL;
using MISA.AMIS.DL.PaymentDL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IEmployeeBL, EmployeeBL>();
builder.Services.AddScoped<IEmployeeDL, EmployeeDL>();
builder.Services.AddScoped<IAccountBL, AccountBL>();
builder.Services.AddScoped<IAccountDL, AccountDL>();
builder.Services.AddScoped<IMObjectBL, MObjectBL>();
builder.Services.AddScoped<IMObjectDL, MObjectDL>();
builder.Services.AddScoped<IDepartmentBL, DepartmentBL>();
builder.Services.AddScoped<IDepartmentDL, DepartmentDL>();
builder.Services.AddScoped(typeof(IBaseBL<>), typeof(BaseBL<>));
builder.Services.AddScoped(typeof(IBaseDL<>), typeof(BaseDL<>));


builder.Services.AddScoped<IPaymentBL, PaymentBL>();
builder.Services.AddScoped<IPaymentDL, PaymentDL>();
builder.Services.AddScoped<IPaymentDetailBL, PaymentDetailBL>();
builder.Services.AddScoped<IPaymentDetailDL, PaymentDetailDL>();

DatabaseContext.ConnectionString =  builder.Configuration.GetConnectionString("MySql");

builder.Services.AddCors(p => p.AddPolicy("corspolicy", build => //1
{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddControllers().AddJsonOptions(opt => opt.JsonSerializerOptions.PropertyNamingPolicy = null); // chuyển pascal case


//Tắt validate mặc định
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("corspolicy"); //2
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

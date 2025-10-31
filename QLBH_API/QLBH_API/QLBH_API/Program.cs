using Microsoft.EntityFrameworkCore;
using QLBH_API.Data;
using QLBH_API.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. Đăng ký DbContext để kết nối CSDL
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("VoHoangDinh")));

// 2. Đăng ký các lớp Repository và Interface (Dependency Injection)
builder.Services.AddScoped<IDanhMucRepository, DanhMucRepository>();
builder.Services.AddScoped<ISanPhamRepository, SanPhamRepository>();
builder.Services.AddScoped<IGioHangRepository, GioHangRepository>();

// 3. Cấu hình CORS để cho phép Frontend gọi đến
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// 4. Các dịch vụ mặc định
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 5. Kích hoạt CORS
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
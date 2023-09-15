using Work2.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IDepartmentRepository, DepartmentRepository>(provider => new DepartmentRepository("Server=GNIL\\SQLEXPRESS;Initial Catalog=Work;Integrated Security=True;TrustServerCertificate=True"));
builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>(provider => new EmployeeRepository("Server=GNIL\\SQLEXPRESS;Initial Catalog=Work;Integrated Security=True;TrustServerCertificate=True"));

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

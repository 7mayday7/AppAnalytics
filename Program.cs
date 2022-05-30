using AppAnalytics.Models; // ������������ ���� ��������� ������ UserContext
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
//var connectionString = builder.Configuration.GetConnectionString("VehicleQuotesContext");
builder.Services.AddDbContext<NoteDb>(options =>
options.UseNpgsql(connectionString));


builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(options => //CookieAuthenticationOptions
{
    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
});

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MapGet("/notes", async (NoteDb db) =>  await db.Notes.ToListAsync());

app.MapPost("/note", async (Note n, NoteDb db) =>
{
    db.Notes.Add(n);

    await db.SaveChangesAsync();

    return Results.Ok(n);
});

app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<NoteDb>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}

app.Run();

record Note(int id)
{
    public string text { get; set; } = default!;
    public bool done { get; set; } = default!;
}

class NoteDb: DbContext
{
    public NoteDb(DbContextOptions<NoteDb> options): base(options)
    {

    }
    public DbSet<Note> Notes => Set<Note>();
}





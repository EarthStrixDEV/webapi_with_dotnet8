using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using model;
using controller;

public static class User {
    public static void GetAllUser(this IEndpointRouteBuilder endpoints) {
        endpoints.MapGet("/getAllUser", async(AppDbContext dbContext) => {
            var users = await dbContext.Users.ToListAsync();
            var options = new JsonSerializerOptions {
                WriteIndented = true
            };
            return Results.Json(users ,options);
        }).WithName("GetAllUser").WithOpenApi();
    }

    public static void GetUserWithId(this IEndpointRouteBuilder endpoints) {
        endpoints.MapGet("/getUser/{id}", async(int id ,AppDbContext dbContext) => {
            var users = await dbContext.Users.FindAsync(id);
            var options = new JsonSerializerOptions {
                WriteIndented = true
            };
            return users is not null ? Results.Json(users ,options) : Results.NotFound();
        }).WithName("GetUserWithId").WithOpenApi();
    }

    public static void CreateUser(this IEndpointRouteBuilder endpoints) {
        endpoints.MapPost("/createUser", async (AppDbContext dbContext, Users newUser) => {
            dbContext.Users.Add(newUser);
            await dbContext.SaveChangesAsync();
            return Results.Created("/getUser/{id}", newUser);
        }).WithName("CreateUser").WithOpenApi();
    }

    public static void UpdateUser(this IEndpointRouteBuilder endpoints) {
        endpoints.MapPost("/updateUser/{id}" ,async (int id, Users user_ , UserService userService) => {
            var user = await userService.UpdateUserAsync(id ,user_);
            return user is not null ? Results.Ok() : Results.NotFound();
        });
    }

    public static void DeleteUserAsync(this IEndpointRouteBuilder endpoints) {
        endpoints.MapGet("/deleteUser/{id}", async (int id ,Users user_ ,UserService userService) => {
            var user = await userService.DeleteUserAsync(id);
            return user is not null ? Results.Ok() : Results.NotFound();
        });
    }
}
using controller;
using model;

public static class Auth {
    public static void AuthSignIn(this IEndpointRouteBuilder endPoints) {
        endPoints.MapPost("/SignIn", async (UserAuth user ,AuthService authService) => {
            AuthResult userSignIn = await authService.UserSignIn(user);
            return userSignIn.Status switch {
                StatusCodes.Status200OK => Results.Json(new {message = userSignIn.Message} ,statusCode: StatusCodes.Status200OK),
                StatusCodes.Status404NotFound => Results.Json(new {message = userSignIn.Message} ,statusCode: StatusCodes.Status404NotFound),
                _ => Results.Problem(detail: "Unexpected error occurred in the server", statusCode: StatusCodes.Status500InternalServerError ,title: "Internal Server Error")
            };
        }).WithName("SignIn").WithOpenApi();
    }

    public static void AuthSignUp(this IEndpointRouteBuilder endPoints) {
        endPoints.MapPost("/SignUp", async(Users user ,AuthService authService) => {
            AuthResult userSignUp = await authService.UserSignUp(user);
            return userSignUp.Status switch {
                StatusCodes.Status200OK => Results.Json(new {message = userSignUp.Message} ,statusCode: StatusCodes.Status200OK),
                StatusCodes.Status400BadRequest => Results.Json(new {message = userSignUp.Message} ,statusCode: StatusCodes.Status400BadRequest),
                _ => Results.Problem(detail: "Unexpected error occurred in the server", statusCode: StatusCodes.Status500InternalServerError, title: "internal Server Error")
            };
        }).WithName("SignUp").WithOpenApi();
    }
}
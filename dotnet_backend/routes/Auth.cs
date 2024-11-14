using controller;
using model;
using EnumStatus;

public static class Auth {
    public static void AuthSignIn(this IEndpointRouteBuilder endPoints) {
        endPoints.MapPost("/SignIn", async (Users user_ ,AuthService authService) => {
            AuthResult userSignIn = await authService.UserSignIn(user_);
            if (userSignIn.Status == StatusCodes.Status404NotFound) {
                return Results.Json(new {message = "Sign in failed"}, statusCode: StatusCodes.Status404NotFound);
            }
            return Results.Ok(new {message = userSignIn.Message ,status = userSignIn.Status});
        });
    }

    public static void AuthSignUp(this IEndpointRouteBuilder endPoints) {
        endPoints.MapPost("/signUp", async(Users user_ ,AuthService authService) => {
            AuthResult userSignUp = await authService.UserSignUp(user_);
            if (userSignUp.Status == StatusCodes.Status400BadRequest) {
                return Results.BadRequest(new {status = userSignUp.Status, message = userSignUp.Message});
            }
            return Results.Ok(new {message = userSignUp.Message , status = userSignUp.Status});
        });
    }
}
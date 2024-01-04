using AuthenticationAPI.Extensions;
using Dapper;
using Entities;
using System.Security.Claims;
using UserService.Dtos;
using UserService.Service;

namespace UserService.Endpoints
{
    public static class UserEndpoints
    {
        public static void MapUserEndpoints(this IEndpointRouteBuilder builder)
        {
            var userWeightGroup = builder.MapGroup("userWeight").RequireAuthorization();

            userWeightGroup.MapGet("userWeights", async (SqlConnectionFactory sqlConnectionFactory, ClaimsPrincipal user) =>
            {
                int userId = user.GetLoggedInUserSub();
                using var connection = sqlConnectionFactory.Create();

                const string sql = $"SELECT UserId, Weight, Date FROM UserWeight WHERE UserId = @UserId";
                var result = await connection.QueryAsync<UserWeight>(sql, new { UserId = userId });
                return Results.Ok(result);
            });

            userWeightGroup.MapGet("{userWeightId}", async (int userWeightId, SqlConnectionFactory sqlConnectionFactory, ClaimsPrincipal user) =>
            {
                int userId = user.GetLoggedInUserSub();
                using var connection = sqlConnectionFactory.Create();

                const string sql = $"SELECT UserId, Weight, Date FROM UserWeight WHERE Id = @UserWeightId AND UserId = @UserId";
                var result = await connection.QuerySingleOrDefaultAsync<UserWeight>(sql, new { UserWeightId = userWeightId, UserId = userId });
                return Results.Ok(result);
            });

            userWeightGroup.MapPost("", async (UserWeightDto userWeight, SqlConnectionFactory sqlConnectionFactory, ClaimsPrincipal user) =>
            {
                int userId = user.GetLoggedInUserSub();
                using var connection = sqlConnectionFactory.Create();

                const string sql = $"INSERT INTO UserWeight (Weight, Date)" +
                $"VALUES(@Weight, @Date) FROM  WHERE Id = @UserId";
                await connection.ExecuteAsync(sql, new { Weight = userWeight.Weight, Date = userWeight.Date, UserId = userId });
                return Results.Ok();
            });

            userWeightGroup.MapPut("{userWeightId}", async (int userWeightId, UserWeightDto userWeight, SqlConnectionFactory sqlConnectionFactory, ClaimsPrincipal user) =>
            {
                int userId = user.GetLoggedInUserSub();
                using var connection = sqlConnectionFactory.Create();

                const string sql = $"UPDATE UserWeight SET Weight = @Weight, Date = @Date WHERE Id = @UserWeightId AND UserId = @UserId";
                await connection.ExecuteAsync(sql, new { Weight = userWeight.Weight, Date = userWeight.Date, UserWeightId = userWeightId, UserId = userId });
                return Results.NoContent();
            });

            userWeightGroup.MapDelete("{userWeightId}", async (int userWeightId, SqlConnectionFactory sqlConnectionFactory, ClaimsPrincipal user) =>
            {
                int userId = user.GetLoggedInUserSub();
                using var connection = sqlConnectionFactory.Create();

                const string sql = $"DELETE FROM UserWeight WHERE Id = @UserWeightId AND UserId = @UserId";
                await connection.ExecuteAsync(sql, new { UserWeightId = userWeightId, UserId = userId });
                return Results.NoContent();
            });


            var UserNutritionGroup = builder.MapGroup("UserNutrition").RequireAuthorization();

            UserNutritionGroup.MapGet("UserNutritions", async (SqlConnectionFactory sqlConnectionFactory, ClaimsPrincipal user) =>
            {
                int userId = user.GetLoggedInUserSub();
                using var connection = sqlConnectionFactory.Create();

                const string sql = $"SELECT UserId, Weight, Date FROM UserNutrition WHERE UserId = @UserId";
                var result = await connection.QueryAsync<UserNutrition>(sql, new { UserId = userId });
                return Results.Ok(result);
            });

            UserNutritionGroup.MapGet("{UserNutritionId}", async (int userNutritionId, SqlConnectionFactory sqlConnectionFactory, ClaimsPrincipal user) =>
            {
                int userId = user.GetLoggedInUserSub();
                using var connection = sqlConnectionFactory.Create();

                const string sql = $"SELECT UserId, Weight, Date FROM UserNutrition WHERE Id = @UserNutritionId AND UserId = @UserId";
                var result = await connection.QuerySingleOrDefaultAsync<UserNutrition>(sql, new { UserNutritionId = userNutritionId, UserId = userId });
                return Results.Ok(result);
            });

            UserNutritionGroup.MapPost("", async (UserNutritionDto userNutrition, SqlConnectionFactory sqlConnectionFactory, ClaimsPrincipal user) =>
            {
                int userId = user.GetLoggedInUserSub();
                using var connection = sqlConnectionFactory.Create();

                const string sql = $"INSERT INTO UserNutrition (Protein, Carbohydrates, Fat, UserId, Date)" +
                $"VALUES(@Protein, @Carbohydrates, @Fat, @UserId, @Date)";
                await connection.ExecuteAsync(sql, new { Protein = userNutrition.Protein, Carbohydrates = userNutrition.Carbohydrates, Fat = userNutrition.Fat, Date = userNutrition.Date, UserId = userId });
                return Results.Ok();
            });

            UserNutritionGroup.MapPut("{UserNutritionId}", async (int userNutritionId, UserNutritionDto userNutrition, SqlConnectionFactory sqlConnectionFactory, ClaimsPrincipal user) =>
            {
                int userId = user.GetLoggedInUserSub();
                using var connection = sqlConnectionFactory.Create();

                const string sql = $"UPDATE UserNutrition SET Weight = @Weight, Date = @Date WHERE Id = @UserNutritionId AND UserId = @UserId";
                await connection.ExecuteAsync(sql, new { Weight = userNutrition, Date = userNutrition.Date, UserNutritionId = userNutritionId, UserId = userId });
                return Results.NoContent();
            });

            UserNutritionGroup.MapDelete("{UserNutritionId}", async (int userNutritionId, SqlConnectionFactory sqlConnectionFactory, ClaimsPrincipal user) =>
            {
                int userId = user.GetLoggedInUserSub();
                using var connection = sqlConnectionFactory.Create();

                const string sql = $"DELETE FROM UserNutrition WHERE Id = @UserNutritionId AND UserId = @UserId";
                await connection.ExecuteAsync(sql, new { UserNutritionId = userNutritionId, UserId = userId });
                return Results.NoContent();
            });
        }
    }
}

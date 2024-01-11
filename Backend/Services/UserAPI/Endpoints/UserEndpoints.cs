using Common.Auth;
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
            var userWeightGroup = builder.MapGroup("api/userweight").RequireAuthorization();

            userWeightGroup.MapGet("all", async (SqlConnectionFactory sqlConnectionFactory, ClaimsPrincipal user) =>
            {
                Guid userId = user.GetLoggedInUserSub();
                using var connection = sqlConnectionFactory.Create();

                const string sql = $"SELECT UserId, Weight, Date FROM UserWeight WHERE UserId = @UserId";
                var result = await connection.QueryAsync<UserWeight>(sql, new { UserId = userId });
                return Results.Ok(result);
            });

            userWeightGroup.MapGet("{userWeightId}", async (int userWeightId, SqlConnectionFactory sqlConnectionFactory, ClaimsPrincipal user) =>
            {
                Guid userId = user.GetLoggedInUserSub();
                using var connection = sqlConnectionFactory.Create();

                const string sql = $"SELECT UserId, Weight, Date FROM UserWeight WHERE Id = @UserWeightId AND UserId = @UserId";
                var result = await connection.QuerySingleOrDefaultAsync<UserWeight>(sql, new { UserWeightId = userWeightId, UserId = userId });
                return Results.Ok(result);
            });

            userWeightGroup.MapPost("", async (UserWeightDto userWeight, SqlConnectionFactory sqlConnectionFactory, ClaimsPrincipal user) =>
            {
                Guid userId = user.GetLoggedInUserSub();
                using var connection = sqlConnectionFactory.Create();

                const string sql = $"INSERT INTO UserWeight (Weight, Date)" +
                $"VALUES(@Weight, @Date) FROM  WHERE Id = @UserId";
                await connection.ExecuteAsync(sql, new { Weight = userWeight.Weight, Date = userWeight.Date, UserId = userId });
                return Results.Ok();
            });

            userWeightGroup.MapPut("{userweightid}", async (int userweightid, UserWeightDto userWeight, SqlConnectionFactory sqlConnectionFactory, ClaimsPrincipal user) =>
            {
                Guid userId = user.GetLoggedInUserSub();
                using var connection = sqlConnectionFactory.Create();

                const string sql = $"UPDATE UserWeight SET Weight = @Weight, Date = @Date WHERE Id = @UserWeightId AND UserId = @UserId";
                await connection.ExecuteAsync(sql, new { Weight = userWeight.Weight, Date = userWeight.Date, UserWeightId = userweightid, UserId = userId });
                return Results.NoContent();
            });

            userWeightGroup.MapDelete("{userweightid}", async (int userweightid, SqlConnectionFactory sqlConnectionFactory, ClaimsPrincipal user) =>
            {
                Guid userId = user.GetLoggedInUserSub();
                using var connection = sqlConnectionFactory.Create();

                const string sql = $"DELETE FROM UserWeight WHERE Id = @UserWeightId AND UserId = @UserId";
                await connection.ExecuteAsync(sql, new { UserWeightId = userweightid, UserId = userId });
                return Results.NoContent();
            });


            var userNutritionGroup = builder.MapGroup("api/usernutrition").RequireAuthorization();

            userNutritionGroup.MapGet("all", async (SqlConnectionFactory sqlConnectionFactory, ClaimsPrincipal user) =>
            {
                Guid userId = user.GetLoggedInUserSub();
                using var connection = sqlConnectionFactory.Create();

                const string sql = $"SELECT UserId, Kcal, Protein, Carbohydrates, Fat, Date FROM UserNutrition WHERE UserId = @UserId";
                var result = await connection.QueryAsync<UserNutrition>(sql, new { UserId = userId });
                return Results.Ok(result);
            });

            userNutritionGroup.MapGet("{usernutritionid}", async (int usernutritionid, SqlConnectionFactory sqlConnectionFactory, ClaimsPrincipal user) =>
            {
                Guid userId = user.GetLoggedInUserSub();
                using var connection = sqlConnectionFactory.Create();

                const string sql = $"SELECT UserId, Kcal, Protein, Carbohydrates, Fat, Date FROM UserNutrition WHERE Id = @UserNutritionId AND UserId = @UserId";
                var result = await connection.QuerySingleOrDefaultAsync<UserNutrition>(sql, new { UserNutritionId = usernutritionid, UserId = userId });
                return Results.Ok(result);
            });

            userNutritionGroup.MapPost("", async (UserNutritionDto userNutrition, SqlConnectionFactory sqlConnectionFactory, ClaimsPrincipal user) =>
            {
                Guid userId = user.GetLoggedInUserSub();
                using var connection = sqlConnectionFactory.Create();

                const string sql = $"INSERT INTO UserNutrition (Kcal, Protein, Carbohydrates, Fat, UserId, Date)" +
                $"VALUES(@Protein, @Carbohydrates, @Fat, @UserId, @Date)";
                await connection.ExecuteAsync(sql, new { Kcal = userNutrition.Kcal, Protein = userNutrition.Protein, Carbohydrates = userNutrition.Carbohydrates, Fat = userNutrition.Fat, Date = userNutrition.Date, UserId = userId });
                return Results.Ok();
            });

            userNutritionGroup.MapPut("{usernutritionid}", async (int usernutritionid, UserNutritionDto userNutrition, SqlConnectionFactory sqlConnectionFactory, ClaimsPrincipal user) =>
            {
                Guid userId = user.GetLoggedInUserSub();
                using var connection = sqlConnectionFactory.Create();

                const string sql = $"UPDATE UserNutrition SET Kcal = @Kcal, Protein = @Protein, Carbohydrates = @Carbohydrates, Fat = @Fat, Date = @Date WHERE Id = @UserNutritionId AND UserId = @UserId";
                await connection.ExecuteAsync(sql, new { Protein = userNutrition.Protein, Carbohydrates = userNutrition.Carbohydrates, Fat = userNutrition.Fat, Kcal = userNutrition.Kcal, Date = userNutrition.Date, UserNutritionId = usernutritionid, UserId = userId });
                return Results.NoContent();
            });

            userNutritionGroup.MapDelete("{usernutritionid}", async (int usernutritionid, SqlConnectionFactory sqlConnectionFactory, ClaimsPrincipal user) =>
            {
                Guid userId = user.GetLoggedInUserSub();
                using var connection = sqlConnectionFactory.Create();

                const string sql = $"DELETE FROM UserNutrition WHERE Id = @UserNutritionId AND UserId = @UserId";
                await connection.ExecuteAsync(sql, new { UserNutritionId = usernutritionid, UserId = userId });
                return Results.NoContent();
            });
        }
    }
}

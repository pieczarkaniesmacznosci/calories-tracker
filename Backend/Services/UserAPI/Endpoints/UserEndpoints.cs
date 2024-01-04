using Dapper;
using Entities;
using UserService.Dtos;
using UserService.Service;

namespace UserService.Endpoints
{
    public static class UserEndpoints
    {
        public static void MapUserEndpoints(this IEndpointRouteBuilder builder, IServiceProvider serviceProvider)
        {
            var group = builder.MapGroup("userWeight").RequireAuthorization();

            group.MapGet("userWeights", async (SqlConnectionFactory sqlConnectionFactory) =>
            {
                using var connection = sqlConnectionFactory.Create();

                const string sql = $"SELECT UserId, Weight, Date FROM UserWeights WHER UserId = @UserId";
                var result = await connection.QueryAsync<UserWeight>(sql, new { UserId = 1 });
                return Results.Ok(result);
            });

            group.MapGet("{userWeightId}", async (int userWeightId, SqlConnectionFactory sqlConnectionFactory) =>
            {
                using var connection = sqlConnectionFactory.Create();

                const string sql = $"SELECT UserId, Weight, Date FROM UserWeights WHERE Id = @UserWeightId AND UserId = @UserId";
                var result = await connection.QuerySingleOrDefaultAsync<UserWeight>(sql, new { UserWeightId = userWeightId, UserId = 1 });
                return Results.Ok(result);
            });

            group.MapPost("", async (UserWeightDto userWeight, SqlConnectionFactory sqlConnectionFactory) =>
            {
                using var connection = sqlConnectionFactory.Create();

                const string sql = $"INSERT INTO UserWeights (Weight, Date)" +
                $"VALUES(@Weight, @Date) FROM  WHERE Id = @UserId";
                await connection.ExecuteAsync(sql, new { Weight = userWeight.Weight, Date = userWeight.Date, UserId = 1 });
                return Results.Ok();
            });

            group.MapPut("{userWeightId}", async (int userWeightId, UserWeightDto userWeight, SqlConnectionFactory sqlConnectionFactory) =>
            {
                using var connection = sqlConnectionFactory.Create();

                const string sql = $"UPDATE UserWeights SET Weight = @Weight, Date = @Date WHERE Id = @UserWeightId AND UserId = @UserId";
                await connection.ExecuteAsync(sql, new { Weight = userWeight.Weight, Date = userWeight.Date, UserWeightId = userWeightId, UserId = 1 });
                return Results.NoContent();
            });

            group.MapDelete("{userWeightId}", async (int userWeightId, SqlConnectionFactory sqlConnectionFactory) =>
            {
                using var connection = sqlConnectionFactory.Create();

                const string sql = $"DELETE FROM UserWeights WHERE Id = @UserWeightId AND UserId = @UserId";
                await connection.ExecuteAsync(sql, new { UserWeightId = userWeightId, UserId = 1 });
                return Results.NoContent();
            });
        }
    }
}

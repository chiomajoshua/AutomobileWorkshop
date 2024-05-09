namespace BuildingBlocks.Domain.Constants;

public static class ApiResponseMessages
{
    public static Dictionary<int, string> DefaultMessages { get; } = new Dictionary<int, string>
    {
        { 200, "Success" },
        { 201, "Created" },
        { 204, "No Content" },
        { 400, "Bad Request" },
        { 401, "Unauthorized" },
        { 403, "Forbidden" },
        { 404, "Not Found" },
        { 500, "Internal Server Error" },
    };
}
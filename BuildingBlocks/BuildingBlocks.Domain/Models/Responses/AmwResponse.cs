using BuildingBlocks.Domain.Constants;
using System.Text.Json;

namespace BuildingBlocks.Domain.Models.Responses;

public class AmwResponse
{
    public bool Status { get; set; }
    public int ResponseCode { get; set; }
    public string? ResponseMessage { get; set; }
    public object? ResponseData { get; set; }

    public override string ToString() => JsonSerializer.Serialize(this);

    public static AmwResponse SuccessResponse(object? data)
    {
        return new AmwResponse
        {
            Status = true,
            ResponseData = data,
            ResponseCode = ApiStatusConstants.OK
        };
    }

    public static AmwResponse ExistsResponse<T>(T? data)
    {
        bool isSuccess = data is not null;
        int statusCode = isSuccess ? ApiStatusConstants.OK : ApiStatusConstants.NotFound;

        return new AmwResponse
        {
            Status = isSuccess,
            ResponseCode = statusCode,
            ResponseMessage = isSuccess ? "Record(s) Found" : "No record(s) found",
            ResponseData = data,
        };
    }
}

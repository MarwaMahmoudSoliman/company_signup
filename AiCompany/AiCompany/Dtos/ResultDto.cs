namespace AiCompany.Dtos;

public class ResultDto
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = "";

    public static ResultDto Success(string message = "Success")
        => new ResultDto { IsSuccess = true, Message = message };

    public static ResultDto Failure(string message = "Failed")
        => new ResultDto { IsSuccess = false, Message = message };
}

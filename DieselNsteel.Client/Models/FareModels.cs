namespace DieselNsteel.Client.Models
{
    public record FareResult(
        decimal BaseFarePerPassenger,
        decimal TotalFare,
        decimal Change,
        string ErrorMessage
    );
}

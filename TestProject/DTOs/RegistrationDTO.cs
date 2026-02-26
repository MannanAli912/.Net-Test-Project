namespace TestProject.DTOs
{
    public record Step1Request(string ICNumber, string FullName, string Mobile, string Email, string UserType);

    public record VerifyOtpRequest(string ICNumber, string Otp, string Type);
 
    public record PinRequest(string ICNumber, string Pin, string ConfirmPin);
}
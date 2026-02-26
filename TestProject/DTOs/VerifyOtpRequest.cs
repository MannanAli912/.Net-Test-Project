namespace TestProject.DTOs
{
    public class VerifyOtpRequest
    {
        public string ICNumber { get; set; } = string.Empty;
        public string Otp { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }
}
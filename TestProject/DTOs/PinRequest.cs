namespace TestProject.DTOs
{
    public class PinRequest
    {
        public string ICNumber { get; set; } = string.Empty;
        public string Pin { get; set; } = string.Empty;
        public string ConfirmPin { get; set; } = string.Empty;
    }
}
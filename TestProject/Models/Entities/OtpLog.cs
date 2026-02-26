namespace TestProject.Models.Entities
{
    public class OtpLog
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string OtpCode { get; set; } = string.Empty;
        public string OtpType { get; set; } = string.Empty;  
        public bool IsUsed { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}

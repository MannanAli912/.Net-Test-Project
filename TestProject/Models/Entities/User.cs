//namespace TestProject.Models.Entities
//{
//    public class User
//    {
//        public int Id { get; set; }
//        public string ICNumber { get; set; } = string.Empty;
//        public string? FullName { get; set; }
//        public string? MobileNumber { get; set; }
//        public string? EmailAddress { get; set; }
//        public string? PinHash { get; set; }
//        public bool IsPrivacyAccepted { get; set; }
//        public bool IsBiometricEnabled { get; set; }
//        public string UserType { get; set; } = "New"; // New or Migrated
//        public int RegistrationStep { get; set; } = 1; // Tracks flow from 1-4 
//        public DateTime CreatedAt { get; set; } = DateTime.Now;
//    }
//}


namespace TestProject.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string ICNumber { get; set; } = string.Empty;
        public string? FullName { get; set; }
        public string? MobileNumber { get; set; }
        public string? EmailAddress { get; set; }
        public string? PinHash { get; set; }
        public bool IsPrivacyAccepted { get; set; }

   
        public bool IsMobileVerified { get; set; } 
        public bool IsEmailVerified { get; set; }    

        public bool IsBiometricEnabled { get; set; }
        public string UserType { get; set; } = "New";  
        public int RegistrationStep { get; set; } = 1;  
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
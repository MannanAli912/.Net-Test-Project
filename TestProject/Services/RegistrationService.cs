//using Microsoft.EntityFrameworkCore; // YE SABSE ZAROORI HAI: Iske bagair FirstOrDefaultAsync nahi chalega
//using TestProject.Data;
//using TestProject.DTOs;
//using TestProject.Interfaces;
//using TestProject.Models.Entities;

//namespace TestProject.Services
//{
//    public class RegistrationService : IRegistrationService
//    {
//        private readonly AppDbContext _db;
//        public RegistrationService(AppDbContext db) => _db = db;

//        public async Task<object> GetUserStatus(string icNumber)
//        {
//            // Ab ye error nahi dega kyunke Microsoft.EntityFrameworkCore upar add kar diya hai
//            var user = await _db.Users.FirstOrDefaultAsync(u => u.ICNumber == icNumber);

//            return user == null
//                ? new { Existing = false }
//                : new { Existing = true, Step = user.RegistrationStep, Type = user.UserType };
//        }

//        public async Task<int> ProcessStep1(Step1Request request)
//        {
//            var user = await _db.Users.FirstOrDefaultAsync(u => u.ICNumber == request.ICNumber) ?? new User();

//            user.ICNumber = request.ICNumber;
//            user.FullName = request.FullName;
//            user.MobileNumber = request.Mobile;
//            user.EmailAddress = request.Email;
//            user.UserType = request.UserType;
//            user.RegistrationStep = 1;

//            if (user.Id == 0)
//                _db.Users.Add(user);

//            // FIX: _db.Db nahi balki direct _db use karein
//            await _db.SaveChangesAsync();
//            return user.Id;
//        }

//        public async Task<string> GenerateMockOtp(string icNumber, string type)
//        {
//            var user = await _db.Users.FirstAsync(u => u.ICNumber == icNumber);
//            string mockOtp = "1234"; // Wireframe ke mutabiq mock function 

//            var log = new OtpLog
//            {
//                UserId = user.Id,
//                OtpCode = mockOtp,
//                OtpType = type,
//                ExpiryDate = DateTime.Now.AddMinutes(5)
//            };

//            _db.OtpLogs.Add(log);
//            await _db.SaveChangesAsync();
//            return mockOtp;
//        }

//        public async Task<bool> ValidateOtp(VerifyOtpRequest request)
//        {
//            var user = await _db.Users.FirstAsync(u => u.ICNumber == request.ICNumber);

//            // Mock validation logic jaisa aapne kaha tha
//            var log = await _db.OtpLogs
//                .FirstOrDefaultAsync(l => l.UserId == user.Id && l.OtpCode == request.Otp && !l.IsUsed);

//            if (log == null) return false;

//            log.IsUsed = true;
//            user.RegistrationStep = 2; // Step 2 (Verification) complete [cite: 1]
//            await _db.SaveChangesAsync();
//            return true;
//        }

//        public async Task<bool> CompleteRegistration(PinRequest request)
//        {
//            var user = await _db.Users.FirstAsync(u => u.ICNumber == request.ICNumber);

//            user.PinHash = request.Pin; // Secure PIN setup 
//            user.IsPrivacyAccepted = true;
//            user.RegistrationStep = 4; // Final flow coverage [cite: 1]

//            return await _db.SaveChangesAsync() > 0;
//        }
//    }
//}

using Microsoft.EntityFrameworkCore;
using TestProject.Data;
using TestProject.DTOs;
using TestProject.Interfaces;
using TestProject.Models.Entities;

namespace TestProject.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly AppDbContext _db;

        public RegistrationService(AppDbContext db)
        {
            _db = db;
        }

 
        public async Task<object> GetUserStatus(string icNumber)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.ICNumber == icNumber);

            return user == null
                ? new { Existing = false }
                : new { Existing = true, Step = user.RegistrationStep, Type = user.UserType };
        }
 
        public async Task<int> ProcessStep1(Step1Request request)
        {
            var existingUser = await _db.Users.FirstOrDefaultAsync(u => u.ICNumber == request.ICNumber);

    
            if (existingUser != null && existingUser.RegistrationStep == 4)
            {
                throw new Exception("Account already exists. Please login.");
            }

            var user = existingUser ?? new User();
            user.ICNumber = request.ICNumber;
            user.FullName = request.FullName;
            user.MobileNumber = request.Mobile;
            user.EmailAddress = request.Email;
            user.UserType = request.UserType;
            user.RegistrationStep = 1;  

            if (user.Id == 0)
            {
                _db.Users.Add(user);
            }

            await _db.SaveChangesAsync();
            return user.Id;
        }

 
        public async Task<string> GenerateMockOtp(string icNumber, string type)
        {
            var user = await _db.Users.FirstAsync(u => u.ICNumber == icNumber);

            string mockOtp = "1234";  

            var log = new OtpLog
            {
                UserId = user.Id,
                OtpCode = mockOtp,
                OtpType = type,
                IsUsed = false,
                ExpiryDate = DateTime.Now.AddMinutes(5)
            };

            _db.OtpLogs.Add(log);
            await _db.SaveChangesAsync();
            return mockOtp;
        }
 
        public async Task<bool> ValidateOtp(VerifyOtpRequest request)
        {
            var user = await _db.Users.FirstAsync(u => u.ICNumber == request.ICNumber);

 
            var log = await _db.OtpLogs
                .FirstOrDefaultAsync(l => l.UserId == user.Id &&
                                         l.OtpCode == request.Otp &&
                                         !l.IsUsed);
 
            if (log == null)
            {
                throw new Exception("Incorrect OTP. Please try again.");
            }

            log.IsUsed = true;
            user.RegistrationStep = 2;  
            await _db.SaveChangesAsync();
            return true;
        }
 
        public async Task<bool> CompleteRegistration(PinRequest request)
        {
       
            if (request.Pin != request.ConfirmPin)
            {
                throw new Exception("Unmatched PIN. Both fields must be identical.");
            }

            var user = await _db.Users.FirstAsync(u => u.ICNumber == request.ICNumber);

            user.PinHash = request.Pin; 
            user.IsPrivacyAccepted = true;
            user.RegistrationStep = 4;  

            return await _db.SaveChangesAsync() > 0;
        }
    }
}
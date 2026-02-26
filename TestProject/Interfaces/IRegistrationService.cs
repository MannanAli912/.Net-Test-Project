using TestProject.DTOs;

namespace TestProject.Interfaces
{
    public interface IRegistrationService
    {
        Task<object> GetUserStatus(string icNumber);
        Task<int> ProcessStep1(Step1Request request);
        Task<string> GenerateMockOtp(string icNumber, string type);
        Task<bool> ValidateOtp(VerifyOtpRequest request);
        Task<bool> CompleteRegistration(PinRequest request);
    }
}

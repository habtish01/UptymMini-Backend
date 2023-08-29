using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Uptym.Core.Interfaces;

namespace Uptym.Services.Global.Twilio
{
    public interface ITwilioService
    {
        Task<IResponseDTO> SendPhoneVerificationCode(string to);
        Task<IResponseDTO> PhoneVerificationCheck(string to, string verificationCode);
        Task<IResponseDTO> SendSMSNotification(string to, string message);

        #region Report a Problem
        Task<IResponseDTO> ReportProblem(List<string> toNumbers, string description, string equipmentName, string facilityName, DateTime date);
        Task<IResponseDTO> ReportProblemApproved(List<string> toNumbers, string description, string equipmentName, string facilityName, DateTime date);
        Task<IResponseDTO> ReportProblemDeclined(List<string> toNumbers, string description, string equipmentName, string facilityName, string declinedReason, DateTime date);
        #endregion

        #region Work order Schedule
        Task<IResponseDTO> WorkorderScheduleForMaintenancer(List<string> toNumbers, string description, string equipmentName, string facilityName, string engineerName, DateTime date);
        Task<IResponseDTO> WorkorderScheduleForEngineer(List<string> toNumbers, string description, string equipmentName, string facilityName, string maintenancerName, string workOrder, DateTime date);
        #endregion

        #region Work order Completion
        Task<IResponseDTO> WorkorderCompletionForEngineer(List<string> toNumbers, string workOrder, string equipmentName, string facilityName, string facilityManagerName, DateTime date);
        Task<IResponseDTO> WorkorderCompletionForFacilitymanager(List<string> toNumbers, string engineerName, string equipmentName, DateTime date);
        Task<IResponseDTO> WorkorderCompletionForMaintenancer(List<string> toNumbers, string engineerName, string workOrder, string equipmentName, string facilityName, DateTime date);
        #endregion

    }
}

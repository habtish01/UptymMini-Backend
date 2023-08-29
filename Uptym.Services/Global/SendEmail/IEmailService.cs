using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Uptym.Core.Common;

namespace Uptym.Services.Global.SendEmail
{
    public interface IEmailService
    {
        Task Send(EmailMessage emailMessage);


        // Templates
        #region User transactions
        Task AfterRegistiration(string email, string vaildToken);
        Task UnlockUserEmail(string email, string vaildToken);
        Task RequestToResetPassword(string email, string validToken);
        Task AfterResetPassword(string email);
        Task AfterEmailChanges(string email, DateTime date, string ip, string country);
        Task AfterPasswordChanges(string email, DateTime date, string ip, string country);
        Task AfterUserRoleChanges(string email, string companyLogoPath, string companyName, string from, string to);
        Task SendEmailConfirmationRequest(string email, string token);
        Task SendEmailReply(string email, string message);
        #endregion

        #region Security Emails
        Task AfterIpAddressChanges(string email, DateTime date, string ip, string country);
        Task AfterAccountIsDisabled(string email, DateTime date, string ip, string country);
        Task WrongPasswordAttempt(string email, DateTime date, string ip, string country);
        Task TwoFactorAuthCode(string email, string code);
        #endregion

        #region Ticket
        Task AfterCreatingTicketForAdmin(List<string> emails, string companyLogoPath, string fromEmail, string companyName, string ticketType, string ticketDetails, int ticketId);
        Task AfterTicketReplyFromAdmin(string email, string ticketDetails, int ticketId);
        #endregion

        #region Subscription
        Task SubscriptionReminder(string email, string plan, DateTime targetDate);
        Task CreateUserForAdmin(string email, string adminName, string userName, string userRole, string organization = "organization");
        Task CreateUserForUser(string email, string adminName, string userName, string userRole, string vaildToken, string organization = "organization");
        Task ManualPaymentRequest(string email, string planName, string bankName, string accName, string accNo, string financeEmail);
        #endregion

        #region Report a Problem
        Task ReportProblem(List<string> emails, string description, string equipmentName, string facilityName, string ManagerName, DateTime date);
        Task ReportProblemApproved(List<string> emails, string description, string equipmentName, string facilityName, DateTime date);
        Task ReportProblemDeclined(List<string> emails, string description, string equipmentName, string facilityName, string declinedReason, DateTime date);
        #endregion

        #region Work order Schedule
        Task WorkorderScheduleForMaintenancer(List<string> emails, string description, string equipmentName, string facilityName, string engineerName,string managerName, DateTime startdate, string starttime, DateTime enddate, DateTime date);
        Task WorkorderScheduleForEngineer(List<string> emails, string description, string equipmentName, string facilityName, string managerName, DateTime startdate, string starttime, string maintenancerName, string workOrder, DateTime date);
        #endregion

        #region Work order Completion
        Task WorkorderRejectForFacilitymanager(List<string> emails, string engineerName, string equipmentName, string ManagerName, string EngineerName, string FacilityName, string workorderTitle, DateTime date);
        Task WorkorderAccptedForFacilitymanager(List<string> emails, string engineerName, string equipmentName, string ManagerName, string EngineerName, string FacilityName, string workorderTitle, DateTime date);
        Task WorkorderCompletionForEngineer(List<string> emails, string workOrder, string equipmentName, string facilityName, string facilityManagerName, DateTime date);
        Task WorkorderCompletionForFacilitymanager(List<string> emails, string engineerName, string equipmentName, string ManagerName, string EngineerName,string FacilityName, string workorderTitle, DateTime date);
        Task WorkorderCompletionForMaintenancer(List<string> emails, string engineerName, string workOrder, string equipmentName, string facilityName, string ManagerName, string EngineerName, string FacilityName,DateTime reportdate, DateTime date);
        #endregion
    }
}

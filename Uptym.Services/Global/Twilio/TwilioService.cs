using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Rest.Verify.V2.Service;
using Twilio.Types;
using Uptym.Core.Interfaces;

namespace Uptym.Services.Global.Twilio
{
    public class TwilioService : ITwilioService
    {
        private readonly IConfiguration _configuration;
        private readonly IResponseDTO _responseDTO;
        private readonly string _accountSID;
        private readonly string _authToken;
        private readonly string _fromNumber;
        private readonly string _pathServiceSID;
        public TwilioService(IConfiguration configuration, IResponseDTO responseDTO)
        {
            _configuration = configuration;
            _responseDTO = responseDTO;
            _accountSID = _configuration["Twilio:AccountSID"];
            _authToken = _configuration["Twilio:AuthToken"];
            _fromNumber = _configuration["Twilio:FromNumber"];
            _pathServiceSID = _configuration["Twilio:PathServiceSID"];
        }
        public async Task<IResponseDTO> PhoneVerificationCheck(string to, string verificationCode)
        {
            if (string.IsNullOrEmpty(to))
            {
                _responseDTO.Message = "Invalid phone number";
                _responseDTO.IsPassed = false;
                return _responseDTO;
            }
            if (string.IsNullOrEmpty(verificationCode) || verificationCode?.Length < 4)
            {
                _responseDTO.Message = "Invalid verification code";
                _responseDTO.IsPassed = false;
                return _responseDTO;
            }

            try
            {
                TwilioClient.Init(_accountSID, _authToken);
                var verificationCheck = await VerificationCheckResource.CreateAsync(to: to, code: verificationCode, pathServiceSid: _pathServiceSID);
                if (!verificationCheck.Valid.Value)
                {
                    _responseDTO.Data = null;
                    _responseDTO.Message = "Verification code is wrong";
                    _responseDTO.IsPassed = false;
                    return _responseDTO;
                }
                if (verificationCheck.Status == "pending")
                {
                    _responseDTO.Data = null;
                    _responseDTO.Message = "Verification code is wrong";
                    _responseDTO.IsPassed = false;
                    return _responseDTO;
                }
            }
            catch (Exception ex)
            {
                _responseDTO.Data = null;
                _responseDTO.Message = $"Error {ex.Message}";
                _responseDTO.IsPassed = false;
            }


            return _responseDTO;
        }

        public async Task<IResponseDTO> SendPhoneVerificationCode(string to)
        {
            try
            {
                if (string.IsNullOrEmpty(to))
                {
                    _responseDTO.Message = "Invalid Phone Number";
                    _responseDTO.IsPassed = false;
                    return _responseDTO;
                }

                TwilioClient.Init(_accountSID, _authToken);
                var verification = await VerificationResource.CreateAsync(to: to, channel: "sms", pathServiceSid: _pathServiceSID);
                _responseDTO.Data = null;
                _responseDTO.Message = "Verification code is sent";
                _responseDTO.IsPassed = true;
            }
            catch (Exception ex)
            {
                _responseDTO.Data = null;
                _responseDTO.Message = $"Error {ex.Message}";
                _responseDTO.IsPassed = false;
            }

            return _responseDTO;
        }

        public async Task<IResponseDTO> SendSMSNotification(string to, string message)
        {
            if (string.IsNullOrEmpty(to))
            {
                _responseDTO.Message = "Invalid phone number";
                _responseDTO.IsPassed = false;
                return _responseDTO;
            }
            try
            {
                TwilioClient.Init(_accountSID, _authToken);
                var smsMessage = await MessageResource.CreateAsync(body: message, from: new PhoneNumber(_fromNumber), to: new PhoneNumber(to));
                _responseDTO.Data = smsMessage;
                _responseDTO.Message = "SMS is sent";
                _responseDTO.IsPassed = true;
            }
            catch (Exception ex)
            {
                _responseDTO.Data = null;
                _responseDTO.Message = $"Error {ex.Message}";
                _responseDTO.IsPassed = false;
            }

            return _responseDTO;
        }

        #region Report a Problem
        public async Task<IResponseDTO> ReportProblem(List<string> toNumbers, string description, string equipmentName, string facilityName, DateTime date)
        {

            var message = $"The {description} problem for {equipmentName} under {facilityName}, has been reported on {date:MM/dd/yyyy}. " +
                $"Kindly schedule work order accordingly";

            try
            {
                TwilioClient.Init(_accountSID, _authToken);
                for (int i = 0; i < toNumbers.Count; i++)
                {
                    if (string.IsNullOrEmpty(toNumbers[i]))
                    {
                        _responseDTO.Message = "Invalid phone number";
                        _responseDTO.IsPassed = false;
                        return _responseDTO;
                    }
                    await MessageResource.CreateAsync(body: message, from: new PhoneNumber(_fromNumber), to: new PhoneNumber(toNumbers[i]));
                }
                _responseDTO.Data = null;
                _responseDTO.Message = "SMS is sent";
                _responseDTO.IsPassed = true;
            }
            catch (Exception ex)
            {
                _responseDTO.Data = null;
                _responseDTO.Message = $"Error {ex.Message}";
                _responseDTO.IsPassed = false;
            }

            return _responseDTO;
        }

        public async Task<IResponseDTO> ReportProblemApproved(List<string> toNumbers, string description, string equipmentName, string facilityName, DateTime date)
        {
            var message = $"Maintenance head has approved {description} you created on {date:MM/dd/yyyy} for {equipmentName} under {equipmentName}.";

            try
            {
                TwilioClient.Init(_accountSID, _authToken);
                for (int i = 0; i < toNumbers.Count; i++)
                {
                    if (string.IsNullOrEmpty(toNumbers[i]))
                    {
                        _responseDTO.Message = "Invalid phone number";
                        _responseDTO.IsPassed = false;
                        return _responseDTO;
                    }
                    await MessageResource.CreateAsync(body: message, from: new PhoneNumber(_fromNumber), to: new PhoneNumber(toNumbers[i]));
                }
                _responseDTO.Data = null;
                _responseDTO.Message = "SMS is sent";
                _responseDTO.IsPassed = true;
            }
            catch (Exception ex)
            {
                _responseDTO.Data = null;
                _responseDTO.Message = $"Error {ex.Message}";
                _responseDTO.IsPassed = false;
            }

            return _responseDTO;
        }

        public async Task<IResponseDTO> ReportProblemDeclined(List<string> toNumbers, string description, string equipmentName, string facilityName, string declinedReason, DateTime date)
        {

            var message = $"Maintenance head has declined {description} problem reported you created on {date:MM/dd/yyyy} for {equipmentName} " +
                $"under {equipmentName}, for {declinedReason}.";

            try
            {
                TwilioClient.Init(_accountSID, _authToken);
                for (int i = 0; i < toNumbers.Count; i++)
                {
                    if (string.IsNullOrEmpty(toNumbers[i]))
                    {
                        _responseDTO.Message = "Invalid phone number";
                        _responseDTO.IsPassed = false;
                        return _responseDTO;
                    }
                    await MessageResource.CreateAsync(body: message, from: new PhoneNumber(_fromNumber), to: new PhoneNumber(toNumbers[i]));
                }
                _responseDTO.Data = null;
                _responseDTO.Message = "SMS is sent";
                _responseDTO.IsPassed = true;
            }
            catch (Exception ex)
            {
                _responseDTO.Data = null;
                _responseDTO.Message = $"Error {ex.Message}";
                _responseDTO.IsPassed = false;
            }

            return _responseDTO;
        }

        #endregion

        #region Work order Schedule
        public async Task<IResponseDTO> WorkorderScheduleForMaintenancer(List<string> toNumbers, string description, string equipmentName, string facilityName, string engineerName, DateTime date)
        {
            var message = $"You have successfully created a work order for {description} problem reported for {equipmentName} under {facilityName} " +
                $"to {engineerName} on {date:MM/dd/yyyy}.";

            try
            {
                TwilioClient.Init(_accountSID, _authToken);
                for (int i = 0; i < toNumbers.Count; i++)
                {
                    if (string.IsNullOrEmpty(toNumbers[i]))
                    {
                        _responseDTO.Message = "Invalid phone number";
                        _responseDTO.IsPassed = false;
                        return _responseDTO;
                    }
                    await MessageResource.CreateAsync(body: message, from: new PhoneNumber(_fromNumber), to: new PhoneNumber(toNumbers[i]));
                }
                _responseDTO.Data = null;
                _responseDTO.Message = "SMS is sent";
                _responseDTO.IsPassed = true;
            }
            catch (Exception ex)
            {
                _responseDTO.Data = null;
                _responseDTO.Message = $"Error {ex.Message}";
                _responseDTO.IsPassed = false;
            }

            return _responseDTO;
        }

        public async Task<IResponseDTO> WorkorderScheduleForEngineer(List<string> toNumbers, string description, string equipmentName, string facilityName, string maintenancerName, string workOrder, DateTime date)
        {

            var message = $"You have been assigned by {maintenancerName} to work on {description} problem for {equipmentName} located at {facilityName} on {date:MM/dd/yyyy}.";
            try
            {
                TwilioClient.Init(_accountSID, _authToken);
                for (int i = 0; i < toNumbers.Count; i++)
                {
                    if (string.IsNullOrEmpty(toNumbers[i]))
                    {
                        _responseDTO.Message = "Invalid phone number";
                        _responseDTO.IsPassed = false;
                        return _responseDTO;
                    }
                    await MessageResource.CreateAsync(body: message, from: new PhoneNumber(_fromNumber), to: new PhoneNumber(toNumbers[i]));
                }
                _responseDTO.Data = null;
                _responseDTO.Message = "SMS is sent";
                _responseDTO.IsPassed = true;
            }
            catch (Exception ex)
            {
                _responseDTO.Data = null;
                _responseDTO.Message = $"Error {ex.Message}";
                _responseDTO.IsPassed = false;
            }

            return _responseDTO;
        }

        #endregion

        #region Work order Completion
        public async Task<IResponseDTO> WorkorderCompletionForEngineer(List<string> toNumbers, string workOrder, string equipmentName, string facilityName, string facilityManagerName, DateTime date)
        {
            var message = $"You have successfully completed the maintenance work for {workOrder} on {equipmentName} under {facilityName}.";
            try
            {
                TwilioClient.Init(_accountSID, _authToken);
                for (int i = 0; i < toNumbers.Count; i++)
                {
                    if (string.IsNullOrEmpty(toNumbers[i]))
                    {
                        _responseDTO.Message = "Invalid phone number";
                        _responseDTO.IsPassed = false;
                        return _responseDTO;
                    }
                    await MessageResource.CreateAsync(body: message, from: new PhoneNumber(_fromNumber), to: new PhoneNumber(toNumbers[i]));
                }
                _responseDTO.Data = null;
                _responseDTO.Message = "SMS is sent";
                _responseDTO.IsPassed = true;
            }
            catch (Exception ex)
            {
                _responseDTO.Data = null;
                _responseDTO.Message = $"Error {ex.Message}";
                _responseDTO.IsPassed = false;
            }

            return _responseDTO;
        }

        public async Task<IResponseDTO> WorkorderCompletionForFacilitymanager(List<string> toNumbers, string engineerName, string equipmentName, DateTime date)
        {
            var message = $"{engineerName} has completed the work for {equipmentName}. Please Confirm/Reject the recorded maintenance work to complete the work order"; ;
            try
            {
                TwilioClient.Init(_accountSID, _authToken);
                for (int i = 0; i < toNumbers.Count; i++)
                {
                    if (string.IsNullOrEmpty(toNumbers[i]))
                    {
                        _responseDTO.Message = "Invalid phone number";
                        _responseDTO.IsPassed = false;
                        return _responseDTO;
                    }
                    await MessageResource.CreateAsync(body: message, from: new PhoneNumber(_fromNumber), to: new PhoneNumber(toNumbers[i]));
                }
                _responseDTO.Data = null;
                _responseDTO.Message = "SMS is sent";
                _responseDTO.IsPassed = true;
            }
            catch (Exception ex)
            {
                _responseDTO.Data = null;
                _responseDTO.Message = $"Error {ex.Message}";
                _responseDTO.IsPassed = false;
            }

            return _responseDTO;
        }
        public async Task<IResponseDTO> WorkorderCompletionForMaintenancer(List<string> toNumbers, string engineerName, string workOrder, string equipmentName, string facilityName, DateTime date)
        {

            var message = $"A maintenance record for {workOrder} for {equipmentName} under {facilityName} has been completed by {engineerName}";
            try
            {
                TwilioClient.Init(_accountSID, _authToken);
                for (int i = 0; i < toNumbers.Count; i++)
                {
                    if (string.IsNullOrEmpty(toNumbers[i]))
                    {
                        _responseDTO.Message = "Invalid phone number";
                        _responseDTO.IsPassed = false;
                        return _responseDTO;
                    }
                    await MessageResource.CreateAsync(body: message, from: new PhoneNumber(_fromNumber), to: new PhoneNumber(toNumbers[i]));
                }
                _responseDTO.Data = null;
                _responseDTO.Message = "SMS is sent";
                _responseDTO.IsPassed = true;
            }
            catch (Exception ex)
            {
                _responseDTO.Data = null;
                _responseDTO.Message = $"Error {ex.Message}";
                _responseDTO.IsPassed = false;
            }

            return _responseDTO;
        }

        #endregion
    }
}

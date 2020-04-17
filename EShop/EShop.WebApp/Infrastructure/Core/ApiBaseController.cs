using EShop.Model.Models;
using EShop.Service.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Net.Http;

namespace EShop.WebApp.Infrastructure.Core
{
  
    public class ApiBaseController : ControllerBase
    {
        private IErrorService _errorService;

        public ApiBaseController(IErrorService errorService)
        {
            this._errorService = errorService;
        }

        /// <summary>
        /// Log lỗi(nếu có) khi nhận request
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        protected HttpResponseMessage CreateHttpResponse(HttpRequestMessage requestMessage, Func<HttpResponseMessage> func)
        {
            HttpResponseMessage responseMessage = null;
            
            try
            {
                responseMessage = func.Invoke();
            }
        
            catch (DbUpdateException ex) 
            {
                LogError(ex);
                responseMessage = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                LogError(ex);
                responseMessage = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return responseMessage;


        }

        /// <summary>
        /// Log Error của toàn bộ chương trình vào csdl
        /// </summary>
        /// <param name="ex"></param>
        private void LogError(Exception ex)
        {
            try
            {
                Error error = new Error();
                error.CreatedDate = DateTime.Now;
                error.Message = ex.Message;
                error.StackTrace = ex.StackTrace;
                _errorService.Create(error);
                _errorService.Save();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
using EShop.Server.Data;
using EShop.Server.Repository;
using EShop.Server.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Server.Service
{

    public interface IErrorService
    {
        Error Create(Error error);
        void Save();

    }
    public class ErrorService : IErrorService
    {
        // khai báo
        /// <summary>
        /// _errorRepository: repository tương ứng với service cung cấp
        /// IUnitOfWork đơn vị công việc dùng để lưu database bằng _unitOfWork.Commit
        /// </summary>
        IErrorRepository _errorRepository; 
        IUnitOfWork _unitOfWork;

        public ErrorService(IErrorRepository errorRepository, IUnitOfWork unitOfWork) 
        {
            this._errorRepository = errorRepository;
            this._unitOfWork = unitOfWork;
        }
        public Error Create(Error error)
        {
             return _errorRepository.Add(error);
        }

       
        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}

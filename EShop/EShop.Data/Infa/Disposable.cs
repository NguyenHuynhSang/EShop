using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Data.In
{
    /// <summary>
    /// dọn rác
    /// </summary>
    public class Disposable : IDisposable
    {
        private bool isDisposed;
        
        ~Disposable()
        {
            isDisposed = false;
        }
        public void Dispose(bool disposing)
        {
            if (!isDisposed && disposing)
            {
                DisposeCore();
            }
            isDisposed = true;
        }

        public void Dispose()
        {
           // throw new NotImplementedException();
        }

        protected virtual void DisposeCore() 
        {

        }

    }
}

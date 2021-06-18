using Firebase.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Firebase.BLL.Services.Interfaces
{
    public interface IFirebaseService: IDisposable
    {
        Task<bool> MessagePush(string firebaseMessageDtoJson);
        void Start();
    }
}

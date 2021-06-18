using MessageReciever.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageReciever.BLL.Services.Interfaces
{
    public interface ISendMessageService: IDisposable
    {
        void Send(MessageDto message);
    }
}

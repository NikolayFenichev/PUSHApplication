using System;

namespace PUSHApplication.DAL.Repositories.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        public IRegistrationRepository RegistrationRepository { get; }
        public IMessageProcessingRepository MessageProcessingRepository { get; }
        public IStatisticRepository StatisticRepository { get; }
    }
}

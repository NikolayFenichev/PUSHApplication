using AutoMapper;
using Paging;
using PUSHApplication.DAL.Models;
using PUSHApplication.DAL.Repositories.Interfaces;
using Statistic.BLL.Models;
using Statistic.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistic.BLL.Services
{
    public class StatisticService : IStatisticService
    {
        private readonly IUnitOfWork _database;

        public StatisticService(IUnitOfWork uow)
        {
            _database = uow;
        }

        public async Task<List<VersionInfoDto>> GetRegisteredVersionsAsync()
        {
            var versionsInfo = await _database.StatisticRepository.GetRegisteredVersionsAsync();
            var mapper = new Mapper(new MapperConfiguration(cfg =>
                cfg.CreateMap<VersionInfo, VersionInfoDto>()));
            var versionsInfoDto = mapper.Map<List<VersionInfoDto>>(versionsInfo);

            return versionsInfoDto;
        }

        public async Task<PagedList<MessageDto>> GetMessagesByPhoneNumberAsync(PageParameters pageParameters, string phoneNumber)
        {
            var messages = await _database.StatisticRepository.GetMessagesByPhoneNumberAsync(pageParameters, phoneNumber);

            var messagesDto = new PagedList<MessageDto>(messages.Select(m =>
                new MessageDto { Header = m.Header, MessageSendTime = m.MessageSendTime, Text = m.Text }).ToList(),
                messages.TotalCount, messages.CurrentPage, messages.PageSize);

            return messagesDto;
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}

﻿using Echack.Application.ViewModels.Chack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Echack.Application.Interfaces
{
    public interface IChackService
    {
        Task<ChackViewModel> CreateCheck(ChackCreateViewModel model);
        Task<ChackEditViewModel> EditChack(ChackEditViewModel model);
        Task<ChackViewModel> GetChack(Guid id);
        Task<List<ChackViewModel>> GetUserChacksByUserId(int ownerId, int skip);
        Task<ChackViewModel> AddChackToGroup();
        Task<ChackViewModel> RemoveChackFromGroup();
        Task<List<ChackViewModel>> GetAllChacks(int skip);
    }
}
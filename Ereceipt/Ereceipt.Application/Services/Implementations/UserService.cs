﻿using AutoMapper;
using Ereceipt.Application.Extensions;
using Ereceipt.Application.Results.Users;
using Ereceipt.Application.Services.Interfaces;
using Ereceipt.Application.ViewModels.User;
using Ereceipt.Domain.Interfaces;
using Ereceipt.Domain.Models;
using Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Ereceipt.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserResult> EditUserAsync(UserEditViewModel model)
        {
            var user = await _userRepository.GetByIdAsTrackingAsync(model.UserId);
            if (user == null)
                return null;
            user.Name = model.Name;
            user.SetUpdateData(model);
            return new UserResult(_mapper.Map<UserViewModel>(await _userRepository.UpdateAsync(user)));
        }

        public async Task<ListUsersResult> GetAllUsersAsync(int afterId)
        {
            return new ListUsersResult(_mapper.Map<List<UserViewModel>>(await _userRepository.GetAllAsync(afterId)));
        }

        public async Task<UserResult> GetUserByIdAsync(int id)
        {
            return new UserResult(_mapper.Map<UserViewModel>(await _userRepository.FindAsync(d => d.Id == id)));
        }
        public async Task<ListUsersResult> SearchUsersAsync(string user, int afterId)
        {
            return new ListUsersResult(_mapper.Map<List<UserViewModel>>(await _userRepository.SearchUsersAsync(user, afterId)));
        }
    }
}
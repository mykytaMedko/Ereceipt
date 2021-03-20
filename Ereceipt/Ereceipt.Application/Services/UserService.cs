﻿using AutoMapper;
using Ereceipt.Application.Interfaces;
using Ereceipt.Application.Results;
using Ereceipt.Application.ViewModels.User;
using Ereceipt.Domain.Interfaces;
using Ereceipt.Domain.Models;
using Ereceipt.Infrastructure.Data;
using Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Ereceipt.Application.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserVMResult> CreateUser(UserCreateViewModel model)
        {
            var user = new User
            {
                Name = model.Name,
                Login = model.Login,
                PasswordHash = PasswordManager.GeneratePasswordHash(model.Password),
                CreatedBy = "0",
                Role = "User"
            };
            return new UserVMResult(_mapper.Map<UserViewModel>(await _userRepository.CreateAsync(user)));
        }

        public async Task<UserVMResult> EditUser(UserEditViewModel model)
        {
            var user = await _userRepository.GetByIdAsTrackingAsync(model.UserId);
            if (user == null)
                return null;
            user.Name = model.Name;
            user.UpdatedAt = DateTime.UtcNow;
            user.UpdatedBy = user.Id.ToString();
            return new UserVMResult(_mapper.Map<UserViewModel>(await _userRepository.UpdateAsync(user)));
        }

        public async Task<ListUsersVMResult> GetAllUsers(int afterId)
        {
            return new ListUsersVMResult(_mapper.Map<List<UserViewModel>>(await _userRepository.GetAllAsync(afterId)));
        }

        public async Task<UserVMResult> GetUserById(int id)
        {
            return new UserVMResult(_mapper.Map<UserViewModel>(await _userRepository.FindAsync(d => d.Id == id)));
        }

        public async Task<User> LoginUser(UserLoginViewModel model)
        {
            var user = await _userRepository.FindAsync(d => d.Login == model.Login);
            if (user == null)
                return null;
            if(!PasswordManager.VerifyPasswordHash(model.Password, user.PasswordHash))
            {
                return null;
            }
            return user;
        }

        public async Task<ListUsersVMResult> SearchUsers(string user, int afterId)
        {
            return new ListUsersVMResult(_mapper.Map<List<UserViewModel>>(await _userRepository.SearchUsersAsync(user, afterId)));
        }
    }
}
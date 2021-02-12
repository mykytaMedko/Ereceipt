﻿using AutoMapper;
using Echack.Application.Interfaces;
using Echack.Application.ViewModels.User;
using Echack.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Echack.Application.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserViewModel> GetUserById(int id)
        {
            return _mapper.Map<UserViewModel>(await _unitOfWork.UserRepository.FindAsync(d => d.Id == id));
        }

        public async Task<List<UserViewModel>> SearchUsers(string user, int afterId)
        {
            return _mapper.Map<List<UserViewModel>>(await _unitOfWork.UserRepository.SearchUsersAsync(user, afterId));
        }
    }
}
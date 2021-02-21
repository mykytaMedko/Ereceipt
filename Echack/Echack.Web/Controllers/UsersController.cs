﻿using Echack.Application.Interfaces;
using Echack.Application.MediatR.Commands;
using Echack.Application.MediatR.Queries;
using Echack.Application.ViewModels.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Echack.Web.Controllers
{
    public class UsersController : BaseController
    {
        IMediator _mediator;
        public UsersController(IUserService userService, IMediator mediator)
        {
            _userService = userService;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateCommand model)
        {
            var result = await _mediator.Send(model);
            return Ok(result);
        }

        [HttpGet]
        //[Authorize(Roles = "Admin, SAdmin")]
        public async Task<IActionResult> GetAllUsers(int afterId = 0)
        {
            var result = await _mediator.Send(new GetAllUsersQuery(afterId));
            return Ok(result);
        }

        [HttpPut("{id}")]
        //[Authorize]
        public async Task<IActionResult> EditUser(int id, [FromBody] UserEditCommand model)
        {
            model.UserId = id;
            var result = await _mediator.Send(model);
            return Ok(result);
        }

        [HttpGet("search")]
        //[Authorize(Roles = "Admin, SAdmin")]
        public async Task<IActionResult> SearchUsers(string name, int afterId = 0)
        {
            var result = await _mediator.Send(new SearchUsersQuery(name, afterId));
            return Ok(result);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var result = await _mediator.Send(new GetUserByIdQuery(id));
            return Ok(result);
        }
    }
}

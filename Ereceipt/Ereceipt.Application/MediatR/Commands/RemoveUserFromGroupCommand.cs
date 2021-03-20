﻿using Ereceipt.Application.Interfaces;
using Ereceipt.Application.Results.Groups;
using Ereceipt.Application.ViewModels.GroupMember;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace Ereceipt.Application.MediatR.Commands
{
    public class RemoveUserFromGroupCommand : IRequest<GroupMemberResult>
    {
        public GroupMemberCreateViewModel Member { get; }

        public RemoveUserFromGroupCommand(GroupMemberCreateViewModel member)
        {
            Member = member;
        }
    }

    public class RemoveUserFromGroupCommandHandler : IRequestHandler<RemoveUserFromGroupCommand, GroupMemberResult>
    {
        IGroupService _groupService;
        public RemoveUserFromGroupCommandHandler(IGroupService groupService)
        {
            _groupService = groupService;
        }

        public async Task<GroupMemberResult> Handle(RemoveUserFromGroupCommand request, CancellationToken cancellationToken)
        {
            return await _groupService.RemoveUserFromGroup(request.Member);
        }
    }
}
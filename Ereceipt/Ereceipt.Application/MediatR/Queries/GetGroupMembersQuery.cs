﻿using Ereceipt.Application.Interfaces;
using Ereceipt.Application.Results.Groups;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace Ereceipt.Application.MediatR.Queries
{
    public class GetGroupMembersQuery : IRequest<ListGroupMemberResult>
    {
        public Guid Id { get; }

        public GetGroupMembersQuery(Guid id)
        {
            Id = id;
        }
    }


    public class GetGroupMembersQueryHandler : IRequestHandler<GetGroupMembersQuery, ListGroupMemberResult>
    {
        IGroupService _groupService;
        public GetGroupMembersQueryHandler(IGroupService groupService)
        {
            _groupService = groupService;
        }


        public async Task<ListGroupMemberResult> Handle(GetGroupMembersQuery request, CancellationToken cancellationToken)
        {
            return await _groupService.GetGroupMembers(request.Id);
        }
    }
}
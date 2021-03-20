﻿using Ereceipt.Application.Interfaces;
using Ereceipt.Application.Results.Groups;
using Ereceipt.Application.ViewModels.Group;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace Ereceipt.Application.MediatR.Queries
{
    public class GetReceiptsByGroupIdQuery : IRequest<ListReceiptGroupResult>
    {
        public Guid Id { get; set; }
        public int Skip { get; set; }
        public GetReceiptsByGroupIdQuery(Guid id, int skip)
        {
            Id = id;
            Skip = skip;
        }
    }

    public class GetReceiptsByGroupIdQueryHandler : IRequestHandler<GetReceiptsByGroupIdQuery, ListReceiptGroupResult>
    {
        IGroupService _groupService;

        public GetReceiptsByGroupIdQueryHandler(IGroupService groupService)
        {
            _groupService = groupService;
        }

        public async Task<ListReceiptGroupResult> Handle(GetReceiptsByGroupIdQuery request, CancellationToken cancellationToken)
        {
            return await _groupService.GetReceiptsByGroupId(request.Id, request.Skip);
        }
    }
}
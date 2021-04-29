﻿using Ereceipt.Application.Extensions;
using Ereceipt.Application.MediatR.Commands;
using Ereceipt.Application.MediatR.Queries;
using Ereceipt.Application.ViewModels.Receipt;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
namespace Ereceipt.Web.Controllers
{
    public class ReceiptsController : ApiController
    {
        IMediator _mediator;

        public ReceiptsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReceipt([FromBody] ReceiptCreateViewModel model)
        {
            model.InitDataRequest(GetId(), GetIpAddress());
            var result = await _mediator.Send(new ReceiptCreateCommand(model));
            return Result(result);
        }

        [HttpPut]
        public async Task<IActionResult> EditReceipt([FromBody] ReceiptEditViewModel model)
        {
            model.InitDataRequest(GetId(), GetIpAddress());
            var result = await _mediator.Send(new ReceiptEditCommand(model));
            return Result(result);
        }

        [HttpPost("togroup")]
        public async Task<IActionResult> AddReceiptToGroup([FromBody] ReceiptGroupCreateModel model)
        {
            model.InitDataRequest(GetId(), GetIpAddress());
            var result = await _mediator.Send(new AddReceiptToGroupCommand(model));
            return Result(result);
        }

        [HttpPost("fromgroup")]
        public async Task<IActionResult> RemoveReceiptFromGroup([FromBody] ReceiptGroupCreateModel model)
        {
            model.InitDataRequest(GetId(), GetIpAddress());
            var result = await _mediator.Send(new RemoveReceiptFromGroupCommand(model));
            return Result(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveReceipt(Guid id)
        {
            var result = await _mediator.Send(new RemoveReceiptCommand(GetId(), id));
            return Result(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetReceiptById(Guid id)
        {
            var result = await _mediator.Send(new GetReceiptByIdQuery(id));
            return Result(result);
        }

        [HttpGet("{id}/comments")]
        public async Task<IActionResult> GetCommentsOfReceipt(Guid id)
        {
            var result = await _mediator.Send(new GetCommentsOfReceiptQuery(id));
            return Result(result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, SAdmin")]
        public async Task<IActionResult> GetAllReceipts(int skip = 0)
        {
            var result = await _mediator.Send(new GetAllReceiptsQuery(skip));
            return Result(result);
        }

        [HttpGet("count")]
        [Authorize(Roles = "Admin, SAdmin")]
        public async Task<IActionResult> GetAllReceiptsCount()
        {
            var result = await _mediator.Send(new GetAllReceiptsCountQuery());
            return Result(result);
        }


        [HttpGet("my")]
        public async Task<IActionResult> GetMyReceipts(int skip = 0)
        {
            var result = await _mediator.Send(new GetMyReceiptsQuery(GetId(), skip));
            return Result(result);
        }

        [HttpGet("my/count")]
        public async Task<IActionResult> GetMyReceiptsCount()
        {
            var result = await _mediator.Send(new GetUserReceiptsCountQuery(GetId()));
            return Result(result);
        }
    }
}
using Ereceipt.Application.ViewModels.Group;
using System.Collections.Generic;
namespace Ereceipt.Application.Results.Groups
{
    public class ListGroupResult : Result
    {
        public ListGroupResult(List<GroupViewModel> groups) : base(groups) { }
    }
}
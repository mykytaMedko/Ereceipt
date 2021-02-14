﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Echack.Application.ViewModels.Group
{
    public class GroupViewModel
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Desc { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
﻿using Ereceipt.Application.ViewModels.Group;
using Ereceipt.Application.ViewModels.User;
using Ereceipt.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Ereceipt.Application.ViewModels.Chack
{
    public class ChackViewModel
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string ShopName { get; set; }
        public double TotalPrice { get; set; }
        public List<ProductViewModel> Products { get; set; }
        public bool IsImportant { get; set; }
        public bool CanEdit { get; set; }
        public ChackType ChackType { get; set; }
        public UserChackViewModel User { get; set; }
        public GroupChackViewModel Group { get; set; }
        public double GetTotalPrice()
        {
            if (Products == null || Products.Count == 0)
            {
                TotalPrice = 0;
                return 0;
            }
            var tPrice = Products.Sum(d => d.Price);
            TotalPrice = tPrice;
            return tPrice;
        }
    }
}
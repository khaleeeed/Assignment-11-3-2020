using System;
using System.Collections.Generic;

namespace Assignment.Domain.Entity
{
    public partial class AssignmentTable
    {
        public long Id { get; set; }
        public string Key { get; set; }
        public string ItemCode { get; set; }
        public string ColorCode { get; set; }
        public string Description { get; set; }
        public double? DiscountPrice { get; set; }
        public string DeliveredIn { get; set; }
        public string Q1 { get; set; }
        public double? Size { get; set; }
        public string Color { get; set; }
    }
}

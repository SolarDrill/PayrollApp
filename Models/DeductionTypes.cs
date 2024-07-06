﻿using Microsoft.AspNetCore.Mvc;

namespace payroll.Models
{
    public class DeductionTypes
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool DependsOnSalary { get; set; }
        public bool State { get; set; }
    }
}
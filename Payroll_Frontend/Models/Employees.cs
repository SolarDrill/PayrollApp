using Microsoft.AspNetCore.Mvc;

namespace Payroll_Frontend.Models
{
    public class Employees
    {
        public int Id { get; set; }
        public string Cedula { get; set; }
        public string Name { get; set; }
        public string? Department { get; set; }
        public string? Position { get; set; }
        public decimal MonthlySalary { get; set; }
        public int PayrollId { get; set; }
    }
}
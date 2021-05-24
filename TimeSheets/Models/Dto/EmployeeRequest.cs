using System;

namespace TimeSheets.Models.Dto
{
    public class EmployeeRequest
    {        
        public Guid UserId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
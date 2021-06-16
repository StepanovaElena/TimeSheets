using System;

namespace TimeSheets.Models.Dto.Requests
{
    public class ClientRequest
    {
        public Guid UserId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
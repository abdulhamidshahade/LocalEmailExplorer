using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalEmailExplorer.Application.DTOs.EmailDtos
{
    public class EmailBaseDto
    {
        public string EmailAddress { get; set; }
        public string? Password { get; set; }
        public string? Phone { get; set; }
        public string? RecoveryEmail { get; set; }
        public bool IsActive { get; set; }
        public bool IsAvailable { get; set; }
    }
}

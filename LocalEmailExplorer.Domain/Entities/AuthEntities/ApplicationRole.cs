
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Microsoft.AspNetCore.Identity;

namespace LocalEmailExplorer.Domain.Entities.AuthEntities
{
    public class ApplicationRole : IdentityRole<int>
    {
        public ApplicationRole()
        {

        }

        public ApplicationRole(string role) : base(role)
        {

        }
    }
}

using Identity.Microservice.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Microservice.Application.Commands
{
    public class UpdateUserProfileCommand : IRequest<Profile>
    {
        public int UserId { get; set; }
        public string Fullname { get; set; } = "";
        public string Position { get; set; } = "";
    }
}

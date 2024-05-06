using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EyeC.Application.Common.Models;
using EyeC.Application.TodoItems.Queries.GetTodoItemsWithPagination;
using EyeC.Domain.Entities;

namespace EyeC.Application.Users.Queries.GetUsers;
public class UserViewModel
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set;} = string.Empty;
    public string Role { get; set; } = string.Empty;

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<IdentityUserModel, UserViewModel>();
        }
    }
}

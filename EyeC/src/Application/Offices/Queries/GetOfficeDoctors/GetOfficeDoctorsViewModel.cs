using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EyeC.Domain.Entities;
using EyeC.Domain.Enums;

namespace EyeC.Application.Offices.Queries.GetOfficeDoctors;
public class GetOfficeDoctorsViewModel : Profile
{
    public int DoctorId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    public string PersonalExperience { get; set; } = string.Empty;
    public string Education { get; set; } = string.Empty;
    public string FeatureImagePath { get; set; } = string.Empty;
    public int OfficeId { get; set; }

    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Doctor, GetOfficeDoctorsViewModel>();
        }
    }
}

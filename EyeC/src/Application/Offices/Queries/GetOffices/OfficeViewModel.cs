using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EyeC.Domain.Entities;

namespace EyeC.Application.Offices.Queries.GetOffices;
public class OfficeViewModel
{
    public int OfficeId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string GoogleMapShortLink { get; set; } = string.Empty;
    public string Lat { get; set; } = string.Empty;
    public string Lng { get; set; } = string.Empty;
    public string FeatureImagePath { get; set; } = string.Empty;
    public string Introduction { get; set; } = string.Empty;

    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Office, OfficeViewModel>();
        }
    }
}

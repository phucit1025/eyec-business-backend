using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeC.Domain.Entities;
public class Office : BaseAuditableEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set;} = string.Empty;
    public string Email { get; set;} = string.Empty;
    public string PhoneNumber { get; set;} = string.Empty;
    public string GoogleMapShortLink { get; set;} = string.Empty;
    public string Lat { get; set;} = string.Empty;
    public string Lng { get; set; } = string.Empty;
    public string FeatureImagePath { get; set;} = string.Empty;
    public string Introduction { get; set; } = string.Empty;
}

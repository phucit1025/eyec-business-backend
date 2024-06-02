using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeC.Domain.Entities;
public class Doctor : BaseAuditableEntity
{
    public string FullName { get; set;} = string.Empty;
    public string PhoneNumber { get; set;} = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    public string PersonalExperience { get; set; } = string.Empty;
    public int? OfficeId { get; set;}
    public string Education { get; set; } = string.Empty;
    public string FeatureImagePath { get; set;} = string.Empty;
}

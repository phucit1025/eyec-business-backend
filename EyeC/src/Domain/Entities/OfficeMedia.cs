using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeC.Domain.Entities;
public class OfficeMedia : BaseAuditableEntity
{
    public string MediaPath { get; set; } = string.Empty;
    public int OfficeId { get; set; }
    public int Order { get; set; }
}

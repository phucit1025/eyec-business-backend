using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EyeC.Application.Common.Interfaces;
using EyeC.Application.Common.Models;

namespace EyeC.Application.Offices.Commands.DeleteOffice;
public record DeleteOfficeCommand(int OfficeId) : IRequest<ResultModel>
{
}

public class DeleteOfficeCommandHandler : IRequestHandler<DeleteOfficeCommand, ResultModel>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _dbContext;

    public DeleteOfficeCommandHandler(IMapper mapper, IApplicationDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<ResultModel> Handle(DeleteOfficeCommand request, CancellationToken cancellationToken)
    {
        var result = new ResultModel();
        try
        {
            var office = await _dbContext.Offices.FirstOrDefaultAsync(i => !i.IsDeleted && i.OfficeId == request.OfficeId, cancellationToken);
            if (office != null)
            {
                office.IsDeleted = true;
                _dbContext.Offices.Update(office);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            result.Success = true;
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Error = ex.ToString();
        }
        return result;
    }
}

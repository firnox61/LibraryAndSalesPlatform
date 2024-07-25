using Core.Utilities.Result;
using Entities.Concrete;
using Entities.DTOs.OperationDetailDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IOperationClaimService
    {
        IResult Add(OperationDto operationDto);
        IDataResult<List<OperationDto>> GetAll();
    }
}

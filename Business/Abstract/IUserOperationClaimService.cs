﻿using Core.Utilities.Result;
using Entities.Concrete;
using Entities.DTOs.OperationDetailDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserOperationClaimService
    {
        IResult Add(UserOperationDto userOperationDto);
        IDataResult<List<UserOperationDto>> GetAll();
       // IDataResult<List<UserOperationDto>> GetAllClaim()
       //IDataResult<UserOperationDto> GetUserClaim(int id);
    }
}

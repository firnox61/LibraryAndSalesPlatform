﻿using AutoMapper;
using Business.Abstract;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.OperationDetailDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserOperationClaimManager : IUserOperationClaimService
    {
        IUserOperationClaimDal _claim;
        private readonly IMapper _mapper;

        public UserOperationClaimManager(IUserOperationClaimDal claim, IMapper mapper)
        {
            _claim = claim;
            _mapper = mapper;
        }

        public IResult Add(UserOperationDto userOperationDto)
        {
            var result=_mapper.Map<UserOperationClaim>(userOperationDto);
           _claim.Add(result);
            return new SuccessResult();
        }
    }
}
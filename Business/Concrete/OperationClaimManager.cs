using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.OperationDetailDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class OperationClaimManager : IOperationClaimService
    {
        IOperationClaimDal _claim;
        private readonly IMapper _mapper;

        public OperationClaimManager(IOperationClaimDal claim, IMapper mapper)
        {
            _claim = claim;
            _mapper = mapper;
        }

        public IResult Add(OperationDto operationDto)
        {
            var result=_mapper.Map<OperationClaim>(operationDto);
            _claim.Add(result);
            return new SuccessResult(Messages.ClaimAdded);
        }
    }
}

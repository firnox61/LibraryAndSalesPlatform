﻿using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ShelfManager : IShelfService
    {
        IShelfDal _shelfDal;
        IMapper _mapper;

        public ShelfManager(IShelfDal shelfDal, IMapper mapper)
        {
            _shelfDal = shelfDal;
            _mapper = mapper;
        }

        public IResult Add(Shelf shelf)
        {
            _shelfDal.Add(shelf);
            return new SuccessResult(Messages.ShelfdAdd);
        }

        public IDataResult<List<Shelf>> GetAll()
        {
            var shelfs=_shelfDal.GetAll();
            return new SuccessDataResult<List<Shelf>>(shelfs, Messages.ShelfdList);
        }

        public IDataResult<Shelf> GetById(int id)
        {
            var shelf=_shelfDal.Get(s=>s.Id == id);
            return new SuccessDataResult<Shelf>(shelf, Messages.ShelfDetail);
        }
    }
}

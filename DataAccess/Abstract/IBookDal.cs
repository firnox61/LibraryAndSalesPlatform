﻿using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs.BooksDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IBookDal:IEntityRepository<Book>
    {
        List<BookShelfDetailDto> BookShelfDetail();
    }
}

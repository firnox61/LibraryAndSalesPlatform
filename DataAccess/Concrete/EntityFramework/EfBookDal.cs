using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.BooksDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfBookDal : EfEntityRepositoryBase<Book, DataContext>, IBookDal
    {
        public List<BookShelfDetailDto> BookShelfDetail()
        {
            using (DataContext context = new DataContext())
            {
                var result = (from b in context.Books
                              join s in context.Shelves
                              on b.ShelfId equals s.Id
                              select new BookShelfDetailDto
                              {
                                  Id = b.Id,
                                  Title = b.Title,
                                  Genre = b.Genre,
                                  Description = b.Description,
                                  LayerNumber = s.LayerNumber
                                  ,
                                  SequenceNumber = s.SequenceNumber,
                                  SectionCode = s.SectionCode
                              }).ToList();
                return result;
            }
        }
    }
}

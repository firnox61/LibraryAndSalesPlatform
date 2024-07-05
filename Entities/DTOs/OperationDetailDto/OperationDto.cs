using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.OperationDetailDto
{
    public class OperationDto:IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

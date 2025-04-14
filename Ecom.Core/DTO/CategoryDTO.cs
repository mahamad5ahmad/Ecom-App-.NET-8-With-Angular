using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.DTO
{
    public record CategoryDTO( String Name , String Description );
    public record CategoryDTOUpdate(String Name, String Description , int Id);
}

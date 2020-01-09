using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface IPowerDAL:IGeneralDAL<Power>
    {
        //删除power和与之关联的关系表
        Task<ResponseClass> DeletedAsync(long Id);
    }
}

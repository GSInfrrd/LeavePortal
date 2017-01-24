using LMS_WebAPI_Domain;
using LMS_WebAPI_Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_WebAPI_DAL.Repositories.Interfaces
{
 public interface IWorkFromHome
    {
        IList<WorkFromHomeCommonModel> GetWorkFromHomeList(int EmpId);

        long AddWorkFromHome(WorkFromHome newWorkFromHome);

        long DeleteWorkFromHomeRequest(long id);

        bool UpdateWorkFromHome(WorkFromHome WorkFromHome);

        List<WorkFromHomeReasonModel> GetWorkFromHomeReasonsList();
    }
}

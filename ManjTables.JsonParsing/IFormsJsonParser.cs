using ManjTables.DataModels.Models;
using ManjTables.DataModels.Models.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManjTables.JsonParsing
{
    public interface IFormsJsonParser
    {
        List<FormModel> ParseRawJsonIntoFormModels(List<ChildInfo> childInfos);
        List<FormModel> FilterFormsUsingFormTemplateIds(List<FormModel> formModels, FormTemplateIds formTemplateIds);
    }
}

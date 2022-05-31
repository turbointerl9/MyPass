using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateApp.MVVM.Model;

namespace TemplateApp.Core.Data
{
    public interface IDataService
    {
        public IEnumerable<LogData> GetLogData();
    }
}

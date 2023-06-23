using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppQueueManagementAdmin
{
    public class MainMenu : Page
    {
        public MainMenu(Page previous)
        {
            PageName = "MainMenu";
            PreviousPage = this;
            //pageList = new List<Page>();

            pageList.Add(new CreateCompany(this));
            pageList.Add(new CreateBranch(this));
            pageList.Add(new CreateService(this));
            pageList.Add(new CreateQuestion(this));
            pageList.Add(new ExitPage(this));
        }
      
    }
}

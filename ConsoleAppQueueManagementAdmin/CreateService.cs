using System;
using System.Net.Http;
using System.Net.Http.Json;

namespace ConsoleAppQueueManagementAdmin
{
    internal class CreateService : Page
    {
        public CreateService(Page previous)
        {
            PageName = "Create Service Page";
            PreviousPage = previous;
        }
        public override void Menu()
        {
            service();
        }
        public void service()
        {
            Console.WriteLine(PageName);
            ServiceMenu s = new ServiceMenu();
            s.MenuName = consolereader.getstring("Please Enter Service Menu name To create:- ");

            using (HttpClient client = new HttpClient())
            {
                OutResponse<ServiceMenu> res = client.PostAsJsonAsync("https://localhost:44372/Api/ServiceMenu", s).Result.Content.ReadFromJsonAsync<OutResponse<ServiceMenu>>().Result;

                Console.WriteLine(res.ResMessage);
                Console.WriteLine(res.ResData.MenuId + "-->" + res.ResData.MenuName);
            }
            Console.ReadLine();
        }
    }
}
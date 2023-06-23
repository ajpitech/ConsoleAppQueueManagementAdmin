using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppQueueManagementAdmin
{
    internal class CreateCompany : Page
    {
        public CreateCompany(Page previous)
        {
            PageName = "Create Company Page";
            PreviousPage = previous;
        }
        public override void Menu()
        {
            Companyservice();
        }
        public void Companyservice()
        {
            Console.WriteLine(PageName);
            Company company= new Company();
            company.CompanyName=consolereader.getstring("Please Enter Company name To create:- ");
            
            using (HttpClient client = new HttpClient())
            {
                OutResponse<Company> res = client.PostAsJsonAsync("https://localhost:44372/Api/Company", company).Result.Content.ReadFromJsonAsync<OutResponse<Company>>().Result;

                Console.WriteLine(res.ResMessage);
                Console.WriteLine(res.ResData.CompanyId+"-->"+res.ResData.CompanyName);
            }
            Console.ReadLine();

        }
    }
}

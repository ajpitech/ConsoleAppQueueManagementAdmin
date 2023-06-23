using System;
using System.Net.Http;
using System.Net.Http.Json;

namespace ConsoleAppQueueManagementAdmin
{
    internal class CreateBranch : Page
    {
        public CreateBranch(Page previous)
        {
            PageName = "Create Branch Page";
            PreviousPage = previous;
        }
        public override void Menu()
        {
            Branch();
        }
        public void Branch()
        {
            Console.WriteLine(PageName);
            Branch br = new Branch();
            br.BranchName = consolereader.getstring("Please Enter Branch name To create:- ");

            using (HttpClient client = new HttpClient())
            {
                OutResponse<Branch> res = client.PostAsJsonAsync("https://localhost:44372/Api/Branch", br).Result.Content.ReadFromJsonAsync<OutResponse<Branch>>().Result;

                Console.WriteLine(res.ResMessage);
                Console.WriteLine(res.ResData.BranchID + "-->" + res.ResData.BranchName);
            }
            Console.ReadLine();

        }
    }
}
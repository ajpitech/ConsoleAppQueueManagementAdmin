using System;
using System.Net.Http;
using System.Net.Http.Json;

namespace ConsoleAppQueueManagementAdmin
{
    internal class CreateQuestion : Page
    {
         public CreateQuestion(Page previous)
        {
            PageName = "Create Question Page";
            PreviousPage = previous;
        }
        public override void Menu()
        {
            Questionservice();
        }
        public void Questionservice()
        {
            Console.WriteLine(PageName);
            QuestionMenu q = new QuestionMenu();
            q.Question = consolereader.getstring("Please Enter Question name To create:- ");
            q.MenuId = consolereader.getint("Please Enter Menu No:- ");

            using (HttpClient client = new HttpClient())
            {
                OutResponse<QuestionMenu> res = client.PostAsJsonAsync("https://localhost:44372/Api/Question", q).Result.Content.ReadFromJsonAsync<OutResponse<QuestionMenu>>().Result;

                Console.WriteLine(res.ResMessage);
                Console.WriteLine(res.ResData.QuestionId + "-->" + res.ResData.Question);
            }
            Console.ReadLine();

        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace ConsoleAppQueueManagementAdmin
{
    public class Page
    {
        public string PageName { get; set; }
        public Page PreviousPage { get; set; }
        public int MenuId { get; set; }
        public static int Service_Branch_id { get; set; }
        public List<Page> pageList = new List<Page>();

        public virtual void Menu()
        {
             ShowMenu();
        }
        public void ShowPageName() { Console.WriteLine("This is " + PageName + " page."); }
         static bool Login = false;
        public void ShowMenu()
        {
            if (Login == false)
            {
                Login = LoginPage();
                Console.WriteLine("Login Successful.\nPress Enter to Proceed.");
                Console.ReadLine();
            }
            //ShowPageName();
            if (Login == true)
            {
               
                Options();
            }
            else
            {
                Console.WriteLine("Incorrect Credentials.");
                Thread.Sleep(2000);
                ShowMenu();
            }
        }

        public void Options()
        {
            
            Console.Clear();
            if (pageList.Count > 0)
            {
                Console.WriteLine("Enter Your Choice:");
                foreach (Page p in pageList)
                {
                    Console.WriteLine((pageList.IndexOf(p) + 1) + "." + p.PageName);
                }
                Console.WriteLine((pageList.Count + 1) + "." + PreviousPage.PageName + "(Previous Page)");

                int choice = consolereader.getint("");
                if (choice > 0 && choice <= pageList.Count)
                {
                    pageList[choice - 1].Menu();
                    ShowMenu();

                }
                else if (choice == pageList.Count + 1)
                {
                    PreviousPage.ShowPageName();
                    PreviousPage.Menu();
                }
                else
                {
                    Console.WriteLine("Try Again..");
                    ShowMenu();
                }
            }
        }
      //  string filepath;
        public bool LoginPage()
        {
            Console.Clear();
            Title();
            bool success = false;
          //  OutResponse<List<bool>> res = new OutResponse<List<bool>>();
            Admin admin = new Admin();
            Console.WriteLine("Enter UserName");
            admin.Username = Console.ReadLine();

            Console.WriteLine("Enter Password");
            admin.Password = Console.ReadLine();

            using (HttpClient client = new HttpClient())
            {
                var response = client.PostAsJsonAsync("https://localhost:44372/Api/Admin", admin).Result.Content.ReadFromJsonAsync<OutResponse<List<bool>>>().Result;
                foreach (var item in response.ResData)
                {
                    success = item;
                }
            }
            return success;
        }
        public void Title()
        {
            string s = "";
            string filepath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Ajay_ServicePin.txt";
            if (File.Exists(filepath))
            {
                s = File.ReadAllText(filepath);
            }
            if (s != "" || s != null)
            {
                string[] sarray = s.Split('=');
                Service_Branch_id = Convert.ToInt32(sarray[1]);
                using (HttpClient client = new HttpClient())
                {
                    OutResponse<List<ServiceBranchId>> res = client.GetFromJsonAsync<OutResponse<List<ServiceBranchId>>>("https://localhost:44372/Api/ServiceProvider/" + Service_Branch_id).Result;
                    foreach (ServiceBranchId item in res.ResData)
                    {
                        Console.WriteLine("Welcome To Admin Page " + item.CompanyName + " Of " + item.BranchName + " Branch");

                    }
                }
            }
        }
    }
}
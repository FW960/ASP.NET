using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Proj
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WebClient web = new WebClient();

            string str = web.DownloadString("https://www.lesegais.ru/open-area/deal");
        }
    }
}

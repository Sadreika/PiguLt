using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace CrawlerPiguLt
{
    public class codeExeptions
    {
        public string checkingForLinkExeption(string url)
        {
            string newUrl = "";
            if (url.Equals(@"https://pigu.lt/lt/vaistines-prekes/medicinines-prekes/pirmoji-pagalba?f[13585][3816351]=daugkartines-kaukes&f[13585][3816376]=medicinines-kaukes&f[13585][3816356]=vienkartines-kaukes"))
            {
                newUrl = @"https://pigu.lt/lt/vaistines-prekes/medicinines-prekes/pirmoji-pagalba?f[13585][3816351]=&f[13585][3816356]=&f[13585][3816376]=&page=";
            }
            if (url.Equals(@"https://pigu.lt/lt/namu-remontas/darbo-apranga/galvos-pasauga?f[6457][3772201]=apsauginiai-skydeliai&f[6457][3772181]=apsauginiai-akiniai"))
            {
                newUrl = @"https://pigu.lt/lt/namu-remontas/darbo-apranga/galvos-pasauga?f[6457][3772181]=&f[6457][3772201]=&page=";
            }
            
            return newUrl;
        }
    }
}

using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;

namespace USD
{
    class Program
    {
        static void Main(string[] args)
        {
            // Текущая дата
            string data = string.Empty;
            // Адрес сайта с курсом валюты
            string url = "http://www.cbr.ru/scripts/XML_daily.asp?date_req=";
            // HTML сайта с курсом валюты
            string html = string.Empty;
            // Регулярное выражение
            string pattern = @"Доллар США</Name><Value>[\d]+,[\d]+</Value>";

            string valuePattern = @"[\d]+,[\d]";

            // Определяем текущую дату
            DateTime today = DateTime.Now;
            data = today.Date.ToShortDateString();


            // Формируем адрес сайта
            url += data;

            // Отправляем GET запрос и получаем в ответ HTML-код сайта с курсом валюты
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
            var srcEncoding = Encoding.Default;
            StreamReader myStreamReader = new StreamReader(myHttpWebResponse.GetResponseStream(),srcEncoding);
            html = myStreamReader.ReadToEnd();

            // Вытаскиваем из HTML-кода нужные данные
            Match match = Regex.Match(html, pattern);
            Match dollarRate= Regex.Match(match.ToString(), valuePattern);

            Console.WriteLine("Курс Доллара США на {0} равен {1} руб.", data, dollarRate.ToString());
            Console.ReadLine();
        }
    }
}
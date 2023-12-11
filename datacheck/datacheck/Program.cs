using datacheck.Database;
using datacheck.model;
using HtmlAgilityPack;
using System.Xml;

Console.ForegroundColor = ConsoleColor.DarkGreen;

Console.WriteLine("Adres Gir");

var adress = Console.ReadLine();

Console.WriteLine("Sayfa");

var pages = int.Parse(Console.ReadLine());

for (int i = 1; i <= pages; i++)
{
    var httpClient = new HttpClient();
    var item = new item();


    HttpResponseMessage response = await httpClient.GetAsync($"{adress}/page/{i}/");

    Thread.Sleep(1000);

    // htmlBelgesiument nesnesi oluştur.
    HtmlAgilityPack.HtmlDocument htmlBelgesi = new HtmlAgilityPack.HtmlDocument();

    // Belgeyi çözümle
    htmlBelgesi.LoadHtml(await response.Content.ReadAsStringAsync());

    HtmlNodeCollection hedefDiv = htmlBelgesi.DocumentNode.SelectNodes("//div[contains(@class, 'anabaslik1')]//a");

    HtmlNodeCollection descriptionNode = htmlBelgesi.DocumentNode.SelectNodes("//div[@style='text-align:center;']");

    var count = 0;

    foreach (var sec in hedefDiv)
    {
        
        Console.WriteLine(sec.GetAttributeValue("href",""));

        Console.WriteLine(descriptionNode[count].InnerText);

        var httpClientPage = new HttpClient();

        HttpResponseMessage responsePage = await httpClientPage.GetAsync($"{sec.GetAttributeValue("href", "")}");

        HtmlAgilityPack.HtmlDocument htmlBelgesiNew = new HtmlAgilityPack.HtmlDocument();


        htmlBelgesiNew.LoadHtml(await responsePage.Content.ReadAsStringAsync());


        HtmlNodeCollection resimEtiketi = htmlBelgesiNew.DocumentNode.SelectNodes("//div[@class='maincont alanim2']//img");

        HtmlNodeCollection baslik = htmlBelgesiNew.DocumentNode.SelectNodes("//h1[@id='news-title']");

        if (baslik != null)
        {
            string bas = baslik[0].InnerText.Trim();
            Console.WriteLine(bas);
        }

        if (resimEtiketi != null)
        {
            string srcDegeri = resimEtiketi[0].GetAttributeValue("src", "");
            Console.WriteLine(srcDegeri);
        }

        HtmlNode lastLinkNode = htmlBelgesiNew.DocumentNode.SelectSingleNode("//span[@style='font-size:12pt;']/b/a[last()]");

        string lastLinkHref = lastLinkNode.GetAttributeValue("href", "");

        Console.WriteLine(lastLinkHref.Replace("amp;",""));

        count++;
        Console.WriteLine("/////////////////////////////////////////////////");

    }
}


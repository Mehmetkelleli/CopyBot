using datacheck.Database;
using datacheck.model;
using HtmlAgilityPack;

Console.ForegroundColor = ConsoleColor.DarkGreen;
var context = new DataContext();

Console.WriteLine("Adres Gir");

var adress = Console.ReadLine();

Console.WriteLine("Sayfa");

var pages = int.Parse(Console.ReadLine());

for (int i = 1; i <= pages; i++)
{
    var httpClient = new HttpClient();

    HttpResponseMessage response = await httpClient.GetAsync($"{adress}/page/{i}/");

    Thread.Sleep(1000);

    // htmlBelgesiument nesnesi oluştur.
    HtmlAgilityPack.HtmlDocument htmlBelgesi = new HtmlAgilityPack.HtmlDocument();

    // Belgeyi çözümle
    htmlBelgesi.LoadHtml(await response.Content.ReadAsStringAsync());

    HtmlNodeCollection hedefDiv = htmlBelgesi.DocumentNode.SelectNodes("//div[contains(@class, 'anabaslik1')]//a");



    foreach (var sec in hedefDiv)
    {
        Console.WriteLine(sec.GetAttributeValue("href",""));
        var httpClientPage = new HttpClient();

        HttpResponseMessage responsePage = await httpClientPage.GetAsync($"{sec.GetAttributeValue("href", "")}");

        HtmlAgilityPack.HtmlDocument htmlBelgesiNew = new HtmlAgilityPack.HtmlDocument();


        htmlBelgesiNew.LoadHtml(await responsePage.Content.ReadAsStringAsync());

        var item = new item();

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

        Console.WriteLine("/////////////////////////////////////////////////");

    }
}


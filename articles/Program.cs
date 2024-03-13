// See https://aka.ms/new-console-template for more information
using articles;
using Newtonsoft.Json;

var articles = GetArticles("articles.txt");
if (articles != null)
{
	var response = GetNumberOfPublishedArticles(articles);
	foreach (var r in response)
	{
		Console.WriteLine($"{r.Key} {r.Value}");
	}
}
else
{
	Console.WriteLine("File is Empty");
}


 IList<Article>? GetArticles(string filepath)
{
	string articles = File.ReadAllText(filepath);

	if(string.IsNullOrEmpty(articles))
	{ 
		return null;
	}

	var listOfArticles = JsonConvert.DeserializeObject<List<Article>>(articles);

	return listOfArticles;
}


Dictionary<string, int> GetNumberOfPublishedArticles(IList<Article> articles)
{
	//From the dictionary idea that I had at the end of our chat I was able to optimise and remove all those for loops like I explained
	var monthlyPublishedArticles = new Dictionary<string, int>();

	foreach (var article in articles)
	{
		var monthName = article.PublishedAt.ToString("MMMM");
		var name = $"{monthName} {article.PublishedAt.Year} {article.NewsSite}";
		if (monthlyPublishedArticles.ContainsKey(name))
		{
			monthlyPublishedArticles[name]++;
		}
		else
		{
			monthlyPublishedArticles.Add(name, 1);
		}
	}

	return monthlyPublishedArticles;
}

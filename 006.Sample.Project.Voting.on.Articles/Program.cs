using StackExchange.Redis;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;

ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379");
var db = redis.GetDatabase();

const string articlesKey = "Articles";
const string articleIdCounterKey = "ArticleIdCounterKey";
const string articleVoteKey = "ArticleVote";
const string articleKeyPrefix = "Article";
const string keySeparator = ":";

while (true)
{
    try
    {

        Console.Write("[1] Add article\n[2] Show all articles\n[3] Vote article\n? ");

        string command = Console.ReadLine();
        RunCommand(Enum.Parse<Command>(command));
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
    }
}


void RunCommand(Command command)
{
    if (command == Command.AddArticle)
    {
        AddArticle();
    }
    else if (command == Command.ShowAllArticles)
    {
        ShowAllArticles();
    }
    else if (command == Command.VoteArticle)
    {
        VoteUpArticle();
    }
}

void AddArticle()
{
    var articleId = db.StringGet(articleIdCounterKey);

    if (!articleId.HasValue)
    {
        articleId = 1;
        db.StringSet(articleIdCounterKey, articleId);
    }

    db.StringSet(articleIdCounterKey, Convert.ToInt32(articleId) + 1);

    Article article = new Article();
    
    Console.Write("Title: ");
    article.Title = Console.ReadLine();

    Console.Write("Text: ");
    article.Text = Console.ReadLine();

    Console.Write("Author: ");
    article.Author = Console.ReadLine();

    string name = $"{articleKeyPrefix}{keySeparator}{articleId}";
    string value = JsonSerializer.Serialize(article);

    db.HashSet(articlesKey, name, value);
}

void ShowAllArticles()
{
    var values = db.HashGetAll(articlesKey);

    foreach(var value in values)
    {
        Console.WriteLine($"{value.Name}> {value.Value}");
    }
}

void VoteUpArticle()
{
    Console.Write("Articleid? article:");
    string articleId = Console.ReadLine();

    string member = GetArticleKey(Convert.ToInt32(articleId));
    db.sortedset
    db.SortedSetAdd(articleVoteKey, new RedisValue()
}

string GetArticleKey(int articleId)=> $"{articleKeyPrefix}{keySeparator}{articleId}";

public class Article
{
    public string Title { get; set; }
    public string Text { get; set; }
    public string Author { get; set; }
}

public enum Command
{
    AddArticle = 1,
    ShowAllArticles = 2,
    VoteArticle = 3
}


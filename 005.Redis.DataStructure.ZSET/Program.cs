using StackExchange.Redis;

ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379");
var db = redis.GetDatabase();

string key = "mykey01";
db.KeyDelete(key);

db.SortedSetAdd(key, [new SortedSetEntry("member01", 11), 
    new SortedSetEntry("member02", 2), 
    new SortedSetEntry("member03", 18),
    new SortedSetEntry("member04", 20),
    new SortedSetEntry("member05", 6),
    new SortedSetEntry("member06", 6),
    new SortedSetEntry("member07", 8)
    ]);

//Range
var values = db.SortedSetRangeByScore(key);
Console.WriteLine($"Values: {string.Join(',', values)}");

var valuesWithScores = db.SortedSetRangeByScoreWithScores(key);
Console.WriteLine($"Values: {string.Join(',', valuesWithScores)}");

valuesWithScores = db.SortedSetRangeByScoreWithScores(key, 0, 10);
Console.WriteLine($"Values: {string.Join(',', valuesWithScores)}");

//Remove
db.SortedSetRemove(key, "member06");
valuesWithScores = db.SortedSetRangeByScoreWithScores(key);
Console.WriteLine($"Values: {string.Join(',', valuesWithScores)}");

Console.ReadLine();
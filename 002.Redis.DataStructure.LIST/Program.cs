using StackExchange.Redis;

ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379");

string key = "mykey01";

IDatabase db = redis.GetDatabase();
db.KeyDelete(key);

//Push
db.ListRightPush(key,["value03", "Value01", "Value02"]);

//Range
var values = db.ListRange(key, 0, -1);

Console.WriteLine($"Values: {string.Join(',', values)}");

//Index
var value = db.ListGetByIndex(key, 1);
Console.WriteLine(value);

Console.WriteLine("----");

//Pop
value = db.ListRightPop(key);
Console.WriteLine(value);

value = db.ListRightPop(key);
Console.WriteLine(value);

values = db.ListRange(key, 0, -1);

Console.WriteLine($"Values: {string.Join(',', values)}");


Console.ReadLine();
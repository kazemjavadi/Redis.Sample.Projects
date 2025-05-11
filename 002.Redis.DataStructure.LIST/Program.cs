using StackExchange.Redis;

ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379");

IDatabase db = redis.GetDatabase();

//Push
db.ListRightPush("MyKey01",["Value01", "Value02"]);

//Range
var values = db.ListRange("MyKey01", 0, -1);

Console.WriteLine($"Values: {string.Join(',', values)}");

//Index
var value = db.ListGetByIndex("MyKey01", 3);
Console.WriteLine(value);

Console.ReadLine();
using StackExchange.Redis;

ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379");

string key = "mykey01";

var db = redis.GetDatabase();
db.KeyDelete(key);

//Add
db.SetAdd(key, ["Value03", "Value01", "Value02", "Value01", "value01"]);

//Members
var values = db.SetMembers(key);
Console.WriteLine($"Values: {string.Join(',', values)}");

//Contains
Console.WriteLine(db.SetContains(key, "Value04"));
Console.WriteLine(db.SetContains(key, "Value01"));

db.SetRemove(key, "value01");
values = db.SetMembers(key);
Console.WriteLine($"Values: {string.Join(',', values)}");

Console.ReadLine();


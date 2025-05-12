using StackExchange.Redis;

ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379");
var db = redis.GetDatabase();

string key = "mykey01";

db.KeyDelete(key);

//Set
db.HashSet(key, [new HashEntry("name01", "value01"), new HashEntry("name02", 2)]);

//Get
var value = db.HashGet(key,"name01");
Console.WriteLine(value);

value = db.HashGet(key, "name01");
Console.WriteLine(value);

value = db.HashGet(key, "name02");
Console.WriteLine(value);

//Get all
var values = db.HashGetAll(key);
Console.WriteLine($"Values: {string.Join(',', values)}");

//Delete
db.HashDelete(key, "name01");
values = db.HashGetAll(key);
Console.WriteLine($"Values: {string.Join(',', values)}");

//Increment
db.HashIncrement(key, "name02", 1000);
values = db.HashGetAll(key);
Console.WriteLine($"Values: {string.Join(',', values)}");

//Decrement
db.HashDecrement(key, "name02", 2000);
values = db.HashGetAll(key);
Console.WriteLine($"Values: {string.Join(',', values)}");

Console.ReadLine();
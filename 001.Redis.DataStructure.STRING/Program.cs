using StackExchange.Redis;
using static System.Console;

var config = new ConfigurationOptions
{
    EndPoints = { "localhost:6379" },
    AbortOnConnectFail = false,
    ConnectTimeout = 5000,
    ConnectRetry = 3
};

ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(config);

IDatabase db = redis.GetDatabase();

//Set
db.StringSet("MyKey01", "MyValue01");

//Get
string stringValue = db.StringGet("MyKey01");

if (stringValue is null)
    WriteLine("Key does not exist!");
else
    WriteLine($"String value of key is: {stringValue}");

//Delete
db.StringGetDelete("MyKey01");

stringValue = db.StringGet("MyKey01");

if (stringValue is null)
    WriteLine("Key does not exist!");
else
    WriteLine($"String value of key is: {stringValue}");

ReadLine();
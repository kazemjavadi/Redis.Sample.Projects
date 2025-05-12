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

string key = "mykey01";

//Set
db.StringSet(key, "MyValue01");

//Get
string stringValue = db.StringGet(key);

if (stringValue is null)
    WriteLine("Key does not exist!");
else
    WriteLine($"String value of key is: {stringValue}");

//Delete
db.StringGetDelete(key);

stringValue = db.StringGet(key);

if (stringValue is null)
    WriteLine("Key does not exist!");
else
    WriteLine($"String value of key is: {stringValue}");

ReadLine();
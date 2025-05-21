using StackExchange.Redis;

ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379");
var db = redis.GetDatabase();

const string key1 = "list:key_01";
const string key2 = "list:key_02";

db.KeyDelete(key1);
db.KeyDelete(key2);

//RPUSH, LPUSH, RPOP, LPOP, LINDEX, LRANGE, LTRIM
db.ListLeftPush(key1, new RedisValue[] { "value01", "value02", "value03" });
ShowListKey01Items();

db.ListLeftPush(key1, "value00");
ShowListKey01Items();

db.ListRightPush(key1, "value04");
ShowListKey01Items();

var value = db.ListRightPop(key1);
Console.WriteLine(value);
ShowListKey01Items();

value = db.ListLeftPop(key1);
Console.WriteLine(value);
ShowListKey01Items();

value = db.ListGetByIndex(key1, 3);
Console.WriteLine(value.HasValue ? value : "null");

value = db.ListGetByIndex(key1, 1);
Console.WriteLine(value.HasValue ? value : "null");
ShowListKey01Items();

var values = db.ListRange(key1, 0, 1);
Console.WriteLine(string.Join(", ", values));
ShowListKey01Items();

for (int i = 5; i < 10; i++)
    db.ListRightPush(key1, $"value{i:d2}");
ShowListKey01Items();

db.ListTrim(key1, 0, 5);
ShowListKey01Items();

db.ListRightPopLeftPush(key1, key2);
ShowListKey01Items();
ShowListKey02Items();

Console.ReadLine();



void ShowListKey01Items() => ShowListItems(key1);
void ShowListKey02Items() => ShowListItems(key2);

void ShowListItems(string key)
{
    var values = db.ListRange(key, 0, -1);

    Console.WriteLine(string.Join(", ", values));
}
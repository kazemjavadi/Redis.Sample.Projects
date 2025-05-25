using StackExchange.Redis;

ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379");
var db = redis.GetDatabase();
const string key01 = "hash:01";
db.KeyDelete(key01);

//HMGET, HMSET, HDEL, HLEN, HEXISTS, HEKYS, HVALS, HGETALL, HINCRBY, HINCRBYFLOAT

//HMSET
List<HashEntry> hashEntries = new List<HashEntry>();
for (int i = 0; i <= 6; i++)
{
    if (i < 3)
        hashEntries.Add(new HashEntry($"key{i:d2}", $"name{i:d2}"));
    else
        hashEntries.Add(new HashEntry($"key{i:d2}", i));
}

db.HashSet(key01, hashEntries.ToArray());

//HMGET
Console.WriteLine(db.HashGet(key01, "key02"));


//HDEL
db.HashDelete(key01, "key02");
var result = db.HashGet(key01, "key02");
Console.WriteLine(result.HasValue ? result : "null");

//HLEN
Console.WriteLine($"Count of key-name paris in the hash: {db.HashLength(key01)}");

//HEXISTS
Console.WriteLine($"Is key03 exist: {db.HashExists(key01, "key03")}");
Console.WriteLine($"Is key02 exist: {db.HashExists(key01, "key02")}");

//HVALS
var vals = db.HashValues(key01);
Console.WriteLine(string.Join(", ", vals));

//HGETALL
var valsAll = db.HashGetAll(key01);
Console.WriteLine(string.Join(", ", valsAll));

//HINCRBY
db.HashIncrement(key01, "key03", 100);
valsAll = db.HashGetAll(key01);
Console.WriteLine(string.Join(", ", valsAll));


Console.ReadLine();
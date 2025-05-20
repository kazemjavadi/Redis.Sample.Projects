using StackExchange.Redis;

ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379");
var db = redis.GetDatabase();
const string key_integer_value = "key_integer_value";
db.StringSet(key_integer_value, 10);

//INC, DECR, INCBY, DECBY

Console.WriteLine(db.StringGet(key_integer_value));
db.StringIncrement(key_integer_value);
Console.WriteLine(db.StringGet(key_integer_value));
db.StringDecrement(key_integer_value);
Console.WriteLine(db.StringGet(key_integer_value));
db.StringIncrement(key_integer_value, 10);
Console.WriteLine(db.StringGet(key_integer_value));
db.StringDecrement(key_integer_value, 10);
Console.WriteLine(db.StringGet(key_integer_value));
db.StringIncrement(key_integer_value, -5);
Console.WriteLine(db.StringGet(key_integer_value));
db.StringDecrement(key_integer_value, -10);
Console.WriteLine(db.StringGet(key_integer_value));

PrintLine();

db.StringIncrement("not_exit_key", 10);
Console.WriteLine(db.StringGet("not_exit_key"));

try
{
    db.StringSet("empty_key", string.Empty);
    db.StringIncrement("empty_key", 20);
    Console.WriteLine(db.StringGet("empty_key"));
}
catch (Exception exc)
{
    Console.WriteLine(exc.ToString());
}

db.StringSet("empty_key_2", new RedisValue());
db.StringIncrement("empty_key_2", 20);
Console.WriteLine(db.StringGet("empty_key_2"));


PrintLongLine();

//APPEND, GETRANGE, SETRANGE, GETBIT, SETBIT, BITCOUNT, BITOP

const string key = "key";

db.StringSet(key, "Kazem");
db.StringAppend(key, " Javadi");
Console.WriteLine(db.StringGet(key));

Console.WriteLine(db.StringGetRange(key, 0, 4));

Console.WriteLine(db.StringGetBit(key, 10));

Console.WriteLine(db.StringSetBit(key, 0, !db.StringGetBit(key, 0)));
Console.WriteLine(db.StringGet(key));

Console.WriteLine(db.StringBitCount(key));
Console.WriteLine(db.StringBitCount(key, 0, -1));
Console.WriteLine(db.StringBitCount(key, 0, db.StringGet(key).Length() - 3));

db.StringSet("key_1", "kazem");
db.StringSet("key_2", "javadi");
db.StringBitOperation(Bitwise.Xor, "key_3", "key_1", "key_2");
Console.WriteLine($"key_3: {db.StringGet("key_3")}");
db.StringBitOperation(Bitwise.Xor, "key_3", "key_1", "key_3");
Console.WriteLine($"key_3: {db.StringGet("key_3")}");

PrintLongLine();

db.StringSet("key_4", "Kazem");
Console.WriteLine($"Number of true bits: {db.StringBitCount("key_4")}");
Console.WriteLine($"Number of total bits: {db.StringGet("key_4").Length() * 8}");

for (int i = 20; i <= 26; i++)
    Console.WriteLine($"{i}: {db.StringGetBit("key_4", i)}");

Console.WriteLine();

for (int i = 35; i <= 45; i++)
    Console.WriteLine($"{i}: {db.StringGetBit("key_4", i)}");

Console.WriteLine();

for (int i = 75; i <= 85; i++)
    Console.WriteLine($"{i}: {db.StringGetBit("key_4", i)}");

Console.ReadLine();



void PrintLine() => Console.WriteLine(new string('-', 20));
void PrintLongLine() => Console.WriteLine(new string('-', 40));




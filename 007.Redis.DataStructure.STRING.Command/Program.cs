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


Console.ReadLine();





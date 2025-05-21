using StackExchange.Redis;

ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379");
var db = redis.GetDatabase();

const string key01 = "set:key:01";
const string key02 = "set:key:02";
db.KeyDelete(key01);
db.KeyDelete(key02);

//SADD, SREM, SISMEMBER, SCARD, SMEMBERS, SRANDMEMBER, SPOP, SMOVE, SDIFF, SDIFFSTORE, SINTER, SINTERSTORE, SUNION, SUNIONSTORE

long numberOfMemeberInSet = db.SetAdd(key01, ["value01", "value03", "value02", "value04", "value06", "value05", "value08", "value09", "value10", "value11", "value12"]);
ShowNumberOfMembersInTheSet(numberOfMemeberInSet);
ShowSetMembersKey01();

bool isDeleted = db.SetRemove(key01, "VALUE");
Console.WriteLine(isDeleted);

isDeleted = db.SetRemove(key01, "value01");
Console.WriteLine(isDeleted);
ShowSetMembersKey01();

var setLength = db.SetLength(key01);
Console.WriteLine($"Set length is: {setLength}");

var setMembers = db.SetMembers(key01);
Console.WriteLine(string.Join(", ", setMembers));

Console.WriteLine();

for (int i = 0; i < 3; i++)
{
    setMembers = db.SetRandomMembers(key01, 2);
    Console.WriteLine(string.Join(", ", setMembers));
}

Console.WriteLine();

for (int i = 0; i < 3; i++)
{
    setMembers = db.SetRandomMembers(key01, -2);
    Console.WriteLine(string.Join(", ", setMembers));
}

Console.WriteLine();

for (int i = 0; i < 3; i++)
{
    var setMemeber = db.SetPop(key01);
    Console.WriteLine(setMemeber);
}
ShowSetMembersKey01();

Console.WriteLine();

db.SetMove(key01, key02, "value12");
Console.WriteLine("Key01 set members: ");
ShowSetMembersKey01();
Console.WriteLine("Key02 set members: ");
ShowSetMembersKey02();


Console.WriteLine();

db.


Console.ReadLine();

void ShowSetMembersKey01()=>ShowSetMembers(key01);
void ShowSetMembersKey02() => ShowSetMembers(key02);


void ShowSetMembers(string key)
{
    var values = db.SetMembers(key);
    Console.WriteLine(string.Join(", ", values));
}

void ShowNumberOfMembersInTheSet(long numberOfMemeberInSet) => Console.WriteLine($"Number of items in the set: {numberOfMemeberInSet}");
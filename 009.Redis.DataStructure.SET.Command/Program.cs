using StackExchange.Redis;

ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379");
var db = redis.GetDatabase();

const string key01 = "set:key:01";
const string key02 = "set:key:02";
const string key03 = "set:key:03";
const string key04 = "set:key:04";

db.KeyDelete(key01);
db.KeyDelete(key02);

//SADD, SREM, SISMEMBER, SCARD, SMEMBERS, SRANDMEMBER, SPOP, SMOVE, SDIFF, SDIFFSTORE, SINTER, SINTERSTORE, SUNION, SUNIONSTORE

long numberOfMemeberInSet = db.SetAdd(key01, ["value01", "value03", "value02", "value04", "value06", "value05", "value08", "value09", "value10", "value11", "value12"]);
ShowNumberOfMembersInTheSet(numberOfMemeberInSet);
ShowSetMembersKey01();

//SREM
bool isDeleted = db.SetRemove(key01, "VALUE");
Console.WriteLine(isDeleted);

isDeleted = db.SetRemove(key01, "value01");
Console.WriteLine(isDeleted);
ShowSetMembersKey01();

var setLength = db.SetLength(key01);
Console.WriteLine($"Set length is: {setLength}");

//SMEMEBERS
var setMembers = db.SetMembers(key01);
Console.WriteLine(string.Join(", ", setMembers));

Console.WriteLine();

//SRANDMEMBER
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

//SPOP
for (int i = 0; i < 3; i++)
{
    var setMemeber = db.SetPop(key01);
    Console.WriteLine(setMemeber);
}
ShowSetMembersKey01();

Console.WriteLine();

//SMOVE
db.SetMove(key01, key02, "value12");
Console.WriteLine("Key01 set members: ");
ShowSetMembersKey01();
Console.WriteLine("Key02 set members: ");
ShowSetMembersKey02();


Console.WriteLine();

//SDIFF
db.SetAdd(key03, ["v1", "v2", "v3"]);
db.SetAdd(key04, ["v1", "v4", "v5"]);
var setDiffMems1 = db.SetCombine(SetOperation.Difference, [key03, key04]);
Console.WriteLine("SETDIFF:");
Console.WriteLine(string.Join(", ", setDiffMems1));

//SDIFFSTORE
Console.WriteLine("SETDIFFSTORE:");
long setDiffCount2 = db.SetCombineAndStore(SetOperation.Difference, "set:key:05", [key03, key04]);
var setDiffMems2 = db.SetMembers("set:key:05");
Console.WriteLine(string.Join(", ", setDiffMems2));

//SINETER
Console.WriteLine("SINTER:");
var setInterMems1 = db.SetCombine(SetOperation.Intersect, [key03, key04]);
Console.WriteLine(string.Join(", ", setInterMems1));

//SINTERSTORE
Console.WriteLine("SINTERSTORE:");
long setInterCount2 = db.SetCombineAndStore(SetOperation.Intersect, "set:key:06", [key03, key04]);
var setInterMems2 = db.SetMembers("set:key:06");
Console.WriteLine(string.Join(", ", setInterMems2));

//SUNION
Console.WriteLine("SUNION:");
var setUnionMems1 = db.SetCombine(SetOperation.Union, [key03, key04]);
Console.WriteLine(string.Join(", ", setUnionMems1));

//SUNIONSTORE
Console.WriteLine("SUNIONSTORE:");
long setUnionCount2 = db.SetCombineAndStore(SetOperation.Union, "set:key:07", [key03, key04]);
var setUnionMems2 = db.SetMembers("set:key:07");
Console.WriteLine(string.Join(", ", setUnionMems2));

void ShowSetMembersKey01()=>ShowSetMembers(key01);
void ShowSetMembersKey02() => ShowSetMembers(key02);


void ShowSetMembers(string key)
{
    var values = db.SetMembers(key);
    Console.WriteLine(string.Join(", ", values));
}

void ShowNumberOfMembersInTheSet(long numberOfMemeberInSet) => Console.WriteLine($"Number of items in the set: {numberOfMemeberInSet}");
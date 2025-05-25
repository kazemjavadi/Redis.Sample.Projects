using StackExchange.Redis;

ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379");
var db = redis.GetDatabase();
const string key01 = "sortedset:01";
const int CountOfSortedSetSeedItems = 8;
db.KeyDelete(key01);

//ZADD, ZREM, ZCARD, ZINCRBY, ZCOUNT, ZRANK, ZSCORE, ZRANGE, ZREVRANK, ZREVRANGE, ZREVRANGEBYSCORE, ZREVRANGEBYRANK, ZREMRANGEBYSCORE, ZINTERSCORE, ZUNIONSCORE

//ZADD
for (int i = 0; i < CountOfSortedSetSeedItems; i++)
    db.SortedSetAdd(key01, [new SortedSetEntry($"name{i:d2}", new Random().Next(1, 5000))]);

ShowAllItems();

//ZREM
long countOfItemsInMySortedSet = db.SortedSetLength(key01);

for (int i = 0; i< CountOfSortedSetSeedItems; i++)
{
    if (i % 2 != 0)
        db.SortedSetRemove(key01, $"name{i:d2}");
}

ShowAllItems();

//ZCARD
countOfItemsInMySortedSet = db.SortedSetLength(key01);
Console.WriteLine($"Count of items in the sorted set: {countOfItemsInMySortedSet}");

//ZINCRBY
db.SortedSetIncrement(key01, "name02", 1_000_000.2);
ShowAllItems();

//ZCOUNT
long min = 1000;
long max = 2000;
long countOfItemsBetweenMinAndMaxScoreValue = db.SortedSetLength(key01, min, max);
Console.WriteLine($"Count of items between {min} and {max}: {countOfItemsBetweenMinAndMaxScoreValue}");

//ZRANK
string memberKey = "name02";
var rankOfMember = db.SortedSetRank(key01, memberKey);
Console.WriteLine($"Rank of member with the name of \"name02\": {rankOfMember}");

//ZSCORE
var scoreOfMember = db.SortedSetScore(key01, memberKey);
Console.WriteLine($"Score of the member with the name of \"name02\": {scoreOfMember}");

//ZREVRANK
var rankOfMemberReverse = db.SortedSetRank(key01, memberKey, Order.Descending);
Console.WriteLine($"Rank of member with the name of \"name02\" in reverse: {rankOfMemberReverse}");

//ZREVRANGE
ShowAllItemsReverse();

//ZRANGE
void ShowAllItems()
{
    var items = db.SortedSetRangeByRankWithScores(key01);
    Console.WriteLine(string.Join(", ", items));
}

void ShowAllItemsReverse()
{
    var items = db.SortedSetRangeByRankWithScores(key01, order: Order.Descending);
    Console.WriteLine(string.Join(", ", items));
}
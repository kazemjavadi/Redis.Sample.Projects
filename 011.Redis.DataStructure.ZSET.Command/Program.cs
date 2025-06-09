using StackExchange.Redis;

ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379");
var db = redis.GetDatabase();
const string key01 = "sortedset:01";
const int CountOfSortedSetSeedItems = 8;
db.KeyDelete(key01);

//ZADD, ZREM, ZCARD, ZINCRBY, ZCOUNT, ZRANK, ZSCORE, ZRANGE, ZREVRANK, ZREVRANGE, ZREVRANGEBYSCORE, ZREVRANGEBYRANK, ZREMRANGEBYSCORE, ZINTERSCORE, ZUNIONSCORE

//ZADD
Console.WriteLine("ZADD>");
for (int i = 0; i < CountOfSortedSetSeedItems; i++)
    db.SortedSetAdd(key01, [new SortedSetEntry($"name{i:d2}", new Random().Next(1, 5000))]);

ShowKey01AllItems();

//ZREM
Console.WriteLine("ZREM>");
long countOfItemsInMySortedSet = db.SortedSetLength(key01);

for (int i = 0; i < CountOfSortedSetSeedItems; i++)
{
    if (i % 2 != 0)
        db.SortedSetRemove(key01, $"name{i:d2}");
}

ShowKey01AllItems();

//ZCARD
Console.WriteLine("ZCARD>");
countOfItemsInMySortedSet = db.SortedSetLength(key01);
Console.WriteLine($"Count of items in the sorted set: {countOfItemsInMySortedSet}");

//ZINCRBY
Console.WriteLine("ZINCRBY>");
db.SortedSetIncrement(key01, "name02", 1_000_000.2);
ShowKey01AllItems();

//ZCOUNT
Console.WriteLine("ZCOUNT>");
long min = 1000;
long max = 2000;
long countOfItemsBetweenMinAndMaxScoreValue = db.SortedSetLength(key01, min, max);
Console.WriteLine($"Count of items between {min} and {max}: {countOfItemsBetweenMinAndMaxScoreValue}");

//ZRANK
Console.WriteLine("ZRANK>");
string memberKey = "name02";
var rankOfMember = db.SortedSetRank(key01, memberKey);
Console.WriteLine($"Rank of member with the name of \"name02\": {rankOfMember}");

//ZSCORE
Console.WriteLine("ZSCORE>");
var scoreOfMember = db.SortedSetScore(key01, memberKey);
Console.WriteLine($"Score of the member with the name of \"name02\": {scoreOfMember}");

//ZREVRANK
Console.WriteLine("ZREVRANK>");
var rankOfMemberReverse = db.SortedSetRank(key01, memberKey, Order.Descending);
Console.WriteLine($"Rank of member with the name of \"name02\" in reverse: {rankOfMemberReverse}");

//ZRANGE
Console.WriteLine("ZRANGE>");
ShowKey01AllItems();

//ZREVRANGE
Console.WriteLine("ZREVRANGE>");
ShowAllItemsReverse();

//ZRANGEBYSCORE
Console.WriteLine("ZRANGEBYSCORE>");
long min2 = 0;
long max2 = 3000;
var rangeScoreBetweenMinAndMax = db.SortedSetRangeByScore(key01, min2, max2);
Console.WriteLine(string.Join(", ", rangeScoreBetweenMinAndMax));

//ZREVRANGEBYSCORE
Console.WriteLine("ZREVRANGEBYSCORE>");
long min3 = 0;
long max3 = 1500;
var rangeScoreReverseBetweenMinAndMax = db.SortedSetRangeByScore(key01, min3, max3, order: Order.Descending);
Console.WriteLine(string.Join(", ", rangeScoreReverseBetweenMinAndMax));

//ZREMRANGEBYRANK
Console.WriteLine("ZREMRANGEBYRANK>");
long minRank1 = 0;
long maxRank1 = 1;
ShowKey01AllItems();
long countOfRemovedItems1 = db.SortedSetRemoveRangeByRank(key01, minRank1, maxRank1);
Console.WriteLine($"Count of removed items: {countOfRemovedItems1}");
ShowKey01AllItems();

//ZREMRANGEBYSCORE
Console.WriteLine("ZREMRANGEBYSCORE>");
long minScore1 = 0;
long maxScore2 = 5000;
ShowKey01AllItems();
long countOfRemovedItems2 = db.SortedSetRemoveRangeByScore(key01, minScore1, maxScore2);
ShowKey01AllItems();

//ZADD
Console.WriteLine("ZADD>");
const string key02 = "sortedset:02";
db.SortedSetAdd(key02, [new SortedSetEntry("name02", -1_000_000.2)]);
db.SortedSetAdd(key02, [new SortedSetEntry("name08", 5)]);
ShowKey02AllItems();
ShowKey01AllItems();

//ZINTERSTORE
const string key03 = "sortedset:03";
Console.WriteLine("ZINTERSTORE>");
db.SortedSetCombineAndStore(SetOperation.Intersect, key03, key01, key02, Aggregate.Sum);
ShowKey03AllItems();

void ShowKey01AllItems() => ShowAllItems(key01);
void ShowKey02AllItems() => ShowAllItems(key02);
void ShowKey03AllItems() => ShowAllItems(key03);

//ZUNIONSTORE
Console.WriteLine("ZUNIONSTORE>");
var unionSumAggregateResult = db.SortedSetCombine(SetOperation.Union, [key02, key03, key01], aggregate: Aggregate.Sum);
Console.WriteLine(string.Join(", ", unionSumAggregateResult));

var unionMaxAggregateResult = db.SortedSetCombine(SetOperation.Union, [key02, key03, key01], aggregate: Aggregate.Max);
Console.WriteLine(string.Join(", ", unionMaxAggregateResult));

var unionMinAggregateResult = db.SortedSetCombine(SetOperation.Union, [key02, key03, key01], aggregate: Aggregate.Min);
Console.WriteLine(string.Join(", ", unionMinAggregateResult));

void ShowAllItems(string key)
{
    var items = db.SortedSetRangeByRankWithScores(key);
    Console.WriteLine(string.Join(", ", items));
}

void ShowAllItemsReverse()
{
    var items = db.SortedSetRangeByRankWithScores(key01, order: Order.Descending);
    Console.WriteLine(string.Join(", ", items));
}
using Microsoft.AspNetCore.Identity;
using System;
using Wam.Core.Helpers;
using Wam.Models.Score;

namespace Wam.Models.UnitTests;

[TestClass]
public class SerializationTests
{
    private readonly Action<string> log = Console.WriteLine;
    private readonly NewtonsoftSerializationHelper jsonSerialization = new NewtonsoftSerializationHelper();
    private readonly SystemTextSerializationHelper systemSerialization = new SystemTextSerializationHelper();

    [TestMethod]
    public void TestUserScoreSerialization()
    {
        var datetime = new DateTime(2021, 1, 1, 1, 1, 1, DateTimeKind.Utc);
        var userScore = new UserScore("1", datetime, "justin.time@myserver.com", 3);

        TestUserScoreSerialization( jsonSerialization, userScore);
        TestUserScoreSerialization(systemSerialization, userScore);
    }

    private void TestUserScoreSerialization(ISerializationHelper serializer, UserScore userScore) {
        var json = serializer.Serialize(userScore);
        var deserialized = serializer.Deserialize<UserScore>(json);

        log.Invoke(json);
        Assert.IsNotNull(deserialized);
        Assert.AreEqual(userScore, deserialized);
        Assert.AreEqual(userScore.Id, deserialized.Id);
        Assert.AreEqual(userScore.TimeStamp, deserialized.TimeStamp);
        Assert.AreEqual(userScore.UserId, deserialized.UserId);
        Assert.AreEqual(userScore.ScoringValue, deserialized.ScoringValue);

    }
}
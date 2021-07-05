using System.Text.Json;
using NUnit.Framework;

namespace CaptainSkuEngineUnitTests
{
    public class Helpers
    {
        public static void AssertJsonEqual(object expected, object actual)
        {
            var jsonExpected = JsonSerializer.Serialize(expected);
            var jsonActual = JsonSerializer.Serialize(actual);

            Assert.AreEqual(jsonExpected, jsonActual);
        }
    }
}
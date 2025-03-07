using NUnit.Framework;
using TestProject2.Instances;

namespace TestProject2.Data
{
    public class SimplePracticeData
    {
        public static IEnumerable<TestCaseData> ValidUser()
        {
            yield return new TestCaseData(UserInstances.ValidUser, ClientInstances.ValidClient)
                .SetName("SimpleTest")
                .SetDescription("This is the test for Simple Practice interview.");
        }
    }
}

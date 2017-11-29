using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RoadStatus.Test
{
    [TestClass]
    public class RoadStatusMain
    {
        [TestMethod]
        public void Main_Always_ReturnInt()
        {
            var result = Program.Main(null);
            Assert.IsInstanceOfType(result, typeof(int));
        }

        [TestMethod]
        public void Main_IfSuccess_ReturnZero()
        {
            int result = Program.Main(new string[]{ "A2"});
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Main_IfException_ReturnOne()
        {
            int result = Program.Main(null);
            Assert.AreEqual(1, result);
        }
    }
}

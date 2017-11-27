using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RoadStatus.Test
{
    [TestClass]
    public class RoadStatusMain
    {
        [TestMethod]
        public void MainShouldReturnInt()
        {
            var result = Program.Main(null);
            Assert.IsInstanceOfType(result, typeof(int));
        }

        [TestMethod]
        public void MainShouldReturnZeroIfSuccess()
        {
            int result = Program.Main(new string[]{ "A2"});
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void MainShouldReturnOneIfException()
        {
            int result = Program.Main(null);
            Assert.AreEqual(1, result);
        }
    }
}

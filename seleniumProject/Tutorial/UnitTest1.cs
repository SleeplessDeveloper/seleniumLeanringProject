namespace seleniumProject.Tutorial
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            TestContext.Progress.WriteLine("Setup method execution");
        }

        [Test]
        public void Test1()
        {
            TestContext.Progress.WriteLine("test 1");
        }
        [Test]
        public void Test2()
        {
            TestContext.Progress.WriteLine("test 2");
        }
        [TearDown]
        public void CloseBrowser()
        {
            TestContext.Progress.WriteLine("Tear down method");
        }
    }
}
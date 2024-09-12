namespace XUnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Assert.Equal(2, 2);
        }

        [Fact]
        public void Test2()
        {
            Assert.NotEmpty("one");
        }

        [Fact]
        public void Test3()
        {
            Assert.NotEqual("one", "two");
        }

        [Fact]
        public void Test4()
        {
            Assert.True(true);
        }

        [Fact]
        public void Test5()
        {
            Assert.False(false);
        }


    }
}
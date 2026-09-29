using Xunit;
using System;
using Sortowanie_Przez_Wstawianie;

namespace Sortowanie_Przez_Wstawianie_Testy
{
    public class SortowanieTests
    {
        private readonly Sortowanie _sorter = new Sortowanie();

        [Fact]
        public void Test1()
        {
            int[] input = { 4, 2, 1, 3 };
            int[] expected = { 1, 2, 3, 4 };
            Assert.Equal(expected, _sorter.Sort(input));
        }

        [Fact]
        public void Test2()
        {
            int[] input = { 1, 2, 3 };
            int[] expected = { 1, 2, 3 };
            Assert.Equal(expected, _sorter.Sort(input));
        }
	[Fact]
        public void Test3()
        {
            int[] input = Array.Empty<int>();
            int[] expected = Array.Empty<int>();
            Assert.Equal(expected, _sorter.Sort(input));
        }
	[Fact]
	public void Test4()
	{
    	    Assert.Throws<ArgumentNullException>(() => _sorter.Sort(null));
	}

    }
}

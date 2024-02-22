using Xunit;

namespace FestivalApp.BL.Tests
{
    public class AreEqualMethod
    {
        public void Equals<T>(params T[] items)
        {
            for (int i = 1; i < items.Length; i++)
            {
                Assert.Equal(items[0], items[i]);
            }
        }
    }
}

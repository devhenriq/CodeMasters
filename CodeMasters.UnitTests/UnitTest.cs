namespace CodeMasters.UnitTests
{
    public abstract class UnitTest
    {
        protected readonly Faker _faker;
        protected UnitTest()
        {
            _faker = new Faker();
        }
    }
}

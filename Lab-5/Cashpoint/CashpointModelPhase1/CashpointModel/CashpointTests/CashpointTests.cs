namespace CashpointModel.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class CashpointTests
    {
        [Test]
        public void AddBanknote_SingleBanknote_ShouldIncrementTotal()
        {
            var cashpoint = new Cashpoint();
            cashpoint.AddBanknote(5);

            Assert.That(
                cashpoint.Total,
                Is.EqualTo(5u),
                "Добавление единственной банкноты не было произведено");
        }

        [Test]
        public void AddBanknote_MultipleBanknotes_ShouldIncrementTotal()
        {
            var cashpoint = new Cashpoint();

            cashpoint.AddBanknote(5);
            cashpoint.AddBanknote(10);

            Assert.That(
                cashpoint.Total,
                Is.EqualTo(15u),
                "Добавление второй банкноты не было произведено");
        }

        [Test]
        public void RemoveBanknote_CashpointIsEmpty_ShouldPreserveTotal()
        {
            var cashpoint = new Cashpoint();

            cashpoint.RemoveBanknote(5);

            Assert.That(
                cashpoint.Total,
                Is.EqualTo(0),
                "Извлечена несуществующая банкнота из пустого банкомата");
        }

        [Test]
        public void RemoveBanknote_UnknownBanknote_ShouldPreserveTotal()
        {
            var cashpoint = new Cashpoint();

            cashpoint.AddBanknote(7);
            cashpoint.RemoveBanknote(5);

            Assert.That(
                cashpoint.Total,
                Is.EqualTo(7),
                "Извлечена несуществующая купюра");
        }

        [Test]
        public void RemoveBanknote_ExistingBanknote_ShouldDecrementTotal()
        {
            var cashpoint = new Cashpoint();

            cashpoint.AddBanknote(5);
            cashpoint.AddBanknote(10);
            cashpoint.RemoveBanknote(5);

            Assert.That(
                cashpoint.Total,
                Is.EqualTo(10),
                "Купюра извлечена некорректно");
        }

        [Test]
        public void CanGrant_SumIsZero_ShouldGrant()
        {
            var cashpoint = new Cashpoint();

            Assert.That(
                cashpoint.CanGrant(0),
                "Банкомат не смог выдать 0");

            cashpoint.AddBanknote(5);

            Assert.That(
                cashpoint.CanGrant(0),
                "Банкомат не смог выдать 0 после добавления купюры");
        }

        [Test]
        public void CanGrant_SumEqualsToSingleBanknote_ShouldGrant()
        {
            var cashpoint = new Cashpoint();

            cashpoint.AddBanknote(5);

            Assert.That(
                cashpoint.CanGrant(5),
                "Банкомат не смог выдать единственную банкноту");
        }

        [Test]
        public void CanGrant_SumNotEqualToSingleBanknote_ShouldNotGrant()
        {
            var cashpoint = new Cashpoint();

            cashpoint.AddBanknote(5);

            Assert.That(
                cashpoint.CanGrant(4),
                Is.False,
                "Банкомат смог выдать значение меньше номинала единственной купюры");

            Assert.That(
                cashpoint.CanGrant(6),
                Is.False,
                "Банкомат смог выдать значение больше номинала единственной купюры");
        }

        [Test]
        public void CanGrant_SumEqualsToBanknotesTotal_ShouldGrant()
        {
            var cashpoint = new Cashpoint();

            cashpoint.AddBanknote(5);
            cashpoint.AddBanknote(3);

            Assert.That(
                cashpoint.CanGrant(8),
                "Банкомат не смог выдать сумму двух купюр");
        }

        [Test]
        public void CanGrant_MultipleBanknotesIntermediateValues_ShouldNotGrant()
        {
            var cashpoint = new Cashpoint();

            cashpoint.AddBanknote(5);
            cashpoint.AddBanknote(3);

            Assert.That(
                cashpoint.CanGrant(6),
                Is.False,
                "Банкомат смог выдать значение между номиналами купюр");
        }

        [Test]
        public void AddBanknote_CanAddOrRemoveMultipleTimes()
        {
            var cashpoint = new Cashpoint();

            cashpoint.AddBanknote(50, 50);

            Assert.That(
                cashpoint.Total == 2500,
                "Банкомат не смог принять все купюры");

            cashpoint.RemoveBanknote(50, 50);

            Assert.That(
                cashpoint.Total == 0,
                "Банкомат не смог извлечь все купюры");
        }

        [Test]
        public void CanGrant_AfterAddingOrRemovingMultipleBanknotes()
        {
            var cashpoint = new Cashpoint();

            cashpoint.AddBanknote(50, 30);

            Assert.That(
                cashpoint.CanGrant(1400),
                "Банкомат не смог выдать банкноты");

            cashpoint.RemoveBanknote(50, 20);

            Assert.That(
                cashpoint.CanGrant(1600),
                Is.False,
                "Банкомат выдал значение больше, чем в нём есть");
        }

        [Test]
        public void CanGrant_AfterRemovingBanknotes()
        {
            var cashpoint = new Cashpoint();

            cashpoint.AddBanknote(5, 2);
            cashpoint.AddBanknote(3);
            cashpoint.RemoveBanknote(3);

            Assert.That(
                cashpoint.CanGrant(3),
                Is.False,
                "Банкомат смог выдать несуществующую купюру");
        }

        [Test]
        [Ignore("Результат теста зависит от мощности машины")]
        [Timeout(20000)]
        public void CanGrant_PerformanceTest()
        {
            var cashpoint = new Cashpoint();

            cashpoint.AddBanknote(10, 100);

            Assert.That(
                cashpoint.CanGrant(1000),
                "Банкомат не смог выдать банкноты");
        }
    }
}
namespace CashpointModel.Tests
{
    using System.Linq;
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
                "���������� ������������ �������� �� ���� �����������");
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
                "���������� ������ �������� �� ���� �����������");
        }

        [Test]
        public void RemoveBanknote_CashpointIsEmpty_ShouldPreserveTotal()
        {
            var cashpoint = new Cashpoint();

            cashpoint.RemoveBanknote(5);

            Assert.That(
                cashpoint.Total,
                Is.EqualTo(0),
                "��������� �������������� �������� �� ������� ���������");
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
                "��������� �������������� ������");
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
                "������ ��������� �����������");
        }

        [Test]
        public void CanGrant_SumIsZero_ShouldGrant()
        {
            var cashpoint = new Cashpoint();

            Assert.That(
                cashpoint.CanGrant(0),
                "�������� �� ���� ������ 0");

            cashpoint.AddBanknote(5);

            Assert.That(
                cashpoint.CanGrant(0),
                "�������� �� ���� ������ 0 ����� ���������� ������");
        }

        [Test]
        public void CanGrant_SumEqualsToSingleBanknote_ShouldGrant()
        {
            var cashpoint = new Cashpoint();

            cashpoint.AddBanknote(5);

            Assert.That(
                cashpoint.CanGrant(5),
                "�������� �� ���� ������ ������������ ��������");
        }

        [Test]
        public void CanGrant_SumNotEqualToSingleBanknote_ShouldNotGrant()
        {
            var cashpoint = new Cashpoint();

            cashpoint.AddBanknote(5);

            Assert.That(
                cashpoint.CanGrant(4),
                Is.False,
                "�������� ���� ������ �������� ������ �������� ������������ ������");

            Assert.That(
                cashpoint.CanGrant(6),
                Is.False,
                "�������� ���� ������ �������� ������ �������� ������������ ������");
        }

        [Test]
        public void CanGrant_SumEqualsToBanknotesTotal_ShouldGrant()
        {
            var cashpoint = new Cashpoint();

            cashpoint.AddBanknote(5);
            cashpoint.AddBanknote(3);

            Assert.That(
                cashpoint.CanGrant(8),
                "�������� �� ���� ������ ����� ���� �����");
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
                "�������� ���� ������ �������� ����� ���������� �����");
        }

        [Test]
        public void AddBanknote_CanAddOrRemoveMultipleTimes()
        {
            var cashpoint = new Cashpoint();

            cashpoint.AddBanknote(50, 50);

            Assert.That(
                cashpoint.Total,
                Is.EqualTo(2500),
                "�������� �� ���� ������� ��� ������");

            cashpoint.RemoveBanknote(50, 50);

            Assert.That(
                cashpoint.Total,
                Is.EqualTo(0),
                "�������� �� ���� ������� ��� ������");
        }

        [Test]
        public void CanGrant_AfterAddingOrRemovingMultipleBanknotes()
        {
            var cashpoint = new Cashpoint();

            cashpoint.AddBanknote(50, 30);

            Assert.That(
                cashpoint.CanGrant(1400),
                "�������� �� ���� ������ ��������");

            cashpoint.RemoveBanknote(50, 20);

            Assert.That(
                cashpoint.CanGrant(1600),
                Is.False,
                "�������� ����� �������� ������, ��� � �� ����");
        }

        [Test]
        public void AddBanknotes_RestrictionsOnNumberOfDenominations()
        {
            var cashpoint = new Cashpoint();

            cashpoint.AddBanknote(10, 255);
            cashpoint.AddBanknote(10, 5);

            Assert.That(
                cashpoint.Total,
                Is.EqualTo(2550),
                "�������� ���� ������� ������� ������ �������� ������ �����������");
        }

        [Test]
        public void RemoveBanknotes_CanRemoveBanknotesWithTheSameDenomination()
        {
            var cashpoint = new Cashpoint();

            cashpoint.AddBanknote(50, 200);

            cashpoint.RemoveBanknote(50, 150);

            Assert.That(
                cashpoint.Total,
                Is.EqualTo(2500),
                "�������� �� ���� ��������� ������� ������");
        }

        [Test]
        public void CanGrant_GrantAfterRemoving_ShouldNotGrant()
        {
            var cashpoint = new Cashpoint();

            cashpoint.AddBanknote(5);
            cashpoint.AddBanknote(3);
            cashpoint.RemoveBanknote(3);

            Assert.That(
                cashpoint.CanGrant(8),
                Is.False,
                "�������� ����� �����, ���� �� ����� ������� ������������");
        }

        [Test]
        public void CanGrant_GrantAfterRemovingMultipleTimes_ShouldGrant()
        {
            var cashpoint = new Cashpoint();

            cashpoint.AddBanknote(5, 5);
            cashpoint.AddBanknote(3);
            cashpoint.RemoveBanknote(5, 3);

            Assert.That(
                cashpoint.CanGrant(13),
                Is.True,
                "�������� �� ���� ������ ����� ������ �����");
        }

        [Test]
        [Ignore("��������� ����� ������� �� �������� ������")]
        [Timeout(20000)]
        public void CanGrant_PerformanceTest()
        {
            var cashpoint = new Cashpoint();

            cashpoint.AddBanknote(10, 100);

            Assert.That(
                cashpoint.CanGrant(1000),
                "�������� �� ���� ������ ��������");
        }

        [Test]
        public void Regression1()
        {
            var cashpoint = new Cashpoint();

            cashpoint.AddBanknote(100);

            cashpoint.AddBanknote(5, 3);
            cashpoint.RemoveBanknote(5, 2);

            Assert.That(cashpoint.CanGrant(5), Is.True);

            cashpoint.RemoveBanknote(5);

            Assert.That(cashpoint.CanGrant(5), Is.False);
        }

        [Test]
        public void Regression2()
        {
            var cashpoint = new Cashpoint();

            cashpoint.AddBanknote(100);

            cashpoint.AddBanknote(5, 2);
            cashpoint.RemoveBanknote(5, 2);

            Assert.That(cashpoint.CanGrant(5), Is.False);
        }

        [Test]
        public void Regression3()
        {
            var cashpoint = new Cashpoint();

            cashpoint.AddBanknote(100);

            cashpoint.AddBanknote(5, 4);
            cashpoint.RemoveBanknote(5, 2);

            Assert.That(cashpoint.CanGrant(5), Is.True);
            Assert.That(cashpoint.CanGrant(10), Is.True);
            Assert.That(cashpoint.CanGrant(15), Is.False);
            Assert.That(cashpoint.CanGrant(20), Is.False);
        }

        [Test]
        public void Regression5()
        {
            var cashpoint = new Cashpoint();

            cashpoint.AddBanknote(5, 2);
            cashpoint.AddBanknote(2, 2);
            cashpoint.AddBanknote(3, 2);

            cashpoint.RemoveBanknote(5);
            cashpoint.RemoveBanknote(2);
            cashpoint.RemoveBanknote(3);

            AssertCanGrantOnly(cashpoint, 5, 7, 8, 10);
        }

        private void AssertCanGrantOnly(Cashpoint cashpoint, params int[] exactValues)
        {
            for (int i = exactValues.Min(); i <= exactValues.Max(); i++)
            {
                bool expectedCanGrant = exactValues.Contains(i);

                Assert.That(cashpoint.CanGrant((uint)i), Is.EqualTo(expectedCanGrant), "CanGrant " + i);
            }
        }
    }
}
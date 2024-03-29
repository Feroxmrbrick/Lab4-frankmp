using System;
using NUnit.Framework;
using Expedia;
using Rhino.Mocks;

namespace ExpediaTest
{
	[TestFixture()]
	public class CarTest
	{	
		private Car targetCar;
		private MockRepository mocks;
		
		[SetUp()]
		public void SetUp()
		{
			targetCar = new Car(5);
			mocks = new MockRepository();
		}
		
		[Test()]
		public void TestThatCarInitializes()
		{
			Assert.IsNotNull(targetCar);
		}	
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForFiveDays()
		{
			Assert.AreEqual(50, targetCar.getBasePrice()	);
		}
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForTenDays()
		{
            var target = new Car(10);
			Assert.AreEqual(80, target.getBasePrice());	
		}
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForSevenDays()
		{
			var target = new Car(7);
			Assert.AreEqual(10*7*.8, target.getBasePrice());
		}
		
		[Test()]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestThatCarThrowsOnBadLength()
		{
			new Car(-5);
		}

        [Test()]
        public void TestThatCarCanGetLocation()
        {
            IDatabase mockDatabase = mocks.Stub<IDatabase>();

            var target = new Car(10);
            target.Database = mockDatabase;
            String carLoc = target.getCarLocation(1);
            Assert.AreEqual(carLoc, mockDatabase.getCarLocation(1));
        }

        [Test()]
        public void TestThatCarMileageWorks()
        {
            IDatabase mockDatabase = mocks.Stub<IDatabase>();
            mockDatabase.Miles = 15;

            var target = ObjectMother.BMW();
            target.Database = mockDatabase;
            int mileCount = target.Mileage;
            Assert.AreEqual(mileCount, 15);
        }
	}
}

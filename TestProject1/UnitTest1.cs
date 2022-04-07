using System;
using TROMP;
using Xunit;

namespace TestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            AirPlanCourse.Forward(4);
            AirPlanCourse.UP(2);
            AirPlanCourse.Forward(8);

            Assert.Equal(192, AirPlanCourse.Result());
        }
    }
}

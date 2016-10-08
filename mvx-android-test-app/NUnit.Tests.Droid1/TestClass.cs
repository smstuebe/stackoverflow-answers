using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Platform;
using MyApp.Core;

namespace NUnit.Tests.Droid1
{
    [TestFixture]
    public class TestClass
    {
        [Test]
        public void TestMethod()
        {
            var service = Mvx.Resolve<IDataStorageService>();
            Assert.IsNull(service.UserData);
        }
    }
}

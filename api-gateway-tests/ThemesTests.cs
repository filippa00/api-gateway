using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_gateway_tests
{
    public class ThemesTests : BaseTestClass
    {
        [SetUp]
        public void AdminLogin()
        {
            _adminToken = GetAdminToken();
        }

        //[Test]
        //private async Task GetThemes()
        //{

        //}
    }
}

using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using P2PVpn.Utilities;

namespace P2PVpn.Tests
{
    [TestClass]
    public class VPNGateTest
    {
        [TestMethod]
        public void GetServersTest()
        {
            VPNGate.ServersCSV = Path.GetFullPath(Directory.GetCurrentDirectory() + @"\Assets\VPNGateServers.csv");

            var fastestServers = VPNGate.GetFastestServers();

            Assert.IsTrue(fastestServers.Count > 0);

            VPNGate.SelectServer(fastestServers[0]);

            //Assert.IsTrue(servers.Count > 0);
        }
    }
}

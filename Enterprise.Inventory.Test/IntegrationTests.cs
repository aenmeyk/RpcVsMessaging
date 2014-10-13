using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enterprise.Inventory.Test
{
    [TestClass]
    public class IntegrationTests
    {
        [TestMethod]
        public void RequestStockReplenishmentTest()
        {
            var stockManager = new StockManager();

            // Test that no exception is thrown.
            stockManager.RequestStockReplenishment(itemCode: Guid.NewGuid().ToString());
        }
    }
}

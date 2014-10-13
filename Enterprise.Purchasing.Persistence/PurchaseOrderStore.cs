using System.IO;
using System.Threading;

namespace Enterprise.Purchasing.Persistence
{
    public class PurchaseOrderStore
    {
        private static readonly object FileLock = new object();
        private const string FilePath = @"C:\Temp\PurchaseOrders.txt";

        public void SubmitPurchaseOrder(string itemCode)
        {
            // Simulate a long running, processor and memory intensive operation
            var bigString = new string('a', 120000000);
            Thread.Sleep(500);

            lock (FileLock)
            {
                using (var file = File.AppendText(FilePath))
                {
                    file.WriteLine("Purchase order for item: " + itemCode);
                    file.Close();
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Core
{
    public class APIThanhToan
    {
        public class WebhookRequest
        {
            public int Error { get; set; }
            public List<TransactionData> Data { get; set; }
        }

        public class TransactionData
        {
            public int Id { get; set; }
            public string Tid { get; set; }
            public string Description { get; set; }
            public int Amount { get; set; }
            public int Cusum_Balance { get; set; }
            public string When { get; set; }
            public string Bank_Sub_Acc_Id { get; set; }
            public string SubAccId { get; set; }
            public string BankName { get; set; }
            public string BankAbbreviation { get; set; }
            public string VirtualAccount { get; set; }
            public string VirtualAccountName { get; set; }
            public string CorresponsiveName { get; set; }
            public string CorresponsiveAccount { get; set; }
            public string CorresponsiveBankId { get; set; }
            public string CorresponsiveBankName { get; set; }
        }
    }
}

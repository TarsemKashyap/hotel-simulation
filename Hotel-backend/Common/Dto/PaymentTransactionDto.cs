using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto
{
    public class PaymentTransactionDto
    {
        public string PayerID { get; set; }
        public string St { get; set; }
        public string Tx { get; set; }
        public string Cc { get; set; }
        public string Amount { get; set; }
        public string Cm { get; set; }
        public string Payer_email { get; set; }
        public string Payer_id { get; set; }
        public string Payer_status { get; set; }
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public string Address_name { get; set; }
        public string Address_street { get; set; }
        public string Address_city { get; set; }
        public string Address_state { get; set; }
        public string Address_country_code { get; set; }
        public string Residence_country { get; set; }
        public string Txn_id { get; set; }
        public string Custom { get; set; }
        public string Handling_amount { get; set; }
        public string Mc_currency { get; set; }
        public string Mc_fee { get; set; }
        public string Mc_gross { get; set; }
        public string Notify_version { get; set; }
        public string Payment_date { get; set; }
        public string Payment_fee { get; set; }
        public int Payment_gross { get; set; }
        public PaymentStatus Payment_status { get; set; }
        public string Payment_type { get; set; }
        public string Protection_eligibility { get; set; }
        public string Quantity { get; set; }
        public string Receiver_id { get; set; }
        public string Shipping { get; set; }
        public string Txn_type { get; set; }
        public string Verify_sign { get; set; }
        public string Address_zip { get; set; }
        public string RawTransactionResponse { get; set; }

    }
}

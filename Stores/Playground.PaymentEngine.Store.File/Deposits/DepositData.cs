using System.Collections.Generic;
using System.Xml.Serialization;
using Playground.PaymentEngine.Store.Deposits.Model;

namespace Playground.PaymentEngine.Store.File.Deposits {
    [XmlRoot("repository")]
    public class DepositData {
        [XmlArray("deposits")]
        [XmlArrayItem("deposit")]
        public List<Deposit> Deposits { get; set; } = new();
    }
}
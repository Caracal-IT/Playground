using System.Collections.Generic;
using System.Xml.Serialization;
using Playground.PaymentEngine.Stores.Withdrawals.Model;

namespace Playground.PaymentEngine.Stores.Withdrawals.File {
	[XmlRoot(ElementName="repository")]
	public class WithdrawalData {
		[XmlArray("withdrawals")]
		[XmlArrayItem("withdrawal")]
		public List<Withdrawal> Withdrawals { get; set; }
		
		[XmlArray("withdrawal-statuses")]
		[XmlArrayItem("withdrawal-status")]
		public List<WithdrawalStatus> WithdrawalStatuses { get; set; }
		
		[XmlArray("withdrawal-groups")]
		[XmlArrayItem("withdrawal-group")]
		public List<WithdrawalGroup> WithdrawalGroups { get; set; }
	}
}
using System.Collections.Generic;
using System.Xml.Serialization;
using Playground.PaymentEngine.Stores.CustomerStores.Model;

namespace Playground.PaymentEngine.Model {
	[XmlRoot(ElementName="store")]
	public class Store {
		[XmlArray("withdrawals")]
		[XmlArrayItem("withdrawal")]
		public List<Withdrawal> Withdrawals { get; set; }
		
		[XmlArray("withdrawal-statuses")]
		[XmlArrayItem("withdrawal-status")]
		public List<WithdrawalStatus> WithdrawalStatuses { get; set; }
		
		[XmlArray("withdrawal-groups")]
		[XmlArrayItem("withdrawal-group")]
		public List<WithdrawalGroup> WithdrawalGroups { get; set; }
		
		[XmlArray("rule-histories")]
		[XmlArrayItem("rule-history")]
		public List<RuleHistory> RuleHistories { get; set; } = new();
		
		[XmlArray("allocations")]
		[XmlArrayItem("allocation")]
		public List<Allocation> Allocations { get; set; }
		
		[XmlArray("allocation-statuses")]
		[XmlArrayItem("allocation-status")]
		public List<AllocationStatus> AllocationStatuses { get; set; }
	}
}
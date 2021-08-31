using System.Collections.Generic;
using System.Xml.Serialization;
using Playground.PaymentEngine.Store.Withdrawals.Model;

namespace Playground.PaymentEngine.Store.File.Withdrawals {
	[XmlRoot(ElementName="repository")]
	public class WithdrawalData {
		[XmlArray("withdrawals")]
		[XmlArrayItem("withdrawal")]
		public List<Withdrawal> Withdrawals { get; set; } = new();

		[XmlArray("withdrawal-statuses")]
		[XmlArrayItem("withdrawal-status")]
		public List<WithdrawalStatus> WithdrawalStatuses { get; set; } = new();

		[XmlArray("withdrawal-groups")]
		[XmlArrayItem("withdrawal-group")]
		public List<WithdrawalGroup> WithdrawalGroups { get; set; } = new();
	}
}
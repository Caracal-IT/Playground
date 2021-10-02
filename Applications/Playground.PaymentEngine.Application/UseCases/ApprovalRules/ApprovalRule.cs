namespace Playground.PaymentEngine.Application.UseCases.ApprovalRules;

using Playground.Core.Model;

public record ApprovalRule: Entity {
    public string RuleName { get; set; } = "Rule1";
    public bool IsSuccessful { get; set; }
    public string? Message { get; set; }
}
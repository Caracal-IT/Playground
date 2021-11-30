Set-Location  '/Users/ettienemare/Documents/Development/CS/Playground/Stores/Playground.PaymentEngine.Store.EF'

Write-Host 'Update Database'
Write-Host 'Update EFAccountStore'
dotnet ef database update --no-build --context EFAccountStore

Write-Host 'Update EFCustomerStore'
dotnet ef database update --no-build --context EFCustomerStore

Write-Host 'Update EFDepositStore'
dotnet ef database update --no-build --context EFDepositStore

Write-Host 'Update EFAllocationStore'
dotnet ef database update --no-build --context EFAllocationStore

Write-Host 'Update EFApprovalRuleStore'
dotnet ef database update --no-build --context EFApprovalRuleStore

Write-Host 'Update EFTerminalStore'
dotnet ef database update --no-build --context EFTerminalStore

Write-Host 'Update EFWithdrawalStore'
dotnet ef database update --no-build --context EFWithdrawalStore
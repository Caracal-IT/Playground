Set-Location  '/Users/ettienemare/Documents/Development/CS/Playground/Stores/Playground.PaymentEngine.Store.EF'

Write-Host 'Update Database'
Write-Host 'Update EFAccountStore'
dotnet ef database update --no-build --context EFAccountStore

Write-Host 'Update EFCustomerStore'
dotnet ef database update --no-build --context EFCustomerStore

Write-Host 'Update EFDepositStore'
dotnet ef database update --no-build --context EFDepositStore 
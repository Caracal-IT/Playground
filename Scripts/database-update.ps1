Set-Location  '/Users/ettienemare/Documents/Development/CS/Playground/Stores/Playground.PaymentEngine.Store.EF'

dotnet ef database update --no-build --context EFAccountStore
dotnet ef database update --no-build --context EFCustomerStore
dotnet ef database update --no-build --context EFDepositStore 
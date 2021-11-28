Set-Location  '/Users/ettienemare/Documents/Development/CS/Playground/Stores/Playground.PaymentEngine.Store.EF' || exit

dotnet ef database update --context EFAccountStore --no-build
dotnet ef database update --context EFCustomerStore --no-build
dotnet ef database update --context EFDepositStore --no-build 
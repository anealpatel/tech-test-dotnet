### Change list 

1. Implemented IAccountDataStore.cs interface so as to abstract away the implementation of AccountDataStore and BackupAccountDataStore
2. Refactored MakePayment so that it more closely complied to the SRP and OCP
3. I moved the account validation into its own class and reworked the double falsy statements to be more readable.
4. I initially wrote unit test in xunit using Theory and Inline statements, but then changed that to reqnroll to be more readable. This took longer but in the end it is more readable.


### Change list if time was not a constraint

1. Improve the code coverage. I would have written more tests to cover the full range of sceanrios between Accounts and Payments
2. The "AllowedPaymentSchemes" enum make use of bitwise operations and I have not catered for that in my testing. My tests only allow for a single value of AllowedPaymentSchemes.
3. I was unsure of whether to refactor the AccountValidator.cs so it adheres to SRP and OCP. I prefer the case statement. I would consider it if there was to be a lot of change in that class, like regularly swapping out different payments etc.

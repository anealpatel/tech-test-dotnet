### Change list if time was not a constraint

1. Improve the code coverage. I would have written more tests to cover the full range of sceanrios between Accounts and Payments
2. The "AllowedPaymentSchemes" enum make use of bitwise operations and I have not catered for that in my testing. My tests only allow for a single value of AllowedPaymentSchemes.
3. I was unsure of whether to refactor the AccountValidator.cs so it adheres to SRP and OCP. I prefer the case statement. I would consider it if there was to be a lot of change in that class, like regularly swapping out different payments etc.

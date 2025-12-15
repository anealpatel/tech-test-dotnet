Feature: Payments

Tests various payment and account scenanrios

@ChapsPayments
Scenario: Test a Chaps payment against Live Chaps account with sufficient funds
	Given an account status is Live 
	And a has a balance of $1000 
	And a has a payment scheme of Chaps
	And a Chaps payment of $100 is requested
	When the payment is made
	Then the result should be successful
	And the account balance should be $900

@ChapsPayments
Scenario: Test a Chaps payment against Live Chaps account with insufficient funds
	Given an account status is Live 
	And a has a balance of $900 
	And a has a payment scheme of Chaps
	And a Chaps payment of $1000 is requested
	When the payment is made
	Then the result should be successful
	And the account balance should be $-100

@ChapsPayments
Scenario: Test a Chaps payment against Disabled Chaps account with sufficient funds
	Given an account status is Disabled 
	And a has a balance of $1000 
	And a has a payment scheme of Chaps
	And a Chaps payment of $100 is requested
	When the payment is made
	Then the result should be unsuccessful
	And the account balance should be $1000

@ChapsPayments
Scenario: Test a Chaps payment against Live FasterPayments account with sufficient funds
	Given an account status is Live 
	And a has a balance of $1000 
	And a has a payment scheme of FasterPayments
	And a Chaps payment of $100 is requested
	When the payment is made
	Then the result should be unsuccessful
	And the account balance should be $1000

@FasterPayments
Scenario: Test a FasterPayments payment against Live FasterPayments account with sufficient funds
	Given an account status is Live 
	And a has a balance of $1000 
	And a has a payment scheme of FasterPayments
	And a FasterPayments payment of $100 is requested
	When the payment is made
	Then the result should be successful
	And the account balance should be $900

@FasterPayments
Scenario: Test a FasterPayments payment against Live FasterPayments account with insufficient funds
	Given an account status is Live 
	And a has a balance of $1000 
	And a has a payment scheme of FasterPayments
	And a FasterPayments payment of $1001 is requested
	When the payment is made
	Then the result should be unsuccessful
	And the account balance should be $1000

# tests to show where code logic code be improved
@FasterPayments
Scenario: Test a FasterPayments payment against Disabled FasterPayments account with sufficient funds
	Given an account status is Disabled 
	And a has a balance of $1000 
	And a has a payment scheme of FasterPayments
	And a FasterPayments payment of $100 is requested
	When the payment is made
	Then the result should be successful
	And the account balance should be $900

@BacPayments
Scenario: Test a Bacs payment against Disabled Bacs account with sufficient funds
	Given an account status is Disabled 
	And a has a balance of $1000 
	And a has a payment scheme of Bacs
	And a Bacs payment of $100 is requested
	When the payment is made
	Then the result should be successful
	And the account balance should be $900

@BacPayments
Scenario: Test a Bacs payment against Disabled Bacs account with insufficient funds
	Given an account status is Disabled 
	And a has a balance of $100 
	And a has a payment scheme of Bacs
	And a Bacs payment of $1000 is requested
	When the payment is made
	Then the result should be successful
	And the account balance should be $-900

@BacsChapsAccount
Scenario: Test a Bacs payment against a Disabled account with insufficient funds, configured with Bacs and Chaps payment schemes
	Given an account status is Disabled 
	And a has a balance of $100 
	And a has a payment scheme of Bacs
	And a has a payment scheme of Chaps
	And a Bacs payment of $1000 is requested
	When the payment is made
	Then the result should be successful
	And the account balance should be $-900

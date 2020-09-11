Feature: Authentication
	reqres site verification

	Background: 
	Given I get authentication for the user with login details
	| Email              | Password   |
	| eve.holt@reqres.in | cityslicka |


Scenario: Verify Name for the user 1 
	Given I perform GET operation for "users/{userid}"
	And I perform operation for post "1"
	Then I should see the "total" name as "12"
	
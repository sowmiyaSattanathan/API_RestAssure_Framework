Feature: Complex
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@mytag
Scenario Outline: Get the first User 
Given I perform GET operation for "users?id={id}"
When I perform operation for user in "<id>"
Then I should see the "total" name as "12" reponse is OK


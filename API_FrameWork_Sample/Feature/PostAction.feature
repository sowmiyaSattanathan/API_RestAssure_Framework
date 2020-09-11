Feature: PostAction
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

Scenario: Verify Post operation for Profile
    Given I Perform POST operation for "/posts/{profileNo}/profile" with body
      | name | profile |
      | Mic | 3       |
	Then I should see the "name" name as "Mic"

	
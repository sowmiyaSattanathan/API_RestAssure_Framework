Feature: GetPosts
	Test GET posts operation with Restsharp.net
	(Checked with fake JSON)
		
	

	Scenario Outline: Verify author of the posts 1 
	Given I perform GET operation for "posts/{postid}"
	And I perform operation for post "<postid>"
	Then I should see the "author" name as "<Author name>"
	Examples: 
	| postid | Author name |
	| 1      | typicode    |
	| 2      | Test        |

	
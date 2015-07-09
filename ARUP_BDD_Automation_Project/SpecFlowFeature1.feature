Feature: SpecFlowFeature1
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@mytag
Scenario: Add two numbers
	Given I have entered 50 into the calculator
	Given I press add
	And I have entered 70 into the calculator
	When I press equals
	Then the result should be 120 on the screen


@mytag
Scenario Outline: Add two numbers for different scenarios
	Given I have entered "<Input1>" into the calculator
	Given I press add
	And I have entered "<Input2>" into the calculator
	When I press equals
	Then the result should be <Result> on the screen

Examples: 
| Input1 | Input2 | Result |
| 20     | 70     | 90     |
| 50     | 100    | 150    |
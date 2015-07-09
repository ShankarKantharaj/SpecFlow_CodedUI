Feature: Guru99_Feature

@mytag
Scenario: Create New Customer
	Given I have Logged into Guru99 Manager Homepage
	When I create a New Customer
	Then The Customer is successfully created

Scenario Outline: Create New Customer1111
Given I have Logged into Guru99 Manager Homepage<UserName> and  <Password>
| CustomerName | Phone Number |
| Raj          | 097774394983 |
| Raj12122      | 097774394983 |

When I create a New Customer
Then The Customer is successfully created

Examples: 
| UserName | Password |
| Raj          | 097774394983 |
| Raj12122         | 097774394983 |
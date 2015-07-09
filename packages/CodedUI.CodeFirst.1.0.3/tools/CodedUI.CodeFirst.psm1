function Invoke-WorkItemQuery {
	
	<#
	.SYNOPSIS
	Executes work item query. Appends the current team project to the query
	.PARAMETER query
	The work item query to execute
	#>

	[cmdletbinding()]
	param([string]$query)

	$tfs = $dte.GetObject("Microsoft.VisualStudio.TeamFoundation.TeamFoundationServerExt")
	[System.Reflection.Assembly]::Load("Microsoft.TeamFoundation.WorkItemTracking.Client, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a") > $null
	$client = new-object -typename Microsoft.TeamFoundation.Client.TfsTeamProjectCollection -argumentlist $tfs.ActiveProjectContext.DomainUri
	$wit = $client.GetService([Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStore])
	
	$wit.Query("$query and [Team Project] = '$($tfs.ActiveProjectContext.ProjectName)'")
}


function Get-TestCase {

	<#
	.SYNOPSIS
	Retrieves the Test Case with the specified id
	.DESCRIPTION
	Retrieves the Test Case with the specified id
	The Test Case has the following properties:
		- id:               Test case id (123)
		- title:            Test case title (Test case title)
		- name:             Proper title, no spaces and id suffix (TestCaseTitle123)
		- testmethodname:   Proper title (TestCaseTitle) 
		- steps:            Array of test case steps.
		- parameters:       Array of parameter names.
		- parametervalues:  Array of test case iterations/parameter values
	.PARAMETER id
	The work item id of the Test Case to retrieve
	.EXAMPLE
	$testcase = (Get-TestCase 3242)
	write-host $testcase.title 
	#>

	[cmdletbinding()]
	param($id)
	
	$testcase = @{}
	$witestcase = (Invoke-WorkItemQuery "SELECT [Title] FROM WorkItems WHERE [Work Item Type] = 'Test Case' and [Id] = $id")
	
	#steps
	$global:steps = @()
	$wisteps = [xml]$witestcase.Fields["Steps"].Value
	Get-TestCaseSteps $wisteps
	$testcase.steps = $global:steps
	
	#parameter data
	$datasource = [xml]$witestcase.Fields["Local Data Source"].Value
	if($datasource -is [xml]){
		$testcase.parametervalues = [Array](Get-TestCaseParameterValues $datasource)
	}
	
	#parameter names
	if($witestcase.Fields["Parameters"].Value.Length -gt 0){
		$parameters = [xml]$witestcase.Fields["Parameters"].Value
		if($parameters -is [xml]) {
			$testcase.parameters = [Array](Get-TestCaseParameters $parameters)
		}
	}
	
	#id
	$testcase.id = $id
	
	#title
	$testcase.title = $witestcase.Title
	
	#name
	$propername = (Get-Culture).TextInfo.ToTitleCase($testcase.title.ToLower())
	$name = ""
	[RegEx]::Matches($propername, "([\w_]+)") | foreach { $name += $_.Value }
	$testcase.name = "$name$id"
	
	#methodname
	$testcase.testmethodname = $name
	
	$testcase
}

function Get-TestCaseSteps {

	<#
	.SYNOPSIS
	Converts a test case steps xml document to an array of objects
	#>

	[cmdletbinding()]
	param($node)

	if($node.Name -eq "step"){
		$step = @{}
		$action = (Get-TestCaseParameterizedString $node.ChildNodes.Item(0))
		$step.action = $action
		
		$assert = (Get-TestCaseParameterizedString $node.ChildNodes.Item(1))
		if(![string]::IsNullOrEmpty($assert)){
			$step.assert = $assert
		}
		$global:steps += $step
	}
	
	#shared steps are currently not supported
	if($node.Name -eq "compref") {
		$step = @{}
		$ref = $node.Attributes.GetNamedItem("ref").Value
		$step.shared = "shared step $ref"
		$global:steps += $step
	}
	
	$node.ChildNodes | foreach { (Get-TestCaseSteps $_ )}
}


function Get-TestCaseParameterizedString {

	<#
	.SYNOPSIS
	Flattens a parameterizedString element. Concatenates its children (text and parameters)
	#>

	[cmdletbinding()]
	param($node)
	
	$parameterizedString = ""
	
	$node.ChildNodes | foreach {
		if( $_.Name -eq "parameter" ) {
			$parameterizedString += "@"
		} 
		$text = $_.InnerText.Trim()
		$parameterizedString += "$text "
	}
	
	$parameterizedString
}

function Get-TestCaseParameterValues {

	<#
	.SYNOPSIS
	Converts a test case default local data source into an array of objects
	#>

	[cmdletbinding()]
	param($doc)
	
	$parameters = @()
	$doc.NewDataSet.Table1 | foreach { 

		$parameter = @{}
		$_.ChildNodes | foreach {
			if(![string]::IsNullOrEmpty($_.Name)){
				$parameter.Add($_.Name, $_.InnerText)
			}
		}
		
		if($parameter.count -gt 0){
			$parameters += $parameter
		}
	}
	$parameters
}

function Get-TestCaseParameters {

	<#
	.SYNOPSIS
	Converts a test case parameters document to an array of parameter names
	#>

	[cmdletbinding()]
	param($node)

	$parameters = @()
	$node.parameters.param | select "name" | foreach { $parameters += $_.name }
	$parameters
}
[T4Scaffolding.Scaffolder(Description = "Enter a description of TestCase here")]
[CmdletBinding()]
param(
    [parameter(Mandatory = $true, ValueFromPipelineByPropertyName = $true)]
    [string]$TestCaseId,   
    [string]$Project,
	[string]$CodeLanguage,
	[string[]]$TemplateFolders,
	[switch]$Force = $false
)

$testcase = (Get-TestCase $TestCaseId)
$outputPath = $testcase.name
$namespace = (Get-Project $Project).Properties.Item("DefaultNamespace").Value

$template = "TestCaseTemplate"
if($testcase.parameters.length -gt 0){
    $template = "TestCaseWithParametersTemplate"
}


Add-ProjectItemViaTemplate $outputPath -Template $template `
	-Model @{ Namespace = $namespace; TestCase = $testcase; } `
	-SuccessMessage "Added TestCase output at {0}" `
	-TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force

$directory = [System.IO.Path]::GetDirectoryName((Get-Project).FullName)
$dte.ItemOperations.OpenFile("$directory\$outputPath.cs")
$dte.ExecuteCommand("Edit.FormatDocument")
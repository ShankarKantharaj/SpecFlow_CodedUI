param($rootPath, $toolsPath, $package, $project)

if (-not (Get-Module T4Scaffolding)) { return }

[System.Reflection.Assembly]::LoadFrom("$rootPath\lib\net40\Microsoft.Services.TestTools.UITesting.Html.dll") > $null

$modulePath = join-path $toolsPath CodedUI.CodeFirst.psm1
Import-Module $modulePath -Force -DisableNamechecking   

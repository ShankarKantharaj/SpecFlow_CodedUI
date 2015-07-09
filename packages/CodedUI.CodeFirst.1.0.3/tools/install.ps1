param($rootPath, $toolsPath, $package, $project)

# Bail out if scaffolding is disabled (probably because you're running an incompatible version of T4Scaffolding.dll)
if (-not (Get-Module T4Scaffolding)) { return }

# Try to delete InstallationDummyFile.txt
if ($project) {
	Get-ProjectItem "InstallationDummyFile.txt" -Project $project.Name | %{ $_.Delete() }
    @(
        "Microsoft.VisualStudio.QualityTools.CodedUITestFramework",
        "Microsoft.VisualStudio.QualityTools.UnitTestFramework",
        "Microsoft.VisualStudio.TestTools.UITest.Common",
        "Microsoft.VisualStudio.TestTools.UITest.Extension",
        "Microsoft.VisualStudio.TestTools.UITesting"
    ) | foreach { $project.Object.References.Item($_).CopyLocal = $false }
}
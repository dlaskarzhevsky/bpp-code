[CmdletBinding()]
param
(
    [string] $nugetPackageVersionConst="NUGET_PACKAGE_VERSION",
    [Parameter(Mandatory=$true)][ValidateNotNullOrEmpty()] [string] $nugetPackageVersion
)

$_NUGET_PACKAGE_VERSION_CONST = $nugetPackageVersionConst

$_current_version = $nugetPackageVersion.Trim()

Write-Host "Current version: $_current_version"

$constant_part = $_current_version.substring(0, $_current_version.lastIndexOf('.'))
$variable_part = $_current_version.substring($_current_version.lastIndexOf('.') + 1)

$variable_part_increased = [int]$variable_part + 1

$new_version = $constant_part + '.' + $variable_part_increased

Write-Host "Increased version: $new_version"

# Set Azure DevOps pipeline variable for future tasks
Write-Host "##vso[task.setvariable variable=$_NUGET_PACKAGE_VERSION_CONST;]$new_version"

# Set GitHub workflow variable for future steps
"$_NUGET_PACKAGE_VERSION_CONST=$new_version" | Out-File -FilePath $env:GITHUB_ENV -Append
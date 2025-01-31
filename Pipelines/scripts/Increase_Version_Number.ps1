
$_NUGET_PACKAGE_VERSION_CONST = "NUGET_PACKAGE_VERSION";

$_current_version = [Environment]::GetEnvironmentVariable($_NUGET_PACKAGE_VERSION_CONST).Trim()

Write-Host "Current version: $_current_version"

$constant_part = $_current_version.substring(0, $_current_version.lastIndexOf('.'))
$variable_part = $_current_version.substring($_current_version.lastIndexOf('.') + 1)

$variable_part_increased = [int]$variable_part + 1

$new_version = $constant_part + '.' + $variable_part_increased

Write-Host "Increased version: $new_version"

# Set the pipeline for future tasks
Write-Host "##vso[task.setvariable variable=$_NUGET_PACKAGE_VERSION_CONST;]$new_version"
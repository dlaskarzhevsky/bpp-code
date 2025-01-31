const ado = require('azure-pipelines-task-lib');

const _NUGET_PACKAGE_VERSION_CONST = "NUGET_PACKAGE_VERSION";

let _current_version = eval(`process.env.${_NUGET_PACKAGE_VERSION_CONST}`).trim();

console.log(`Current version: ${_current_version}`);

const constant_part = _current_version.substring(0, _current_version.lastIndexOf('.'));
let variable_part = _current_version.substring(_current_version.lastIndexOf('.') + 1);

const variable_part_increased = parseInt(variable_part) + 1;
const new_version = constant_part + '.' + variable_part_increased;

console.log(`Increased version: ${new_version}`);

// Set the pipeline for future tasks
ado.setVariable(_NUGET_PACKAGE_VERSION_CONST, new_version, false, false);

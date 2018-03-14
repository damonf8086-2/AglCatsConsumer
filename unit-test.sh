#!/bin/bash

# Stop the script if any commands fail
set -euo pipefail

dotnet restore ./test/UnitTest/UnitTest.csproj
dotnet test ./test/UnitTest/UnitTest.csproj
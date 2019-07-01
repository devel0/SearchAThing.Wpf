#!/bin/bash

exdir=$(dirname `readlink -f "$0"`)

cd "$exdir"/SearchAThing.Wpf

# to create nupkg from vs shell
# msbuild -t:pack -p:Configuration=Release

dotnet nuget push bin/Release/*.nupkg -k $(cat ~/security/nuget-api.key) -s https://api.nuget.org/v3/index.json

cd "$exdir"

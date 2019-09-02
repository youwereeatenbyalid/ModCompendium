#!/bin/bash
parent_path=$( cd "$(dirname "${BASH_SOURCE[0]}")" ; pwd -P )

echo "$parent_path"
cd $1
WINEPREFIX=/home/lbnegroponte/.local/share/wineprefixes/dotnet46 wine $parent_path/$2 $3 $4 $5

#!/bin/bash
parent_path=$( cd "$(dirname "${BASH_SOURCE[0]}")" ; pwd -P )
WP=/home/lbnegroponte/.local/share/wineprefixes/dotnet46
#this is the lamest script I've ever had the misfortune of writing. If you would like to fix it, please do so and submit a pull request.
#echo "$parent_path"
#echo "$@"
cd $2
WINEPREFIX=$WP wine $parent_path/$1 -input "$(winepath -w "$3")" -output "$(winepath -w "$4")" -file "$(winepath -w "$5")" -file "$(winepath -w "$6")" -file "$(winepath -w "$7")" -file "$(winepath -w "$8")" -file "$(winepath -w "$9")" -file "$(winepath -w "$10")" -file "$(winepath -w "$11")" -file "$(winepath -w "$12")"

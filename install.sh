#!/bin/bash

MODULE="FantasyTweaks"
GAMEDIR="/c/Program Files (x86)/Steam/steamapps/common/Mount & Blade II Bannerlord"

cp -rf "./$MODULE/bin/Release/$MODULE.dll" "$GAMEDIR/Modules/$MODULE/bin/Win64_Shipping_Client/"

echo "Installation succcessful."
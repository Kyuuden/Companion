#!/bin/bash
if [ $# -eq 0 ]; then
    >&2 echo "Usage: create-release version"
    exit 1
fi

mkdir NewRelease
cp -r ../BizHawk/ExternalTools NewRelease
cd NewRelease
7z a -tzip $HOME/OneDrive/Documents/FF.Rando.Companion-$1.zip ExternalTools
rm -r ExternalTools/FF.Rando.Companion/Updater
cd ExternalTools
7z a -tzip $HOME/OneDrive/Documents/Update-$1.zip FF.Rando.Companion/*
7z a -tzip $HOME/OneDrive/Documents/Update-$1.zip FF.Rando.Companion.dll
cd ../..
rm -r NewRelease
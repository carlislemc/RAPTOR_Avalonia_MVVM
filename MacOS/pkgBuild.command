#!/bin/sh
rm -Rf appfolder
mkdir -p appfolder/raptor.app/Contents/MacOS
cp -R ../bin/Release/netcoreapp3.1/publish/* appfolder/raptor.app/Contents/MacOS

mkdir -p appfolder/raptor.app/Contents/Resources
cp AppIcon.icns appfolder/raptor.app/Contents/Resources

cp Info.plist appfolder/raptor.app/Contents

pkgbuild --root appfolder --install-location /Applications --identifier RAPTOR_Avalonia raptor.pkg
rm -Rf appfolder

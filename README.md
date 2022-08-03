# RAPTOR_Avalonia_MVVM
```
Make app pkg Raptor -- MacOS
Cd to publish folder
rm -rf appfolder/raptor.app
mkdir -p appfolder/raptor.app/Contents/MacOS
mv * appfolder/raptor.app/Contents/MacOS
pkgbuild --root appfolder --install-location /Applications --identifier RAPTOR_Avalonia raptor.pkg
```

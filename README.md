# RAPTOR_Avalonia_MVVM

Build/Release
Build/Publish netcoreapp3.1 portable to bin\Release\publish\
Make a zip file of publish using Windows (publish.zip)
Rename it to raptor_avalonia.zip and copy to C:\Users\carlislem\Documents\mcc_html\raptor
In a DOS prompt, run C:\Users\carlislem\source\repos\RAPTOR_Avalonia_MVVM\wix_compile_installer.bat
del raptor_avalonia.msi
rename wixedit_raptor.msi raptor_avalonia.msi
copy raptor_avalonia.msi C:\Users\carlislem\Documents\mcc_html\raptor\

From wsl prompt
cd  /mnt/c/users/carlislem/source/repos/RAPTOR_Avalonia_MVVM
scp bin/release/raptor_avalonia.zip vagrantboxes@hulkcybr1.dlh.tamu.edu:raptor/

On Hulk:
cd raptor
bash step1.sh
bash updatepayload.sh
vi flat/PackageInfo

Back in wsl
scp vagrantboxes@hulkcybr1.dlh.tamu.edu:raptor/raptor_avalonia.pkg /mnt/c/users/carlislem/Documents/mcc_html/raptor

Using FileZilla, upload raptor_avalonia.pkg, raptor_avalonia.msi and raptor_avalonia.zip to website public_html/raptor

```
Make app pkg Raptor -- MacOS
Cd to publish folder
rm -rf appfolder/raptor.app
mkdir -p appfolder/raptor.app/Contents/MacOS
mv * appfolder/raptor.app/Contents/MacOS
pkgbuild --root appfolder --install-location /Applications --identifier RAPTOR_Avalonia raptor.pkg
```

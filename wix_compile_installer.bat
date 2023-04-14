copy bin\release\CodeGenerators.dll bin\release\publish
"C:\Program Files (x86)\WixEdit\wix-3.0.5419.0\candle.exe" -nologo "C:\Users\carlislem\source\repos\RAPTOR_Avalonia_MVVM\wixedit_raptor.wxs" -out "C:\Users\carlislem\source\repos\RAPTOR_Avalonia_MVVM\wixedit_raptor.wixobj"  -ext WixUtilExtension  -ext WixUIExtension  -ext WixNetFxExtension
"C:\Program Files (x86)\WixEdit\wix-3.0.5419.0\light.exe" -nologo "C:\Users\carlislem\source\repos\RAPTOR_Avalonia_MVVM\wixedit_raptor.wixobj" -out "C:\Users\carlislem\source\repos\RAPTOR_Avalonia_MVVM\wixedit_raptor.msi"  -ext WixUtilExtension  -ext WixUIExtension  -ext WixNetFxExtension
cd bin\release
del raptor_avalonia.zip
zip raptor_avalonia.zip -r publish
cd ..\..
echo "On Hulk, update the Payload folder in raptor and run updatepayload.sh"

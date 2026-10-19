copy bin\release\CodeGenerators.dll bin\release\publish
"C:\Program Files (x86)\WiX Toolset v3.14\bin\candle.exe" -fips -nologo "C:\Users\carlislem\Documents\RAPTOR_Avalonia_MVVM\wixedit_raptor.wxs" -out "C:\Users\carlislem\Documents\RAPTOR_Avalonia_MVVM\wixedit_raptor.wixobj"  -ext WixUtilExtension  -ext WixUIExtension  -ext WixNetFxExtension
"C:\Program Files (x86)\WiX Toolset v3.14\bin\light.exe" -nologo "C:\Users\carlislem\Documents\RAPTOR_Avalonia_MVVM\wixedit_raptor.wixobj" -out "C:\Users\carlislem\Documents\RAPTOR_Avalonia_MVVM\RAPTOR_Avalonia_MVVM\wixedit_raptor.msi"  -ext WixUtilExtension  -ext WixUIExtension  -ext WixNetFxExtension
cd bin\release
del raptor_avalonia.zip
zip raptor_avalonia.zip -r publish
cd ..\..
copy bin\release\raptor_avalonia.zip .
echo "On Hulk, update the Payload folder in raptor and run updatepayload.sh"

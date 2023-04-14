rm -R publish.old
mv publish publish.old
unzip raptor_avalonia.zip
cp -v -r -u publish/* Payload/raptor.app/Contents/MacOS

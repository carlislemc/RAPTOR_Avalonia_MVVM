( cd Payload && find . | cpio -o --format odc --owner 0:80 | gzip -c ) > flat/Payload
mkbom -u 0 -g 80 ~/raptor/Payload/ flat/Bom
du -BK ~/raptor/Payload
echo "Check this value in PackageInfo"
( cd flat && xar --compression none -cf "../raptor_avalonia.pkg" * )

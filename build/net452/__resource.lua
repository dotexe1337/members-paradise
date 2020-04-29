resource_manifest_version '44febabe-d386-4d18-afbe-5e627f4af937'

name 'vMenu-forked'
description 'Server sided trainer for FiveM with custom permissions, using a custom MenuAPI. Forked by dotexe'
author 'Tom Grobbe (www.vespura.com), dotexe'
version 'v3.0.3'
url 'https://github.com/TomGrobbe/vMenu/'
client_debug_mode 'false'
server_debug_mode 'false'
experimental_features_enabled '0' -- leave this set to '0' to prevent compatibility issues and to keep the save files your users.

files {
    '*.dll',
    'MenuAPI.dll',
    'config/tracks.json',
    'config/permissions.cfg',
    'config/ls.json',
    'config/addons.json',
    'config/Cars.json',
    'config/DriftAssist.json',
    'stream/commonmenu.ytd',
    'cars.json',
    'cars.xml'
}

client_script {
    'vMenuClient.net.dll',
    'carcats.lua',
    'config/permissions.cfg',
}
 
server_script 'vMenuServer.net.dll'
# Spreetail Work Sample
A simple POC for a dictionary app with the following commands:
- ADD key value
- KEYS
- MEMBERS
- REMOVE key value
- REMOVEALL key
- CLEAR
- KEYEXISTS key
- MEMBEREXISTS key value
- ALLMEMBERS
- ITEMS
- EXIT

## System Requirements
This is a .NET Core app which can be run on any modern OS (Windows, Linux, Mac), if built properly. Memory usage is minimal and the
app has no external dependencies outside of the .NET Core runtime environment.

## Build Instructions
Compiled binaries are not available. You must compile from source. The easy way is to publish it through Visual Studio and specify
the OS to run on.
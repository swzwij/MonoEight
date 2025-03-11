
- Large release build
dotnet publish -c Release -r win-x64 --self-contained

- Compact release build
dotnet publish -c Release -r win-x64 --self-contained -p:PublishSingleFile=true

- Clean  Build
dotnet clean
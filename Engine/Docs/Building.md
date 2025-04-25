## Cheat sheet for build commands

- Large release build
dotnet publish -c Release -r win-x64 --self-contained

- Compact release build
dotnet publish -c Release -r win-x64 --self-contained -p:PublishSingleFile=true
dotnet publish -c Release -r linux-x64 --self-contained -p:PublishSingleFile=true

- Clean Build Directory
dotnet clean
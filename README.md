# Ad Libitum
Ad Libitum is a configurability-focused quality-of-life content and utility mod built for tModLoader 1.4, with the express goal of allowing players to tweak each feature to their liking to maximize enjoyment.

## Building
This mod uses [Tea Framework](https://github.com/rejuvena/tea-framework), and requires you to have it installed.

```bash
# when cloning, you need to specify the folder name, as tModLoader does not accept improper folder names
# run this in your ModSources folder
git clone https://github.com/rejuvena/ad-libitum.git AdLibitum

# detour dotnet packages
dotnet restore

# build
dotnet build
```
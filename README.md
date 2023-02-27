# UnityExtensions
[![openupm](https://img.shields.io/npm/v/net.slothsoft.unity-extensions?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/net.slothsoft.unity-extensions/)

C# extension functions, attributes and editor tools for common Unity problems.

## Key features:
- Additional Linq-like IEnumerable extensions.
- Expandable attribute, to in-place edit asset references.
- HDRP <=> URP Asset converter.
- Project file fixer, to set C# version and warning level for .csproj files.
- A .cs template for proper namespace support.
- A WebGL template for use in iframes.

## Requirements
- Unity 2020.3

## Installation
### Install via manifest.json
The package is available on the [openupm registry](https://openupm.com/packages/net.slothsoft.unity-extensions/). The easiest way to install it is to set up a scoped registry via Unity's manifest.json:
```
{
  "scopedRegistries": [
    {
      "name": "OpenUPM",
      "url": "https://package.openupm.com",
      "scopes": [
		"net.slothsoft.unity-extensions"
      ]
    }
  ],
  "dependencies": {
    "net.slothsoft.unity-extensions": "2.4.0",
  }
}
```

### Install via OpenUPM-CLI
Alternatively, you may install it via [openupm-cli](https://github.com/openupm/openupm-cli):
```
openupm add net.slothsoft.unity-extensions
```

### Install via git
Alternatively, you may install it as a git repository:
- Open Unity.
- Navigate to Window > Package Manager > + > Add package from git URL...
- Enter this repository's URL: https://github.com/Faulo/UnityExtensions

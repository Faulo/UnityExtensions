# UnityExtensions
[![openupm](https://img.shields.io/npm/v/net.slothsoft.unity-extensions?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/net.slothsoft.unity-extensions/)
Some C# Extension functions for common Unity problems.

## Key features:
- Additional Linq-like IEnumerable extensions.
- Expandable attribute, to in-place edit asset references.
- HDRP <=> URP Asset converter.
- Project file fixer, to set C# version and warning level for .csproj files.

## Requirements
- Unity 2019.3

## Installation
### Install via manifest.json
The package is available on the [openupm registry](https://openupm.com). The easiest way to install is is to set up a scoped registry via Unity's manifest.json:
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
    "net.slothsoft.unity-extensions": "1.0.0",
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
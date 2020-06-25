# TypeRef Hasher

[![license](https://img.shields.io/github/license/GDATASoftwareAG/TypeRefHasher.svg)](https://raw.githubusercontent.com/GDATASoftwareAG/TypeRefHasher/master/LICENSE)
[![Build](https://img.shields.io/azure-devops/build/gdatasoftware/TypeRefHasher/6.svg)](https://dev.azure.com/gdatasoftware/TypeRefHasher/_build?definitionId=6)
[![Test](https://img.shields.io/azure-devops/tests/gdatasoftware/TypeRefHasher/6.svg)](https://dev.azure.com/gdatasoftware/TypeRefHasher/_build?definitionId=6)

CLI tool to compute the [TypeRefHash (TRH)](https://www.gdatasoftware.com/blog/2020/06/36164-introducing-the-typerefhash-trh) for .NET binaries.

## Installation

Just download the binary from the [GitHub Releases](https://github.com/GDATASoftwareAG/TypeRefHasher/releases) tab. There are two versions:

1. `trh` -> Linux x64
2. `trh.exe` -> Windows x64

## Usage

The usage is as straight forward as possible.

Windows:

```powershell
> trh.exe file
```

Linux:

```bash
> trh file
```

In both cases the output is the TRH (example: 1defec485ab3060a9201f35d69cfcdec4b70b84a2b71c83b53795ca30d1ae8be) for the given file or an error message with a description why the hash could not be computed.

## Release

To release a new version, push a tagged commit like shown below.

```powershell
git tag -a v1.0.0 -m "Release version 1.0.0"
git push origin v1.0.0
```

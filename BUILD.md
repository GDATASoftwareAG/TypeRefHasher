# How to build and publish TypeRefHasher

Most of the work is done by the *Azure Pipeline*, except for the publish to *winget*.

## Release a new version to GitHub

To release a new version to *Github*, you need to push a tagged commit. Replace the version with the one you want to release. The *Azure Pipeline* does the rest.

```powershell
git tag -a v1.0.0 -m "Release version 1.0.0"
git push origin v1.0.0
```

## Release to winget

To release to *winget-pkgs* you need to release to *Github* first. If you have already done that and a new `trh.msi` is available on the [Github Release](https://github.com/GDATASoftwareAG/TypeRefHasher/releases) page, you need to create a PR to *winget*.

1. Clone the [G DATA fork](https://github.com/GDATASoftwareAG/winget-pkgs) of the *winget* repo.
2. Add a new `manifests\GDATA\TypeRefHasher\x.x.x.yaml` file for the new version you want to release. Just copy an older version for that. You can find an example [here](manifests\GDATA\TypeRefHasher\1.0.0.yaml). 
   1. Name the `x.x.x.yaml` file after the version to release, e.g. `1.0.0.yaml`.
   2. Update the `Version` tag to the new version.
   3. Update the `Installers\Url` tag to point to the new `trh.msi` on [Github Release](https://github.com/GDATASoftwareAG/TypeRefHasher/releases).
   4. Update the `Sha256` of the `trh.msi`.

If you want to verify your changes you can test the manifest with the following commands:

```powershell
# Check the schema of the manifest
winget validate <manifest>

# Install the package locally.
winget install -m <manifest>
```

If you are sure that your manifest works, create a PR to the official [winget-pkgs repository](https://github.com/microsoft/winget-pkgs).
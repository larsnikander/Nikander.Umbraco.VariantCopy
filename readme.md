# Copy Variants

Umbraco Plugin for copying content from one variant to another.

inspired by https://github.com/jkulker/TFE.CopyVariantContent

# Getting Started
This package is supported on 10 and 11
Use 0.1.7 for Umbraco 9 support

# Installation

1. Install Package
```
Install-Package Nikander.Umbraco.CopyVariants
```
2. Register in Startup

```
services.AddUmbraco(_env, _config)
.AddBackOffice()
.AddWebsite()
.AddComposers()
.AddVariantCopyPlugin()
.Build();
```

# Demo

The plugin adds a new menu item for content that allows the editor to copy from a published culture to other cultures.

https://www.youtube.com/watch?v=DBmITKFwgU8

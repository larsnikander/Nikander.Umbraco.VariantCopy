# Copy Variants

Umbraco Plugin for copying content from one variant to another.

inspired by https://github.com/jkulker/TFE.CopyVariantContent

# Getting Started
This package is supported on Umbraco 9, 10 and 11

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

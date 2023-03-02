[![CI](https://github.com/lucasteles/PagedResult/actions/workflows/ci.yml/badge.svg)](https://github.com/lucasteles/PagedResult/actions/workflows/ci.yml)
[![Nuget](https://img.shields.io/nuget/v/PagedResult.svg?style=flat)](https://www.nuget.org/packages/PagedResult)
![](https://raw.githubusercontent.com/lucasteles/PagedResult/badges/badge_linecoverage.svg)
![](https://raw.githubusercontent.com/lucasteles/PagedResult/badges/badge_branchcoverage.svg)
![](https://raw.githubusercontent.com/lucasteles/PagedResult/badges/test_report_badge.svg)
![](https://raw.githubusercontent.com/lucasteles/PagedResult/badges/lines_badge.svg)

![](https://raw.githubusercontent.com/lucasteles/PagedResult/badges/dotnet_version_badge.svg)
![](https://img.shields.io/badge/Lang-C%23-green)
![https://editorconfig.org/](https://img.shields.io/badge/style-EditorConfig-black)

# PagedResult

Simple pagination result for EF Core and AspNet Core

## Getting started

[NuGet package](https://www.nuget.org/packages/PagedResult) available:

```ps
$ dotnet add package PagedResult
```

## How To Use:

```csharp
app.MapGet("/foo",
    async Task<Paged<Foo>>(
            ApplicationDbContext db,
            [AsParameters] Pagination pagination
        ) =>
        await db.Terms
            .OrderBy(x => x.CreatedAt)
            .ToPagedAsync(pagination)
);
```


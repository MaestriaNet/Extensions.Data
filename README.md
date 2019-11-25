# Maestria Data Extensions

[![Build status](https://ci.appveyor.com/api/projects/status/rwh6n141nm97vm0t/branch/master?svg=true)](https://ci.appveyor.com/project/fabionaspolini/dataextensions/branch/master)
[![NuGet](https://buildstats.info/nuget/Maestria.Data.Extensions)](https://www.nuget.org/packages/Maestria.Data.Extensions)
[![MyGet](https://buildstats.info/myget/maestrianet/Maestria.Data.Extensions)](https://www.myget.org/feed/maestrianet/package/nuget/Maestria.Data.Extensions)

[![Build History](https://buildstats.info/appveyor/chart/fabionaspolini/dataextensions?branch=master)](https://ci.appveyor.com/project/fabionaspolini/dataextensions/history?branch=master)

## What is Maestria Data Extensions?

Extension functions package for IDataReader implementations.

## What is Maestria Project?

This library is part of Maestria Project.

Maestria is a project to provide maximum productivity and elegance to your code.

## How do I get started?

First, import "Maestria.Data.Extensions" reference:

```csharp
using Maestria.Data.Extensions;
```

Then in your application code, use fluent syntax to obtain field value:

```csharp
// Configuring data connection
var connection = // your .net data provider db connection
var cmd = connection.CreateCommand();
cmd.CommandText = "select * from...";
var reader = cmd.ExecuteReader();
reader.Read();

// In this case throw exception when field value is null
var int16Value = reader.GetInt16("fieldName");
var int32Value = reader.GetInt32("fieldName");
var decimalValue = reader.GetDecimal("fieldName");

// With safe method when field value is null, it will return default value of the second argument or INullable<?> for data type 
var decimalSafeValue = reader.GetDecimalSafe("fieldName", 0);   // output is 0 when invalid field value 
var decimalSafeValue2 = reader.GetDecimalSafe("fieldName");     // output is nyll when invalid field value

// But safe methods, throw exception when field name is invalid
var temp = reader.GetDecimalSafe("invalid field name"); // throw IndexOutOfRangeException
```

Where can I get it?

First, [install NuGet](http://docs.nuget.org/docs/start-here/installing-nuget). Then, install [Maestria Data Extensions](https://www.nuget.org/packages/Maestria.Data.Extensions/) from the package manager console:

```bash
PM> Install-Package Maestria.Data.Extensions
```

or install from the dotnet cli command line:

```bash
> dotnet add package Maestria.Data.Extensions
```

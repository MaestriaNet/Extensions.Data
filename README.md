# Maestria Data Extensions

[![Build status](https://ci.appveyor.com/api/projects/status/rwh6n141nm97vm0t/branch/master?svg=true)](https://ci.appveyor.com/project/fabionaspolini/maestria-extensions-data/branch/master)
[![NuGet](https://buildstats.info/nuget/Maestria.Extensions.Data)](https://www.nuget.org/packages/Maestria.Extensions.Data)
[![MyGet](https://img.shields.io/myget/maestrianet/v/Maestria.Extensions.Data?label=MyGet)](https://www.myget.org/feed/maestrianet/package/nuget/Maestria.Extensions.Data)
[![Apimundo](https://img.shields.io/badge/Maestria.Extensions.Data%20API-Apimundo-728199.svg)](https://apimundo.com/organizations/nuget-org/nuget-feeds/public/packages/Maestria.Extensions.Data/versions/latest?tab=types)


[![Build History](https://buildstats.info/appveyor/chart/fabionaspolini/maestria-extensions-data?branch=master)](https://ci.appveyor.com/project/fabionaspolini/maestria-extensions-data/history?branch=master)

[![donate](https://www.paypalobjects.com/en_US/i/btn/btn_donate_LG.gif)](https://www.paypal.com/donate?hosted_button_id=8RSES6GAYH9BL)

## What is Maestria Data Extensions?

Extension functions package for IDataReader implementations.

## What is Maestria Project?

This library is part of Maestria Project.

Maestria is a project to provide maximum productivity and elegance to your code.

## Where can I get it?

First, [install NuGet](http://docs.nuget.org/docs/start-here/installing-nuget). Then, install [Maestria Data Extensions](https://www.nuget.org/packages/Maestria.Extensions.Data/) from the package manager console:

```bash
PM> Install-Package Maestria.Extensions.Data
```

or install from the dotnet cli command line:

```bash
> dotnet add package Maestria.Extensions.Data
```

## How do I get started?

First, import "Maestria.Extensions.Data" reference:

```csharp
using Maestria.Extensions.Data;
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

[![buy-me-a-coffee](resources/buy-me-a-coffee.png)](https://www.paypal.com/donate?hosted_button_id=8RSES6GAYH9BL)
[![smile.png](resources/smile.png)](https://www.paypal.com/donate?hosted_button_id=8RSES6GAYH9BL)

If my contributions helped you, please help me buy a coffee :D

[![donate](https://www.paypalobjects.com/en_US/i/btn/btn_donate_LG.gif)](https://www.paypal.com/donate?hosted_button_id=8RSES6GAYH9BL)

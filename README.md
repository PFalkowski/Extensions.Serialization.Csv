# Extensions.Serialization.Csv

[![CI](https://github.com/PFalkowski/Extensions.Serialization.Csv/actions/workflows/ci.yml/badge.svg)](https://github.com/PFalkowski/Extensions.Serialization.Csv/actions/workflows/ci.yml)
[![NuGet version](https://img.shields.io/nuget/v/Extensions.Serialization.Csv.svg)](https://www.nuget.org/packages/Extensions.Serialization.Csv/)
[![NuGet downloads](https://img.shields.io/nuget/dt/Extensions.Serialization.Csv.svg)](https://www.nuget.org/packages/Extensions.Serialization.Csv/)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=PFalkowski_Extensions.Serialization.Csv&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=PFalkowski_Extensions.Serialization.Csv)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=PFalkowski_Extensions.Serialization.Csv&metric=coverage)](https://sonarcloud.io/summary/new_code?id=PFalkowski_Extensions.Serialization.Csv)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://choosealicense.com/licenses/mit/)
[![Buy Me a Coffee](https://img.shields.io/badge/Buy%20Me%20a%20Coffee-support-yellow.svg)](https://www.buymeacoffee.com/piotrfalkowski)

CSV serialization and deserialization extension methods built on [CsvHelper](https://joshclose.github.io/CsvHelper/).

## Usage

```csharp
// Serialize to CSV (invariant culture by default)
string csv = personList.SerializeToCsv();

// Serialize with custom separator and quote character
string csv = personList.SerializeToCsv(separator: ";", quotation: '"');

// Serialize with custom CsvHelper ClassMap
string csv = personList.SerializeToCsv(new PersonMap(), separator: ",", quotation: '"');

// Serialize with explicit culture (e.g. Polish decimal separator)
string csv = values.SerializeToCsv(info: CultureInfo.GetCultureInfo("pl-PL"));

// Deserialize from CSV string
IEnumerable<Person> people = csv.DeserializeFromCsv<Person>();

// Deserialize with custom ClassMap and culture
IEnumerable<StockQuote> quotes = csv.DeserializeFromCsv(new StockQuoteMap(), CultureInfo.InvariantCulture);
```

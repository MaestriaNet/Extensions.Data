module Biofa.Framework.Utils.Test.DataReaderUtilsTest
open System
open System.Data.Common
open FsUnit
open System.Data.SQLite
open NUnit.Framework
open FluentCast
open Maestria.Data.Extensions

// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global

/// ===========================================
/// Expected values
/// ===========================================
let FixedPointExpected: int64 = 16L
let FloatingPointExpected: decimal = 17.0001m
let DateTimeExpected = DateTime.Today
let StringExpected = "test value"


/// ===========================================
/// Prepare InMemory data base to test
/// ===========================================
let connection = new SQLiteConnection("DataSource=:memory:")
let mutable cmd: SQLiteCommand = null
let mutable reader: DbDataReader = null

[<OneTimeSetUp>]
let prepareDb() =
    connection.Open()

    cmd <- connection.CreateCommand()
    cmd.CommandText <- "create table temp (IntValue int, IntNull int, NumericValue numeric(10,4), NumericNull numeric(10,4), DateValue Date, DateNull Date, StringValue varchar(20), StringNull varchar(20))"
    cmd.ExecuteNonQuery() |> ignore

    cmd.CommandText <- "insert into temp values(@int, null, @numeric, null, @date, null, @string, null)"
    cmd.Parameters.AddWithValue("@int", FixedPointExpected) |> ignore
    cmd.Parameters.AddWithValue("@numeric", FloatingPointExpected) |> ignore
    cmd.Parameters.AddWithValue("@date", DateTimeExpected) |> ignore
    cmd.Parameters.AddWithValue("@string", StringExpected) |> ignore
    cmd.ExecuteNonQuery() |> ignore

[<OneTimeTearDown>]
let closeDb() =
    reader.Close()
    connection.Close()

    reader.Dispose()
    cmd.Dispose()
    connection.Dispose()

[<TearDown>]
let tearDown() =
    reader.Close()

let prepareReader query =
    if (reader <> null) then
        reader.Close()

    cmd.CommandText <- query
    reader <- cmd.ExecuteReader()
    reader.Read() |> ignore
    reader


let somar valor valor2 =
    valor + valor2

/// ===========================================
/// GetInt16
/// ===========================================
[<Test>]
let ``Get Int16 required: OK``() =
    "select IntValue from temp"
    |> prepareReader
    |> fun reader -> reader.GetInt16("IntValue")
    |> should equal FixedPointExpected

[<Test>]
let ``Get Int16 required: Fail by invalid field name``() =
    (fun () -> "select IntNull from temp" |> prepareReader |> fun reader -> reader.GetInt16("InvalidFieldName") |> ignore)
    |> should throw typeof<Exception>

[<Test>]
let ``Get Int16 required: fail by null field value``() =
    (fun () -> "select IntNull from temp" |> prepareReader |> fun reader -> reader.GetInt16("IntNull") |> ignore)
    |> should throw typeof<Exception>

/// ===========================================
/// GetInt16Safe
/// ===========================================
[<Test>]
let ``Get Int16 Safe: OK``() =
    "select IntValue from temp" |> prepareReader |> fun reader -> reader.GetInt16Safe("IntValue")
    |> should equal FixedPointExpected

[<Test>]
let ``Get Int16 Safe: Fail by invalid field name``() =
    (fun () -> "select IntNull from temp" |> prepareReader |> fun reader -> reader.GetInt16Safe("InvalidFieldName") |> ignore)
    |> should throw typeof<Exception>

    (fun () -> "select IntNull from temp" |> prepareReader |> fun reader -> reader.GetInt16Safe("InvalidFieldName", 0s) |> ignore)
    |> should throw typeof<Exception>

[<Test>]
let ``Get Int16 Safe: Null field value``() =
    "select IntNull from temp" |> prepareReader |> fun reader -> reader.GetInt16Safe("IntNull")
    |> should be Null

[<Test>]
let ``Get Int16 Safe: Null field value returning default value``() =
    "select IntNull from temp" |> prepareReader |> fun reader -> reader.GetInt16Safe("IntNull", FixedPointExpected.ToInt16())
    |> should equal FixedPointExpected

/// ===========================================
/// GetInt32
/// ===========================================
[<Test>]
let ``Get Int32 required: OK``() =
    "select IntValue from temp" |> prepareReader |> fun reader -> reader.GetInt32("IntValue")
    |> should equal FixedPointExpected

[<Test>]
let ``Get Int32 required: Fail by invalid field name``() =
    (fun () -> "select IntNull from temp" |> prepareReader |> fun reader -> reader.GetInt32("InvalidFieldName") |> ignore)
    |> should throw typeof<Exception>

[<Test>]
let ``Get Int32 required: fail by null field value``() =
    (fun () -> "select IntNull from temp" |> prepareReader |> fun reader -> reader.GetInt32("IntNull") |> ignore)
    |> should throw typeof<Exception>

/// ===========================================
/// GetInt32Safe
/// ===========================================
[<Test>]
let ``Get Int32 Safe: OK``() =
    "select IntValue from temp" |> prepareReader |> fun reader -> reader.GetInt32Safe("IntValue")
    |> should equal FixedPointExpected

[<Test>]
let ``Get Int32 Safe: Fail by invalid field name``() =
    (fun () -> "select IntNull from temp" |> prepareReader |> fun reader -> reader.GetInt32Safe("InvalidFieldName") |> ignore)
    |> should throw typeof<Exception>

    (fun () -> "select IntNull from temp" |> prepareReader |> fun reader -> reader.GetInt32Safe("InvalidFieldName", 0) |> ignore)
    |> should throw typeof<Exception>

[<Test>]
let ``Get Int32 Safe: Null field value``() =
    "select IntNull from temp" |> prepareReader |> fun reader -> reader.GetInt32Safe("IntNull")
    |> should be Null

[<Test>]
let ``Get Int32 Safe: Null field value returning default value``() =
    "select IntNull from temp" |> prepareReader |> fun reader -> reader.GetInt32Safe("IntNull", FixedPointExpected.ToInt32())
    |> should equal FixedPointExpected

/// ===========================================
/// GetInt64
/// ===========================================
[<Test>]
let ``Get Int64 required: OK``() =
    "select IntValue from temp" |> prepareReader |> fun reader -> reader.GetInt64("IntValue")
    |> should equal FixedPointExpected

[<Test>]
let ``Get Int64 required: Fail by invalid field name``() =
    (fun () -> "select IntNull from temp" |> prepareReader |> fun reader -> reader.GetInt64("InvalidFieldName") |> ignore)
    |> should throw typeof<Exception>

[<Test>]
let ``Get Int64 required: fail by null field value``() =
    (fun () -> "select IntNull from temp" |> prepareReader |> fun reader -> reader.GetInt64("IntNull") |> ignore)
    |> should throw typeof<Exception>

/// ===========================================
/// GetInt64Safe
/// ===========================================
[<Test>]
let ``Get Int64 Safe: OK``() =
    "select IntValue from temp" |> prepareReader |> fun reader -> reader.GetInt64Safe("IntValue")
    |> should equal FixedPointExpected

[<Test>]
let ``Get Int64 Safe: Fail by invalid field name``() =
    (fun () -> "select IntNull from temp" |> prepareReader |> fun reader -> reader.GetInt64Safe("InvalidFieldName") |> ignore)
    |> should throw typeof<Exception>

    (fun () -> "select IntNull from temp" |> prepareReader |> fun reader -> reader.GetInt64Safe("InvalidFieldName", 0L) |> ignore)
    |> should throw typeof<Exception>

[<Test>]
let ``Get Int64 Safe: Null field value``() =
    "select IntNull from temp" |> prepareReader |> fun reader -> reader.GetInt64Safe("IntNull")
    |> should be Null

[<Test>]
let ``Get Int64 Safe: Null field value returning default value``() =
    "select IntNull from temp" |> prepareReader |> fun reader -> reader.GetInt64Safe("IntNull", FixedPointExpected.ToInt64())
    |> should equal FixedPointExpected

/// ===========================================
/// GetDecimal
/// ===========================================
[<Test>]
let ``Get Decimal required: OK``() =
    "select NumericValue from temp" |> prepareReader |> fun reader -> reader.GetDecimal("NumericValue")
    |> should equal FloatingPointExpected

[<Test>]
let ``Get Decimal required: Fail by invalid field name``() =
    (fun () -> "select NumericNull from temp" |> prepareReader |> fun reader -> reader.GetDecimal("InvalidFieldName") |> ignore)
    |> should throw typeof<Exception>

[<Test>]
let ``Get Decimal required: fail by null field value``() =
    (fun () -> "select NumericNull from temp" |> prepareReader |> fun reader -> reader.GetDecimal("NumericNull") |> ignore)
    |> should throw typeof<Exception>

/// ===========================================
/// GetDecimalSafe
/// ===========================================
[<Test>]
let ``Get Decimal Safe: OK``() =
    "select NumericValue from temp" |> prepareReader |> fun reader -> reader.GetDecimalSafe("NumericValue")
    |> should equal FloatingPointExpected

[<Test>]
let ``Get Decimal Safe: Fail by invalid field name``() =
    (fun () -> "select NumericNull from temp" |> prepareReader |> fun reader -> reader.GetDecimalSafe("InvalidFieldName") |> ignore)
    |> should throw typeof<Exception>

    (fun () -> "select NumericNull from temp" |> prepareReader |> fun reader -> reader.GetDecimalSafe("InvalidFieldName", 0m) |> ignore)
    |> should throw typeof<Exception>

[<Test>]
let ``Get Decimal Safe: Null field value``() =
    "select NumericNull from temp" |> prepareReader |> fun reader -> reader.GetDecimalSafe("NumericNull")
    |> should be Null

[<Test>]
let ``Get Decimal Safe: Null field value returning default value``() =
    "select NumericNull from temp" |> prepareReader |> fun reader -> reader.GetDecimalSafe("NumericNull", FloatingPointExpected.ToDecimal())
    |> should equal FloatingPointExpected
    
/// ===========================================
/// GetFloat
/// ===========================================
[<Test>]
let ``Get Float required: OK``() =
    "select NumericValue from temp" |> prepareReader |> fun reader -> reader.GetFloat("NumericValue")
    |> should equal FloatingPointExpected

[<Test>]
let ``Get Float required: Fail by invalid field name``() =
    (fun () -> "select NumericNull from temp" |> prepareReader |> fun reader -> reader.GetFloat("InvalidFieldName") |> ignore)
    |> should throw typeof<Exception>

[<Test>]
let ``Get Float required: fail by null field value``() =
    (fun () -> "select NumericNull from temp" |> prepareReader |> fun reader -> reader.GetFloat("NumericNull") |> ignore)
    |> should throw typeof<Exception>

/// ===========================================
/// GetFloatSafe
/// ===========================================
[<Test>]
let ``Get Float Safe: OK``() =
    "select NumericValue from temp" |> prepareReader |> fun reader -> reader.GetFloatSafe("NumericValue")
    |> should equal FloatingPointExpected

[<Test>]
let ``Get Float Safe: Fail by invalid field name``() =
    (fun () -> "select NumericNull from temp" |> prepareReader |> fun reader -> reader.GetFloatSafe("InvalidFieldName") |> ignore)
    |> should throw typeof<Exception>

    (fun () -> "select NumericNull from temp" |> prepareReader |> fun reader -> reader.GetFloatSafe("InvalidFieldName", 0.0f) |> ignore)
    |> should throw typeof<Exception>

[<Test>]
let ``Get Float Safe: Null field value``() =
    "select NumericNull from temp" |> prepareReader |> fun reader -> reader.GetFloatSafe("NumericNull")
    |> should be Null

[<Test>]
let ``Get Float Safe: Null field value returning default value``() =
    "select NumericNull from temp" |> prepareReader |> fun reader -> reader.GetFloatSafe("NumericNull", FloatingPointExpected.ToFloat())
    |> should equal FloatingPointExpected
    
/// ===========================================
/// GetDouble
/// ===========================================
[<Test>]
let ``Get Double required: OK``() =
    "select NumericValue from temp" |> prepareReader |> fun reader -> reader.GetDouble("NumericValue")
    |> should equal FloatingPointExpected

[<Test>]
let ``Get Double required: Fail by invalid field name``() =
    (fun () -> "select NumericNull from temp" |> prepareReader |> fun reader -> reader.GetDouble("InvalidFieldName") |> ignore)
    |> should throw typeof<Exception>

[<Test>]
let ``Get Double required: fail by null field value``() =
    (fun () -> "select NumericNull from temp" |> prepareReader |> fun reader -> reader.GetDouble("NumericNull") |> ignore)
    |> should throw typeof<Exception>

/// ===========================================
/// GetDoubleSafe
/// ===========================================
[<Test>]
let ``Get Double Safe: OK``() =
    "select NumericValue from temp" |> prepareReader |> fun reader -> reader.GetDoubleSafe("NumericValue")
    |> should equal FloatingPointExpected

[<Test>]
let ``Get Double Safe: Fail by invalid field name``() =
    (fun () -> "select NumericNull from temp" |> prepareReader |> fun reader -> reader.GetDoubleSafe("InvalidFieldName") |> ignore)
    |> should throw typeof<Exception>

    (fun () -> "select NumericNull from temp" |> prepareReader |> fun reader -> reader.GetDoubleSafe("InvalidFieldName", 0.0) |> ignore)
    |> should throw typeof<Exception>

[<Test>]
let ``Get Double Safe: Null field value``() =
    "select NumericNull from temp" |> prepareReader |> fun reader -> reader.GetDoubleSafe("NumericNull")
    |> should be Null

[<Test>]
let ``Get Double Safe: Null field value returning default value``() =
    "select NumericNull from temp" |> prepareReader |> fun reader -> reader.GetDoubleSafe("NumericNull", FloatingPointExpected.ToDouble())
    |> should equal FloatingPointExpected
    
/// ===========================================
/// GetDateTime
/// ===========================================
[<Test>]
let ``Get DateTime required: OK``() =
    "select DateValue from temp" |> prepareReader |> fun reader -> reader.GetDateTime("DateValue")
    |> should equal DateTimeExpected

[<Test>]
let ``Get DateTime required: Fail by invalid field name``() =
    (fun () -> "select DateNull from temp" |> prepareReader |> fun reader -> reader.GetDateTime("InvalidFieldName") |> ignore)
    |> should throw typeof<Exception>

[<Test>]
let ``Get DateTime required: fail by null field value``() =
    (fun () -> "select DateNull from temp" |> prepareReader |> fun reader -> reader.GetDateTime("DateNull") |> ignore)
    |> should throw typeof<Exception>

/// ===========================================
/// GetDateTimeSafe
/// ===========================================
[<Test>]
let ``Get DateTime Safe: OK``() =
    "select DateValue from temp" |> prepareReader |> fun reader -> reader.GetDateTimeSafe("DateValue")
    |> should equal DateTimeExpected

[<Test>]
let ``Get DateTime Safe: Fail by invalid field name``() =
    (fun () -> "select DateNull from temp" |> prepareReader |> fun reader -> reader.GetDateTimeSafe("InvalidFieldName") |> ignore)
    |> should throw typeof<Exception>

    (fun () -> "select DateNull from temp" |> prepareReader |> fun reader -> reader.GetDateTimeSafe("InvalidFieldName", DateTime.Today) |> ignore)
    |> should throw typeof<Exception>

[<Test>]
let ``Get DateTime Safe: Null field value``() =
    "select DateNull from temp" |> prepareReader |> fun reader -> reader.GetDateTimeSafe("DateNull")
    |> should be Null

[<Test>]
let ``Get DateTime Safe: Null field value returning default value``() =
    "select DateNull from temp" |> prepareReader |> fun reader -> reader.GetDateTimeSafe("DateNull", DateTimeExpected.ToDateTime())
    |> should equal DateTimeExpected
    
/// ===========================================
/// GetString
/// ===========================================
[<Test>]
let ``Get String required: OK``() =
    "select StringValue from temp" |> prepareReader |> fun reader -> reader.GetString("StringValue")
    |> should equal StringExpected

[<Test>]
let ``Get String required: Fail by invalid field name``() =
    (fun () -> "select StringNull from temp" |> prepareReader |> fun reader -> reader.GetString("InvalidFieldName") |> ignore)
    |> should throw typeof<Exception>

[<Test>]
let ``Get String required: fail by null field value``() =
    (fun () -> "select StringNull from temp" |> prepareReader |> fun reader -> reader.GetString("StringNull") |> ignore)
    |> should throw typeof<Exception>

/// ===========================================
/// GetStringSafe
/// ===========================================
[<Test>]
let ``Get String Safe: OK``() =
    "select StringValue from temp" |> prepareReader |> fun reader -> reader.GetStringSafe("StringValue")
    |> should equal StringExpected

[<Test>]
let ``Get String Safe: Fail by invalid field name``() =
    (fun () -> "select StringNull from temp" |> prepareReader |> fun reader -> reader.GetStringSafe("InvalidFieldName") |> ignore)
    |> should throw typeof<Exception>

    (fun () -> "select StringNull from temp" |> prepareReader |> fun reader -> reader.GetStringSafe("InvalidFieldName", String.Empty) |> ignore)
    |> should throw typeof<Exception>

[<Test>]
let ``Get String Safe: Null field value``() =
    "select StringNull from temp" |> prepareReader |> fun reader -> reader.GetStringSafe("StringNull")
    |> should be Null

[<Test>]
let ``Get String Safe: Null field value returning default value``() =
    "select StringNull from temp" |> prepareReader |> fun reader -> reader.GetStringSafe("StringNull", StringExpected.ToString())
    |> should equal StringExpected
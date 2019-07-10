module Maestria.Data.Extensions.Test.``Get Decimal``
open NUnit.Framework
open FsUnit
open Const
open System
open System.Data.SqlTypes
open Maestria.Data.Extensions
open FluentCast
open FakeDatabase

module ``Unsafe`` =
    [<Test>]
    let ``Get Decimal required: OK``() =
        "select NumericValue from temp" |> prepareReader |> fun reader -> reader.GetDecimal("NumericValue")
        |> should equal FloatingPointExpected

    [<Test>]
    let ``Get Decimal required: Fail by invalid field name``() =
        (fun () -> "select NumericNull from temp" |> prepareReader |> fun reader -> reader.GetDecimal("InvalidFieldName") |> ignore)
        |> should throw typeof<IndexOutOfRangeException>

    [<Test>]
    let ``Get Decimal required: fail by null field value``() =
        (fun () -> "select NumericNull from temp" |> prepareReader |> fun reader -> reader.GetDecimal("NumericNull") |> ignore)
        |> should throw typeof<SqlNullValueException>

    [<Test>]
    let ``Get Decimal from string field``() =
        "select StringNumber from temp"
        |> prepareReader
        |> fun reader -> reader.GetDecimal("StringNumber")
        |> should equal StringNumberToFloatingPointExpected

    [<Test>]
    let ``Get Decimal from string field (pt-Br)``() =
        "select StringNumberPtBr from temp"
        |> prepareReader
        |> fun reader -> reader.GetDecimal("StringNumberPtBr", CulturePtBr)
        |> should equal StringNumberToFloatingPointExpected

module ``Safe`` =
    [<Test>]
    let ``Get Decimal Safe: OK``() =
        "select NumericValue from temp" |> prepareReader |> fun reader -> reader.GetDecimalSafe("NumericValue")
        |> should equal FloatingPointExpected

    [<Test>]
    let ``Get Decimal Safe: Fail by invalid field name``() =
        (fun () -> "select NumericNull from temp" |> prepareReader |> fun reader -> reader.GetDecimalSafe("InvalidFieldName") |> ignore)
        |> should throw typeof<IndexOutOfRangeException>

        (fun () -> "select NumericNull from temp" |> prepareReader |> fun reader -> reader.GetDecimalSafe("InvalidFieldName", 0m) |> ignore)
        |> should throw typeof<IndexOutOfRangeException>

    [<Test>]
    let ``Get Decimal Safe: Null field value``() =
        "select NumericNull from temp" |> prepareReader |> fun reader -> reader.GetDecimalSafe("NumericNull")
        |> should be Null

    [<Test>]
    let ``Get Decimal Safe: Null field value returning default value``() =
        "select NumericNull from temp" |> prepareReader |> fun reader -> reader.GetDecimalSafe("NumericNull", FloatingPointExpected.ToDecimal())
        |> should equal FloatingPointExpected

    [<Test>]
    let ``Get Decimal Safe from string field``() =
        "select StringNumber from temp"
        |> prepareReader
        |> fun reader -> reader.GetDecimalSafe("StringNumber")
        |> should equal StringNumberToFloatingPointExpected

    [<Test>]
    let ``Get Decimal Safe from string field (pt-Br)``() =
        "select StringNumberPtBr from temp"
        |> prepareReader
        |> fun reader -> reader.GetDecimalSafe("StringNumberPtBr", CulturePtBr)
        |> should equal StringNumberToFloatingPointExpected
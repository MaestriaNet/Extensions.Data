module Maestria.Extensions.Data.Test.``Get Double``
open NUnit.Framework
open FsUnit
open Const
open System
open System.Data.SqlTypes
open Maestria.Extensions.Data
open FluentCast
open FakeDatabase

module ``Unsafe`` =
    [<Test>]
    let ``Get Double required: OK``() =
        "select NumericValue from temp" |> prepareReader |> fun reader -> reader.GetDouble("NumericValue")
        |> should equal FloatingPointExpected

    [<Test>]
    let ``Get Double required: Fail by invalid field name``() =
        (fun () -> "select NumericNull from temp" |> prepareReader |> fun reader -> reader.GetDouble("InvalidFieldName") |> ignore)
        |> should throw typeof<IndexOutOfRangeException>

    [<Test>]
    let ``Get Double required: fail by null field value``() =
        (fun () -> "select NumericNull from temp" |> prepareReader |> fun reader -> reader.GetDouble("NumericNull") |> ignore)
        |> should throw typeof<SqlNullValueException>

    [<Test>]
    let ``Get Double from string field``() =
        "select StringNumber from temp"
        |> prepareReader
        |> fun reader -> reader.GetDouble("StringNumber")
        |> should (equalWithin 0.0001) StringNumberToFloatingPointExpected

    [<Test>]
    let ``Get Double from string field pt-Br``() =
        "select StringNumberPtBr from temp"
        |> prepareReader
        |> fun reader -> reader.GetDouble("StringNumberPtBr", CulturePtBr)
        |> should (equalWithin 0.0001) StringNumberToFloatingPointExpected

module ``Safe`` =
    [<Test>]
    let ``Get Double Safe: OK``() =
        "select NumericValue from temp" |> prepareReader |> fun reader -> reader.GetDoubleSafe("NumericValue")
        |> should equal FloatingPointExpected

    [<Test>]
    let ``Get Double Safe: Fail by invalid field name``() =
        (fun () -> "select NumericNull from temp" |> prepareReader |> fun reader -> reader.GetDoubleSafe("InvalidFieldName") |> ignore)
        |> should throw typeof<IndexOutOfRangeException>

        (fun () -> "select NumericNull from temp" |> prepareReader |> fun reader -> reader.GetDoubleSafe("InvalidFieldName", 0.0) |> ignore)
        |> should throw typeof<IndexOutOfRangeException>

    [<Test>]
    let ``Get Double Safe: Null field value``() =
        "select NumericNull from temp" |> prepareReader |> fun reader -> reader.GetDoubleSafe("NumericNull")
        |> should be Null

    [<Test>]
    let ``Get Double Safe: Null field value returning default value``() =
        "select NumericNull from temp" |> prepareReader |> fun reader -> reader.GetDoubleSafe("NumericNull", FloatingPointExpected.ToDouble())
        |> should equal FloatingPointExpected

    [<Test>]
    let ``Get Double Safe from string field``() =
        "select StringNumber from temp"
        |> prepareReader
        |> fun reader -> reader.GetDoubleSafe("StringNumber")
        |> should (equalWithin 0.0001) StringNumberToFloatingPointExpected

    [<Test>]
    let ``Get Double Safe from string field pt-Br``() =
        "select StringNumberPtBr from temp"
        |> prepareReader
        |> fun reader -> reader.GetDoubleSafe("StringNumberPtBr", CulturePtBr)
        |> should (equalWithin 0.0001) StringNumberToFloatingPointExpected
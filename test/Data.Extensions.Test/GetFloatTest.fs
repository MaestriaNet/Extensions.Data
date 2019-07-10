module Maestria.Data.Extensions.Test.``Get Float``
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
    let ``Get Float required: OK``() =
        "select NumericValue from temp" |> prepareReader |> fun reader -> reader.GetFloat("NumericValue")
        |> should equal FloatingPointExpected

    [<Test>]
    let ``Get Float required: Fail by invalid field name``() =
        (fun () -> "select NumericNull from temp" |> prepareReader |> fun reader -> reader.GetFloat("InvalidFieldName") |> ignore)
        |> should throw typeof<IndexOutOfRangeException>

    [<Test>]
    let ``Get Float required: fail by null field value``() =
        (fun () -> "select NumericNull from temp" |> prepareReader |> fun reader -> reader.GetFloat("NumericNull") |> ignore)
        |> should throw typeof<SqlNullValueException>

    [<Test>]
    let ``Get Float from string field``() =
        "select StringNumber from temp"
        |> prepareReader
        |> fun reader -> reader.GetFloat("StringNumber")
        |> should (equalWithin 0.0001) StringNumberToFloatingPointExpected

    [<Test>]
    let ``Get Float from string field (pt-Br)``() =
        "select StringNumberPtBr from temp"
        |> prepareReader
        |> fun reader -> reader.GetFloat("StringNumberPtBr", CulturePtBr)
        |> should (equalWithin 0.0001) StringNumberToFloatingPointExpected

module ``Safe`` =
    [<Test>]
    let ``Get Float Safe: OK``() =
        "select NumericValue from temp" |> prepareReader |> fun reader -> reader.GetFloatSafe("NumericValue")
        |> should equal FloatingPointExpected

    [<Test>]
    let ``Get Float Safe: Fail by invalid field name``() =
        (fun () -> "select NumericNull from temp" |> prepareReader |> fun reader -> reader.GetFloatSafe("InvalidFieldName") |> ignore)
        |> should throw typeof<IndexOutOfRangeException>

        (fun () -> "select NumericNull from temp" |> prepareReader |> fun reader -> reader.GetFloatSafe("InvalidFieldName", 0.0f) |> ignore)
        |> should throw typeof<IndexOutOfRangeException>

    [<Test>]
    let ``Get Float Safe: Null field value``() =
        "select NumericNull from temp" |> prepareReader |> fun reader -> reader.GetFloatSafe("NumericNull")
        |> should be Null

    [<Test>]
    let ``Get Float Safe: Null field value returning default value``() =
        "select NumericNull from temp" |> prepareReader |> fun reader -> reader.GetFloatSafe("NumericNull", FloatingPointExpected.ToFloat())
        |> should equal FloatingPointExpected

    [<Test>]
    let ``Get Float Safe from string field``() =
        "select StringNumber from temp"
        |> prepareReader
        |> fun reader -> reader.GetFloatSafe("StringNumber")
        |> should (equalWithin 0.0001) StringNumberToFloatingPointExpected

    [<Test>]
    let ``Get Float Safe from string field (pt-Br)``() =
        "select StringNumberPtBr from temp"
        |> prepareReader
        |> fun reader -> reader.GetFloatSafe("StringNumberPtBr", CulturePtBr)
        |> should (equalWithin 0.0001) StringNumberToFloatingPointExpected
module Maestria.Data.Extensions.Test.``Get Int32``
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
    let ``Get Int32 required: OK``() =
        "select IntValue from temp" |> prepareReader |> fun reader -> reader.GetInt32("IntValue")
        |> should equal FixedPointExpected

    [<Test>]
    let ``Get Int32 required: Fail by invalid field name``() =
        (fun () -> "select IntNull from temp" |> prepareReader |> fun reader -> reader.GetInt32("InvalidFieldName") |> ignore)
        |> should throw typeof<IndexOutOfRangeException>

    [<Test>]
    let ``Get Int32 required: fail by null field value``() =
        (fun () -> "select IntNull from temp" |> prepareReader |> fun reader -> reader.GetInt32("IntNull") |> ignore)
        |> should throw typeof<SqlNullValueException>

    [<Test>]
    let ``Get Int32 from string field``() =
        "select StringNumber from temp"
        |> prepareReader
        |> fun reader -> reader.GetInt32("StringNumber")
        |> should equal StringNumberToFixedPointExpected

    [<Test>]
    let ``Get Int32 from string field (pt-Br)``() =
        "select StringNumberPtBr from temp"
        |> prepareReader
        |> fun reader -> reader.GetInt32("StringNumberPtBr", CulturePtBr)
        |> should equal StringNumberToFixedPointExpected

module ``Safe`` =
    [<Test>]
    let ``Get Int32 Safe: OK``() =
        "select IntValue from temp" |> prepareReader |> fun reader -> reader.GetInt32Safe("IntValue")
        |> should equal FixedPointExpected

    [<Test>]
    let ``Get Int32 Safe: Fail by invalid field name``() =
        (fun () -> "select IntNull from temp" |> prepareReader |> fun reader -> reader.GetInt32Safe("InvalidFieldName") |> ignore)
        |> should throw typeof<IndexOutOfRangeException>

        (fun () -> "select IntNull from temp" |> prepareReader |> fun reader -> reader.GetInt32Safe("InvalidFieldName", 0) |> ignore)
        |> should throw typeof<IndexOutOfRangeException>

    [<Test>]
    let ``Get Int32 Safe: Null field value``() =
        "select IntNull from temp" |> prepareReader |> fun reader -> reader.GetInt32Safe("IntNull")
        |> should be Null

    [<Test>]
    let ``Get Int32 Safe: Null field value returning default value``() =
        "select IntNull from temp" |> prepareReader |> fun reader -> reader.GetInt32Safe("IntNull", FixedPointExpected.ToInt32())
        |> should equal FixedPointExpected

    [<Test>]
    let ``Get Int32 Safe from string field``() =
        "select StringNumber from temp"
        |> prepareReader
        |> fun reader -> reader.GetInt32Safe("StringNumber")
        |> should equal StringNumberToFixedPointExpected

    [<Test>]
    let ``Get Int32 Safe from string field (pt-Br)``() =
        "select StringNumberPtBr from temp"
        |> prepareReader
        |> fun reader -> reader.GetInt32Safe("StringNumberPtBr", CulturePtBr)
        |> should equal StringNumberToFixedPointExpected
module Maestria.Extensions.Data.Test.``Get Int16``
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
    let ``Get Int16 required: OK``() =
        "select IntValue from temp"
        |> prepareReader
        |> fun reader -> reader.GetInt16("IntValue")
        |> should equal FixedPointExpected

    [<Test>]
    let ``Get Int16 required: Fail by invalid field name``() =
        (fun () -> "select IntNull from temp" |> prepareReader |> fun reader -> reader.GetInt16("InvalidFieldName") |> ignore)
        |> should throw typeof<IndexOutOfRangeException>

    [<Test>]
    let ``Get Int16 required: fail by null field value``() =
        (fun () -> "select IntNull from temp" |> prepareReader |> fun reader -> reader.GetInt16("IntNull") |> ignore)
        |> should throw typeof<SqlNullValueException>

    [<Test>]
    let ``Get Int16 from string field``() =
        "select StringNumber from temp"
        |> prepareReader
        |> fun reader -> reader.GetInt16("StringNumber")
        |> should equal StringNumberToFixedPointExpected

    [<Test>]
    let ``Get Int16 from string field pt-Br``() =
        "select StringNumberPtBr from temp"
        |> prepareReader
        |> fun reader -> reader.GetInt16("StringNumberPtBr", CulturePtBr)
        |> should equal StringNumberToFixedPointExpected

module ``Safe`` =
    [<Test>]
    let ``Get Int16 Safe: OK``() =
        "select IntValue from temp" |> prepareReader |> fun reader -> reader.GetInt16Safe("IntValue")
        |> should equal FixedPointExpected

    [<Test>]
    let ``Get Int16 Safe: Fail by invalid field name``() =
        (fun () -> "select IntNull from temp" |> prepareReader |> fun reader -> reader.GetInt16Safe("InvalidFieldName") |> ignore)
        |> should throw typeof<IndexOutOfRangeException>

        (fun () -> "select IntNull from temp" |> prepareReader |> fun reader -> reader.GetInt16Safe("InvalidFieldName", 0s) |> ignore)
        |> should throw typeof<IndexOutOfRangeException>

    [<Test>]
    let ``Get Int16 Safe: Null field value``() =
        "select IntNull from temp" |> prepareReader |> fun reader -> reader.GetInt16Safe("IntNull")
        |> should be Null

    [<Test>]
    let ``Get Int16 Safe: Null field value returning default value``() =
        "select IntNull from temp" |> prepareReader |> fun reader -> reader.GetInt16Safe("IntNull", FixedPointExpected.ToInt16())
        |> should equal FixedPointExpected

    [<Test>]
    let ``Get Int16 Safe from string field``() =
        "select StringNumber from temp"
        |> prepareReader
        |> fun reader -> reader.GetInt16Safe("StringNumber")
        |> should equal StringNumberToFixedPointExpected

    [<Test>]
    let ``Get Int16 Safe from string field pt-Br``() =
        "select StringNumberPtBr from temp"
        |> prepareReader
        |> fun reader -> reader.GetInt16Safe("StringNumberPtBr", CulturePtBr)
        |> should equal StringNumberToFixedPointExpected
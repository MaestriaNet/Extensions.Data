module Maestria.Data.Extensions.Test.``Get DateTime``
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
    let ``Get DateTime required: OK``() =
        "select DateValue from temp" |> prepareReader |> fun reader -> reader.GetDateTime("DateValue")
        |> should equal DateTimeExpected

    [<Test>]
    let ``Get DateTime required: Fail by invalid field name``() =
        (fun () -> "select DateNull from temp" |> prepareReader |> fun reader -> reader.GetDateTime("InvalidFieldName") |> ignore)
        |> should throw typeof<IndexOutOfRangeException>

    [<Test>]
    let ``Get DateTime required: fail by null field value``() =
        (fun () -> "select DateNull from temp" |> prepareReader |> fun reader -> reader.GetDateTime("DateNull") |> ignore)
        |> should throw typeof<SqlNullValueException>

    [<Test>]
    let ``Get DateTime from string field``() =
        "select StringDate from temp"
        |> prepareReader
        |> fun reader -> reader.GetDateTime("StringDate")
        |> should equal StringDateExpected

    [<Test>]
    let ``Get DateTime from string field pt-BR``() =
        "select StringDatePtBr from temp"
        |> prepareReader
        |> fun reader -> reader.GetDateTime("StringDatePtBr", CulturePtBr)
        |> should equal StringDateExpected

module ``Safe`` =
    [<Test>]
    let ``Get DateTime Safe: OK``() =
        "select DateValue from temp" |> prepareReader |> fun reader -> reader.GetDateTimeSafe("DateValue")
        |> should equal DateTimeExpected

    [<Test>]
    let ``Get DateTime Safe: Fail by invalid field name``() =
        (fun () -> "select DateNull from temp" |> prepareReader |> fun reader -> reader.GetDateTimeSafe("InvalidFieldName") |> ignore)
        |> should throw typeof<IndexOutOfRangeException>

        (fun () -> "select DateNull from temp" |> prepareReader |> fun reader -> reader.GetDateTimeSafe("InvalidFieldName", DateTime.Today) |> ignore)
        |> should throw typeof<IndexOutOfRangeException>

    [<Test>]
    let ``Get DateTime Safe: Null field value``() =
        "select DateNull from temp" |> prepareReader |> fun reader -> reader.GetDateTimeSafe("DateNull")
        |> should be Null

    [<Test>]
    let ``Get DateTime Safe: Null field value returning default value``() =
        "select DateNull from temp" |> prepareReader |> fun reader -> reader.GetDateTimeSafe("DateNull", DateTimeExpected.ToDateTime())
        |> should equal DateTimeExpected

    [<Test>]
    let ``Get DateTime Safe from string field``() =
        "select StringDate from temp"
        |> prepareReader
        |> fun reader -> reader.GetDateTimeSafe("StringDate")
        |> should equal StringDateExpected

    [<Test>]
    let ``Get DateTime Safe from string field pt-Br``() =
        "select StringDatePtBr from temp"
        |> prepareReader
        |> fun reader -> reader.GetDateTimeSafe("StringDatePtBr", CulturePtBr)
        |> should equal StringDateExpected
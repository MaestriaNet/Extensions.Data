module Maestria.Data.Extensions.Test.``Get DateTime``
open NUnit.Framework
open FsUnit
open Const
open System
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
        |> should throw typeof<Exception>

    [<Test>]
    let ``Get DateTime required: fail by null field value``() =
        (fun () -> "select DateNull from temp" |> prepareReader |> fun reader -> reader.GetDateTime("DateNull") |> ignore)
        |> should throw typeof<Exception>

module ``Safe`` =
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
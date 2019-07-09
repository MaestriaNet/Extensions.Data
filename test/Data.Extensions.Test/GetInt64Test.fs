module Maestria.Data.Extensions.Test.``Get Int64``
open NUnit.Framework
open FsUnit
open Const
open System
open Maestria.Data.Extensions
open FluentCast
open FakeDatabase

module ``Unsafe`` =
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

module ``Safe`` =
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
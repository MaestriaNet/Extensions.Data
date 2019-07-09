module Maestria.Data.Extensions.Test.``Get Int32``
open NUnit.Framework
open FsUnit
open Const
open System
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
        |> should throw typeof<Exception>

    [<Test>]
    let ``Get Int32 required: fail by null field value``() =
        (fun () -> "select IntNull from temp" |> prepareReader |> fun reader -> reader.GetInt32("IntNull") |> ignore)
        |> should throw typeof<Exception>

module ``Safe`` =
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

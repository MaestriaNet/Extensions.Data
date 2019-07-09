module Maestria.Data.Extensions.Test.``Get Double``
open NUnit.Framework
open FsUnit
open Const
open System
open Maestria.Data.Extensions
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
        |> should throw typeof<Exception>

    [<Test>]
    let ``Get Double required: fail by null field value``() =
        (fun () -> "select NumericNull from temp" |> prepareReader |> fun reader -> reader.GetDouble("NumericNull") |> ignore)
        |> should throw typeof<Exception>

module ``Safe`` =
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
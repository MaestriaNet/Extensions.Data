module Maestria.Data.Extensions.Test.``Get Decimal``
open NUnit.Framework
open FsUnit
open Const
open System
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
        |> should throw typeof<Exception>

    [<Test>]
    let ``Get Decimal required: fail by null field value``() =
        (fun () -> "select NumericNull from temp" |> prepareReader |> fun reader -> reader.GetDecimal("NumericNull") |> ignore)
        |> should throw typeof<Exception>

module ``Safe`` =
    [<Test>]
    let ``Get Decimal Safe: OK``() =
        "select NumericValue from temp" |> prepareReader |> fun reader -> reader.GetDecimalSafe("NumericValue")
        |> should equal FloatingPointExpected

    [<Test>]
    let ``Get Decimal Safe: Fail by invalid field name``() =
        (fun () -> "select NumericNull from temp" |> prepareReader |> fun reader -> reader.GetDecimalSafe("InvalidFieldName") |> ignore)
        |> should throw typeof<Exception>

        (fun () -> "select NumericNull from temp" |> prepareReader |> fun reader -> reader.GetDecimalSafe("InvalidFieldName", 0m) |> ignore)
        |> should throw typeof<Exception>

    [<Test>]
    let ``Get Decimal Safe: Null field value``() =
        "select NumericNull from temp" |> prepareReader |> fun reader -> reader.GetDecimalSafe("NumericNull")
        |> should be Null

    [<Test>]
    let ``Get Decimal Safe: Null field value returning default value``() =
        "select NumericNull from temp" |> prepareReader |> fun reader -> reader.GetDecimalSafe("NumericNull", FloatingPointExpected.ToDecimal())
        |> should equal FloatingPointExpected
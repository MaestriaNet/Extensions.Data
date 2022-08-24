module Maestria.Extensions.Data.Test.``Get String``
open NUnit.Framework
open FsUnit
open Const
open System
open System.Data.SqlTypes
open Maestria.Extensions.Data
open FakeDatabase

module ``Unsafe`` =
    [<Test>]
    let ``Get String required: OK``() =
        "select StringValue from temp" |> prepareReader |> fun reader -> reader.GetString("StringValue")
        |> should equal StringExpected

    [<Test>]
    let ``Get String required: Fail by invalid field name``() =
        (fun () -> "select StringNull from temp" |> prepareReader |> fun reader -> reader.GetString("InvalidFieldName") |> ignore)
        |> should throw typeof<IndexOutOfRangeException>

    [<Test>]
    let ``Get String required: fail by null field value``() =
        (fun () -> "select StringNull from temp" |> prepareReader |> fun reader -> reader.GetString("StringNull") |> ignore)
        |> should throw typeof<SqlNullValueException>

module ``Safe`` =
    [<Test>]
    let ``Get String Safe: OK``() =
        "select StringValue from temp" |> prepareReader |> fun reader -> reader.GetStringSafe("StringValue")
        |> should equal StringExpected

    [<Test>]
    let ``Get String Safe: Fail by invalid field name``() =
        (fun () -> "select StringNull from temp" |> prepareReader |> fun reader -> reader.GetStringSafe("InvalidFieldName") |> ignore)
        |> should throw typeof<IndexOutOfRangeException>

        (fun () -> "select StringNull from temp" |> prepareReader |> fun reader -> reader.GetStringSafe("InvalidFieldName", String.Empty) |> ignore)
        |> should throw typeof<IndexOutOfRangeException>

    [<Test>]
    let ``Get String Safe: Null field value``() =
        "select StringNull from temp" |> prepareReader |> fun reader -> reader.GetStringSafe("StringNull")
        |> should be Null

    [<Test>]
    let ``Get String Safe: Null field value returning default value``() =
        "select StringNull from temp" |> prepareReader |> fun reader -> reader.GetStringSafe("StringNull", StringExpected.ToString())
        |> should equal StringExpected
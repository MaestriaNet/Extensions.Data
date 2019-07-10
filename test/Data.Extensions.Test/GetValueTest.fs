module Maestria.Data.Extensions.Test.``Get Value``
open NUnit.Framework
open FsUnit
open Const
open System
open System.Data.SqlTypes
open Maestria.Data.Extensions
open FakeDatabase

module ``Unsafe`` =
    [<Test>]
    let ``Get Value required: OK``() =
        "select StringValue from temp" |> prepareReader |> fun reader -> reader.GetValue("StringValue")
        |> should equal StringExpected

    [<Test>]
    let ``Get Value required: Fail by invalid field name``() =
        (fun () -> "select StringNull from temp" |> prepareReader |> fun reader -> reader.GetValue("InvalidFieldName") |> ignore)
        |> should throw typeof<IndexOutOfRangeException>

    [<Test>]
    let ``Get Value required: fail by null field value``() =
        (fun () -> "select StringNull from temp" |> prepareReader |> fun reader -> reader.GetValue("StringNull") |> ignore)
        |> should throw typeof<SqlNullValueException>

module ``Safe`` =
    [<Test>]
    let ``Get Value Safe: OK``() =
        "select StringValue from temp" |> prepareReader |> fun reader -> reader.GetValueSafe("StringValue")
        |> should equal StringExpected

    [<Test>]
    let ``Get Value Safe: Fail by invalid field name``() =
        (fun () -> "select StringNull from temp" |> prepareReader |> fun reader -> reader.GetValueSafe("InvalidFieldName") |> ignore)
        |> should throw typeof<IndexOutOfRangeException>

        (fun () -> "select StringNull from temp" |> prepareReader |> fun reader -> reader.GetValueSafe("InvalidFieldName", String.Empty) |> ignore)
        |> should throw typeof<IndexOutOfRangeException>

    [<Test>]
    let ``Get Value Safe: Null field value``() =
        "select StringNull from temp" |> prepareReader |> fun reader -> reader.GetValueSafe("StringNull")
        |> should be Null

    [<Test>]
    let ``Get Value Safe: Null field value returning default value``() =
        "select StringNull from temp" |> prepareReader |> fun reader -> reader.GetValueSafe("StringNull", StringExpected.ToString())
        |> should equal StringExpected
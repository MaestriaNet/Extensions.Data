module Maestria.Extensions.Data.Test.Internal.``Data Reader Extensions``
open System
open NUnit.Framework
open FsUnit
open Maestria.Extensions.Data.Test.FakeDatabase
open Maestria.Extensions.Data.Internal

module ``Convert Safe`` =
    [<Test>]
    let ``Null value convert not throw exception``() = "select StringNull from temp"
                                                       |> prepareReader
                                                       |> fun reader -> DataReaderExtensions.GetValueSafe(reader, "StringNull", fun value -> Convert.ToInt32(value))
                                                       |> should be Null

    [<Test>]
    let ``Null value object convert not throw exception``() = "select StringNull from temp"
                                                           |> prepareReader
                                                           |> fun reader -> DataReaderExtensions.GetValueSafeObject(reader, "StringNull", fun value -> Convert.ToString(value))
                                                           |> should be Null
module Maestria.Data.Extensions.Test.FakeDatabase
open Maestria.Data.Extensions.Test
open System.Data.Common
open System.Data.SQLite
open NUnit.Framework
open Const

let mutable connection: SQLiteConnection = new SQLiteConnection("DataSource=:memory:")
let mutable cmd: SQLiteCommand = null
let mutable reader: DbDataReader = null

[<OneTimeSetUp>]
let prepareDb() =
    connection.Open()

    cmd <- connection.CreateCommand()
    cmd.CommandText <- "create table temp (IntValue int, IntNull int, NumericValue numeric(10,4), NumericNull numeric(10,4), DateValue Date, DateNull Date, StringValue varchar(20), StringNull varchar(20))"
    cmd.ExecuteNonQuery() |> ignore

    cmd.CommandText <- "insert into temp values(@int, null, @numeric, null, @date, null, @string, null)"
    cmd.Parameters.AddWithValue("@int", Const.FixedPointExpected) |> ignore
    cmd.Parameters.AddWithValue("@numeric", Const.FloatingPointExpected) |> ignore
    cmd.Parameters.AddWithValue("@date", DateTimeExpected) |> ignore
    cmd.Parameters.AddWithValue("@string", StringExpected) |> ignore
    cmd.ExecuteNonQuery() |> ignore

[<OneTimeTearDown>]
let closeDb() =
    reader.Close()
    connection.Close()

    reader.Dispose()
    cmd.Dispose()
    connection.Dispose()


[<TearDown>]
let tearDown() =
    reader.Close()

let prepareReader query =
    if (reader = null) then
        prepareDb()

    if (reader <> null) then
        reader.Close()

    cmd.CommandText <- query
    reader <- cmd.ExecuteReader()
    reader.Read() |> ignore
    reader
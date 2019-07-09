module Maestria.Data.Extensions.Test.Const
open System

/// ===========================================
/// Expected values
/// ===========================================
let FixedPointExpected: int64 = 16L
let FloatingPointExpected: decimal = 17.0001m
let DateTimeExpected = DateTime.Today
let StringExpected = "test value"
let StringNumberInput = "16.1"
let StringNumberToFixedPointExpected = 16
let StringNumberToFloatingPointExpected = 16.1
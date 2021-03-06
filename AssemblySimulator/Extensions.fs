﻿namespace Simulator

open System

    
module Utils =
    let mkString (seperator : string) toString array =
        array
        |> Array.fold (fun sum item -> sum + seperator + (toString item)) ""

[<AutoOpen>]
module TypeExtensions = 
    open System.Globalization
    open System.Text

    type String with
        member private str.FormatNumber() =
            if str.ToLower().StartsWith "x" then
                (NumberStyles.HexNumber, str.[1..])
            elif str.StartsWith "#" then
                (NumberStyles.Integer, str.[1..])
            else
                (NumberStyles.Integer, str)

        member str.IsInt16() : bool =
            let value = 0L
            let numberStyle, trimmedStr = str.FormatNumber()
            Int64.TryParse(trimmedStr, numberStyle, CultureInfo.InvariantCulture, ref value)
        
        member str.ToInt16() : Int16 =
            let numberStyle, trimmedStr = str.FormatNumber()
            let number = Int64.Parse(trimmedStr, numberStyle)
            let int16Number = int64(int16(number));
            if number <> int16Number then
                printfn "Overflow of %d" number
            int16(int16Number)

            
    type Char with
        member ch.ToAsciiInt16() : Int16 =
            int16(Encoding.ASCII.GetBytes([|ch|]).[0])


    type Int16 with
        member num.ToChar() : char =
            let byteValue = (byte)(num % 256s)
            Encoding.ASCII.GetChars([|byteValue|]).[0]



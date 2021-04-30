module Feliz.AgChart

open Fable.Core
open Fable.Core.JsInterop
open Feliz
open System.Collections.Generic


let agChart : obj = import "AgChartsReact " "ag-charts-react"
type ChartPosition = Bottom | Left | Right | Top
type MarkerShape =
    | Circle
    | Cross
    | Diamond
    | Plus
    | Square
    | Triangle
type SeriesKind = Line | Column | Area
[<RequireQualifiedAccess>]
type AxisKind = Category | Number | Time

[<Erase>]
type Axis =
    static member inline axisKind (axisKind:AxisKind) = "type" ==> axisKind.ToString().ToLower()
    static member inline position (v:ChartPosition) = "position" ==> v.ToString().ToLower()
    static member inline title (v:string) = "title" ==> {| enabled = true; text = v |}
    static member inline create v = createObj v
    static member inline label (rotation:int) = "label" ==> {| rotation = rotation |}
    static member inline min (value:int) = "min" ==> value

type IMarkerParams<'T> =
    { datum : 'T
      fill : string
      stroke : string
      strokeWidth : int
      size : int
      highlighted : bool
      xKey : string
      yKey : string }

type IMarkerReturn =
    {| fill : string
       stroke : string
       strokeWidth : int
       size : int |}

[<Erase>]
type Series =
    static member inline data (v:_ seq) = "data" ==> Seq.toArray v
    static member inline normalizedTo (v:int) = "normalizedTo" ==> v
    static member inline seriesKind (v:SeriesKind) = "type" ==> v.ToString().ToLower()
    static member inline xKey (v:string) = "xKey" ==> v
    static member inline xKey (v: 'a -> string) = "xKey" ==> v (unbox null)
    static member inline xName v = "xName" ==> v
    static member inline yKey (v:string) = "yKey" ==> v
    static member inline yName (v:string) = "yName" ==> v
    static member inline yKeys (v:string seq) = "yKeys" ==> Seq.toArray v
    static member inline yKeys (v:'a -> #seq<string>) = "yKeys" ==> (v(unbox null) |> Seq.toArray)
    static member inline yNames (v:string seq) = "yNames" ==> Seq.toArray v
    static member inline visible (v:bool) = "visible" ==> v
    static member inline showInLegend (v:bool) = "showInLegend" ==> v
    static member inline tooltipEnabled (v:bool) = "tooltipEnabled" ==> v
    static member inline label (v:bool) = "label" ==> {| enabled = v |}
    static member inline tooltipRenderer (f:obj -> string) = "tooltipRenderer" ==> f
    static member inline marker (v:MarkerShape, ?enabled:bool, ?formatter:IMarkerParams<_> -> IMarkerReturn)  = "marker" ==> {| enabled = defaultArg enabled true; shape = v.ToString().ToLower(); formatter = match formatter with Some x -> box x | None -> null |}
    static member inline fills (v:string seq) = "fills" ==> Seq.toArray v
    static member inline strokes (v:string seq) = "strokes" ==> v
    static member inline fillOpacity (v:float) = "fillOpacity" ==> v
    static member inline create v = createObj v

[<Erase>]
type AgChart =
    static member inline title (v:string) = "title" ==> {| text = v |}
    static member inline subtitle (v:string) = "subtitle" ==> {| text = v |}
    static member inline navigator = "navigator" ==> {| enabled = true |}
    static member inline width (v:int) = "width" ==> v
    static member inline height (v:int) = "height" ==> v
    static member inline autoSize = "autoSize" ==> true
    static member inline legend (enabled:bool, spacing:int, position:ChartPosition)  = "legend" ==> {| enabled = enabled; spacing = spacing; position = position.ToString().ToLower() |}
    static member inline data (v:_ seq) = "data" ==> Seq.toArray v

    static member inline series v = "series" ==> Seq.toArray v
    static member inline axes (v:_ seq) = "axes" ==> Seq.toArray v

    static member inline options value = prop.custom("options", createObj value)
    static member inline chart props = Interop.reactApi.createElement (agChart, createObj !!props)


module Index

open Elmish
open Fable.Remoting.Client
open Shared


type Model = { Todos: Todo list; Input: string }

type Msg =
    | GotTodos of Todo list
    | SetInput of string
    | AddTodo
    | AddedTodo of Todo

let todosApi =
    Remoting.createApi ()
    |> Remoting.withRouteBuilder Route.builder
    |> Remoting.buildProxy<ITodosApi>

let init () : Model * Cmd<Msg> =
    let model = { Todos = []; Input = "" }

    let cmd =
        Cmd.OfAsync.perform todosApi.getTodos () GotTodos

    model, cmd

let update (msg: Msg) (model: Model) : Model * Cmd<Msg> =
    match msg with
    | GotTodos todos -> { model with Todos = todos }, Cmd.none
    | SetInput value -> { model with Input = value }, Cmd.none
    | AddTodo ->
        let todo = Todo.create model.Input

        let cmd =
            Cmd.OfAsync.perform todosApi.addTodo todo AddedTodo

        { model with Input = "" }, cmd
    | AddedTodo todo ->
        { model with
              Todos = model.Todos @ [ todo ] },
        Cmd.none

open Feliz
open Feliz.Bulma
//open Feliz.AgChart


let view (model: Model) (dispatch: Msg -> unit) =
    Html.div [
        //AgChart.chart [
        //    AgChart.options [
        //        AgChart.title "Capacity Access Rate Curve"
        //        AgChart.data ([for i in 1..10 do ({| x = i; y = i*i |})])
        //        AgChart.height 500
        //        AgChart.width 1000
        //        AgChart.legend (false, 0, ChartPosition.Left)
        //        AgChart.series [
        //            Series.create [
        //                Series.xKey "x"
        //                Series.yKey "y"
        //                Series.yName "y"
        //                Series.label true
        //            ]
        //        ]
        //        AgChart.axes [
        //            Axis.create [
        //                Axis.axisKind AxisKind.Number
        //                Axis.position AgChart.Bottom
        //                Axis.title "Capacity Achieved"
        //            ]
        //            Axis.create [
        //                Axis.axisKind AxisKind.Number
        //                Axis.position AgChart.Left
        //                Axis.title "Cost in Local Currency"
        //                Axis.min 0
        //            ]
        //        ]
        //    ]
        //]
    ]
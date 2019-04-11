using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using StarshipTraveler.Model;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace StarshipTraveler.Components
{
    public class FlightNetwork : ComponentBase
    {
        [Inject]
        private HttpClient Http { get; set; }

        [Inject]
        private IFlightplan Flightplan { get; set; }

        private BasePoint[] Points { get; set; }

        private ConnectionWithBases[] Connections { get; set; }

        protected override async Task OnInitAsync()
        {
            var connectionTask = Http.GetJsonAsync<Connection[]>("/api/connections");
            var baseTask = Http.GetJsonAsync<Base[]>("/api/bases");
            await Task.WhenAll(connectionTask, baseTask);
            (Points, Connections) = Flightplan.PrepareFlightplan(connectionTask.Result, baseTask.Result, (250d, 250d));
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);
            var seq = 0;

            if (Points != null && Connections != null)
            {
                builder.OpenElement(seq, "svg");
                builder.AddAttribute(++seq, "class", "flightplan-svg");
                builder.AddAttribute(++seq, "viewBox", "0 0 600 600");
                builder.AddAttribute(++seq, "preserveAspectRatio", "xMinYMin");

                builder.OpenElement(++seq, "g");
                builder.AddAttribute(++seq, "class", "flightplan");

                foreach (var conn in Connections)
                {
                    builder.OpenElement(++seq, "path");
                    builder.AddAttribute(++seq, "d", $"M {conn.From.X},{conn.From.Y} C {conn.ControlPoints.pFromX},{conn.ControlPoints.pFromY} {conn.ControlPoints.pToX},{conn.ControlPoints.pToY} {conn.To.X},{conn.To.Y}");
                    builder.AddAttribute(++seq, "class", conn.Active ? "active" : "");
                    builder.CloseElement();
                }

                foreach (var point in Points)
                {
                    builder.OpenElement(++seq, "circle");
                    builder.AddAttribute(++seq, "r", 5);
                    builder.AddAttribute(++seq, "cx", point.X);
                    builder.AddAttribute(++seq, "cy", point.Y);
                    builder.AddAttribute(++seq, "class", "pulse-circle " + (point.Active ? "active" : "inactive"));
                    builder.CloseElement();

                    builder.OpenElement(++seq, "text");
                    builder.AddAttribute(++seq, "x", point.X + 10);
                    builder.AddAttribute(++seq, "y", point.Y + 10);
                    builder.AddContent(++seq, point.Base.Name);
                    builder.CloseElement();

                    builder.OpenElement(++seq, "circle");
                    builder.AddAttribute(++seq, "r", 5);
                    builder.AddAttribute(++seq, "fill", "white");
                    builder.AddAttribute(++seq, "cx", point.X);
                    builder.AddAttribute(++seq, "cy", point.Y);
                    builder.AddAttribute(++seq, "onmouseenter", EventCallback.Factory.Create<UIMouseEventArgs>(this, new Action(() => point.SetActive())));
                    builder.AddAttribute(++seq, "onmouseleave", EventCallback.Factory.Create<UIMouseEventArgs>(this, new Action(() => point.SetActive(false))));
                    builder.CloseElement();
                }

                builder.CloseElement();

                builder.CloseElement();
            }
        }
    }
}

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using StarshipTraveler.Model;
using System;
using System.Globalization;
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
            var connectionTask = Http.GetJsonAsync<Connection[]>("api/connections");
            var baseTask = Http.GetJsonAsync<Base[]>("api/bases");
            await Task.WhenAll(connectionTask, baseTask);
            (Points, Connections) = Flightplan.PrepareFlightplan(connectionTask.Result, baseTask.Result, (250d, 250d));
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);

            if (Points != null && Connections != null)
            {
                builder.OpenElement(1, "svg");
                builder.AddAttribute(2, "class", "flightplan-svg");
                builder.AddAttribute(3, "viewBox", "0 0 600 600");
                builder.AddAttribute(4, "preserveAspectRatio", "xMinYMin");

                builder.OpenElement(5, "g");
                builder.AddAttribute(6, "class", "flightplan");

                foreach (var conn in Connections)
                {
                    builder.OpenElement(7, "path");
                    builder.AddAttribute(8, "d", $"M {conn.From.X.ToString(CultureInfo.InvariantCulture)},{conn.From.Y.ToString(CultureInfo.InvariantCulture)} C {conn.ControlPoints.pFromX.ToString(CultureInfo.InvariantCulture)},{conn.ControlPoints.pFromY.ToString(CultureInfo.InvariantCulture)} {conn.ControlPoints.pToX.ToString(CultureInfo.InvariantCulture)},{conn.ControlPoints.pToY.ToString(CultureInfo.InvariantCulture)} {conn.To.X.ToString(CultureInfo.InvariantCulture)},{conn.To.Y.ToString(CultureInfo.InvariantCulture)}");
                    builder.AddAttribute(9, "class", conn.Active ? "active" : "");
                    builder.CloseElement();
                }

                foreach (var point in Points)
                {
                    builder.OpenElement(10, "circle");
                    builder.AddAttribute(11, "r", 5);
                    builder.AddAttribute(12, "cx", point.X.ToString(CultureInfo.InvariantCulture));
                    builder.AddAttribute(13, "cy", point.Y.ToString(CultureInfo.InvariantCulture));
                    builder.AddAttribute(14, "class", "pulse-circle " + (point.Active ? "active" : "inactive"));
                    builder.CloseElement();

                    builder.OpenElement(15, "text");
                    builder.AddAttribute(16, "x", point.X.ToString(CultureInfo.InvariantCulture) + 10);
                    builder.AddAttribute(17, "y", point.Y.ToString(CultureInfo.InvariantCulture) + 10);
                    builder.AddContent(18, point.Base.Name);
                    builder.CloseElement();

                    builder.OpenElement(19, "circle");
                    builder.AddAttribute(20, "r", 5);
                    builder.AddAttribute(21, "fill", "white");
                    builder.AddAttribute(22, "cx", point.X.ToString(CultureInfo.InvariantCulture));
                    builder.AddAttribute(23, "cy", point.Y.ToString(CultureInfo.InvariantCulture));
                    builder.AddAttribute(24, "onmouseenter", EventCallback.Factory.Create<UIMouseEventArgs>(this, new Action(() => point.SetActive())));
                    builder.AddAttribute(25, "onmouseleave", EventCallback.Factory.Create<UIMouseEventArgs>(this, new Action(() => point.SetActive(false))));
                    builder.CloseElement();
                }

                builder.CloseElement();

                builder.CloseElement();
            }
        }
    }
}

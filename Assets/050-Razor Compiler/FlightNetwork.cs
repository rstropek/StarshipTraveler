﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Components.Web;
using StarshipTraveler.Model;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;

namespace StarshipTraveler.Components
{
    public class FlightNetwork : ComponentBase
    {
        [Inject]
        private IFlightplan? Flightplan { get; set; }

        [Inject]
        private HttpClient? Http { get; set; }

        private BasePoint[]? Points { get; set; }

        private ConnectionWithBases[]? Connections { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (Http == null || Flightplan == null)
            {
                throw new InvalidOperationException("Properties not initialized by DI. Configuration error?");
            }

            var connectionTask = GetConnectionsAsync();
            var baseTask = GetBasisAsync();
            await Task.WhenAll(connectionTask, baseTask);
            (Points, Connections) = Flightplan.PrepareFlightplan(connectionTask.Result.ToArray(), baseTask.Result.ToArray(), (250d, 250d));
        }

        private async Task<Connection[]> GetConnectionsAsync()
        {
            var response = await Http!.GetAsync("http://localhost:5000/api/connections");
            var connectionsJson = await response.Content.ReadAsStringAsync();
            var connections = JsonSerializer.Deserialize<Connection[]>(connectionsJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            })!;
            return connections;
        }

        private async Task<Base[]> GetBasisAsync()
        {
            var response = await Http!.GetAsync("http://localhost:5000/api/bases");
            var basesJson = await response.Content.ReadAsStringAsync();
            var bases = JsonSerializer.Deserialize<Base[]>(basesJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            })!;
            return bases;
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
                    builder.AddAttribute(8, "d", $"M {conn?.From?.X.ToString(CultureInfo.InvariantCulture)},{conn?.From?.Y.ToString(CultureInfo.InvariantCulture)} C {conn?.ControlPoints.pFromX.ToString(CultureInfo.InvariantCulture)},{conn?.ControlPoints.pFromY.ToString(CultureInfo.InvariantCulture)} {conn?.ControlPoints.pToX.ToString(CultureInfo.InvariantCulture)},{conn?.ControlPoints.pToY.ToString(CultureInfo.InvariantCulture)} {conn?.To?.X.ToString(CultureInfo.InvariantCulture)},{conn?.To?.Y.ToString(CultureInfo.InvariantCulture)}");
                    builder.AddAttribute(9, "class", (conn != null && conn.Active) ? "active" : "");
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
                    builder.AddAttribute(16, "x", (point.X + 10).ToString(CultureInfo.InvariantCulture));
                    builder.AddAttribute(17, "y", (point.Y + 10).ToString(CultureInfo.InvariantCulture));
                    builder.AddContent(18, point?.Base?.Name);
                    builder.CloseElement();

                    builder.OpenElement(19, "circle");
                    builder.AddAttribute(20, "r", 5);
                    builder.AddAttribute(21, "fill", "white");
                    builder.AddAttribute(22, "cx", point?.X.ToString(CultureInfo.InvariantCulture));
                    builder.AddAttribute(23, "cy", point?.Y.ToString(CultureInfo.InvariantCulture));
                    builder.AddAttribute(24, "onmouseenter", EventCallback.Factory.Create<MouseEventArgs>(this, new Action(() => point?.SetActive())));
                    builder.AddAttribute(25, "onmouseleave", EventCallback.Factory.Create<MouseEventArgs>(this, new Action(() => point?.SetActive(false))));
                    builder.CloseElement();
                }

                builder.CloseElement();

                builder.CloseElement();
            }
        }
    }
}

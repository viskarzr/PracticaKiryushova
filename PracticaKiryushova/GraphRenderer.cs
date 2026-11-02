using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PracticaKiryushova
{
    public static class GraphRenderer
    {
        public static void DrawGraph(Canvas canvas, int[] data, Brush lineColor, string title = "")
        {
            canvas.Children.Clear();

            if (data == null || data.Length < 2) return;

            double width = canvas.ActualWidth > 0 ? canvas.ActualWidth : 300;
            double height = canvas.ActualHeight > 0 ? canvas.ActualHeight : 200;

            double marginLeft = 40;
            double marginBottom = 30;
            double marginTop = 20;
            double marginRight = 20;

            double plotWidth = width - marginLeft - marginRight;
            double plotHeight = height - marginBottom - marginTop;

            double minVal = data.Min();
            double maxVal = data.Max();
            double range = (maxVal - minVal == 0) ? 1 : (maxVal - minVal);

            var axisBrush = Brushes.Black;

            var lineY = new Line
            {
                X1 = marginLeft,
                Y1 = height - marginBottom,
                X2 = marginLeft,
                Y2 = marginTop,
                Stroke = axisBrush,
                StrokeThickness = 2
            };
            canvas.Children.Add(lineY);

            var lineX = new Line
            {
                X1 = marginLeft,
                Y1 = height - marginBottom,
                X2 = width - marginRight,
                Y2 = height - marginBottom,
                Stroke = axisBrush,
                StrokeThickness = 2
            };
            canvas.Children.Add(lineX);

            int steps = 5;
            double stepValue = range / steps;
            for (int i = 0; i <= steps; i++)
            {
                double currentVal = minVal + (i * stepValue);
                double percent = (currentVal - minVal) / range;
                double yPos = (height - marginBottom) - (percent * plotHeight);

                var tick = new Line
                {
                    X1 = marginLeft - 5,
                    Y1 = yPos,
                    X2 = marginLeft,
                    Y2 = yPos,
                    Stroke = Brushes.Gray,
                    StrokeThickness = 1
                };
                canvas.Children.Add(tick);

                var label = new TextBlock
                {
                    Text = currentVal.ToString("F1"),
                    FontSize = 10,
                    Foreground = Brushes.Black
                };
                Canvas.SetLeft(label, marginLeft - 35);
                Canvas.SetTop(label, yPos - 6);
                canvas.Children.Add(label);
            }

            var points = new PointCollection();
            for (int i = 0; i < data.Length; i++)
            {
                double x = marginLeft + (i * plotWidth) / (data.Length - 1);

                double percentY = (data[i] - minVal) / range;
                double y = (height - marginBottom) - (percentY * plotHeight);

                points.Add(new Point(x, y));

                var dot = new Ellipse
                {
                    Width = 4,
                    Height = 4,
                    Fill = lineColor
                };
                Canvas.SetLeft(dot, x - 2);
                Canvas.SetTop(dot, y - 2);
                canvas.Children.Add(dot);
            }

            var polyline = new Polyline
            {
                Points = points,
                Stroke = lineColor,
                StrokeThickness = 2,
                SnapsToDevicePixels = true
            };
            canvas.Children.Add(polyline);

            if (!string.IsNullOrEmpty(title))
            {
                var txt = new TextBlock { Text = title, FontWeight = FontWeights.Bold };
                Canvas.SetLeft(txt, marginLeft);
                Canvas.SetTop(txt, 0);
                canvas.Children.Add(txt);
            }
        }
    }
}

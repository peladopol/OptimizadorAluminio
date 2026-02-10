using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using OptimizadorAluminio.Domain.Entities;
using System.Collections.Generic;

namespace OptimizadorAluminio.UI.Controls;

public partial class CortesCanvas : UserControl
{
    private const double Escala = 0.08;      // mm → pixels
    private const double AltoBarra = 30;
    private const double SeparacionBarra = 20;

    public CortesCanvas()
    {
        InitializeComponent();
    }

    public void Dibujar(List<BarraCorte> barras)
    {
        RootCanvas.Children.Clear();

        double y = 10;

        foreach (var barra in barras)
        {
            DibujarBarra(barra, y);
            y += AltoBarra + SeparacionBarra;
        }

        RootCanvas.Height = y + 20;
    }

    private void DibujarBarra(BarraCorte barra, double y)
    {
        double x = 10;

        foreach (var tramo in barra.Tramos)
        {
            double ancho = tramo.LargoMM * Escala;

            // rectángulo del tramo
            var rect = new Rectangle
            {
                Width = ancho,
                Height = AltoBarra,
                Stroke = Brushes.Black,
                Fill = Brushes.LightGray
            };

            Canvas.SetLeft(rect, x);
            Canvas.SetTop(rect, y);
            RootCanvas.Children.Add(rect);

            // ángulo izquierdo
            DibujarAngulo(tramo.AnguloIzquierdo, x, y, true);

            // ángulo derecho
            DibujarAngulo(tramo.AnguloDerecho, x + ancho, y, false);

            x += ancho;
        }

        // sobrante
        if (barra.SobranteMM > 0)
        {
            var sobrante = new Rectangle
            {
                Width = barra.SobranteMM * Escala,
                Height = AltoBarra,
                Stroke = Brushes.Red,
                Fill = Brushes.Transparent,
                StrokeDashArray = new DoubleCollection { 4, 2 }
            };

            Canvas.SetLeft(sobrante, x);
            Canvas.SetTop(sobrante, y);
            RootCanvas.Children.Add(sobrante);
        }
    }

    private void DibujarAngulo(CorteAngulo angulo, double x, double y, bool izquierdo)
    {
        if (angulo == CorteAngulo.Grados90)
        {
            var linea = new Line
            {
                X1 = x,
                Y1 = y,
                X2 = x,
                Y2 = y + AltoBarra,
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };
            RootCanvas.Children.Add(linea);
        }
        else // 45°
        {
            var linea = new Line
            {
                X1 = x + (izquierdo ? 0 : -6),
                Y1 = y,
                X2 = x + (izquierdo ? 6 : 0),
                Y2 = y + AltoBarra,
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };
            RootCanvas.Children.Add(linea);
        }
    }
}

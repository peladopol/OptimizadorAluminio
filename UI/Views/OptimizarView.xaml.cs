using System;
using System.Collections.Generic;
using System.IO;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using OptimizadorAluminio.Domain.Entities;
using OptimizadorAluminio.Domain.Services;

namespace OptimizadorAluminio.UI.Views;

public partial class OptimizarView : UserControl
{
    private readonly FlujoPresupuestoService _flujo;

    public OptimizarView()
    {
        InitializeComponent();
        _flujo = new FlujoPresupuestoService();
    }

    /// <summary>
    /// Ejecuta la optimización y muestra el resultado en el canvas
    /// </summary>
    public void EjecutarOptimizacion(List<Tramo> tramos)
    {
        var resultado = _flujo.OptimizarCortes(tramos);
        CortesCanvas.Dibujar(resultado.Barras);
    }

    /// <summary>
    /// Evento del botón Imprimir
    /// </summary>
    private void BtnImprimir_Click(object sender, RoutedEventArgs e)
    {
        ImprimirA4("Plano de cortes – Taller");
    }

    /// <summary>
    /// Imprime el canvas en formato A4 horizontal
    /// </summary>
    private void ImprimirA4(string titulo)
    {
        var printDialog = new PrintDialog();

        printDialog.PrintTicket.PageOrientation = PageOrientation.Landscape;
        printDialog.PrintTicket.PageMediaSize =
            new PageMediaSize(PageMediaSizeName.ISOA4);

        if (printDialog.ShowDialog() != true)
            return;

        // Contenedor preparado para papel
        var printGrid = new Grid
        {
            Width = printDialog.PrintableAreaWidth,
            Background = Brushes.White
        };

        printGrid.RowDefinitions.Add(
            new RowDefinition { Height = GridLength.Auto });
        printGrid.RowDefinitions.Add(
            new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

        // Encabezado
        var header = new StackPanel
        {
            Margin = new Thickness(10)
        };

        header.Children.Add(new TextBlock
        {
            Text = titulo,
            FontSize = 16,
            FontWeight = FontWeights.Bold
        });

        header.Children.Add(new TextBlock
        {
            Text = $"Fecha: {DateTime.Now:dd/MM/yyyy HH:mm}",
            FontSize = 10
        });

        Grid.SetRow(header, 0);
        printGrid.Children.Add(header);

        // Clonar canvas para no tocar la UI
        var canvasClonado = ClonarVisual(CortesCanvas);

        Grid.SetRow(canvasClonado, 1);
        printGrid.Children.Add(canvasClonado);

        // Medir y acomodar para impresión
        printGrid.Measure(new Size(
            printDialog.PrintableAreaWidth,
            printDialog.PrintableAreaHeight));

        printGrid.Arrange(new Rect(
            new Point(0, 0),
            new Size(
                printDialog.PrintableAreaWidth,
                printDialog.PrintableAreaHeight)));

        printDialog.PrintVisual(printGrid, "Plano de cortes");
    }

    /// <summary>
    /// Clona un elemento visual vía XAML (vectorial, no raster)
    /// </summary>
    private UIElement ClonarVisual(UIElement original)
    {
        var xaml = System.Windows.Markup.XamlWriter.Save(original);

        using var stringReader = new StringReader(xaml);
        using var xmlReader = System.Xml.XmlReader.Create(stringReader);

        return (UIElement)System.Windows.Markup.XamlReader.Load(xmlReader);
    }
}

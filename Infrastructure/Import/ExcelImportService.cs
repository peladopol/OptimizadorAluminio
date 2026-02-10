using ClosedXML.Excel;
using OptimizadorAluminio.Domain.Entities;
using OptimizadorAluminio.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.IO;

namespace OptimizadorAluminio.Infrastructure.Import;

public class ExcelImportService
{
    public List<Tramo> ImportarTramos(string pathExcel)
    {
        if (!File.Exists(pathExcel))
            throw new FileNotFoundException("No se encontró el archivo Excel");

        var tramos = new List<Tramo>();

        using var wb = new XLWorkbook(pathExcel);
        var ws = wb.Worksheet(1);

        int fila = 2; // salta encabezado

        while (!ws.Cell(fila, 1).IsEmpty())
        {
            try
            {
                string ventana = ws.Cell(fila, 1).GetString().Trim();
                string codigoPerfil = ws.Cell(fila, 2).GetString().Trim();

                int largo = ws.Cell(fila, 3).GetValue<int>();
                int angIzq = ws.Cell(fila, 4).GetValue<int>();
                int angDer = ws.Cell(fila, 5).GetValue<int>();

                ValidarFila(fila, ventana, codigoPerfil, largo, angIzq, angDer);

                tramos.Add(new Tramo
                {
                    LargoMM = largo,
                    AnguloIzquierdo = ParseAngulo(angIzq),
                    AnguloDerecho = ParseAngulo(angDer)
                });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en fila {fila}: {ex.Message}");
            }

            fila++;
        }

        return tramos;
    }

    private static void ValidarFila(
        int fila,
        string ventana,
        string codigoPerfil,
        int largo,
        int angIzq,
        int angDer)
    {
        if (string.IsNullOrWhiteSpace(ventana))
            throw new Exception("Ventana vacía");

        if (string.IsNullOrWhiteSpace(codigoPerfil))
            throw new Exception("Código de perfil vacío");

        if (largo <= 0)
            throw new Exception("Largo inválido");

        if ((angIzq != 45 && angIzq != 90) ||
            (angDer != 45 && angDer != 90))
            throw new Exception("Ángulos deben ser 45 o 90");
    }

    private static CorteAngulo ParseAngulo(int angulo)
        => angulo == 45 ? CorteAngulo.Grados45 : CorteAngulo.Grados90;
}

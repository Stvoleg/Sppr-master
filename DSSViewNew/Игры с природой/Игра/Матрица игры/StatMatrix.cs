﻿using DSSLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSSView
{
    //Матрица статистической игры
    public class MtxStat : Matrix<Alternative, Case, double>
    {
        public override string ToString()
        {
            return $"{RowsCount}x{ColsCount} матрица статистической игры";
        }

        public MtxStat() : base(new MtxStatFactory())
        {

        }

        public static MtxStat CreateFromSize(int rows, int cols)
        {
            MtxStat newMtx = new MtxStat();
            for (int i = 0; i < rows - 1; i++)
            {
                newMtx.AddRowEnd();
            }
            for (int i = 0; i < cols - 1; i++)
            {
                newMtx.AddColEnd();
            }
            return newMtx;
        }
        public static MtxStat CreateFromXml(StatGameXml xml)
        {
            MtxStat mtx = new MtxStat();
            mtx.SetEmpty();
            for (int r = 0; r < xml.Values.Count; r++)
            {
                Alternative alt = xml.Alternatives[r];
                double[] row = xml.Values[r];
                for (int c = 0; c < row.Length; c++)
                {
                    Case cas = xml.Cases[c];
                    double val = row[c];
                    AltCase cell = new AltCase(Coords.Of(r, c), alt, cas, val);
                    mtx.AddCell(cell);
                }
            }
            return mtx;
        }
    }
}

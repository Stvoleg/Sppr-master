﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSSView
{
    public class CriteriaSavige : Criteria
    {
        public CriteriaSavige(IStatGame game) : base(game)
        {
            Name = "Критерий Сэвиджа";
            ChancesRequired = false;
            Description = "Критерий минимизации риска. Как и критерий Вальда, критерий Сэвиджа очень осторожен. Если критерий вальда это минимальный выигрыш, то Сэвидж определяет максимальную потерю выигрыша по сравнению с тем, чего можно было бы достичь в данных условиях";
            DecizionAlgoritm = "- Необходимо составить матрицу рисков на основе исходной\n- - Определить наилучшую эффективность каждого исхода\n- - Из наилучшей эффективности каждого исхода вычитается оригинальное значение матрицы\n- Определить наибольшие значения по строкам матрицы\n- Выбрать из них наименьшее значение и соотнести с альтернативой ";
        }

        protected override void Count()
        {
            double[] maxInRowsAfter = new double[(int)Rows];
            double[,] riscMatrix = Arr.GetRiscMatrix();
            for (int r = 0; r < Rows; r++)
            {
                double max = double.MinValue;
                for (int c = 0; c < Cols; c++)
                {
                    if (riscMatrix[r, c] > max)
                        max = riscMatrix[r, c];
                }
                maxInRowsAfter[r] = max;
            }
            SetResult(maxInRowsAfter.Min(), maxInRowsAfter);

            AddStep("Максимумы по строке в матрице рисков", maxInRowsAfter);
            AddStep("Минимум", Result);
        }
    }
}

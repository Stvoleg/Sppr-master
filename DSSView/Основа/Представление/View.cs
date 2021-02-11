﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DSSView
{
    /// <summary>
    /// Визуализированные и редактируемая матрица
    /// </summary>
    class PayMatrixView : NotifyObj
    {
        //Источник
        public PayMatrix Matrix { get; private set; }


        //Обновляемые объекты
        public MatrixView MatrixView { get; set; }

        public CriteriasReportView ReportCriterias { get; set; }
        public PayMatrixInfoView Information { get; set; }


        //Выбранная ячейка, строка, столбец
        public Cell SelectedCell
        {
            get => selectedCell;
            set
            {
                if(value != null)
                {
                    selectedCell = value;
                    OnPropertyChanged();
                }
            }
        }
        private Cell selectedCell;

        
        //Команды
        public RelayCommand AddColCommand { get; set; }
        public RelayCommand RemoveColCommand { get; set; }
        public RelayCommand AddRowCommand { get; set; }
        public RelayCommand RemoveRowCommand { get; set; }
        public RelayCommand AddSafeMatrixCommand { get; set; }



        private void AddCol(object obj)
        {
            Matrix.AddCol(Matrix.ColsLen);
            UpdateMatrix();
        }
        private void AddRow(object obj)
        {
            Matrix.AddRow(Matrix.RowsLen);
            UpdateMatrix();
        }
        private void RemoveCol(object obj)
        {
            Matrix.RemoveCol(SelectedCell.ColPosition);
            UpdateMatrix();
        }
        private void RemoveRow(object obj)
        {
            Matrix.RemoveRow(SelectedCell.RowPosition);
            UpdateMatrix();
        }

        private void UpdateMatrix()
        {
            OnPropertyChanged(nameof(Matrix));
        }


        public PayMatrixView(PayMatrix data)
        {
            AddColCommand = new RelayCommand(AddCol, obj => true);
            AddRowCommand = new RelayCommand(AddRow, obj => true);
            RemoveColCommand = new RelayCommand(RemoveCol, obj => SelectedCell != null && Matrix.ColsLen > 1);
            RemoveRowCommand = new RelayCommand(RemoveRow, obj => SelectedCell != null && Matrix.RowsLen > 1);
            AddSafeMatrixCommand = new RelayCommand(obj => View.Ex.AddSafeMatrix(new PayMatrixSafe(Matrix as PayMatrixRisc)), obj => !(Matrix is PayMatrixSafe));


            SetMatrix(data);
        }
        private void SetMatrix(PayMatrix data)
        {
            Matrix = data;
            MatrixView = new MatrixView(this);
            ReportCriterias = new CriteriasReportView(this);
            Information = new PayMatrixInfoView(this);


            UpdateMatrix();
            OnPropertyChanged(nameof(ReportCriterias));
            OnPropertyChanged(nameof(Information));
        }
    }


    class PayMatrixInfoView : NotifyObj
    {
        public PayMatrixView View { get; set; }

        //Источник
        public PayMatrix Matrix => View.Matrix;
        public InfoPayMatrix Info => View.Matrix.Info;


        public Alternative[] Alternatives => Info.Alternatives.ToArray();
        public CaseView[] Cases => Info.Cases.Select(c => new CaseView(View,c)).ToArray();


        //Изменяемые данные
        public string Conditions => Info.InUnknownConditions ? "условия неопределенности" : "условия риска";
        public bool AreChancesOk => Info.AreChancesOk;
        public double CaseChancesSum => Info.ChancesSum;
        public double DefaultChanceUnknown => Info.DefaultChance;

        //Команды
        public RelayCommand RecoundChancesC { get; set; }
        private void RecountChancesExe(object obj)
        {
            Cases.ToList().ForEach(c => { c.Chance = 0;});
            OnPropertyChanged(nameof(Cases));
        }



        public PayMatrixInfoView(PayMatrixView view)
        {
            View = view;
            Matrix.RowChanged += r => OnPropertyChanged(nameof(Alternatives));
            Matrix.ColChanged += c => OnPropertyChanged(nameof(Cases));
            Info.ChancesChanged += Source_ChancesChanged;
            RecoundChancesC = new RelayCommand(RecountChancesExe, obj => true);
            
        }
        private void Source_ChancesChanged()
        {
            OnPropertyChanged(nameof(AreChancesOk));
            OnPropertyChanged(nameof(CaseChancesSum));
            OnPropertyChanged(nameof(DefaultChanceUnknown));
            OnPropertyChanged(nameof(Conditions));
        }
    }


    //Матрица с объектами для строк и столбцов
    class MatrixView
    {
        public PayMatrixView View { get; set; }
        //Содержание матрицы
        public PayMatrix SourceMatrix => View.Matrix;

        public CellView[][] Cells
        {
            get
            {
                CellView[][] cells = new CellView[SourceMatrix.ColsLen][];
                for (int r = 0; r < SourceMatrix.ColsLen; r++)
                {
                    Cells[r] = new CellView[SourceMatrix.RowsLen];
                }

                for (int r = 0; r < SourceMatrix.RowsLen; r++)
                {
                    for (int c = 0; c < SourceMatrix.ColsLen; c++)
                    {
                        Cells[c][r] = new CellView(View,SourceMatrix.Cells[c][r] as Cell);
                    }
                }
                return cells;
            }
        }

        public MatrixView(PayMatrixView view)
        {
            View = view;
        }
    }


    class CriteriasReportView : NotifyObj
    {
        public PayMatrixView View { get; set; }

        //Источник
        public CriteriasReport Report => View.Matrix.Report;
        
        //Данные
        public CriteriaView[] Criterias { get; set; }
        
        //Данные
        public Alternative[] BestAlternatives => Report.BestAlternatives;
        public CriteriasPriorAlternative[] Priorities => Report.Priorities;


        public CriteriasReportView(PayMatrixView view)
        {
            View = view;
            Criterias = Report.Criterias.Select(c => new CriteriaView(view,c)).ToArray();
            Report.CriteriasUpdated += Report_Updated;

        }

        private void Report_Updated()
        {
            OnPropertyChanged(nameof(BestAlternatives));
            OnPropertyChanged(nameof(Priorities));
        }
    }
    class CriteriaView : NotifyObj
    {
        public PayMatrixView View { get; set; }


        //Источник
        public Criteria Criteria { get; set; }


        //Обновляемые данные
        public double Result => Criteria.Result;
        public Alternative[] Choices => Criteria.BestAlternatives;



        public CriteriaView(PayMatrixView view,Criteria criteria)
        {
            View = view;
            Criteria = criteria;
            Criteria.ResultChanged += Criteria_ResultChanged;
        }
        private void Criteria_ResultChanged(double result, Alternative[] alternatives)
        {
            OnPropertyChanged(nameof(Result));
            OnPropertyChanged(nameof(Choices));
        }
    }

    class CaseView : NotifyObj
    {
        public PayMatrixView View { get; set; }


        //Источник
        public InfoPayMatrix Info => View.Matrix.Info;
        public Case Case { get; set; }


        //Обновляемые даные
        public double Chance
        {
            get => Case.Chance;
            set
            {
                Case.Chance = value;
                OnPropertyChanged();
            }
        }
        public bool IsReadOnly => Info.InUnknownConditions;

        public CaseView(PayMatrixView view,Case case1)
        {
            View = view;
            Case = case1;
            Info.ChancesChanged += () => OnPropertyChanged(nameof(IsReadOnly));
        }
    }


    class CellView : NotifyObj
    {
        public PayMatrixView View { get; set; }

        //Источник
        public Cell Cell { get; set; }

        public bool IsEditable => View.Matrix is PayMatrixRisc;


        public CellView(PayMatrixView view,Cell cell)
        {
            View = view;
            Cell = cell;
        }
    }
}

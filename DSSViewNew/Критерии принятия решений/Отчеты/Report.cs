﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSSView
{
    public interface IReport
    {
        void Create();
        void Open();
    }

    public abstract class Report : IReport
    {
        protected StatGame Game { get; set; }
        protected DateTime Creation { get; set; }

        public Report(StatGame game)
        {
            Game = game;
        }
        public abstract void Create();
        public abstract void Open();

        public enum ReportType { Word, PDF, HTML, Excel }
        public static IReport GetReport(StatGame game, ReportType type)
        {
            Report report;
            switch (type)
            {
                case ReportType.Word:
                    report = new ReportWord(game);
                    break;
                case ReportType.PDF:
                    report = new ReportPDF(game);
                    break;
                case ReportType.HTML:
                    report = new ReportHTML(game);
                    break;
                case ReportType.Excel:
                    report = new ReportExcel(game);
                    break;
                default:
                    throw new Exception($"Отчет типа {type} не распознан");
            }
            return report;
        }
    }
}

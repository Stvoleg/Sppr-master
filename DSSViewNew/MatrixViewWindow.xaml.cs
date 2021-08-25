﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DSSView
{
    /// <summary>
    /// Логика взаимодействия для MatrixViewWindow.xaml
    /// </summary>
    public partial class MatrixViewWindow : Window
    {
        public MatrixViewWindow()
        {
            InitializeComponent();
            View.Matrix = DataContext as ViewMatrix;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MtxAlphaWindow w = new MtxAlphaWindow();
            w.ShowDialog();
        }
    }
}

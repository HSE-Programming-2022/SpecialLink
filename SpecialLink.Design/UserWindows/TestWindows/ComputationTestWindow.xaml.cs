﻿using SpecialLink.Core.Models.Tests;
using System;
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

namespace SpecialLink.Design.UserWindows.TestWindows
{
    /// <summary>
    /// Логика взаимодействия для ComputationTestWindow.xaml
    /// </summary>
    public partial class ComputationTestWindow : Window
    {
        Test _test;
        public ComputationTestWindow()
        {
            InitializeComponent();
        }

        public ComputationTestWindow(Test test)
        {
            InitializeComponent();
            _test = test;
        }
    }
}

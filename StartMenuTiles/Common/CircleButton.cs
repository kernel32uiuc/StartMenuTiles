﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace StartMenuTiles.Common
{
    class CircleButton : Canvas
    {
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(string), typeof(CircleButton), new PropertyMetadata(null, OnIconPropertyChanged));
        public static readonly DependencyProperty FontSizeProperty = DependencyProperty.Register("FontSize", typeof(double), typeof(CircleButton), new PropertyMetadata(34, OnFontSizePropertyChanged));
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(CircleButton), new PropertyMetadata(null));

        public string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        TextBlock m_circleText, m_iconText, m_bgText;

        public CircleButton()
        {
            PointerEntered += OnPointerEntered;
            PointerExited += OnPointerExited;
            PointerPressed += OnPointerPressed;
            PointerReleased += OnPointerReleased;

            m_circleText = new TextBlock();
            m_bgText = new TextBlock();
            m_iconText = new TextBlock();

            m_circleText.SizeChanged += OnTextBlockSizeChanged;

            m_circleText.FontFamily = new FontFamily("Segoe MDL2 Assets");
            m_bgText.FontFamily = m_circleText.FontFamily;
            m_iconText.FontFamily = m_circleText.FontFamily;
            m_iconText.HorizontalAlignment = HorizontalAlignment.Center;
            m_circleText.Text = "\uea3a";
            m_bgText.Text = "\uea3b";
            m_bgText.Foreground = new SolidColorBrush(Colors.Transparent);

            Children.Add(m_bgText);
            Children.Add(m_circleText);
            Children.Add(m_iconText);
        }

        private void OnPointerReleased(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            if (Command != null && Command.CanExecute(null))
                Command.Execute(null);
        }

        private void OnPointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            m_bgText.Foreground = new SolidColorBrush(Colors.White);
            m_iconText.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void OnPointerExited(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            m_bgText.Foreground = new SolidColorBrush(Colors.Transparent);
            m_iconText.Foreground = new SolidColorBrush(Colors.White);
        }

        private void OnPointerEntered(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            m_bgText.Foreground = new SolidColorBrush(Color.FromArgb(0x33, 0xff, 0xff, 0xff));
        }

        private void OnTextBlockSizeChanged(object sender, SizeChangedEventArgs e)
        {
            Width = m_circleText.ActualWidth;
            Height = m_circleText.ActualHeight;
            Canvas.SetLeft(m_iconText, (Width - m_iconText.ActualWidth) / 2);
            Canvas.SetTop(m_iconText, (Height - m_iconText.ActualHeight) / 2);
        }

        private static void OnIconPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (CircleButton)d;
            self.m_iconText.Text = self.Icon;
        }

        private static void OnFontSizePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (CircleButton)d;
            self.m_circleText.FontSize = self.FontSize;
            self.m_bgText.FontSize = self.FontSize;
            self.m_iconText.FontSize = self.FontSize * 0.6;
        }
    }
}

﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace WordWrappingRichTextBox.Views
{
    public class BindableRichTextBox : RichTextBox
    {
        #region Dependency Properties

        public static readonly DependencyProperty BindingDocumentProperty = DependencyProperty.Register("BindingDocument", typeof(FlowDocument),
            typeof(BindableRichTextBox), new UIPropertyMetadata(null, BindingDocumentChanged));

        public static readonly DependencyProperty WordWrappingProperty = DependencyProperty.Register("WordWrapping", typeof(bool),
            typeof(BindableRichTextBox), new UIPropertyMetadata(true, WordWrappingChanged));

        #endregion

        #region Properties

        public FlowDocument BindingDocument
        {
            get => (FlowDocument)GetValue(BindingDocumentProperty);
            set => SetValue(BindingDocumentProperty, value);
        }

        public bool WordWrapping
        {
            get => (bool)GetValue(WordWrappingProperty);
            set => SetValue(WordWrappingProperty, value);
        }

        #endregion

        #region Event Methods

        /// <summary>
        /// A callback function that is executed when the value of the BindingDocument is changed.
        /// </summary>
        /// <param name="sender">The object that issued the event.</param>
        /// <param name="e">Event parameter.</param>
        private static void BindingDocumentChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is not RichTextBox control || e.NewValue is not FlowDocument flowDocument)
                return;

            control.Document = flowDocument;
        }

        /// <summary>
        /// A callback function that is executed when the value of the WordWrapping is changed.
        /// </summary>
        /// <param name="sender">The object that issued the event</param>
        /// <param name="e">Event parameter.</param>
        private static void WordWrappingChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is BindableRichTextBox bindableRichTextBox)
            {
                ResizeDocument(bindableRichTextBox);
            }
        }

        #endregion
        
        /// <summary>
        /// Resizes the width of BindableRichTextBox according to the value of WordWrapping.
        /// </summary>
        /// <param name="control">BindableRichTextBox object</param>
        private static void ResizeDocument(BindableRichTextBox control)
        {
            if (control.WordWrapping)
            {
                control.Document.PageWidth = double.NaN;
            }
            else
            {
                control.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;

                var size = MeasureString(control);
                control.Document.PageWidth = size.Width + 15;
            }
        }

        /// <summary>
        /// Calculate text size on RichTextBox.
        /// </summary>
        /// <param name="control">RichTextBox object</param>
        /// <returns>Size of text on RichTextBox.</returns>
        private static System.Windows.Size MeasureString(RichTextBox control)
        {
            using var graphics = Graphics.FromHwnd(IntPtr.Zero);
            var dpiX = graphics.DpiX;

            var text = new TextRange(control.Document.ContentStart, control.Document.ContentEnd).Text;
            var formattedText = new FormattedText(text,
                CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                new Typeface(control.FontFamily, control.FontStyle, control.FontWeight, control.FontStretch),
                control.FontSize,
                System.Windows.Media.Brushes.Black,
                dpiX);

            return new System.Windows.Size(formattedText.Width, formattedText.Height);
        }

        public BindableRichTextBox()
        {
            TextChanged += (_, _) => ResizeDocument(this);
        }
    }
}

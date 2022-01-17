using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace CatchableScrollEventRichTextBox.Views.Controls
{
    public class CatchableScrollEventRichTextBox : RichTextBox
    {
        #region Dependency Properties

        public static readonly DependencyProperty ScrollEndedCommandProperty = DependencyProperty.Register("ScrollEndedCommand", typeof(ICommand),
            typeof(CatchableScrollEventRichTextBox), new UIPropertyMetadata(null));

        #endregion

        #region Properties

        public ICommand ScrollEndedCommand
        {
            get => (ICommand)GetValue(ScrollEndedCommandProperty);
            set => SetValue(ScrollEndedCommandProperty, value);
        }

        #endregion

        public CatchableScrollEventRichTextBox()
        {
            Loaded += (sender, args) =>
            {
                if (Template.FindName("PART_ContentHost", this) is not ScrollViewer scrollViewer)
                    return;

                var prevVerticalOffset = .0;
                var prevScrollableHeight = .0;
                scrollViewer.ScrollChanged += (_, _) =>
                {
                    if (prevVerticalOffset.Equals(scrollViewer.VerticalOffset) && prevScrollableHeight.Equals(scrollViewer.ScrollableHeight))
                        return;

                    prevVerticalOffset = scrollViewer.VerticalOffset;
                    prevScrollableHeight = scrollViewer.ScrollableHeight;

                    var isEnded = scrollViewer.VerticalOffset.Equals(scrollViewer.ScrollableHeight);
                    if (!isEnded)
                        return;

                    ScrollEndedCommand.Execute(Name);
                };
            };
        }
    }
}

using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CatchableScrollEventRichTextBox.Views.Behavior
{
    public class CatchableSrcollEventBehavior : Behavior<RichTextBox>
    {
        #region Dependency Properties

        public static readonly DependencyProperty ScrollEndedCommandProperty = DependencyProperty.Register("ScrollEndedCommand", typeof(ICommand),
            typeof(CatchableSrcollEventBehavior), new UIPropertyMetadata(null));

        #endregion

        #region Properties

        public ICommand ScrollEndedCommand
        {
            get => (ICommand)GetValue(ScrollEndedCommandProperty);
            set => SetValue(ScrollEndedCommandProperty, value);
        }

        #endregion

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.Loaded += Loaded;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.Loaded -= Loaded;
        }

        public void Loaded(object sender, RoutedEventArgs args)
        {
            if (AssociatedObject.Template.FindName("PART_ContentHost", AssociatedObject) is not ScrollViewer scrollViewer)
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

                ScrollEndedCommand.Execute(AssociatedObject.Name);
            };
        }
    }
}

using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using CatchableScrollEventRichTextBox.ViewModels;

namespace CatchableScrollEventRichTextBox.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            CodeBehindRichTextBox.Document = GenerateFlowDocument();
            CustomControlRichTextBox.Document = GenerateFlowDocument();
            BehaviorRichTextBox.Document = GenerateFlowDocument();

            Loaded += (sender, args) =>
            {
                if (CodeBehindRichTextBox.Template.FindName("PART_ContentHost", CodeBehindRichTextBox) is not
                    ScrollViewer scrollViewer)
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

                    if (DataContext is not MainWindowViewModel viewModel)
                        return;

                    viewModel.ReachedScrollEndCommand.Execute(CodeBehindRichTextBox.Name);
                };
            };
        }

        private FlowDocument GenerateFlowDocument()
        {
            var flowDocument = new FlowDocument();
            var blocks = new Block[]
            {
                new Paragraph(new Run("A")),
                new Paragraph(new Run("B")),
                new Paragraph(new Run("C")),
                new Paragraph(new Run("D")),
                new Paragraph(new Run("E")),
                new Paragraph(new Run("F")),
                new Paragraph(new Run("G")),
                new Paragraph(new Run("H")),
                new Paragraph(new Run("I")),
                new Paragraph(new Run("J")),
                new Paragraph(new Run("K")),
            };
            flowDocument.Blocks.AddRange(blocks);

            return flowDocument;
        }
    }
}

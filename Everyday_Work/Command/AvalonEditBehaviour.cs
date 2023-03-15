using ICSharpCode.AvalonEdit;
using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Everyday_Work.Command
{
    public sealed class AvalonEditBehaviour: Behavior<TextEditor>
    {
        public static readonly DependencyProperty InputTextProperty = DependencyProperty.Register("InputText", typeof(string), typeof(AvalonEditBehaviour), new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string InputText
        {
            get
            {
                return (string)GetValue(InputTextProperty);
            }
            set
            {
                SetValue(InputTextProperty, value);
            }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            if (AssociatedObject != null)
            {
                AssociatedObject.TextChanged += AssociatedObjectOnTextChanged;

                AssociatedObject.Text = InputText;
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            if (AssociatedObject != null)
            {
                AssociatedObject.TextChanged -= AssociatedObjectOnTextChanged;
            }
        }

        private void AssociatedObjectOnTextChanged(object sender, EventArgs eventArgs)
        {
            var textEditor = sender as TextEditor;
            if (textEditor != null)
            {
                if (textEditor.Document != null)
                {
                    InputText= textEditor.Document.Text;
                }
            }
        }

        public static string GetText(DependencyObject obj)
        {
            return (string)obj.GetValue(MyPropertyProperty);
        }

        public static void SetText(DependencyObject obj, string value)
        {
            obj.SetValue(MyPropertyProperty, value);
        }

        public static readonly DependencyProperty MyPropertyProperty =DependencyProperty.RegisterAttached("Text", typeof(string), typeof(AvalonEditBehaviour), new PropertyMetadata(null,new PropertyChangedCallback((s, e) =>
        {
            var text_ui = s as TextEditor;
            if (text_ui != null)
            {
               // text_ui.Document.Text = e.NewValue.ToString();
               // Console.WriteLine($"MyPropertyProperty:{text_ui.Text},e.NewValue {e.NewValue}");
            }
            else
            {
                Console.WriteLine($"MyPropertyProperty null");
            }
        })));
    }
}

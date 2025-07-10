using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media;
using Avalonia.Threading;

public class FontFallbackTestWindow : Window
{
    public FontFallbackTestWindow()
    {
        Width = 600;
        Height = 200;
        Content = new Canvas
        {
            Children =
            {
                new TextBlock
                {
                    Text = "English ← 中文 ܗ",
                    FontSize = 32,
                    Foreground = Brushes.Black,
                    // The key part: font fallback list
                    FontFamily = new FontFamily(
                        "Segoe UI, Segoe UI Symbol, Noto Sans, Noto Sans CJK SC, Noto Sans JP, Microsoft YaHei, Meiryo, Arial Unicode MS, Arial, Segoe UI Historic, Noto Sans Syriac, sans-serif"
                    ),
                    Margin = new Thickness(20)
                }
            }
        };
    }



}

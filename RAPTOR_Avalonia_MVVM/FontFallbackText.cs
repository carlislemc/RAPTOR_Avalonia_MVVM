using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Media;

namespace RAPTOR_Avalonia_MVVM
{
    public class FontFallbackText
    {
        private readonly string[] _fontFamilies;
        private readonly double _fontSize;
        private readonly FontStyle _fontStyle;
        private readonly FontWeight _fontWeight;
        private readonly TextAlignment _alignment;
        private readonly TextWrapping _wrapping;
        private readonly Size _constraint;

        public FontFallbackText(
            string fontFamilyList,
            double fontSize,
            FontStyle fontStyle,
            FontWeight fontWeight,
            TextAlignment alignment,
            TextWrapping wrapping,
            Size constraint)
        {
            _fontFamilies = fontFamilyList.Split(',');
            _fontSize = fontSize;
            _fontStyle = fontStyle;
            _fontWeight = fontWeight;
            _alignment = alignment;
            _wrapping = wrapping;
            _constraint = constraint;
        }

        private Typeface FindTypefaceForChar(char c)
        {
            foreach (var fam in _fontFamilies)
            {
                var typeface = new Typeface(fam.Trim(), _fontStyle, _fontWeight);
                try
                {
                    var glyphTypeface = typeface.GlyphTypeface;
                    if (glyphTypeface != null && glyphTypeface.GetGlyph((ushort)c) != 0)
                    {
                        return typeface;
                    }
                }
                catch
                {
                    // Ignore and try next font
                }
            }
            // fallback to first font
            return new Typeface(_fontFamilies[0].Trim(), _fontStyle, _fontWeight);
        }

        // Measures the bounds of the text, respecting the constraint width and wrapping
        public Rect MeasureText(string text)
        {
            if (string.IsNullOrEmpty(text))
                return Rect.Empty;

            double maxWidth = _constraint.Width;
            if (double.IsInfinity(maxWidth) || maxWidth <= 0)
                maxWidth = double.MaxValue;

            double x = 0, y = 0, lineHeight = 0, maxLineWidth = 0;
            int i = 0;
            List<(string run, Typeface typeface, double width, double height)> lineRuns = new();

            while (i < text.Length)
            {
                // Start a new line
                x = 0;
                lineHeight = 0;
                lineRuns.Clear();

                while (i < text.Length && text[i] != '\n')
                {
                    int runStart = i;
                    var typeface = FindTypefaceForChar(text[i]);
                    double runWidth = 0;
                    double runHeight = 0;
                    int runLen = 0;

                    // Build run of same typeface
                    while (i < text.Length && text[i] != '\n' && FindTypefaceForChar(text[i]).FontFamily.Name == typeface.FontFamily.Name)
                    {
                        var ft = new FormattedText(text[i].ToString(), typeface, _fontSize, _alignment, TextWrapping.NoWrap, Size.Infinity);
                        double charWidth = ft.Bounds.Width;
                        double charHeight = ft.Bounds.Height;

                        // If adding this char would exceed the line, break
                        if (x + runWidth + charWidth > maxWidth && runLen > 0)
                            break;

                        runWidth += charWidth;
                        runHeight = Math.Max(runHeight, charHeight);
                        runLen++;
                        i++;
                    }

                    string runText = text.Substring(runStart, runLen);
                    if (runLen > 0)
                    {
                        lineRuns.Add((runText, typeface, runWidth, runHeight));
                        x += runWidth;
                        lineHeight = Math.Max(lineHeight, runHeight);
                    }

                    // If next char would overflow, break line
                    if (i < text.Length && text[i] != '\n')
                    {
                        var ft = new FormattedText(text[i].ToString(), FindTypefaceForChar(text[i]), _fontSize, _alignment, TextWrapping.NoWrap, Size.Infinity);
                        if (x + ft.Bounds.Width > maxWidth)
                            break;
                    }
                }

                // Update max line width and total height
                double lineWidth = 0;
                foreach (var run in lineRuns)
                    lineWidth += run.width;
                maxLineWidth = Math.Max(maxLineWidth, lineWidth);
                y += lineHeight > 0 ? lineHeight : _fontSize;

                // Move to next line if there's a newline
                if (i < text.Length && text[i] == '\n')
                    i++;
            }

            return new Rect(0, 0, maxLineWidth, y);
        }

        // Draws the text, respecting the constraint width and wrapping
        public void DrawText(DrawingContext ctx, Point origin, string text, IBrush brush)
        {
            if (string.IsNullOrEmpty(text))
                return;

            double maxWidth = _constraint.Width;
            if (double.IsInfinity(maxWidth) || maxWidth <= 0)
                maxWidth = double.MaxValue;

            double x = origin.X, y = origin.Y, lineHeight = 0;
            int i = 0;
            List<(string run, Typeface typeface, double width, double height)> lineRuns = new();

            while (i < text.Length)
            {
                // Start a new line
                x = origin.X;
                lineHeight = 0;
                lineRuns.Clear();

                while (i < text.Length && text[i] != '\n')
                {
                    int runStart = i;
                    var typeface = FindTypefaceForChar(text[i]);
                    double runWidth = 0;
                    double runHeight = 0;
                    int runLen = 0;

                    // Build run of same typeface
                    while (i < text.Length && text[i] != '\n' && FindTypefaceForChar(text[i]).FontFamily.Name == typeface.FontFamily.Name)
                    {
                        var ft = new FormattedText(text[i].ToString(), typeface, _fontSize, _alignment, TextWrapping.NoWrap, Size.Infinity);
                        double charWidth = ft.Bounds.Width;
                        double charHeight = ft.Bounds.Height;

                        // If adding this char would exceed the line, break
                        if (x + runWidth + charWidth - origin.X > maxWidth && runLen > 0)
                            break;

                        runWidth += charWidth;
                        runHeight = Math.Max(runHeight, charHeight);
                        runLen++;
                        i++;
                    }

                    string runText = text.Substring(runStart, runLen);
                    if (runLen > 0)
                    {
                        lineRuns.Add((runText, typeface, runWidth, runHeight));
                        x += runWidth;
                        lineHeight = Math.Max(lineHeight, runHeight);
                    }

                    // If next char would overflow, break line
                    if (i < text.Length && text[i] != '\n')
                    {
                        var ft = new FormattedText(text[i].ToString(), FindTypefaceForChar(text[i]), _fontSize, _alignment, TextWrapping.NoWrap, Size.Infinity);
                        if (x + ft.Bounds.Width - origin.X > maxWidth)
                            break;
                    }
                }

                // Draw the line
                double runX = origin.X;
                foreach (var (run, typeface, runWidth, runHeight) in lineRuns)
                {
                    var ft = new FormattedText(run, typeface, _fontSize, _alignment, TextWrapping.NoWrap, Size.Infinity);
                    ctx.DrawText(brush, new Point(runX, y), ft);
                    runX += ft.Bounds.Width;
                }
                y += lineHeight > 0 ? lineHeight : _fontSize;

                // Move to next line if there's a newline
                if (i < text.Length && text[i] == '\n')
                    i++;
            }
        }
    }
}
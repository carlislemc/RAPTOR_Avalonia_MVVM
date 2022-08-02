using System;
using System.Collections;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Skia;
using RAPTOR_Avalonia_MVVM.Views;
using RAPTOR_Avalonia_MVVM.ViewModels;
using NetCoreAudio;
using SkiaSharp;
using Control = Avalonia.Controls.Control;
using KeyEventArgs = Avalonia.Input.KeyEventArgs;
using Timer = System.Timers.Timer;
using Avalonia.Threading;
using raptor;

namespace RAPTOR_Avalonia_MVVM.Controls
{
    public enum Color_Type
    {
        Black,
        Blue,
        Green,
        Cyan,
        Red,
        Magenta,
        Brown,
        Light_Gray,
        Dark_Gray,
        Light_Blue,
        Light_Green,
        Light_Cyan,
        Light_Red,
        Light_Magenta,
        Yellow,
        Pink,
        Purple,
        White,
        RGB_00_00_33,
        RGB_00_00_66,
        RGB_00_00_CC,
        RGB_00_33_00,
        RGB_00_33_33,
        RGB_00_33_66,
        RGB_00_33_99,
        RGB_00_33_CC,
        RGB_00_33_FF,
        RGB_00_66_00,
        RGB_00_66_33,
        RGB_00_66_66,
        RGB_00_66_99,
        RGB_00_66_CC,
        RGB_00_66_FF,
        RGB_00_99_00,
        RGB_00_99_33,
        RGB_00_99_66,
        RGB_00_99_CC,
        RGB_00_99_FF,
        RGB_00_CC_00,
        RGB_00_CC_33,
        RGB_00_CC_66,
        RGB_00_CC_99,
        RGB_00_CC_CC,
        RGB_00_CC_FF,
        RGB_00_FF_33,
        RGB_00_FF_66,
        RGB_00_FF_99,
        RGB_00_FF_CC,
        RGB_33_00_00,
        RGB_33_00_33,
        RGB_33_00_66,
        RGB_33_00_99,
        RGB_33_00_CC,
        RGB_33_00_FF,
        RGB_33_33_00,
        RGB_33_33_33,
        RGB_33_33_66,
        RGB_33_33_99,
        RGB_33_33_CC,
        RGB_33_33_FF,
        RGB_33_66_00,
        RGB_33_66_33,
        RGB_33_66_66,
        RGB_33_66_99,
        RGB_33_66_CC,
        RGB_33_66_FF,
        RGB_33_99_00,
        RGB_33_99_33,
        RGB_33_99_66,
        RGB_33_99_99,
        RGB_33_99_CC,
        RGB_33_99_FF,
        RGB_33_CC_00,
        RGB_33_CC_33,
        RGB_33_CC_66,
        RGB_33_CC_99,
        RGB_33_CC_CC,
        RGB_33_CC_FF,
        RGB_33_FF_00,
        RGB_33_FF_33,
        RGB_33_FF_66,
        RGB_33_FF_99,
        RGB_33_FF_CC,
        RGB_33_FF_FF,
        RGB_66_00_00,
        RGB_66_00_33,
        RGB_66_00_66,
        RGB_66_00_99,
        RGB_66_00_CC,
        RGB_66_00_FF,
        RGB_66_33_00,
        RGB_66_33_33,
        RGB_66_33_66,
        RGB_66_33_99,
        RGB_66_33_CC,
        RGB_66_33_FF,
        RGB_66_66_00,
        RGB_66_66_33,
        RGB_66_66_66,
        RGB_66_66_99,
        RGB_66_66_CC,
        RGB_66_66_FF,
        RGB_66_99_00,
        RGB_66_99_33,
        RGB_66_99_66,
        RGB_66_99_99,
        RGB_66_99_CC,
        RGB_66_99_FF,
        RGB_66_CC_00,
        RGB_66_CC_33,
        RGB_66_CC_66,
        RGB_66_CC_99,
        RGB_66_CC_CC,
        RGB_66_CC_FF,
        RGB_66_FF_00,
        RGB_66_FF_33,
        RGB_66_FF_66,
        RGB_66_FF_99,
        RGB_66_FF_CC,
        RGB_66_FF_FF,
        RGB_99_00_00,
        RGB_99_00_33,
        RGB_99_00_66,
        RGB_99_00_99,
        RGB_99_00_CC,
        RGB_99_00_FF,
        RGB_99_33_00,
        RGB_99_33_33,
        RGB_99_33_66,
        RGB_99_33_99,
        RGB_99_33_CC,
        RGB_99_33_FF,
        RGB_99_66_00,
        RGB_99_66_33,
        RGB_99_66_66,
        RGB_99_66_99,
        RGB_99_66_CC,
        RGB_99_66_FF,
        RGB_99_99_33,
        RGB_99_99_66,
        RGB_99_99_99,
        RGB_99_99_CC,
        RGB_99_99_FF,
        RGB_99_CC_00,
        RGB_99_CC_33,
        RGB_99_CC_66,
        RGB_99_CC_99,
        RGB_99_CC_CC,
        RGB_99_CC_FF,
        RGB_99_FF_00,
        RGB_99_FF_33,
        RGB_99_FF_66,
        RGB_99_FF_99,
        RGB_99_FF_CC,
        RGB_99_FF_FF,
        RGB_CC_00_00,
        RGB_CC_00_33,
        RGB_CC_00_66,
        RGB_CC_00_99,
        RGB_CC_00_CC,
        RGB_CC_00_FF,
        RGB_CC_33_00,
        RGB_CC_33_33,
        RGB_CC_33_66,
        RGB_CC_33_99,
        RGB_CC_33_CC,
        RGB_CC_33_FF,
        RGB_CC_66_00,
        RGB_CC_66_33,
        RGB_CC_66_66,
        RGB_CC_66_99,
        RGB_CC_66_CC,
        RGB_CC_66_FF,
        RGB_CC_99_00,
        RGB_CC_99_33,
        RGB_CC_99_66,
        RGB_CC_99_99,
        RGB_CC_99_CC,
        RGB_CC_99_FF,
        RGB_CC_CC_00,
        RGB_CC_CC_33,
        RGB_CC_CC_66,
        RGB_CC_CC_99,
        RGB_CC_CC_CC,
        RGB_CC_CC_FF,
        RGB_CC_FF_00,
        RGB_CC_FF_33,
        RGB_CC_FF_66,
        RGB_CC_FF_99,
        RGB_CC_FF_CC,
        RGB_CC_FF_FF,
        RGB_FF_00_33,
        RGB_FF_00_66,
        RGB_FF_00_CC,
        RGB_FF_33_00,
        RGB_FF_33_33,
        RGB_FF_33_66,
        RGB_FF_33_99,
        RGB_FF_33_CC,
        RGB_FF_33_FF,
        RGB_FF_66_00,
        RGB_FF_66_33,
        RGB_FF_66_66,
        RGB_FF_66_99,
        RGB_FF_66_CC,
        RGB_FF_66_FF,
        RGB_FF_99_33,
        RGB_FF_99_66,
        RGB_FF_99_99,
        RGB_FF_99_CC,
        RGB_FF_99_FF,
        RGB_FF_CC_00,
        RGB_FF_CC_33,
        RGB_FF_CC_66,
        RGB_FF_CC_99,
        RGB_FF_CC_CC,
        RGB_FF_CC_FF,
        RGB_FF_FF_33,
        RGB_FF_FF_66,
        RGB_FF_FF_99,
        RGB_FF_FF_CC,
        RGB_0D_0D_0D,
        RGB_14_14_14,
        RGB_1A_1A_1A,
        RGB_27_27_27,
        RGB_3B_3B_3B,
        RGB_42_42_42,
        RGB_55_55_55,
        RGB_5C_5C_5C,
        RGB_69_69_69,
        RGB_76_76_76,
        RGB_7C_7C_7C,
        RGB_83_83_83,
        RGB_8A_8A_8A,
        RGB_A4_A4_A4,
        RGB_B1_B1_B1,
        RGB_B7_B7_B7,
        RGB_BE_BE_BE,
        RGB_C5_C5_C5,
        RGB_D2_D2_D2,
        RGB_DA_DA_DA,
        RGB_E5_E5_E5,
        RGB_EC_EC_EC,
        RGB_F5_F5_F5
    }

    public enum Mouse_Button
    {
        Left_Button,
        Right_Button
    }

    public enum Event_Type
    {
        None,
        Moved,
        Left_Up,
        Left_Down,
        Right_Up,
        Right_Down
    }

    public class MyRect
    {
        public int X, Y, Width, Height;
    }

    internal class DotnetGraphControl : Control
    {
        // I originally had the idea of making the BMP bigger than the screen,
        // but then flood fill goes off the visible window and does weird stuff
        public const int slop = 0;
        private const int default_font_size = 14;
        private const int NUM_BASIC_COLORS = 18;
        public static DotnetGraphControl dngw;
        private static DotnetGraph dotnetgraph;
        private static Player loopPlayer;
        //public static dotnetgraph form;
        //private static SKBrush[] brushes;
        //private static SKPen[] pens;
        //private static Font[] fonts;
        private static SKPaint[] paints;
        public static bool start_topmost = false;

        private static readonly uint[] standard_color =
        {
            0xFF000000, /* Black         */
            0xFF000080, /* Blue          */
            0xFF008000, /* Green         */
            0xFF008080, /* Cyan          */
            0xFF800000, /* Red           */
            0xFF800080, /* Magenta       */
            0xFF808000, /* Brown         */
            0xFFACACAC, /* Light_Gray    */
            0xFF4F4F4F, /* Dark_Gray     */
            0xFF0000FF, /* Light_Blue    */
            0xFF00FF00, /* Light_Green   */
            0xFF00FFFF, /* Light_Cyan    */
            0xFFFF0000, /* Light_Red     */
            0xFFFF00FF, /* Light_Magenta */
            0xFFFFFF00, /* Yellow        */
            0xFFFFC0CB, /* Pink          */
            0xFF800080, /* Purple        */
            0xFFFFFFFF, /* White         */
            0xFF000033,
            0xFF000066,
            0xFF0000CC,
            0xFF003300,
            0xFF003333,
            0xFF003366,
            0xFF003399,
            0xFF0033CC,
            0xFF0033FF,
            0xFF006600,
            0xFF006633,
            0xFF006666,
            0xFF006699,
            0xFF0066CC,
            0xFF0066FF,
            0xFF009900,
            0xFF009933,
            0xFF009966,
            0xFF0099CC,
            0xFF0099FF,
            0xFF00CC00,
            0xFF00CC33,
            0xFF00CC66,
            0xFF00CC99,
            0xFF00CCCC,
            0xFF00CCFF,
            0xFF00FF33,
            0xFF00FF66,
            0xFF00FF99,
            0xFF00FFCC,
            0xFF330000,
            0xFF330033,
            0xFF330066,
            0xFF330099,
            0xFF3300CC,
            0xFF3300FF,
            0xFF333300,
            0xFF333333,
            0xFF333366,
            0xFF333399,
            0xFF3333CC,
            0xFF3333FF,
            0xFF336600,
            0xFF336633,
            0xFF336666,
            0xFF336699,
            0xFF3366CC,
            0xFF3366FF,
            0xFF339900,
            0xFF339933,
            0xFF339966,
            0xFF339999,
            0xFF3399CC,
            0xFF3399FF,
            0xFF33CC00,
            0xFF33CC33,
            0xFF33CC66,
            0xFF33CC99,
            0xFF33CCCC,
            0xFF33CCFF,
            0xFF33FF00,
            0xFF33FF33,
            0xFF33FF66,
            0xFF33FF99,
            0xFF33FFCC,
            0xFF33FFFF,
            0xFF660000,
            0xFF660033,
            0xFF660066,
            0xFF660099,
            0xFF6600CC,
            0xFF6600FF,
            0xFF663300,
            0xFF663333,
            0xFF663366,
            0xFF663399,
            0xFF6633CC,
            0xFF6633FF,
            0xFF666600,
            0xFF666633,
            0xFF666666,
            0xFF666699,
            0xFF6666CC,
            0xFF6666FF,
            0xFF669900,
            0xFF669933,
            0xFF669966,
            0xFF669999,
            0xFF6699CC,
            0xFF6699FF,
            0xFF66CC00,
            0xFF66CC33,
            0xFF66CC66,
            0xFF66CC99,
            0xFF66CCCC,
            0xFF66CCFF,
            0xFF66FF00,
            0xFF66FF33,
            0xFF66FF66,
            0xFF66FF99,
            0xFF66FFCC,
            0xFF66FFFF,
            0xFF990000,
            0xFF990033,
            0xFF990066,
            0xFF990099,
            0xFF9900CC,
            0xFF9900FF,
            0xFF993300,
            0xFF993333,
            0xFF993366,
            0xFF993399,
            0xFF9933CC,
            0xFF9933FF,
            0xFF996600,
            0xFF996633,
            0xFF996666,
            0xFF996699,
            0xFF9966CC,
            0xFF9966FF,
            0xFF999933,
            0xFF999966,
            0xFF999999,
            0xFF9999CC,
            0xFF9999FF,
            0xFF99CC00,
            0xFF99CC33,
            0xFF99CC66,
            0xFF99CC99,
            0xFF99CCCC,
            0xFF99CCFF,
            0xFF99FF00,
            0xFF99FF33,
            0xFF99FF66,
            0xFF99FF99,
            0xFF99FFCC,
            0xFF99FFFF,
            0xFFCC0000,
            0xFFCC0033,
            0xFFCC0066,
            0xFFCC0099,
            0xFFCC00CC,
            0xFFCC00FF,
            0xFFCC3300,
            0xFFCC3333,
            0xFFCC3366,
            0xFFCC3399,
            0xFFCC33CC,
            0xFFCC33FF,
            0xFFCC6600,
            0xFFCC6633,
            0xFFCC6666,
            0xFFCC6699,
            0xFFCC66CC,
            0xFFCC66FF,
            0xFFCC9900,
            0xFFCC9933,
            0xFFCC9966,
            0xFFCC9999,
            0xFFCC99CC,
            0xFFCC99FF,
            0xFFCCCC00,
            0xFFCCCC33,
            0xFFCCCC66,
            0xFFCCCC99,
            0xFFCCCCCC,
            0xFFCCCCFF,
            0xFFCCFF00,
            0xFFCCFF33,
            0xFFCCFF66,
            0xFFCCFF99,
            0xFFCCFFCC,
            0xFFCCFFFF,
            0xFFFF0033,
            0xFFFF0066,
            0xFFFF00CC,
            0xFFFF3300,
            0xFFFF3333,
            0xFFFF3366,
            0xFFFF3399,
            0xFFFF33CC,
            0xFFFF33FF,
            0xFFFF6600,
            0xFFFF6633,
            0xFFFF6666,
            0xFFFF6699,
            0xFFFF66CC,
            0xFFFF66FF,
            0xFFFF9933,
            0xFFFF9966,
            0xFFFF9999,
            0xFFFF99CC,
            0xFFFF99FF,
            0xFFFFCC00,
            0xFFFFCC33,
            0xFFFFCC66,
            0xFFFFCC99,
            0xFFFFCCCC,
            0xFFFFCCFF,
            0xFFFFFF33,
            0xFFFFFF66,
            0xFFFFFF99,
            0xFFFFFFCC,
            0xFF0D0D0D,
            0xFF141414,
            0xFF1A1A1A,
            0xFF272727,
            0xFF3B3B3B,
            0xFF424242,
            0xFF555555,
            0xFF5C5C5C,
            0xFF696969,
            0xFF767676,
            0xFF7C7C7C,
            0xFF838383,
            0xFF8A8A8A,
            0xFFA4A4A4,
            0xFFB1B1B1,
            0xFFB7B7B7,
            0xFFBEBEBE,
            0xFFC5C5C5,
            0xFFD2D2D2,
            0xFFDADADA,
            0xFFE5E5E5,
            0xFFECECEC,
            0xFFF5F5F5
        };

        private static readonly int MAX_COLORS = standard_color.Length;

        //This is where we keep the bitmaps that comprise individual layers
        //we use to composite what we present to the user
        private int ActiveLayer;
        private ArrayList bitmaps = new ArrayList();
        private int click_x, click_y;
        private int current_font_size = default_font_size;

        public static int xCord;
        public static int yCord;

        public static int xLoc;
        public static int yLoc;

        private readonly MyRect draw_rect = new MyRect();
        private bool frozen;
        private bool[] key_is_down = new bool[255];
        private int left_click = 0;
        private int left_click_x, left_click_y;
        private bool left_is_down = false;
        private bool looping = false;
        private static bool graphWindowOpen = false;
        public static MouseButton mb;
        private Point mouse;
        private readonly Player player = new Player();
        private bool playInBackground;
        private char pressed_key;


        //Our render target we compile everything to and present to the user
        private RenderTargetBitmap RenderTarget;
        private int right_click_x, right_click_y;
        private bool right_is_down = false;

        //Reference to the currently active drawing tool
        //Should have a OnToolChange Event
        private SKPaint SKBrush;
        private ISkiaDrawingContextImpl SkiaContext;
        private string soundFilePath;
        public static bool waitForKey;
        public static bool waitForMouse;
        private int x_size, y_size;

        public static Key key;
        public static bool keyDown;
        public static bool mouseDown;
        public static MouseButton buttonDown;
        public static bool leftMouseButtonReleased = false;
        public static bool rightMouseButtonReleased = false;
        public static bool leftMouseButtonPressed = false;
        public static bool rightMouseButtonPressed = false;


        public override void EndInit()
        {
            waitForMouse = false;
            waitForKey = false;
            dngw = this;
            SKBrush = new SKPaint();
            SKBrush.IsAntialias = true;
            Width = Parent.Parent.Width;
            Height = Parent.Parent.Height;
            SKBrush.Color = new SKColor(0, 0, 0);
            SKBrush.Shader = SKShader.CreateColor(SKBrush.Color);
            RenderTarget = new RenderTargetBitmap(new PixelSize((int)Width, (int)Height), new Vector(96, 96));

            var context = RenderTarget.CreateDrawingContext(null);
            SkiaContext = context as ISkiaDrawingContextImpl;
            SkiaContext.SkCanvas.Clear(new SKColor(255, 255, 255));

            PointerPressed += DrawingCanvas_PointerPressed;
            PointerMoved += DrawingCanvas_PointerMoved;
            PointerLeave += DrawingCanvas_PointerLeave;
            PointerReleased += DrawingCanvas_PointerReleased;
            PointerEnter += DrawingCanvas_PointerEnter;
            KeyDown += DrawingCanvas_KeyDown;
            KeyUp += DrawingCanvas_KeyUp;
            player.PlaybackFinished += OnPlaybackFinished;
            base.EndInit();

            paints = new SKPaint[standard_color.Length];
            for (var i = 0; i < paints.Length; i++)
            {
                paints[i] = new SKPaint();
                paints[i].Color = new SKColor(standard_color[i]);
            }
        }

        public void WaitForKey()
        {
            waitForKey = true;
            Focus();
        }

        private void DrawingCanvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (SkiaContext != null && waitForKey)
            {
                key = e.Key;
                keyDown = true;
                Key_Down(Key.A);

                //SkiaContext.SkCanvas.DrawText("key pressed", 500, 500, SKBrush);
                waitForKey = false;
                //InvalidateVisual();
            }
        }

        private void DrawingCanvas_KeyUp(object sender, KeyEventArgs e)
        {
            if (SkiaContext != null)
            {
                keyDown = false;
            }
        }

        public static bool keyHit = false;

        public bool KeyHit()
        {
            bool ans = keyHit;
            if (keyHit)
            {
                keyHit = false;
            }

            return ans;
        }

        public Key GetKey()
        {
            if(key + "" == "None")
            {
                Dispatcher.UIThread.InvokeAsync(async () =>
                    {
                        WaitForKey();
                        await DotnetGraph.waitForKey();
                    }
                ).Wait(-1);
            }

            Key temp = key;
            key = new Key();
            return temp;
        }

        public bool Key_Down(Key k)
        {
            bool givenKeyDown = keyDown && key == k;
            //SkiaContext.SkCanvas.DrawText(givenKeyDown.ToString(), 400, 500, SKBrush);
            return keyDown && key == k;
        }

        private void DrawingCanvas_PointerEnter(object sender, PointerEventArgs e)
        {
            if (SkiaContext != null)
            {
            }
        }

        public void WaitForMouseButton(MouseButton button)
        {
            waitForMouse = true;
            mb = button;
        }

        private void DrawingCanvas_PointerReleased(object sender, PointerReleasedEventArgs e)
        {
            if (SkiaContext != null && e.MouseButton == mb && waitForMouse)
            {
                switch (e.MouseButton)
                {
                    case MouseButton.Left:
                        leftMouseButtonReleased = true;
                        break;
                    case MouseButton.Right:
                        rightMouseButtonReleased = true;
                        break;
                }
                mouseDown = false;
                var p = e.GetPosition(this);
                //SkiaContext.SkCanvas.DrawText("mouse pressed", (float)p.X, (float)p.Y, SKBrush);
                //InvalidateVisual();
                waitForMouse = false;
            }
        }

        private void DrawingCanvas_PointerLeave(object sender, PointerEventArgs e)
        {
            if (SkiaContext != null)
            {
            }
        }

        private void DrawingCanvas_PointerMoved(object sender, PointerEventArgs e)
        {
            if (SkiaContext != null)
            {
                var p = e.GetPosition(this);
                xCord = Make_0_Based((int)p.X);
                yCord = Make_0_Based_And_Unflip((int)p.Y);
            }
        }

        private void DrawingCanvas_PointerPressed(object sender, PointerPressedEventArgs e)
        {
            if (SkiaContext != null)
            {
                switch (e.MouseButton)
                {
                    case MouseButton.Left:
                        leftMouseButtonPressed = true;
                        break;
                    case MouseButton.Right:
                        rightMouseButtonPressed = true;
                        break;
                }
                var p = e.GetPosition(this);
                SkiaContext.SkCanvas.DrawRect(new SKRect((float)p.X, (float)p.Y,
                    (float)p.X + 10, (float)p.Y + 10), SKBrush);
                //InvalidateVisual();
                mouseDown = true;
                buttonDown = e.MouseButton;
                MouseButtonDown(MouseButton.Left);
            }
            GetMouseX();
            GetMouseY();
        }
        public bool MouseButtonPressed(MouseButton mb)
        {
            bool ans = false;
            switch (mb)
            {
                case MouseButton.Left:
                    ans = leftMouseButtonPressed;
                    leftMouseButtonPressed = false;
                    return ans;
                case MouseButton.Right:
                    ans = rightMouseButtonPressed;
                    rightMouseButtonPressed = false;
                    return ans;
            }
            return false;
        }
        public bool MouseButtonReleased(MouseButton mb)
        {
            bool ans;
            switch (mb)
            {
                case MouseButton.Left:
                    ans = leftMouseButtonReleased;
                    leftMouseButtonReleased = false;
                    return ans;
                case MouseButton.Right:
                    ans = rightMouseButtonReleased;
                    rightMouseButtonReleased = false;
                    return ans;
            }
            return false;
        }
        public bool MouseButtonDown(MouseButton button)
        {
            bool givenButtonDown = mouseDown && button == buttonDown;
            //SkiaContext.SkCanvas.DrawText(givenButtonDown.ToString(), 400, 500, SKBrush);
            //InvalidateVisual();
            return givenButtonDown;
        }

        

        public async Task GetMouseButton(MouseButton button)
        {
            if(mb + "" == "None")
            {
                mb = button;
                //Dispatcher.UIThread.InvokeAsync(async () =>
                //{
                WaitForMouseButton(button);
                await DotnetGraph.WaitForMouseButton(button);
                //}).Wait(-1);
            }

        }

        public int GetFontHeight()
        {
            Avalonia.Media.FormattedText formattedtextYes = new Avalonia.Media.FormattedText(
                "W", new Avalonia.Media.Typeface("Lucida Console"), current_font_size, Avalonia.Media.TextAlignment.Center,
                Avalonia.Media.TextWrapping.NoWrap, Avalonia.Size.Infinity);
            var height_of_text = (int)Math.Ceiling(formattedtextYes.Bounds.Height);
            SkiaContext.SkCanvas.DrawText(height_of_text.ToString(), 400, 500, SKBrush);
            return height_of_text;
        }
        public int GetFontWidth()
        {
            Avalonia.Media.FormattedText formattedtextYes = new Avalonia.Media.FormattedText(
                "MMMMMMMMMMMM", new Avalonia.Media.Typeface("Lucida Console"), current_font_size, Avalonia.Media.TextAlignment.Center,
                Avalonia.Media.TextWrapping.NoWrap, Avalonia.Size.Infinity);
            var width_of_text = (int)Math.Ceiling(formattedtextYes.Bounds.Width) / 12;
            SkiaContext.SkCanvas.DrawText(width_of_text.ToString(), 400, 550, SKBrush);
            return width_of_text;
        }

        public Task<bool> SaveAsync(string path)
        {
            return Task.Run(() =>
            {
                try
                {
                    RenderTarget.Save(path);
                }
                catch (Exception)
                {
                    return false;
                }

                return true;
            });
        }

        public int GetWindowWidth()
        {
            return (int)Width;
        }

        public int GetWindowHeight()
        {
            return (int)Height;
        }
        //--------------------------------------------------------------------
        //--
        //-- The Make_0_Based function converts an x coordinate from
        //-- 1-based to 0-based referencing.
        //--
        //--------------------------------------------------------------------

        private int Make_0_Based(
            int Coordinate)
        {
            return Coordinate - 1;
        }

        //--------------------------------------------------------------------
        //--
        //-- The Make_0_Based_And_Flip function converts a y coordinate from
        //-- 1-based to 0-based referencing and unflips the orientation.
        //--
        //--------------------------------------------------------------------

        private int Make_0_Based_And_Unflip(
            int Coordinate)
        {
            return GetWindowHeight() - Coordinate;
        }

        //--------------------------------------------------------------------
        //--
        //-- The Make_1_Based function converts an x coordinate from
        //-- 0-based to 1-based referencing.
        //--
        //--------------------------------------------------------------------

        private int Make_1_Based(
            int Coordinate)
        {
            return Coordinate + 1;
        }

        //--------------------------------------------------------------------
        //--
        //-- The Make_1_Based_And_Flip function converts a y coordinate from
        //-- 0-based to 1-based referencing and flips the orientation.
        //--
        //--------------------------------------------------------------------

        private int Make_1_Based_And_Flip(
            int Coordinate)
        {
            try
            {
                return GetWindowHeight() + 1 - Coordinate;
            }
            catch (Exception error)
            {
                throw new Exception("ERROR!: Graph Window not open");
            }
        }
        //--------------------------------------------------------------------
        //--
        //-- The Crop_Coordinates procedure crops the end point coordinates
        //-- to move them to the edge of the window, only if one of them = -1 AND 
        //-- only if appropriate (object not refside window)
        //-- We need to pass the line boolean parameter in because, for
        //-- lines we want to slide a -1 coordinate to 0, but for boxes
        //-- we want to slide a -1 coordinate to -2 (so that line of the
        //-- box doesn't get displayed)
        //-- Algorithm :
        //--    If Y1 is above the top of the window
        //--       Adjust X1 and Y1 if Y2 not above top of window and 
        //--       X1 and X2 not both to left of window
        //--    If Y2 is above the top of the window
        //--       Adjust X2 and Y2 if Y1 not above top of window and 
        //--       X1 and X2 not both to left of window
        //--    If X1 is to the left of the window
        //--       Adjust X1 and Y1 if X2 not to the left of the window and 
        //--       Y1 and Y2 not both above top of window
        //--    If X2 is to the left of the window
        //--       Adjust X2 and Y2 if X1 not to the left of the window and 
        //--       Y1 and Y2 not both above top of window
        //--
        //--------------------------------------------------------------------

        private void Crop_Coordinates(
            bool Line,
            float Slope,
            bool Undefined_Slope,
            ref int X1,
            ref int Y1,
            ref int X2,
            ref int Y2)
        {
            //--    If Y1 is above the top of the window
            if (Y1 == -1)
            {
                //--       Adjust X1 and Y1 if Y2 not above top of window and 
                //--       X1 and X2 not both to left of window
                if (Y2 >= 0 && !(X1 < 0 && X2 < 0))
                {
                    if (!Undefined_Slope) X1 = X1 + (int)((0 - Y1) * Slope);

                    if (Line)
                        Y1 = 0;
                    else
                        Y1 = -2;
                }
                //-- set Y1 to -2, since the object shouldn't appear anyway
                else
                {
                    Y1 = -2;
                }
            }

            //--    If Y2 is above the top of the window
            if (Y2 == -1)
            {
                //--       Adjust X2 and Y2 if Y1 not above top of window and 
                //--       X1 and X2 not both to left of window
                if (Y1 >= 0 && !(X1 < 0 && X2 < 0))
                {
                    if (!Undefined_Slope) X2 = X2 + (int)((0 - Y2) * Slope);

                    if (Line)
                        Y2 = 0;
                    else
                        Y2 = -2;
                } //-- set Y2 to -2, since the object shouldn't appear anyway
                else
                {
                    Y2 = -2;
                }
            }


            //--    If X1 is to the left of the window
            if (X1 == -1)
            {
                //--       Adjust X1 and Y1 if X2 not to the left of the window and 
                //--       Y1 and Y2 not both above top of window
                if (X2 >= 0 && !(Y1 < 0 && Y2 < 0))
                {
                    if (!Undefined_Slope) Y1 = Y1 + (int)((0 - X1) * Slope);

                    if (Line)
                        X1 = 0;
                    else
                        X1 = -2;
                }
                //-- set X1 to -2, since the object shouldn't appear anyway
                else
                {
                    X1 = -2;
                }
            }

            //--    If X2 is to the left of the window
            if (X2 == -1)
            {
                //--       Adjust X2 and Y2 if X1 not to the left of the window and 
                //--       Y1 and Y2 not both above top of window
                if (X1 >= 0 && !(Y1 < 0 && Y2 < 0))
                {
                    if (!Undefined_Slope) Y2 = Y2 + (int)((0 - X2) * Slope);

                    if (Line)
                        X2 = 0;
                    else
                        X2 = -2;
                }
                //-- set X2 to -2, since the object shouldn't appear anyway
                else
                {
                    X2 = -2;
                }
            }
        }


        public void DrawLine(
            int x1,
            int y1,
            int x2,
            int y2,
            Color_Type hue)
        {
            int x_coord1, y_coord1;
            x_coord1 = Make_0_Based(x1);
            y_coord1 = Make_0_Based_And_Unflip(y1);
            int x_coord2, y_coord2;
            x_coord2 = Make_0_Based(x2);
            y_coord2 = Make_0_Based_And_Unflip(y2);
            bool Undefined_Slope;
            float Slope;

            //-- Calculate slope of the line
            if (x_coord2 - x_coord1 != 0)
            {
                Undefined_Slope = false;
                Slope = (y_coord2 - y_coord1) / (float)(x_coord2 -
                                                        x_coord1);
            }
            //-- undefined slope
            else
            {
                Undefined_Slope = true;
                Slope = 0.0F;
            }

            //-- Adjust invalid (-1) coordinates to crop the line 
            //-- to fit in the window
            Crop_Coordinates(
                true,
                Slope,
                Undefined_Slope,
                ref x_coord1,
                ref y_coord1,
                ref x_coord2,
                ref y_coord2);
            if (x_coord2 >= x_coord1)
            {
                draw_rect.X = x_coord1 > 0 ? x_coord1 : 0;
                draw_rect.Width = (x_coord2 < x_size ? x_coord2 + 1 : x_size)
                                  - draw_rect.X;
            }
            else
            {
                draw_rect.X = x_coord2 > 0 ? x_coord2 : 0;
                draw_rect.Width = (x_coord1 < x_size ? x_coord1 + 1 : x_size)
                                  - draw_rect.X;
            }

            if (y_coord2 >= y_coord1)
            {
                draw_rect.Y = y_coord1 > 0 ? y_coord1 : 0;
                draw_rect.Height = (y_coord2 < y_size ? y_coord2 + 1 : y_size)
                                   - draw_rect.Y;
            }
            else
            {
                draw_rect.Y = y_coord2 > 0 ? y_coord2 : 0;
                draw_rect.Height = (y_coord1 < y_size ? y_coord1 + 1 : y_size)
                                   - draw_rect.Y;
            }

            try
            {
                SkiaContext.SkCanvas.DrawLine(new SKPoint(x_coord1, y_coord1),
                    new SKPoint(x_coord2, y_coord2),
                    paints[(int)hue]);
            }
            catch (Exception error)
            {
                throw new Exception("ERROR!: Draw_Line failed (" + x1 + "," + y1 +
                                    "," + x2 + "," + y2 + ")" +
                                    "color:" + hue + " message: " + error.Message);
            }

            UpdateWindowUnlessFrozen();
        }

        public void DrawBox(
            int x1,
            int y1,
            int x2,
            int y2,
            Color_Type hue,
            bool filled
        )
        {
            int x_coord1, y_coord1;
            x_coord1 = Make_0_Based(x1);
            y_coord1 = Make_0_Based_And_Unflip(y1);
            int x_coord2, y_coord2;
            x_coord2 = Make_0_Based(x2);
            y_coord2 = Make_0_Based_And_Unflip(y2);
            try
            {
                var paint = paints[(int)hue];
                paint.Style = filled ? SKPaintStyle.Fill : SKPaintStyle.Stroke;

                SkiaContext.SkCanvas.DrawRect(x_coord1, y_coord1,
                    Math.Abs(x_coord2 - x_coord1), -1 * Math.Abs(y_coord2 - y_coord1),
                    paint);
            }
            catch (Exception error)
            {
            }

            UpdateWindowUnlessFrozen();
        }

        public void DrawCircle(
            int x1,
            int y1,
            int rad,
            Color_Type hue,
            bool filled
        )
        {
            int x_coord1, y_coord1;
            x_coord1 = Make_0_Based(x1);
            y_coord1 = Make_0_Based_And_Unflip(y1);

            try
            {
                var paint = paints[(int)hue];
                paint.Style = filled ? SKPaintStyle.Fill : SKPaintStyle.Stroke;
                SkiaContext.SkCanvas.DrawCircle(x_coord1, y_coord1,
                    rad, paint);
            }
            catch (Exception error)
            {
            }

            UpdateWindowUnlessFrozen();
        }

        public void DrawEllipse(
            int x1,
            int y1,
            int x2,
            int y2,
            Color_Type hue,
            bool filled
        )
        {
            int x_coord1, y_coord1;
            x_coord1 = Make_0_Based(x1);
            y_coord1 = Make_0_Based_And_Unflip(y1);
            int x_coord2, y_coord2;
            x_coord2 = Make_0_Based(x2);
            y_coord2 = Make_0_Based_And_Unflip(y2);
            try
            {
                var paint = paints[(int)hue];
                paint.Style = filled ? SKPaintStyle.Fill : SKPaintStyle.Stroke;
                var cx = (x_coord1 + x_coord2) / 2;
                var cy = (y_coord1 + y_coord2) / 2;
                var rx = (x_coord2 - x_coord1) / 2;
                var ry = (y_coord2 - y_coord1) / 2;
                SkiaContext.SkCanvas.DrawOval(cx, cy, rx, ry, paint);
            }
            catch (Exception error)
            {
            }

            UpdateWindowUnlessFrozen();
        }

        public void DrawEllipseRotate(
            int x1,
            int y1,
            int x2,
            int y2,
            double angle,
            Color_Type hue,
            bool filled
        )
        {
            int x_coord1, y_coord1;
            x_coord1 = Make_0_Based(x1);
            y_coord1 = Make_0_Based_And_Unflip(y1);
            int x_coord2, y_coord2;
            x_coord2 = Make_0_Based(x2);
            y_coord2 = Make_0_Based_And_Unflip(y2);
            try
            {
                var paint = paints[(int)hue];
                paint.Style = filled ? SKPaintStyle.Fill : SKPaintStyle.Stroke;
                var cx = (x_coord1 + x_coord2) / 2;
                var cy = (y_coord1 + y_coord2) / 2;
                var rx = (x_coord2 - x_coord1) / 2;
                var ry = (y_coord2 - y_coord1) / 2;
                angle = 2 * Math.PI - angle;
                SkiaContext.SkCanvas.RotateRadians((float)angle, cx, cy);
                SkiaContext.SkCanvas.DrawOval(cx, cy, rx, ry, paint);
                SkiaContext.SkCanvas.RotateRadians(-1 * (float)angle, cx, cy);
            }
            catch (Exception error)
            {
            }

            UpdateWindowUnlessFrozen();
        }

        public void DrawArc(
            int x1,
            int y1,
            int x2,
            int y2,
            int startx,
            int starty,
            int endx,
            int endy,
            Color_Type hue
        )
        {
            int x_coord1, y_coord1;
            x_coord1 = Make_0_Based(x1);
            y_coord1 = Make_0_Based_And_Unflip(y1);
            int x_coord2, y_coord2;
            x_coord2 = Make_0_Based(x2);
            y_coord2 = Make_0_Based_And_Unflip(y2);

            try
            {
                var paint = paints[(int)hue];
                paint.Style = SKPaintStyle.Stroke;
                // SKRect oval = new SKRect(cx, cy, rx, ry);
                var oval = new SKRect(x_coord1, y_coord1, x_coord2, y_coord2);
                var mid_x = ((double)x1 + x2) / 2.0;
                var mid_y = ((double)y1 + y2) / 2.0;
                var end_theta = Math.Atan2(starty - mid_y, startx - mid_x);
                var start_theta = Math.Atan2(endy - mid_y, endx - mid_x);
                if (end_theta < 0.0) end_theta += 2.0 * Math.PI;

                if (start_theta < 0.0) start_theta += 2.0 * Math.PI;

                var range = start_theta - end_theta;
                if (range <= 0.0) range += 2.0 * Math.PI;

                SkiaContext.SkCanvas.DrawArc(oval, (float)((2.0 * Math.PI - start_theta) * 180.0 / Math.PI),
                    (float)(range * 180.0 / Math.PI), false, paint);
            }
            catch (Exception error)
            {
            }

            UpdateWindowUnlessFrozen();
        }

        public void DisplayText(
            int x1,
            int y1,
            string text,
            Color_Type hue
        )
        {
            try
            {
                var paint = paints[(int)hue];
                paint.Style = SKPaintStyle.Fill;
                paint.IsAntialias = true;
                int x_coord1, y_coord1;
                x_coord1 = Make_0_Based(x1);
                y_coord1 = Make_0_Based_And_Unflip(y1 - current_font_size);
                paint.TextAlign = SKTextAlign.Left;
                SkiaContext.SkCanvas.DrawText(text, x_coord1, y_coord1,
                    new SKFont(SKTypeface.FromFamilyName("Lucida Console"), current_font_size), paint);
            }
            catch (Exception error)
            {
            }

            UpdateWindowUnlessFrozen();
        }

        public void DisplayNumber(
            int x1,
            int y1,
            double number,
            Color_Type hue
        )
        {
            try
            {
                var paint = paints[(int)hue];
                int x_coord1, y_coord1;
                x_coord1 = Make_0_Based(x1);
                y_coord1 = Make_0_Based_And_Unflip(y1 - current_font_size);
                SKFont f = new SKFont(SKTypeface.FromFamilyName("Lucida Console"));
                SkiaContext.SkCanvas.DrawText(number.ToString(), x_coord1, y_coord1,
                     new SKFont(SKTypeface.FromFamilyName("Lucida Console"), current_font_size), paint);
            }
            catch (Exception error)
            {
            }

            UpdateWindowUnlessFrozen();
        }

        public void SetFontSize(
            int size
        )
        {
            if (size == 0)
                current_font_size = default_font_size;
            else if (size > 0 && size <= 100)
                current_font_size = (int)(size * 1.4);
            else
                throw new Exception("Error in Set_Font_Size: size must be in [0,100]");
        }

        private void OnPlaybackFinished(object sender, EventArgs e)
        {
            if (!playInBackground)
            {
                //SkiaContext.SkCanvas.DrawText("sound finished", 200, 500, SKBrush);
                //InvalidateVisual();
            }
        }

        public void PlaySound(
            string soundFile
        )
        {
            playInBackground = false;
            player.Play(soundFile);
        }

        public void PlaySoundBackground(
            string soundFile
        )
        {
            if (loopPlayer != null)
            {
                loopPlayer.Stop();
            }
            playInBackground = true;
            player.Play(soundFile);
        }

        public void PlaySoundBackgroundLoop(
            string soundFile
        )
        {
            if (loopPlayer != null)
            {
                loopPlayer.Stop();
            }
            soundFilePath = soundFile;
            loopPlayer = new Player();
            loopPlayer.Play(soundFile);
            loopPlayer.PlaybackFinished += (sender, e) => { PlaySoundBackgroundLoop(soundFile); };
        }

        public int GetMouseX()
        {
            int x = xCord;
            //SkiaContext.SkCanvas.DrawText("xCord: " + x.ToString(), 100, 100, SKBrush);
            //InvalidateVisual();
            return x;
        }
        public int GetMouseY()
        {
            int y = yCord;
            //SkiaContext.SkCanvas.DrawText("yCord: " + y.ToString(), 100, 200, SKBrush);
            //InvalidateVisual();
            return y;
        }

        public void ClearWindow(
            Color_Type hue
        )
        {
            try
            {
                var paint = paints[(int)hue];
                paint.Style = SKPaintStyle.Fill;
                SkiaContext.SkCanvas.DrawRect(0, 0, (float)Width, (float)Height, paint);
            }
            catch (Exception error)
            {
            }

            UpdateWindowUnlessFrozen();
        }

        public void saveGraphWindow(string filename)
        {
            RenderTarget.Save(filename);
        }

        public bool IsOpen()
        {
            return graphWindowOpen;
        }
        public int LoadBitmap(string path)
        {
            SKImage x = SKImage.FromEncodedData(path);
            bitmaps.Add(x);
            return bitmaps.IndexOf(x);
        }
        public void DrawBitmap(int index, int x, int y, int width, int height)
        {
            SKBitmap bmp = SKBitmap.FromImage((SKImage)bitmaps[index]);
            bmp = bmp.Resize(new SKImageInfo(width, height), (SKFilterQuality)3);
            SkiaContext.SkCanvas.DrawBitmap(bmp, x, y, SKBrush);
            UpdateWindowUnlessFrozen();
        }

        public static void DelayFor(
            double seconds
        )
        {
            System.Threading.Thread.Sleep((int)(seconds * 1000.0));

            //var timer = new Timer(seconds * 1000);
            //timer.Elapsed += (sender, e) =>
            //{
            //    SkiaContext.SkCanvas.DrawText("timer finished", 200, 500, SKBrush);
            //    InvalidateVisual();
            //};
            //timer.Enabled = true;
        }

        public static int RED(uint color)
        {
            return (int)((color >> 16) & 0xFF);
        }

        public static int GREEN(uint color)
        {
            return (int)((color >> 8) & 0xFF);
        }

        public static int BLUE(uint color)
        {
            return (int)(color & 0xFF);
        }

        public Color_Type GetPixel(
            int x,
            int y
        )
        {
            var x_coord = Make_0_Based(x);
            var y_coord = Make_0_Based_And_Unflip(y);
            var color = SkiaContext.SkSurface.PeekPixels().GetPixelColor(x_coord, y_coord);
            return (Color_Type)GetClosestColor(color.Red, color.Green, color.Blue);
        }

        public static int GetClosestColor(int red, int green, int blue)
        {
            int i;
            int difference;
            var min_difference = 3 * 256 * 256;
            int red_diff, green_diff, blue_diff;

            var closest = 0;
            for (i = 0; i < MAX_COLORS; i++)
            {
                red_diff = RED(standard_color[i]) - red;
                green_diff = GREEN(standard_color[i]) - green;
                blue_diff = BLUE(standard_color[i]) - blue;
                difference = red_diff * red_diff + green_diff * green_diff +
                             blue_diff * blue_diff;
                if (difference < min_difference)
                {
                    min_difference = difference;
                    closest = i;
                }
            }

            return closest;
        }

        public void PutPixel(
            int x,
            int y,
            Color_Type hue)
        {
            var paint = paints[(int)hue];
            paint.Style = SKPaintStyle.Fill;
            var x_coord = Make_0_Based(x);
            var y_coord = Make_0_Based_And_Unflip(y);
            SkiaContext.SkCanvas.DrawPoint(((float)x_coord)+0.5f, ((float)y_coord)+0.5f, paint);
            UpdateWindowUnlessFrozen();
        }

        public void FloodFill(
            int x,
            int y,
            Color_Type hue
        )
        {
            if (x < 1 || y < 1 || x > GetWindowWidth() || y > GetWindowHeight()) return;
            FloodFillRecursive(x, y, SkiaContext.SkSurface.PeekPixels().GetPixelColor(x, y), hue);
            UpdateWindowUnlessFrozen();
        }

        public void FloodFillRecursive(
            int x,
            int y,
            SKColor start_color,
            Color_Type to_color

        )
        {
            int x_coord, y_coord;
            Queue flood_queue;
            int[,] marked;
            x_coord = Make_0_Based(x);
            y_coord = Make_0_Based_And_Unflip(y);
            var color = SkiaContext.SkSurface.PeekPixels().GetPixelColor(x_coord, y_coord);
            marked = new int[GetWindowWidth(), GetWindowHeight()];

            {
                flood_queue = new Queue();
                flood_queue.Enqueue(new Point(x_coord, y_coord));
                marked[x_coord, y_coord] = 1;
                while (flood_queue.Count > 0)
                {
                    var p = (Point)flood_queue.Dequeue();
                    var worked = false;

                    //string s1 = ("x,y,color:" +
                    //    p.X + "," + p.Y + "," + this.bmp.GetPixel(p.X, p.Y));
                    //System.Diagnostics.Trace.WriteLine(s1);
                    //while (!worked)
                    {
                        //try
                        {
                            var paint = paints[(int)to_color];
                            paint.Style = SKPaintStyle.Fill;
                            paint.StrokeCap = SKStrokeCap.Round;
                            paint.StrokeWidth = 1.0f;
                            SkiaContext.SkCanvas.DrawPoint(((float)p.X)+0.5f, ((float)p.Y)+0.5f, paint);
                            worked = true;
                        }
                        //catch
                        {
                            //    System.Threading.Thread.Sleep(1);
                        }
                    }
                    marked[(int)p.X, (int)p.Y] = 1;
                    check_and_enqueue((int)p.X + 1, (int)p.Y, flood_queue, marked, color);
                    check_and_enqueue((int)p.X, (int)p.Y + 1, flood_queue, marked, color);
                    check_and_enqueue((int)p.X - 1, (int)p.Y, flood_queue, marked, color);
                    check_and_enqueue((int)p.X, (int)p.Y - 1, flood_queue, marked, color);
                }
            }
        }

        public void check_and_enqueue(
            int x,
            int y,
            Queue flood_queue,
            int[,] marked,
            SKColor color
        )
        {
            SKColor pix_color;
            if (x < 0 || x >= Width ||
                y < 0 || y >= Height ||
                marked[x, y] == 1)
                return;
            pix_color = SkiaContext.SkSurface.PeekPixels().GetPixelColor(x, y);
            if (pix_color != color) return;

            marked[x, y] = 1;
            flood_queue.Enqueue(new Point(x, y));
        }

        public void SetWindowTitle(string title)
        {
            dotnetgraph.Title = title;
            UpdateWindowUnlessFrozen();
        }

        //public int GetMaxHeight()
        //{
        //    var maxHeight = Screen.PrimaryScreen.Bounds.Height;
        //    SkiaContext.SkCanvas.DrawText(maxHeight.ToString(), 100, 100, SKBrush);
        //    InvalidateVisual();
        //    return maxHeight;

        //}

        //public int GetMaxWidth()
        //{
        //    var maxWidth = Screen.PrimaryScreen.Bounds.Width;
        //    SkiaContext.SkCanvas.DrawText(maxWidth.ToString(), 100, 200, SKBrush);
        //    InvalidateVisual();
        //    return maxWidth;
        //}

        public static void OpenGraphWindow(int width, int height)
        {
            if (!graphWindowOpen)
            {
                Dispatcher.UIThread.InvokeAsync(() =>
                {
                    dotnetgraph = new DotnetGraph(width, height);
                    dotnetgraph.Show();
                    graphWindowOpen = true;
                
                }).Wait(-1);


        }
        }

        public static void onClosingCommand(){
            graphWindowOpen = false;
        }

        public void CloseGraphWindow()
        {
            if (graphWindowOpen)
            {
                dotnetgraph.Close();
                graphWindowOpen = false;
            }
        }

        public void FreezeGraphWindow()
        {
            frozen = true;
        }

        public void UnfreezeGraphWindow()
        {
            frozen = false;
        }

        public void UpdateGraphWindow()
        {
            //Dispatcher.UIThread.InvokeAsync(() =>
            //{
                InvalidateVisual();
            //});
        }

        public void UpdateWindowUnlessFrozen()
        {
            if (!frozen) UpdateGraphWindow();
        }

        public override void Render(DrawingContext context)
        {
            context.DrawImage(RenderTarget,
                new Rect(0, 0, RenderTarget.PixelSize.Width, RenderTarget.PixelSize.Height)
            );
        }
    }
}
using System;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using static Tizen.NUI.Gesture;

namespace StickerSample
{
    class Program : NUIApplication
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }
        View baseView;
        TapGestureDetector tgd;
        PanGestureDetector pgd;
        LongPressGestureDetector lpgd;

        void Initialize()
        {
            Window.Instance.KeyEvent += OnKeyEvent;

            baseView = new View()
            {
                WidthSpecification = Window.Instance.Size.Width,
                HeightSpecification = Window.Instance.Size.Height,
                BackgroundColor = Color.Cyan
            };


            tgd = new TapGestureDetector();
            tgd.Detected += OnTapGestureDected;

            pgd = new PanGestureDetector();
            pgd.Detected += OnPanGestureDetected;

            lpgd = new LongPressGestureDetector();
            lpgd.Detected += OnLongPressGestureDetector;

            tgd.Attach(baseView);
            Window.Instance.GetDefaultLayer().Add(baseView);
        }

        private void OnTapGestureDected(object source, TapGestureDetector.DetectedEventArgs e)
        {
            if (e.View == baseView)
            {
                View stickerBaseView = new View()
                {
                    WidthSpecification = 200,
                    HeightSpecification = 200
                };
                tgd.Attach(stickerBaseView);
                lpgd.Attach(stickerBaseView);
                pgd.Attach(stickerBaseView);
                baseView.Add(stickerBaseView);

                Random random = new Random();
                int number = random.Next(1, 10);

                string path = ApplicationInfo.SharedResourcePath;
                string file = "fb_emoji_500_0" + number + ".png.gif";
                string gif = path + file;

                ImageView stickerImageView = new ImageView(gif)
                {
                    WidthResizePolicy = ResizePolicyType.FillToParent,
                    HeightResizePolicy = ResizePolicyType.FillToParent,
                    ReleasePolicy = ReleasePolicyType.Never
                };
                stickerBaseView.Add(stickerImageView);
            }
        }

        private int stickerCount = 1;

        private bool OnTouchBaseView(object source, View.TouchEventArgs e)
        {
            Tizen.Log.Debug("rkdehdrud", "ENTER");
            Tizen.Log.Debug("rkdehdrud", $"count is {e.Touch.GetPointCount()}");
            if (e.Touch.GetPointCount() == 1 && e.Touch.GetState(0) == PointStateType.Down)
            {
                string path = ApplicationInfo.SharedResourcePath;

                View stickerBaseView = new View()
                {
                    WidthSpecification = 200,
                    HeightSpecification = 200
                };
                lpgd.Attach(stickerBaseView);
                pgd.Attach(stickerBaseView);
                baseView.Add(stickerBaseView);

                string file = "fb_emoji_500_0" + stickerCount + ".png.gif";
                string gif = path + file;
                ImageView stickerImageView = new ImageView(gif)
                {
                    WidthResizePolicy = ResizePolicyType.FillToParent,
                    HeightResizePolicy = ResizePolicyType.FillToParent,
                    ReleasePolicy = ReleasePolicyType.Never
                };
                stickerBaseView.Add(stickerImageView);

                stickerCount++;
            }
            return true;
        }

        private View focusedView = null;

        private void OnLongPressGestureDetector(object source, LongPressGestureDetector.DetectedEventArgs e)
        {
            Tizen.Log.Debug("rkdehdrud", $"state is {e.LongPressGesture.State}");
            StateType state = e.LongPressGesture.State;
            if (state == StateType.Started)
            {
                focusedView = e.View;
                e.View.BackgroundColor = Color.Red;
                Window.Instance.GetDefaultLayer().Add(e.View);
            }
            else if (state == StateType.Finished)
            {
                focusedView = null;
                e.View.BackgroundColor = Color.White;
                baseView.Add(e.View);
            }

        }

        private void OnPanGestureDetected(object source, PanGestureDetector.DetectedEventArgs e)
        {
            Tizen.Log.Debug("rkdehdrud", $"state is {e.PanGesture.State}");
            Tizen.Log.Debug("rkdehdrud", $"X is {e.PanGesture.Displacement.X}");
            Tizen.Log.Debug("rkdehdrud", $"Y is {e.PanGesture.Displacement.Y}");

            if (focusedView == e.View)
            {
                e.View.PositionX += e.PanGesture.Displacement.X;
                e.View.PositionY += e.PanGesture.Displacement.Y;
            }
        }

        public void OnKeyEvent(object sender, Window.KeyEventArgs e)
        {
            Tizen.Log.Debug("rkdehdrud", $"key name is {e.Key.KeyPressedName}");
            if (e.Key.State == Key.StateType.Down && (e.Key.KeyPressedName == "XF86Back" || e.Key.KeyPressedName == "Escape"))
            {
                Exit();
            }
        }

        static void Main(string[] args)
        {
            var app = new Program();
            app.Run(args);
        }
    }
}

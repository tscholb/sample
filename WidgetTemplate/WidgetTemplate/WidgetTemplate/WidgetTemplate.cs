using System;
using System.Collections.Generic; // for Dictionary
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.Applications;

namespace WidgetTemplate
{
    class RedWidget : Widget
    {
        protected override void OnCreate(string contentInfo, Window window)
        {
            Bundle bundle = Bundle.Decode(contentInfo);

            View rootView = new View();
            rootView.BackgroundColor = Color.Red;
            rootView.Size2D = window.Size;
            rootView.PivotPoint = PivotPoint.Center;
            window.GetDefaultLayer().Add(rootView);

            TextLabel sampleLabel = new TextLabel("Red Widget");
            sampleLabel.FontFamily = "SamsungOneUI 500";
            sampleLabel.PointSize = 10;
            sampleLabel.TextColor = Color.Black;
            sampleLabel.SizeWidth = 500;
            sampleLabel.PivotPoint = PivotPoint.Center;

            rootView.Add(sampleLabel);

            window.TouchEvent += OnTouchEvent;
            rootView.TouchEvent += OnViewTouched;
        }

        protected override void OnPause()
        {
            base.OnPause();
        }

        protected override void OnResume()
        {
            base.OnResume();
        }

        protected override void OnResize(Window window)
        {
            base.OnResize(window);
        }

        protected override void OnTerminate(string contentInfo, TerminationType type)
        {
            base.OnTerminate(contentInfo, type);
        }

        protected override void OnUpdate(string contentInfo, int force)
        {
            base.OnUpdate(contentInfo, force);
        }

        private void OnTouchEvent(object source, Window.TouchEventArgs e)
        {
            PointStateType state = e.Touch.GetState(0);
            if(state == PointStateType.Down)
            {
                SetContentInfo("count:"+ mCount);
                mCount++;
            }
        }

        private bool OnViewTouched(object sender, View.TouchEventArgs e)
        {
            if (e.Touch.GetState(0) == PointStateType.Down)
            {
                PointStateType state = e.Touch.GetState(0);
                if (state == PointStateType.Down)
                {
                    Bundle bundle = new Bundle();
                    String encodedBundle = bundle.Encode();
                    SetContentInfo(encodedBundle);
                    mCount++;
                }
            }
            return true;
        }

        private int mCount;
    }

    class BlueWidget : Widget
    {
        protected override void OnCreate(string contentInfo, Window window)
        {
            Bundle bundle = Bundle.Decode(contentInfo);
            string outString;
            if (bundle.TryGetItem("COUNT", out outString))
            {
                Tizen.Log.Info("NUI", "Get Count is success : " + outString + "\n");
            }

            View rootView = new View();
            rootView.BackgroundColor = Color.Blue;
            rootView.Size2D = window.Size;
            rootView.PivotPoint = PivotPoint.Center;
            window.GetDefaultLayer().Add(rootView);

            TextLabel sampleLabel = new TextLabel("Blue Widget");
            sampleLabel.FontFamily = "SamsungOneUI 500";
            sampleLabel.PointSize = 7;
            sampleLabel.TextColor = Color.Black;
            sampleLabel.SizeWidth = 300;
            sampleLabel.PivotPoint = PivotPoint.Center;

            rootView.Add(sampleLabel);

            window.TouchEvent += OnTouchEvent;
            rootView.TouchEvent += OnViewTouched;
            mCount = 1;
        }

        protected override void OnPause()
        {
            base.OnPause();
        }

        protected override void OnResume()
        {
            base.OnResume();
        }

        protected override void OnResize(Window window)
        {
            base.OnResize(window);
        }

        protected override void OnTerminate(string contentInfo, TerminationType type)
        {
            base.OnTerminate(contentInfo, type);
        }

        protected override void OnUpdate(string contentInfo, int force)
        {
            base.OnUpdate(contentInfo, force);
        }

        private void OnTouchEvent(object source, Window.TouchEventArgs e)
        {
            PointStateType state = e.Touch.GetState(0);
            if (state == PointStateType.Down)
            {
                SetContentInfo("count:" + mCount);
                mCount++;
            }
        }

        private bool OnViewTouched(object sender, View.TouchEventArgs e)
        {
            if (e.Touch.GetState(0) == PointStateType.Down)
            {
                PointStateType state = e.Touch.GetState(0);
                if (state == PointStateType.Down)
                {
                    Bundle bundle = new Bundle();
                    bundle.AddItem("COUNT", "" + mCount);
                    String encodedBundle = bundle.Encode();
                    SetContentInfo(encodedBundle);
                    mCount++;
                }
            }
            return true;
        }

        private int mCount;
    }

    class Program : NUIWidgetApplication
    {
        public Program(Dictionary<System.Type, string> widgetSet) : base(widgetSet)
        {

        }

        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        void Initialize()
        {
        }

        public void OnKeyEvent(object sender, Window.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Down && (e.Key.KeyPressedName == "XF86Back" || e.Key.KeyPressedName == "Escape"))
            {
                Exit();
            }
        }

        static void Main(string[] args)
        {
            Dictionary<System.Type, string> widgetSet = new Dictionary<Type, string>();
            widgetSet.Add(typeof(RedWidget), "class1@org.tizen.example.WidgetTemplate");
            widgetSet.Add(typeof(BlueWidget), "class2@org.tizen.example.WidgetTemplate");
            var app = new Program(widgetSet);
            app.Run(args);
        }
    }
}


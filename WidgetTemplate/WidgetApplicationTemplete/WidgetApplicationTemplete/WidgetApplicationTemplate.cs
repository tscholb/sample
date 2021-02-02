using System;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.Applications;

namespace WidgetApplicationTemplate
{
    class Program : NUIApplication
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }
        void Initialize()
        {
            Window window = GetDefaultWindow();

            window.KeyEvent += OnKeyEvent;
            window.TouchEvent += OnTouchEvent;
            
            rootView = new View();
            rootView.BackgroundColor = Color.White;
            rootView.Size = Window.Instance.Size;
            rootView.PivotPoint = PivotPoint.Center;
            window.GetDefaultLayer().Add(rootView);

            TextLabel sampleLabel = new TextLabel("Widget Viewer ");
            sampleLabel.FontFamily = "SamsungOneUI 500";
            sampleLabel.PointSize = 8;
            sampleLabel.TextColor = Color.Black;
            sampleLabel.SizeWidth = 300;
            sampleLabel.PivotPoint = PivotPoint.Center;
            rootView.Add(sampleLabel);

            Bundle bundle = new Bundle();
            bundle.AddItem("COUNT", "1");
            String encodedBundle = bundle.Encode();

            WidgetViewManager widgetViewManager = new WidgetViewManager(this, this.ApplicationInfo.ApplicationId);

            WidgetView myWidgetView = widgetViewManager.AddWidget("class1@org.tizen.example.WidgetTemplate", encodedBundle, 600, 600, 3.0f);
            myWidgetView.Position = new Position(50, 100);
            window.GetDefaultLayer().Add(myWidgetView);

             myWidgetView.WidgetContentUpdated += OnWidgetContentUpdatedCB;
        }

        public void OnKeyEvent(object sender, Window.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Down && (e.Key.KeyPressedName == "XF86Back" || e.Key.KeyPressedName == "Escape"))
            {
                Exit();
            }
        }
        private void OnTouchEvent(object source, Window.TouchEventArgs e)
        {
        }
        private void OnWidgetContentUpdatedCB(object sender, WidgetView.WidgetViewEventArgs e)
        {
            String encodedBundle = e.WidgetView.ContentInfo;
            Bundle bundle = Bundle.Decode(encodedBundle);
            string outString;
            if (bundle.TryGetItem("COUNT", out outString))
            {
                Tizen.Log.Info("NUI", "OnWidgetContentUpdatedCB : " + outString + "\n");
            }

        }

        static void Main(string[] args)
        {
            var app = new Program();
            app.Run(args);
        }

        private View rootView;
    }
}

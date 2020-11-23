using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using EdgeSwipeSample;
using EdgeSwipeSample.iOS.Renderers;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using MasterDetailPage = Xamarin.Forms.MasterDetailPage;

[assembly: ExportRenderer(typeof(MasterDetailPage), typeof(MyPhoneMasterDetailRenderer))]


namespace EdgeSwipeSample.iOS.Renderers
{
    public class MyPhoneMasterDetailRenderer : PhoneMasterDetailRenderer
    {
        readonly UIRectEdge swipeEdge;

        // This constructor aint probably needed.
        public MyPhoneMasterDetailRenderer(UIRectEdge edge)
        {
            swipeEdge = edge;
        }

        public MyPhoneMasterDetailRenderer()
        {

        }



        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Attach the Edge Gesture.
            var edgeSwipeOpenRecognizer = new UIScreenEdgePanGestureRecognizer();
            edgeSwipeOpenRecognizer.AddTarget(() => InteractiveTransitionRecognizerAction(edgeSwipeOpenRecognizer, true));
            edgeSwipeOpenRecognizer.Edges = UIRectEdge.Left;
            View.AddGestureRecognizer(edgeSwipeOpenRecognizer);

            var edgeSwipeCloseRecognizer = new UIScreenEdgePanGestureRecognizer();
            edgeSwipeCloseRecognizer.AddTarget(() => InteractiveTransitionRecognizerAction(edgeSwipeCloseRecognizer, false));
            edgeSwipeCloseRecognizer.Edges = UIRectEdge.Right;
            View.AddGestureRecognizer(edgeSwipeCloseRecognizer);
        }

        void InteractiveTransitionRecognizerAction(UIScreenEdgePanGestureRecognizer sender, bool presentedValue)
        {
            // If we get swipe from the left edge, we show the menu.

            if (sender.State == UIGestureRecognizerState.Began)
                SetPresentedValue(presentedValue);
        }





        /// <summary>
        /// Attempt to set the internal Presented property.
        /// </summary>
        private void SetPresentedValue(bool newValue)
        {
            foreach (PropertyInfo info in typeof(Xamarin.Forms.Platform.iOS.PhoneMasterDetailRenderer)
                .GetRuntimeProperties())
            {
                if (info.Name == "Presented")
                {
                    info.SetValue(this, newValue);
                    break;
                }
            }
        }


    }
}

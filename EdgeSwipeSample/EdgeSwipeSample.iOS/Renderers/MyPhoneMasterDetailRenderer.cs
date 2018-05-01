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
            var interactiveTransitionRecognizer = new UIScreenEdgePanGestureRecognizer();
            interactiveTransitionRecognizer.AddTarget(() =>
                InteractiveTransitionRecognizerAction(interactiveTransitionRecognizer));
            interactiveTransitionRecognizer.Edges = UIRectEdge.Left;
            View.AddGestureRecognizer(interactiveTransitionRecognizer);
        }

        void InteractiveTransitionRecognizerAction(UIScreenEdgePanGestureRecognizer sender)
        {
            // If we get swipe from the left edge, we show the menu.

            if (sender.State == UIGestureRecognizerState.Began)
                SetPresentedValue(true);
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
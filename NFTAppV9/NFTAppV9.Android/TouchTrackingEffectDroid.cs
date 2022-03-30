﻿using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

using Android.Views;

[assembly: ResolutionGroupName("XamarinDocs")]
[assembly: ExportEffect(typeof(TouchTrackingPlatformEffects.Droid.TouchTrackingEffect), "TouchTrackingEffect")]

namespace TouchTrackingPlatformEffects.Droid
{
    public class TouchTrackingEffect : PlatformEffect
    {
        Android.Views.View view;
        DateTime dtStart;
        DateTime dtEnd;
        DateTime dtPrev;

        protected override void OnAttached()
        {
            System.Diagnostics.Debug.WriteLine("TouchTrackingEffect.OnAttached():entered");

            dtStart = DateTime.UtcNow;
            dtPrev = dtStart;

            view = Control == null ? Container : Control;
            if (view != null)
            {
                view.Touch += OnTouchTrackingHandler;
            }

        }

        protected override void OnDetached()
        {
            System.Diagnostics.Debug.WriteLine("TouchTrackingEffect.OnDetached():entered");

            dtEnd = DateTime.UtcNow;
            TimeSpan timeSpan = dtEnd - dtStart;
            System.Diagnostics.Debug.WriteLine("TouchTrackingEffect.OnDetached(): " + timeSpan.TotalMilliseconds.ToString() + "ms");

            if (view != null)
            {
                view.Touch -= OnTouchTrackingHandler;
            }
        }

        void OnTouchTrackingHandler(object sender, Android.Views.View.TouchEventArgs args)
        {
            Android.Views.View senderView = sender as Android.Views.View;
            MotionEvent motionEvent = args.Event;

            switch (args.Event.ActionMasked)
            {
                case MotionEventActions.ButtonPress:
                case MotionEventActions.ButtonRelease:
                    {
                        break;
                    }
                case MotionEventActions.Down:
                    {
                        DateTime dtDown = DateTime.UtcNow;
                        TimeSpan timeSpan = dtDown - dtPrev;
                        System.Diagnostics.Debug.WriteLine("OnTouchTrackingHandler: " + args.Event.ActionMasked.ToString() + ": "
                                                                                      + motionEvent.Pressure.ToString() + "\t"
                                                                                      + timeSpan.TotalMilliseconds.ToString() + "ms");
                        dtPrev = dtDown;
                        break;
                    }
                case MotionEventActions.Move:
                    {
                        DateTime dtMove = DateTime.UtcNow;
                        TimeSpan timeSpan = dtMove - dtPrev;
                        System.Diagnostics.Debug.WriteLine("OnTouchTrackingHandler: " + args.Event.ActionMasked.ToString() + ": "
                                                                                      + motionEvent.Pressure.ToString() + "\t"
                                                                                      + timeSpan.TotalMilliseconds.ToString() + "ms");
                        dtPrev = dtMove;
                        break;
                    }
                case MotionEventActions.Up:
                    {
                        DateTime dtUp = DateTime.UtcNow;
                        TimeSpan timeSpan = dtUp - dtPrev;
                        System.Diagnostics.Debug.WriteLine("OnTouchTrackingHandler: " + args.Event.ActionMasked.ToString() + ": "
                                                              + motionEvent.Pressure.ToString() + "\t"
                                                              + timeSpan.TotalMilliseconds.ToString() + "ms");
                        dtPrev = dtUp;
                        break;
                    }
                case MotionEventActions.Outside:
                    {
                        break;
                    }
                case MotionEventActions.HoverEnter:
                case MotionEventActions.HoverExit:
                case MotionEventActions.HoverMove:
                    {
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
    }
}
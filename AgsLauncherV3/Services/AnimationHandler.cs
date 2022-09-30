﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace AgsLauncherV3.Services
{
    internal class AnimationHandler
    {
        public static void FadeIn(DependencyObject targetObject)
        {
            var b = targetObject;
            var fade = new DoubleAnimation()
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.15),
            };
            Storyboard.SetTarget(fade, b);
            Storyboard.SetTargetProperty(fade, new PropertyPath(Button.OpacityProperty));
            var sb = new Storyboard();
            sb.Children.Add(fade);
            sb.Begin();
        }
        public static void FadeOut(DependencyObject targetObject)
        {
            var b = targetObject;
            var fade = new DoubleAnimation()
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.15),
            };
            Storyboard.SetTarget(fade, b);
            Storyboard.SetTargetProperty(fade, new PropertyPath(Button.OpacityProperty));
            var sb = new Storyboard();
            sb.Children.Add(fade);
            sb.Begin();
        }

        public static void MovementAnimation(DependencyObject targetObject, double time, Thickness from, Thickness to)
        {
            var b = targetObject;
            var fade = new ThicknessAnimation()
            {
                From = from,
                To = to,
                Duration = TimeSpan.FromSeconds(time),
            };
            Storyboard.SetTarget(fade, b);
            Storyboard.SetTargetProperty(fade, new PropertyPath(Button.MarginProperty));
            var sb = new Storyboard();
            sb.Children.Add(fade);
            sb.Begin();
        }
    }
}

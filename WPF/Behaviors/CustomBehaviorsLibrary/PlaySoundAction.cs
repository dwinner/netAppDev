using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Shapes;
using i = System.Windows.Interactivity;

namespace CustomBehaviorsLibrary
{
   [i.DefaultTriggerAttribute(typeof (ButtonBase), typeof (i.EventTrigger), new object[] {"Click"})]
   [i.DefaultTriggerAttribute(typeof (Shape), typeof (i.EventTrigger), new object[] {"MouseEnter"})]
   [i.DefaultTriggerAttribute(typeof (UIElement), typeof (i.EventTrigger),
      new object[] {"MouseLeftButtonDown"})]
   public class PlaySoundAction : i.TriggerAction<FrameworkElement>
   {
      public static readonly DependencyProperty SourceProperty =
         DependencyProperty.Register("Source", typeof (Uri),
            typeof (PlaySoundAction), new PropertyMetadata(null));

      public Uri Source
      {
         get { return (Uri) GetValue(SourceProperty); }
         set { SetValue(SourceProperty, value); }
      }

      protected override void Invoke(object args)
      {
         // Find a place to insert the MediaElement.
         var container = FindContainer();

         if (container != null)
         {
            // Create and configure the MediaElement.
            var media = new MediaElement {Source = Source};

            // Hook up handlers that will clean up when playback finishes.
            media.MediaEnded += delegate { container.Children.Remove(media); };

            media.MediaFailed += delegate { container.Children.Remove(media); };

            // Add the MediaElement and begin playback.                
            container.Children.Add(media);
         }
      }

      private Panel FindContainer()
      {
         var element = AssociatedObject;

         // Search for some sort of panel where the MediaElement can be inserted.            
         while (element != null)
         {
            var panel = element as Panel;
            if (panel != null)
               return panel;

            element = VisualTreeHelper.GetParent(element) as FrameworkElement;
         }

         return null;
      }
   }
}
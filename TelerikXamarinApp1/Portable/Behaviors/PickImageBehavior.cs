using Plugin.Media;
using Telerik.XamarinForms.RichTextEditor;
using TelerikXamarinApp1.Portable.Helpers;
using Xamarin.Forms;

namespace TelerikXamarinApp1.Portable.Behaviors
{
    public class PickImageBehavior : Behavior<RadRichTextEditor>
    {
        protected override void OnAttachedTo(RadRichTextEditor richTextEditor)
        {
            base.OnAttachedTo(richTextEditor);

            richTextEditor.PickImage += OnPickImage;
        }

        protected override void OnDetachingFrom(RadRichTextEditor richTextEditor)
        {
            base.OnDetachingFrom(richTextEditor);

            richTextEditor.PickImage -= OnPickImage;
        }

        private static async void OnPickImage(object sender, PickImageEventArgs eventArgs)
        {
            var mediaPlugin = CrossMedia.Current;

            if (mediaPlugin.IsPickPhotoSupported)
            {
                if (!await PermissionsHelper.RequestPhotosAccess())
                {
                    return;
                }

                if (!await PermissionsHelper.RequestStorageAccess())
                {
                    return;
                }

                var mediaFile = await mediaPlugin.PickPhotoAsync();

                if (mediaFile != null)
                {
                    var imageSource = RichTextImageSource.FromFile(mediaFile.Path);
                    eventArgs.Accept(imageSource);
                    return;
                }

            }

            eventArgs.Cancel();
        }
    }
}

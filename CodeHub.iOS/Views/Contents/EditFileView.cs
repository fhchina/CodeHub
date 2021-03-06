using System;
using UIKit;
using ReactiveUI;
using CodeHub.iOS.ViewControllers;
using System.Reactive.Linq;
using CodeHub.Core.ViewModels.Contents;

namespace CodeHub.iOS.Views.Contents
{
    public class EditFileView : MessageComposerViewController<EditFileViewModel>
    {
		public EditFileView()
		{
			EdgesForExtendedLayout = UIRectEdge.None;
            TextView.Font = UIFont.FromName("Courier", UIFont.PreferredBody.PointSize);
            TextView.Changed += (sender, e) => ViewModel.Text = Text;
            this.WhenAnyValue(x => x.ViewModel.Text).IsNotNull().Take(1).Subscribe(x => Text = x);
            this.WhenAnyValue(x => x.ViewModel.Text).IsNotNull().Skip(1).Where(x => !string.Equals(x, TextView.Text)).Subscribe(x => TextView.Text = x);
            NavigationItem.RightBarButtonItem = new UIBarButtonItem(Images.SaveButton, UIBarButtonItemStyle.Plain, PromptForCommitMessage);
		}

        private void PromptForCommitMessage(object sender, EventArgs args)
        {
            var viewController = new MessageComposerViewController();
            viewController.Title = "Commit Message";
            ViewModel.WhenAnyValue(x => x.CommitMessage).Subscribe(x => viewController.TextView.Text = x);
            viewController.TextView.Changed += (s, e) => ViewModel.CommitMessage = viewController.TextView.Text;
            viewController.NavigationItem.RightBarButtonItem = ViewModel.SaveCommand.ToBarButtonItem(UIBarButtonSystemItem.Save);
            NavigationController.PushViewController(viewController, true);
        }
    }
}


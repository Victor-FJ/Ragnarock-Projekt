using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace RagnarockApp.Common
{
    /// <summary>
    /// A helper class for displaying popup messages to the user
    /// </summary>
    public class MessageDialogHelper
    {
        /// <summary>
        /// Displays a popup message the user can close
        /// </summary>
        /// <param name="content">The content of the message</param>
        /// <param name="title">The title of the message</param>
        public static async void Show(string content, string title)
        {
            MessageDialog messageDialog = new MessageDialog(content, title);
            await messageDialog.ShowAsync();
        }

        /// <summary>
        /// Displays a popup yes or no quistion for the user to answer. Caller must await boolean answer
        /// </summary>
        /// <param name="content">The content of the message</param>
        /// <param name="title">The title of the message</param>
        /// <returns></returns>
        public static async Task<bool> ShowWInput(string content, string title)
        {
            MessageDialog dialog = new MessageDialog(content, title);
            dialog.Commands.Add(new UICommand("Yes"));
            dialog.Commands.Add(new UICommand("No"));
            IUICommand result = await dialog.ShowAsync();
            return result.Label == "Yes";
        }
    }
}

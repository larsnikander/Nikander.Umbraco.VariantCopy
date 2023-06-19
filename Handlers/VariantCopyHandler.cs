using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Models.Trees;
using Umbraco.Cms.Core.Notifications;

namespace Nikander.Umbraco.VariantCopy.Handlers
{
    public class VariantCopyHandler : INotificationHandler<MenuRenderingNotification>
    {
        public void Handle(MenuRenderingNotification notification)
        {
            if (notification.TreeAlias.Equals("content"))
            {
                MenuItem menuItem = new MenuItem("variantCopy", "Variant Copy");
                menuItem.AdditionalData.Add("actionView", "../App_Plugins/Nikander.Umbraco.VariantCopy/html/variants_add.html");
                menuItem.Icon = "umb-translation";
                int index = notification.Menu.Items.Count;
                notification.Menu.Items.Insert(index, menuItem);
            }
        }
    }
}

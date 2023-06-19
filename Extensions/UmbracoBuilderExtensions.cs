using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Notifications;
using Nikander.Umbraco.VariantCopy.Handlers;

namespace Nikander.Umbraco.VariantCopy.Extensions
{
    public static class UmbracoBuilderExtensions
    {
        public static IUmbracoBuilder AddVariantCopyPlugin(this IUmbracoBuilder builder)
            => builder.AddNotificationHandler<MenuRenderingNotification, VariantCopyHandler>();
    }
}

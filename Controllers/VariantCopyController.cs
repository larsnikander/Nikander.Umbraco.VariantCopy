using System.Linq;
using System;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Web.BackOffice.Controllers;
using System.Collections.Generic;
using Umbraco.Extensions;
using Umbraco.Cms.Web.Common.Attributes;
using Nikander.Umbraco.VariantCopy.Models;

namespace Nikander.Umbraco.VariantCopy.Controllers
{
    [PluginController("Nikander")]
    public class VariantCopyController : UmbracoAuthorizedApiController
    {
        private readonly IContentService _contentService;
        private readonly ILocalizationService _localizationService;

        public VariantCopyController(IContentService contentService, ILocalizationService localizationService)
        {
            _contentService = contentService;
            _localizationService = localizationService;
        }

        public IEnumerable<PublishedCultureResponse> RetrieveCultures(int nodeId)
        {
            var content = _contentService.GetById(nodeId);

            return _localizationService
                .GetAllLanguages()
                .Select(lang => Map(content, lang));

        }

        private PublishedCultureResponse Map(IContent content, ILanguage language)
        {
            return new PublishedCultureResponse()
            {
                Id = language.Id,
                IsoCode = language.IsoCode,
                IsPublished = content.PublishedCultures.Contains(language.IsoCode),
                Name = language.CultureName,
            };

        }

        public VariantResult CreateContentVariants(int? nodeId, bool includeChilderen, string fromCulture, string toCulture)
        {

            if (string.IsNullOrEmpty(fromCulture)) return VariantResult.ErrorResult("Missing From Language");
            if (string.IsNullOrEmpty(toCulture)) return VariantResult.ErrorResult("Missing To Language");
            if (nodeId is null) return VariantResult.ErrorResult("NodeId is not set");

            var contentItems = new List<IContent>();
            IContent? contentItem = _contentService.GetById(nodeId.Value);

            if (contentItem == null) return VariantResult.ErrorResult("content is not found");

            contentItems.Add(contentItem);

            if (includeChilderen)
            {
                contentItems.AddRange(_contentService.GetPagedDescendants(contentItem.Id, 0, 1000000, out long _));
            }

            foreach (var content in contentItems)
            {
                content.SetCultureName(content.Name, toCulture);
                foreach (var property in content.Properties)
                {

                    var value = property
                        .Values
                        .FirstOrDefault(x => x.Culture != null && x.Culture.Equals(fromCulture, StringComparison.InvariantCultureIgnoreCase))?.PublishedValue;
                    if (value != null)
                    {
                        try
                        {
                            content.SetValue(property.Alias, value, toCulture);
                        }
                        catch { continue; }
                    }
                }

                _contentService.Save(content);
            }

            return VariantResult.OkResult();

        }
    }

}

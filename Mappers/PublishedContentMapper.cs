using Examine;
using Phases.Umb.Examine.Search.Extension.Helpers;
using System;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Mapping;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Phases.Umb.Examine.Search.Extension.Mappers
{
    internal class PublishedContentMapper : IMapDefinition
    {
        private readonly PublishedContentHelper _publishedContentHelper;

        public PublishedContentMapper(PublishedContentHelper publishedContentHelper)
        {
            _publishedContentHelper = publishedContentHelper;
        }

        public void DefineMaps(IUmbracoMapper mapper)
        {
            mapper.Define<ISearchResult, IPublishedContent>((source, context) => _publishedContentHelper.GetByString(source.Id));

            mapper.Define<string, IPublishedContent>((source, context) => _publishedContentHelper.GetByString(source));

            mapper.Define<Guid, IPublishedContent>((source, context) => _publishedContentHelper.GetByGuid(source));

            mapper.Define<Udi, IPublishedContent>((source, context) => _publishedContentHelper.GetByUdi(source));
        }
    }
}
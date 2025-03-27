using Microsoft.Extensions.DependencyInjection;
using Phases.Umb.Examine.Search.Extension.Helpers;
using Phases.Umb.Examine.Search.Extension.Interfaces;
using Phases.Umb.Examine.Search.Extension.Mappers;
using Phases.Umb.Examine.Search.Extension.Services;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Web.Common.DependencyInjection;

namespace Phases.Umb.Examine.Search.Extension.Composing
{
    public class SearchComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.Services.AddTransient<ICustomService, CustomService>();
            builder.Services.AddTransient<ISuggestionService, SuggestionService>();
            builder.Services.AddTransient<IStringFormatHelper, StringFormatHelper>();
            builder.Services.AddSingleton<PublishedContentHelper>();

            builder.MapDefinitions().Add<PublishedContentMapper>();

            ServiceLocator.Configure(type => StaticServiceProvider.Instance.GetService(type));
        }
    }
}
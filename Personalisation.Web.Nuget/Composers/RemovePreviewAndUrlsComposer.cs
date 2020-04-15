using Personalisation.Web.Models.DocumentTypes;
using System.Web.Mvc;
using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Web;
using Umbraco.Web.Editors;
using Umbraco.Web.Models.ContentEditing;

namespace Personalisation.Web.Composers
{
    public class RegisterRemovePreviewAndUrlsEventComposer : ComponentComposer<RegisterRemovePreviewAndUrlsEventComponent>, IUserComposer
    { }

    public class RegisterRemovePreviewAndUrlsEventComponent : IComponent
    {
        public void Initialize()
        {
            EditorModelEventManager.SendingContentModel += EditorModelEventManager_SendingContentModel;

        }
        public static void EditorModelEventManager_SendingContentModel(System.Web.Http.Filters.HttpActionExecutedContext sender, EditorModelEventArgs<ContentItemDisplay> e)
        {
            if (e.Model is ContentItemDisplay contentModel)
            {
                var umbracoContext = Current.Factory.GetInstance<IUmbracoContextFactory>();

                if (umbracoContext != null)
                    using (var cref = umbracoContext.EnsureUmbracoContext())
                    {
                        var contentItem = cref.UmbracoContext?.Content?.GetById(contentModel.Id);

                        // Hide Preview Button and URL's if no template assigned. 
                        if (string.IsNullOrEmpty(contentModel.TemplateAlias))
                        {
                            contentModel.AllowPreview = false;
                            contentModel.Urls = null;
                        }

                        // Hide Preview Button and URL's if inside data repository.
                        if (contentItem?.AncestorOrSelf<DataRepository>() != null)
                        {
                            contentModel.AllowPreview = false;
                            contentModel.Urls = null;
                        }
                    }
            }
        }

        public void Terminate() { }
    }
}

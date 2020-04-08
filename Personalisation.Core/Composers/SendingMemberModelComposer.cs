using Personalisation.Core.Config;
using Personalisation.Core.Models;
using Personalisation.Core.Services;
using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Web;
using Umbraco.Web.Editors;
using Umbraco.Web.Models.ContentEditing;

namespace Personalisation.Core.Composers
{
    public class SendingMemberModelComposer : ComponentComposer<SendingMemberModelComponent>, IUserComposer
    { }

    public class SendingMemberModelComponent : IComponent
    {
        public void Initialize()
        {
            EditorModelEventManager.SendingMemberModel += EditorModelEventManager_SendingMemberModel;
        }

        private void EditorModelEventManager_SendingMemberModel(System.Web.Http.Filters.HttpActionExecutedContext sender, EditorModelEventArgs<MemberDisplay> e)
        {
            if (e.Model is MemberDisplay memberDisplay)
            {
                var personalisationConfig = DependencyResolver.Current.GetService<PersonalisationConfig>();
                if (personalisationConfig == null) return;

                if (!memberDisplay.Properties?.Any() ?? true) return;
                var property = memberDisplay.Properties.FirstOrDefault(x => x.Alias == personalisationConfig.PropertyAlias);
                if (property == null) return;

                var personalisationService = DependencyResolver.Current.GetService<IPersonalisationService>();
                if (personalisationService == null) return;

                var tags = personalisationService.GetFromStore((int)memberDisplay.Id);
                if (tags?.Any() ?? false)
                {
                    var sb = new StringBuilder();
                    sb.Append("<table class=\"table\" style=\"max-width:800px;\">");
                    sb.Append($"<tr><th>{nameof(PersonalisationTag.Tag)}</th><th>{nameof(PersonalisationTag.Score)}</th></tr>");

                    foreach (var x in tags.OrderByDescending(x => x.Score))
                        sb.Append($"<tr><td>{x.Tag}</td><td>{x.Score}<td></tr>");

                    sb.Append("</table>"); 

                    property.Value = sb.ToString();
                    property.Readonly = true;  
                }
            }
        }

        public void Terminate() { }
    }
}
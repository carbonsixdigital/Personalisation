using System.Collections.Generic;
using System.Linq;
using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Core.Services;
using Umbraco.Web.Trees;

namespace Personalisation.Web.Composers
{
    [RuntimeLevel(MinLevel = RuntimeLevel.Run)]
    public class TreeNodeRenderingComposer : ComponentComposer<TreeNodeRenderingComponent>, IUserComposer
    { }

    public class TreeNodeRenderingComponent : IComponent
    {
        private readonly IContentTypeService _contentTypeService;
        private readonly IMediaTypeService _mediaTypeService;
        private readonly IMemberTypeService _memberTypeService;

        public TreeNodeRenderingComponent(IContentTypeService contentTypeService, IMediaTypeService mediaTypeService, IMemberTypeService memberTypeService)
        {
            _contentTypeService = contentTypeService;
            _mediaTypeService = mediaTypeService;
            _memberTypeService = memberTypeService;
        }

        public void Initialize()
        {
            TreeControllerBase.TreeNodesRendering += TreeControllerBase_TreeNodesRendering;
        }

        public void Terminate()
        { }

        private void TreeControllerBase_TreeNodesRendering(TreeControllerBase sender, TreeNodesRenderingEventArgs e)
        {
            switch (sender.TreeAlias)
            {
                case Constants.Trees.DocumentTypes:
                    UpdateNodeIcons(e, _contentTypeService.GetAll()?.ToDictionary(c => c.Id, c => c.Icon), Constants.Trees.DocumentTypes);
                    break;
                case Constants.Trees.MemberTypes:
                    UpdateNodeIcons(e, _memberTypeService.GetAll()?.ToDictionary(c => c.Id, c => c.Icon), Constants.Trees.MemberTypes);
                    break;
                case Constants.Trees.MediaTypes:
                    UpdateNodeIcons(e, _mediaTypeService.GetAll()?.ToDictionary(c => c.Id, c => c.Icon), Constants.Trees.MediaTypes);
                    break;
            }
        }

        private static void UpdateNodeIcons(TreeNodesRenderingEventArgs e, Dictionary<int, string> nodeIcons, string nodeTypeAlias)
        {
            foreach (var node in e.Nodes)
                if (node.NodeType == nodeTypeAlias)
                    if (int.TryParse(node.Id.ToString(), out var nodeId) && nodeId > 0)
                        node.Icon = nodeIcons[nodeId];
        }
    }
}


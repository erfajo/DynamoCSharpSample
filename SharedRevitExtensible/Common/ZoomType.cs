// Copyright(c) 2014 by Author
// This file is covered by the LICENSE at the repository root folder

using System.Collections.Generic;
using System.Linq;
using CoreNodeModels;
using Dynamo.Graph.Nodes;
using Dynamo.Utilities;
using Newtonsoft.Json;
using ProtoCore.AST.AssociativeAST;

namespace DynamoCSharpSample.Common
{
    [NodeName("Print ZoomType"),
        NodeCategory("DynamoCSharpSample.Common.Print"),
        NodeDescription("Dropdown for printer zoom type values"),
        OutPortDescriptions("Print zoom type as string"),
        IsDesignScriptCompatible]
    public class ZoomType : DSDropDownBase
    {
        private const string outputName = "ZoomType";

        public ZoomType() : base(outputName) { }

        [JsonConstructor]
        public ZoomType(IEnumerable<PortModel> inPorts, IEnumerable<PortModel> outPorts) 
            : base(outputName, inPorts, outPorts) { }

        protected override SelectionState PopulateItemsCore(string currentSelection)
        {
            var elements = new Dictionary<string, object>
            {
                { "Fit To Page", Autodesk.Revit.DB.ZoomType.FitToPage },
                { "Zoom", Autodesk.Revit.DB.ZoomType.Zoom }
            };

            Items = elements
                .Select(x => new DynamoDropDownItem(x.Key, x.Value.ToString()))
                .ToObservableCollection();

            SelectedIndex = 1;
            return SelectionState.Done;
        }

        public override IEnumerable<AssociativeNode> BuildOutputAst(List<AssociativeNode> inputAstNodes)
        {
            if (Items.Count == 0 || Items[0].Name == "NoTypesFound" || SelectedIndex == -1)
            { return new[] { AstFactory.BuildAssignment(GetAstIdentifierForOutputIndex(0), AstFactory.BuildNullNode()) }; }

            var assign = AstFactory.BuildStringNode((string)Items[SelectedIndex].Item);
            return new[] { AstFactory.BuildAssignment(GetAstIdentifierForOutputIndex(0), assign) };
        }
    }
}

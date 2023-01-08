// Copyright(c) 2014 by Author
// This file is covered by the LICENSE at the repository root folder

using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using CoreNodeModels;
using Dynamo.Graph.Nodes;
using Dynamo.Utilities;
using Newtonsoft.Json;
using ProtoCore.AST.AssociativeAST;

namespace DynamoCSharpSample.Common
{
    [NodeName("Printer"), 
        NodeCategory("DynamoCSharpSample.Common.Print"),        
        NodeDescription("Dropdown for available printer(s)"),
        OutPortDescriptions("Printer as string"),
        IsDesignScriptCompatible]
    public class Printer : DSDropDownBase
    {
        private const string OutputName = "Printer";

        public Printer() : base(OutputName) { }

        [JsonConstructor]
        public Printer(IEnumerable<PortModel> inPorts, IEnumerable<PortModel> outPorts)
            : base(OutputName, inPorts, outPorts) { }

        protected override SelectionState PopulateItemsCore(string currentSelection)
        {
            // get installed printers and turn the coollection into a list
            var printers = PrinterSettings.InstalledPrinters.Cast<string>().ToList();

            // error case, if no printers are installed
            if (!printers.Any())
            {
                Items.Clear();
                Items.Add(new DynamoDropDownItem("NoTypesFound", null));
                SelectedIndex = 0;
                return SelectionState.Done;
            }

            Items = printers
                .Select(x => new DynamoDropDownItem(x, x))
                .ToObservableCollection();

            SelectedIndex = 0;
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

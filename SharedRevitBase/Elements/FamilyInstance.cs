// Copyright(c) 2014 by Author
// This file is covered by the LICENSE at the repository root folder

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Autodesk.DesignScript.Runtime;
using Autodesk.Revit.DB;
using Dynamo.Graph.Nodes;
using DynamoSample.Notifications;
using Revit.Elements;
using RevitServices.Persistence;
using dynElement = Revit.Elements.Element;
using dynFamilyInstance = Revit.Elements.FamilyInstance;
using rvtElement = Autodesk.Revit.DB.Element;
using rvtFamilyInstance = Autodesk.Revit.DB.FamilyInstance;

namespace DynamoCSharpSample.Elements
{
    public class FamilyInstance
    {
        #region Constructor
        private FamilyInstance() { }
        #endregion Constructor


        #region How notifications can be used
        /// <summary>
        /// Get element by id.
        /// </summary>
        /// <param name="id">Id as integer value.</param>
        /// <returns name="Element">Element.</returns>
        /// <search>element,id,elementid</search>
        [NodeCategory("Query")]
        public static dynElement ById(int id)
        {
            var document = DocumentManager.Instance.CurrentDBDocument;
            try
            {
                var elementId = new ElementId(id);
                var element = document.GetElement(elementId);
                return element.ToDSType(true);
            }
            catch (System.NullReferenceException)
            {
                throw new System.Exception(Notifications.InvalidElementId);
            }            
        }
        #endregion How notifications can be used


        #region Boolean flipped methods for elements
        /// <summary>
        /// Get the family instance hand flip condition, output as boolean
        /// </summary>
        /// <param name="familyInstance">FamilyInstance as Dynamo type</param>
        /// <returns name="bool">Boolean value</returns>
        public static bool IsHandFlipped(dynFamilyInstance familyInstance)
        {
            var item = familyInstance.InternalElement as rvtFamilyInstance;
            return item.HandFlipped != item.FacingFlipped;
        }

        /// <summary>
        /// Get the family instance face flip condition, output as boolean
        /// </summary>
        /// <param name="familyInstance">FamilyInstance as Dynamo type</param>
        /// <returns name="bool">Boolean value</returns>
        public static bool IsFaceFlipped(dynFamilyInstance familyInstance)
        {
            var item = familyInstance.InternalElement as rvtFamilyInstance;
            return item.FacingFlipped;
        }
        #endregion Boolean flipped methods for elements


        #region Flipped methods for elements
        /// <summary>
        /// Get the family instance hand flip condition, output as element
        /// </summary>
        /// <param name="familyInstance">FamilyInstance as Dynamo type</param>
        /// <returns name="true">Flipped element</returns>
        /// <returns name="false">Not flipped element</returns>
        [MultiReturn(new[] { "d0", "d1" })]
        public static IDictionary HandFlipped(List<dynFamilyInstance> familyInstance)
        {
            var keys = familyInstance
                .Select(x => x.InternalElement as rvtFamilyInstance)
                .Select(x => x.HandFlipped != x.FacingFlipped);

            var groups = familyInstance
                .Zip(keys, (item, key) => new { item, key });

            var flippedTrue = groups
                .Where(x => x.key == true)
                .Select(x => x.item);

            var flippedFalse = groups
                .Where(x => x.key == false)
                .Select(x => x.item);

            return new Dictionary<string, object>
            {
                {"d0", flippedTrue},
                {"d1", flippedFalse}
            };

        }

        /// <summary>
        /// Get the family instance face flip condition, output as element
        /// </summary>
        /// <param name="familyInstance">FamilyInstance as Dynamo type</param>
        /// <returns name="true">Flipped element</returns>
        /// <returns name="false">Not flipped element</returns>
        [MultiReturn(new[] { "d0", "d1" })]
        public static IDictionary FaceFlipped(List<dynFamilyInstance> familyInstance)
        {
            var keys = familyInstance
                .Select(x => x.InternalElement as rvtFamilyInstance)
                .Select(x => x.FacingFlipped);

            var groups = familyInstance
                .Zip(keys, (item, key) => new { item, key });

            var flippedTrue = groups
                .Where(x => x.key == true)
                .Select(x => x.item);

            var flippedFalse = groups
                .Where(x => x.key == false)
                .Select(x => x.item);

            return new Dictionary<string, object>
            {
                {"d0", flippedTrue},
                {"d1", flippedFalse}
            };

        }
        #endregion Flipped methods for elements

    }
}

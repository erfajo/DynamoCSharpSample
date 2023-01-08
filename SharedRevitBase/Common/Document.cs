// Copyright(c) 2014 by Author
// This file is covered by the LICENSE at the repository root folder

using RevitServices.Persistence;

namespace DynamoCSharpSample.Common
{
    public class Document
    {
        #region Constructor
        private Document() { }
        #endregion Constructor


        /// <summary>
        /// Get current document
        /// </summary>
        /// <returns name="Document">Document as Orchid type</returns>
        public static Autodesk.Revit.DB.Document Current => DocumentManager.Instance.CurrentDBDocument;

    }
}

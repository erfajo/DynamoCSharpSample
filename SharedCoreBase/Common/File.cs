// Copyright(c) 2014 by Author
// This file is covered by the LICENSE at the repository root folder

using System;
using System.IO;

#pragma warning disable CS1724 // Type names should not match namespaces
namespace DynamoCSharpSample.Common
{
    public class File
    {
        #region Constructor
        private File() { }
        #endregion Constructor

        /// <summary>
        /// Get file from a path
        /// </summary>
        /// <param name="path">Path to the file</param>
        /// <returns name="file">File object</returns>
        public static string FromPath(string path)
        {
            string obj = Environment.ExpandEnvironmentVariables(path);
            FileAttributes att = FileAttributes.Directory;
            if (!System.IO.File.GetAttributes(obj).HasFlag(att))
            {
                return obj;
            }
            else
            {
                throw new FileNotFoundException();
            }
        }

    }
}

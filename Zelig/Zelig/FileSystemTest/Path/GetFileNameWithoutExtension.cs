////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Microsoft Corporation.  All rights reserved.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;



using System.IO;

namespace FileSystemTest
{
    public class GetFileNameWithoutExtension : IMFTestInterface
    {
        [SetUp]
        public InitializeResult Initialize()
        {
            Log.Comment("Adding set up for the tests");

            return InitializeResult.ReadyToGo;
        }

        [TearDown]
        public void CleanUp()
        {
        }

        #region helper methods

        private bool TestGetFileNameWithoutExtension(string path, string expected)
        {
            string result = Path.GetFileNameWithoutExtension(path);
            Log.Comment("Path: '" + path + "'");
            Log.Comment("Expected: '" + expected + "'");
            if (result != expected)
            {
                Log.Exception("Got: '" + result + "'");
                return false;
            }
            return true;
        }

        #endregion helper methods

        #region Test Cases

        [TestMethod]
        public MFTestResults Null()
        {
            MFTestResults result = MFTestResults.Pass;
            try
            {
                if (!TestGetFileNameWithoutExtension(null, null))
                {
                    return MFTestResults.Fail;
                }
                if (!TestGetFileNameWithoutExtension("", ""))
                {
                    return MFTestResults.Fail;
                }
                if (!TestGetFileNameWithoutExtension(string.Empty, string.Empty))
                {
                    return MFTestResults.Fail;
                }
                if (!TestGetFileNameWithoutExtension(".ext", ""))
                {
                    return MFTestResults.Fail;
                }
            }
            catch (Exception ex)
            {
                Log.Exception("Unexpected exception: " + ex.Message);
                return MFTestResults.Fail;
            }
            return result;
        }

        [TestMethod]
        public MFTestResults ValidCases()
        {
            MFTestResults result = MFTestResults.Pass;
            try
            {
                if (!TestGetFileNameWithoutExtension("file", "file"))
                {
                    return MFTestResults.Fail;
                }
                if (!TestGetFileNameWithoutExtension("file.txt", "file"))
                {
                    return MFTestResults.Fail;
                }
                if (!TestGetFileNameWithoutExtension(@"\sd1\dir1\file.txt", "file"))
                {
                    return MFTestResults.Fail;
                }
                if (!TestGetFileNameWithoutExtension(@"dir1\dir2\file", "file"))
                {
                    return MFTestResults.Fail;
                }
                if (!TestGetFileNameWithoutExtension("file.........ext...", "file.........ext.."))
                {
                    return MFTestResults.Fail;
                }
                string file = "foo.bar.fkl;fkds92-509450-4359.$#%()#%().%#(%)_#(%_)";
                if (!TestGetFileNameWithoutExtension(file + ".cool", file))
                {
                    return MFTestResults.Fail;
                }
                file = new string('x', 256);
                if (!TestGetFileNameWithoutExtension("cool." + file, "cool"))
                {
                    return MFTestResults.Fail;
                }
                if (!TestGetFileNameWithoutExtension(file + ".c", file))
                {
                    return MFTestResults.Fail;
                }
                if (!TestGetFileNameWithoutExtension(file + "." + file, file))
                {
                    return MFTestResults.Fail;
                }
                if (!TestGetFileNameWithoutExtension("file.....ext", "file...."))
                {
                    return MFTestResults.Fail;
                }
            }
            catch (Exception ex)
            {
                Log.Exception("Unexpected exception: " + ex.Message);
                return MFTestResults.Fail;
            }
            return result;
        }

        [TestMethod]
        public MFTestResults ArgumentExceptionTests()
        {
            MFTestResults result = MFTestResults.Pass;
            try
            {
                foreach (char invalidChar in Path.GetInvalidPathChars())
                {
                    try
                    {
                        Log.Comment("Invalid char ascii val = " + (int)invalidChar);
                        string path = new string(new char[] { invalidChar, 'b', 'a', 'd', '.', invalidChar, 'p', 'a', 't', 'h', invalidChar });
                        string dir = Path.GetFileNameWithoutExtension(path);
                        if ((path.Length == 0) && (dir.Length == 0))
                        {
                            /// If path is empty string, returned value is also empty string (same behavior in desktop)
                            /// no exception thrown.
                        }
                        else
                        {
                            Log.Exception("Expected Argument exception for '" + path + "' but got: '" + dir + "'");
                            return MFTestResults.Fail;
                        }
                    }
                    catch (ArgumentException ae) { /* pass case */ Log.Comment( "Got correct exception: " + ae.Message ); }
                }
            }
            catch (Exception ex)
            {
                Log.Exception("Unexpected exception: " + ex.Message);
                return MFTestResults.Fail;
            }
            return result;
        }

        #endregion Test Cases

        public MFTestMethod[] Tests
        {
            get
            {
                return new MFTestMethod[] 
                {
                    new MFTestMethod( Null, "Null" ),
                    new MFTestMethod( ValidCases, "ValidCases" ),
                    new MFTestMethod( ArgumentExceptionTests, "ArgumentExceptionTests" ),
                };
             }
        }
    }
}

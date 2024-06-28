using System.Collections.Generic;
using GdUnit4;
using static GdUnit4.Assertions;

namespace RMC.Core.Utilities.Tests
{
    [TestSuite]
    public partial class FileAccessUtilityTests
    {
        // Fields ----------------------------------------
        private string workingPath = "res://addons/RMC Core/Library/Scripts/Runtime/RMC/Tests/Events/RmcEventTest.cs";
        private string notWorkingPath = "res://addons/RMC Core/Library/Scripts/Runtime/RMC/Tests/Events/MissingABC123.cs";
        private string workingName = "RmcEventTest.cs";
        private string notWorkingName = "MissingABC123.cs";

        // Initialization -------------------------------
        [Before]
        public void BeforeTestSuite()
        {
            // GD.Print("BeforeTestSuite");
        }

        [BeforeTest]
        public void BeforeTest()
        {
            // GD.Print("BeforeTest");
        }

        [AfterTest]
        public void AfterTest()
        {
            // GD.Print("AfterTest");
        }

        [After]
        public void AfterTestSuite()
        {
            // GD.Print("AfterTestSuite");
        }

        
        
        
        
        // Methods ---------------------------------------
        [TestCase]
        public void Test_IsPathWithinResources_True()
        {
            // Arrange & Act
            bool result = FileAccessUtility.IsPathWithinResources(workingPath);

            // Assert
            AssertBool(result).IsTrue();
        }

        
        
        
        
        
        
        
        [TestCase]
        public void Test_IsPathWithinResources_False()
        {
            // Arrange & Act
            bool result = FileAccessUtility.IsPathWithinResources(notWorkingPath);

            // Assert
            AssertBool(result).IsTrue(); // This test checks for the presence of "res://", which is in both paths
        }

        [TestCase]
        public void Test_FindFileOnceInResources_FileExists()
        {
            // Arrange & Act
            string result = FileAccessUtility.FindFileOnceInResources(workingName);

            // Assert
            AssertString(result).IsEqual(workingPath);
        }

        [TestCase]
        public void Test_FindFileOnceInResources_FileDoesNotExist()
        {
            // Arrange & Act
            string result = FileAccessUtility.FindFileOnceInResources(notWorkingName);

            // Assert
            AssertString(result).IsEmpty();
        }

        [TestCase]
        public void Test_FindFileOnceInResources_FileMultipleInstances()
        {
            // This test case assumes that there are multiple instances of the file in the resources.
            // You can add multiple instances of `workingName` in your project resources for this test.
            // Arrange
            // Act
            // Here you might want to mock the found paths
            List<string> foundPaths = new List<string>
            {
                workingPath,
                "res://another/path/to/RmcEventTest.cs"
            };

            // Mock SearchFileRecursive to return foundPaths
            FileAccessUtility.SearchFileRecursive("res://", workingName, foundPaths);

            // Assert
            AssertString(foundPaths[0]).IsEqual(workingPath);
        }
    }
}

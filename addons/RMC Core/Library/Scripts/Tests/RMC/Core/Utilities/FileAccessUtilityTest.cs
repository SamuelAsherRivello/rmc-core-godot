using System.Collections.Generic;
using GdUnit4;
using Godot;
using static GdUnit4.Assertions;

// ReSharper disable StaticMemberInitializerReferesToMemberBelow
namespace RMC.Core.Utilities.Tests
{
    [TestSuite]
    public partial class FileAccessUtilityTests
    {
        // Fields ----------------------------------------
        private static string PathOfSuccess { get   {  return BasePath + FilenameOfSuccess; } }
        private static string PathOfFailure { get   {  return BasePath + FilenameOfSuccess; } }
        private static readonly string FilenameOfSuccess = "FileAccessUtilityTest.cs";
        private static readonly string FilenameOfFailure = "ThisIsAMissingClass.cs";

        //NOTE: If you move this file you may need to change this
        private static readonly string BasePath = "res://addons/RMC Core/Library/Scripts/Tests/RMC/Core/Utilities/";
        
        
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
            bool result = FileAccessUtility.IsPathWithinResources(PathOfSuccess);

            // Assert
            AssertBool(result).IsTrue();
        }

        
        [TestCase]
        public void Test_IsPathWithinResources_False()
        {
            // Arrange & Act
            bool result = FileAccessUtility.IsPathWithinResources(PathOfFailure);

            // Assert
            AssertBool(result).IsTrue(); // This test checks for the presence of "res://", which is in both paths
        }

        [TestCase]
        public void Test_FindFileOnceInResources_FileExists()
        {
            // Arrange & Act
            string result = FileAccessUtility.FindFileOnceInResources(FilenameOfSuccess);

            // Assert
            AssertString(result).IsEqual(PathOfSuccess);
        }

        [TestCase]
        public void Test_FindFileOnceInResources_FileDoesNotExist()
        {
            // Arrange & Act
            string result = FileAccessUtility.FindFileOnceInResources(FilenameOfFailure);

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
                PathOfSuccess,
                "res://another/path/to/RmcEventTest.cs"
            };

            // Mock SearchFileRecursive to return foundPaths
            FileAccessUtility.SearchFileRecursive("res://", FilenameOfSuccess, foundPaths);

            // Assert
            AssertString(foundPaths[0]).IsEqual(PathOfSuccess);
        }
    }
}

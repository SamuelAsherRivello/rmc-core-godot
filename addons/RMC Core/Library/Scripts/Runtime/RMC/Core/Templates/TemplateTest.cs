using GdUnit4;
using Godot;
using static GdUnit4.Assertions;

namespace RMC.Core.Templates
{
	/// <summary>
	/// TODO: Add comments
	/// </summary>
    [TestSuite]
	public partial class TemplateTest 
	{
		//  Clases ----------------------------------------
        public partial class SampleClass { }
        public partial class SampleNode : Node { }
        
		//  Fields ----------------------------------------
        
		
		//  Initialization  -------------------------------
        
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
		
        
		//  Methods ---------------------------------------
        [TestCase]
        public void Add_ResultIs30_WhenX10Y20()
        {
            // Arrange
            var x = 10;
            var y = 20;

            // Act
            var result = x + y;

            // Assert
            AssertInt(result).IsEqual(x + y);
        }
        
		
		//  Event Handlers --------------------------------
	}
}

using GdUnit4;
using static GdUnit4.Assertions;

namespace RMC.Core.Observables.Tests
{
    [TestSuite]
    public partial class ObservableTests
    {
        // Fields ----------------------------------------
        private (int, int) _receivedValues;

        // Initialization -------------------------------
        [Before]
        public void BeforeTestSuite()
        {
            // GD.Print("BeforeTestSuite");
        }

        [BeforeTest]
        public void BeforeTest()
        {
            _receivedValues = (0, 0);
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
        public void Test_Constructor_Default()
        {
            // Arrange & Act
            var observable = new Observable<int>();

            // Assert
            AssertInt(observable.Value).IsEqual(0);
        }

        [TestCase]
        public void Test_Constructor_WithInitialValue()
        {
            // Arrange & Act
            var observable = new Observable<int>(10);

            // Assert
            AssertInt(observable.Value).IsEqual(10);
        }

        [TestCase]
        public void Test_SetValue()
        {
            // Arrange
            var observable = new Observable<int>();
            observable.OnValueChanged.AddListener((oldValue, newValue) => _receivedValues = (oldValue, newValue));

            // Act
            observable.Value = 10;

            // Assert
            AssertInt(observable.Value).IsEqual(10);
            AssertInt(_receivedValues.Item1).IsEqual(0);
            AssertInt(_receivedValues.Item2).IsEqual(10);
        }

        [TestCase]
        public void Test_OnValueChangedRefresh()
        {
            // Arrange
            var observable = new Observable<int>();
            observable.OnValueChanged.AddListener((oldValue, newValue) => _receivedValues = (oldValue, newValue));
            observable.Value = 10;

            // Act
            observable.OnValueChangedRefresh();

            // Assert
            AssertInt(_receivedValues.Item1).IsEqual(0);
            AssertInt(_receivedValues.Item2).IsEqual(10);
        }

        [TestCase]
        public void Test_OnValueChanging()
        {
            // Arrange
            var observable = new Observable<int>();
            observable.OnValueChanged.AddListener((oldValue, newValue) => _receivedValues = (oldValue, newValue));

            // Act
            observable.Value = 10;

            // Assert
            AssertInt(observable.Value).IsEqual(10);
            AssertInt(_receivedValues.Item1).IsEqual(0);
            AssertInt(_receivedValues.Item2).IsEqual(10);
        }
    }
}

using GdUnit4;
using static GdUnit4.Assertions;

namespace RMC.Core.Events
{
    [TestSuite]
    public partial class RmcEventTests
    {
        // Fields ----------------------------------------
        private int _counter;
        private string _receivedValue;
        private (int, string) _receivedValues;

        // Initialization -------------------------------
        [Before]
        public void BeforeTestSuite()
        {
            // GD.Print("BeforeTestSuite");
        }

        [BeforeTest]
        public void BeforeTest()
        {
            _counter = 0;
            _receivedValue = null;
            _receivedValues = (0, null);
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
        public void Test_Add_Remove_Listener_RmcEvent()
        {
            // Arrange
            var rmcEvent = new RmcEvent();
            RmcEventHandler handler = () => _counter++;

            // Act
            rmcEvent.AddListener(handler);
            rmcEvent.Invoke();
            rmcEvent.RemoveListener(handler);
            rmcEvent.Invoke();

            // Assert
            AssertInt(_counter).IsEqual(1);
        }

        [TestCase]
        public void Test_Invoke_RmcEvent()
        {
            // Arrange
            var rmcEvent = new RmcEvent();
            RmcEventHandler handler = () => _counter++;
            rmcEvent.AddListener(handler);

            // Act
            rmcEvent.Invoke();

            // Assert
            AssertInt(_counter).IsEqual(1);
        }

        [TestCase]
        public void Test_OnValueChangedRefresh_RmcEvent()
        {
            // Arrange
            var rmcEvent = new RmcEvent();
            RmcEventHandler handler = () => _counter++;
            rmcEvent.AddListener(handler);

            // Act
            rmcEvent.OnValueChangedRefresh();

            // Assert
            AssertInt(_counter).IsEqual(1);
        }

        [TestCase]
        public void Test_Add_Remove_Listener_RmcEvent_T()
        {
            // Arrange
            var rmcEvent = new RmcEvent<string>();
            RmcEventHandler<string> handler = value => _receivedValue = value;

            // Act
            rmcEvent.AddListener(handler);
            rmcEvent.Invoke("Hello");
            rmcEvent.RemoveListener(handler);
            rmcEvent.Invoke("World");

            // Assert
            AssertString(_receivedValue).IsEqual("Hello");
        }

        [TestCase]
        public void Test_Invoke_RmcEvent_T()
        {
            // Arrange
            var rmcEvent = new RmcEvent<string>();
            RmcEventHandler<string> handler = value => _receivedValue = value;
            rmcEvent.AddListener(handler);

            // Act
            rmcEvent.Invoke("Hello");

            // Assert
            AssertString(_receivedValue).IsEqual("Hello");
        }

        [TestCase]
        public void Test_OnValueChangedRefresh_RmcEvent_T()
        {
            // Arrange
            var rmcEvent = new RmcEvent<string>();
            RmcEventHandler<string> handler = value => _receivedValue = value;
            rmcEvent.AddListener(handler);

            // Act
            rmcEvent.OnValueChangedRefresh("Hello");

            // Assert
            AssertString(_receivedValue).IsEqual("Hello");
        }

        [TestCase]
        public void Test_Add_Remove_Listener_RmcEvent_T_U()
        {
            // Arrange
            var rmcEvent = new RmcEvent<int, string>();
            RmcEventHandler<int, string> handler = (oldValue, newValue) => _receivedValues = (oldValue, newValue);

            // Act
            rmcEvent.AddListener(handler);
            rmcEvent.Invoke(1, "Hello");
            rmcEvent.RemoveListener(handler);
            rmcEvent.Invoke(2, "World");

            // Assert
            AssertInt(_receivedValues.Item1).IsEqual(1);
            AssertString(_receivedValues.Item2).IsEqual("Hello");
        }

        [TestCase]
        public void Test_Invoke_RmcEvent_T_U()
        {
            // Arrange
            var rmcEvent = new RmcEvent<int, string>();
            RmcEventHandler<int, string> handler = (oldValue, newValue) => _receivedValues = (oldValue, newValue);
            rmcEvent.AddListener(handler);

            // Act
            rmcEvent.Invoke(1, "Hello");

            // Assert
            AssertInt(_receivedValues.Item1).IsEqual(1);
            AssertString(_receivedValues.Item2).IsEqual("Hello");
        }

        [TestCase]
        public void Test_OnValueChangedRefresh_RmcEvent_T_U()
        {
            // Arrange
            var rmcEvent = new RmcEvent<int, string>();
            RmcEventHandler<int, string> handler = (oldValue, newValue) => _receivedValues = (oldValue, newValue);
            rmcEvent.AddListener(handler);

            // Act
            rmcEvent.OnValueChangedRefresh(1, "Hello");

            // Assert
            AssertInt(_receivedValues.Item1).IsEqual(1);
            AssertString(_receivedValues.Item2).IsEqual("Hello");
        }
    }
}

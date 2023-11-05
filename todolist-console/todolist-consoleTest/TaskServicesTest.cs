using todolist_console.Enums;

namespace todolist_consoleTest
{
    [TestClass]
    public class TaskServicesTest
    {
        [TestMethod]
        public void CreateTask_ValidInput_ReturnsTask()
        {
            // Arrange
            var mockConsole = new Mock<IConsoleInput>();
            mockConsole.SetupSequence(c => c.ReadLine())
                .Returns("Valid Title");

            var service = new TaskService(mockConsole.Object);

            // Actual
            var task = service.CreateTask();

            // Assert
            Assert.IsNotNull(task);
            Assert.AreEqual("Valid Title", task.Title);
        }

        [TestMethod]
        public void EditTask_ValidInput_ChangesTaskStatus()
        {
            // Arrange
            var mockConsole = new Mock<IConsoleInput>();
            mockConsole.SetupSequence(c => c.ReadLine())
                .Returns("3");
            var service = new TaskService(mockConsole.Object);
            var task = new Tasks("Test task", TasksStatus.ToDo);
            // Actual
            service.EditTask(task);

            // Assert
            Assert.AreEqual(TasksStatus.Done, task.Status);
        }

        [TestMethod]
        public void CheckTaskByOne_NoTasksInList_DoesNothing()
        {
            // Arrange
            var tasks = new DoublyLinkedList<Tasks>();
            var service = new TaskService();

            using StringWriter sw = new StringWriter();
            Console.SetOut(sw);
            service.CheckTaskByOne(tasks);
            string consoleOutput = sw.ToString().Trim();

            // Assert
            Assert.AreEqual("", consoleOutput);
        }
    }
}

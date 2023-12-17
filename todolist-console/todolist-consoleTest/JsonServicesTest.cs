namespace todolist_consoleTest
{
    [TestClass]
    public class JsonServicesTest
    {
        [TestMethod]
        public void LoadData_FileDoesNotExist_ReturnsNull()
        {
            // Arrange
            var path = "non-existent-file.json";

            // Actual
            var loadedData = JsonService.LoadData<Notes>(path);

            // Assert
            Assert.IsNull(loadedData);
        }

        [TestMethod]
        public void WriteData_ExistingPath_Serialize()
        {
            // Arrange
            var path = "existing-file.json";
            var expectedData = new Notes("Test title", "Test descr");

            // Actual
            JsonService.WriteData(expectedData, path);
            var loadedData = JsonService.LoadData<Notes>(path);

            // Assert
            Assert.IsNotNull(loadedData);
            Assert.AreEqual(loadedData.Title, expectedData.Title);
            Assert.AreEqual(loadedData.Description, expectedData.Description);
        }
    }
}

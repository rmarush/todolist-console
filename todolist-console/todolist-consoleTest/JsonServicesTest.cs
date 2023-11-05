using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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

    }
}

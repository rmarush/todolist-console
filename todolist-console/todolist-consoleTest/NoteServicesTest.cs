namespace todolist_consoleTest
{
    [TestClass]
    public class NoteServicesTest
    {
        [TestMethod]
        public void CreateNote_ValidInput_ReturnsNote()
        {
            // Arrange
            var mockConsole = new Mock<IConsoleInput>();
            mockConsole.SetupSequence(c => c.ReadLine())
                .Returns("Valid Title")
                .Returns("Valid Description");

            var service = new NotesService(mockConsole.Object);

            // Actual
            var note = service.CreateNote();

            // Assert
            Assert.IsNotNull(note);
            Assert.AreEqual("Valid Title", note.Title);
            Assert.AreEqual("Valid Description", note.Description);
        }
        [TestMethod]
        public void EditNote_ValidInput_TitleChanged()
        {
            // Arrange
            var mockConsole = new Mock<IConsoleInput>();
            var foundedNote = new Notes("Test title", "Test descr");
            mockConsole.SetupSequence(c => c.ReadLine())
                .Returns("0")
                .Returns("0")
                .Returns("New title");

            var service = new NotesService(mockConsole.Object);

            // Actual
            service.EditNote(foundedNote);

            // Assert
            Assert.AreEqual("New title", foundedNote.Title);
            Assert.AreEqual("Test descr", foundedNote.Description);
        }
        [TestMethod]
        public void EditNote_ValidInput_DescriptionChanged()
        {
            // Arrange
            var mockConsole = new Mock<IConsoleInput>();
            var foundedNote = new Notes("Test title", "Test descr");
            mockConsole.SetupSequence(c => c.ReadLine())
                .Returns("0")
                .Returns("1")
                .Returns("New descr");

            var service = new NotesService(mockConsole.Object);

            // Actual
            service.EditNote(foundedNote);

            // Assert
            Assert.AreEqual("Test title", foundedNote.Title);
            Assert.AreEqual("New descr", foundedNote.Description);
        }
        [TestMethod]
        public void DeleteNote_NoNotes_ReturnsDefaultHashCode()
        {
            // Arrange
            var service = new NotesService();
            // Actual
            var result = service.DeleteNote(new Dictionary<int, Notes>());
            // Assert
            Assert.AreEqual(new Notes().Date.GetHashCode(), result);
        }

        [TestMethod]
        public void DeleteNote_FoundNoteReviewed_ReturnsNoteHashCode()
        {
            // Arrange
            var mockNote = new Notes("Title", "Description");
            var mockNotes = new Dictionary<int, Notes> { { mockNote.Date.GetHashCode(), mockNote } };

            var mockService = new Mock<NotesService>();
            mockService.Setup(s => s.FindNote(mockNotes)).Returns(mockNote);
            mockService.Setup(s => s.ReviewNote(mockNote)).Returns(false);

            // Act
            var result = mockService.Object.DeleteNote(mockNotes);

            // Assert
            Assert.AreEqual(new Notes().Date.GetHashCode(), result);
        }
    }
}
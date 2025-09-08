using FinalTourplanner.Logging;
using log4net;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFinaltourplanner
{
    internal class LoggingTests
    {
        [Test]
        public void LogDebug()
        {
            // Arrange
            var mockLog = new Mock<ILog>();
            var logger = new Log4NetWrapper(mockLog.Object);
            var message = "Test debug message";

            // Act
            logger.Debug(message);

            // Assert
            mockLog.Verify(l => l.Debug(message), Times.Once);
        }
        [Test]
        public void LogInfo()
        {
            // Arrange
            var mockLog = new Mock<ILog>();
            var logger = new Log4NetWrapper(mockLog.Object);
            var message = "Test info message";

            // Act
            logger.Info(message);

            // Assert
            mockLog.Verify(l => l.Info(message), Times.Once);
        }
        [Test]
        public void LogError()
        {
            // Arrange
            var mockLog = new Mock<ILog>();
            var logger = new Log4NetWrapper(mockLog.Object);
            var message = "Test error message";

            // Act
            logger.Error(message);

            // Assert
            mockLog.Verify(l => l.Error(message), Times.Once);
        }
        [Test]
        public void LogFatal()
        {
            // Arrange
            var mockLog = new Mock<ILog>();
            var logger = new Log4NetWrapper(mockLog.Object);
            var message = "Test fatal message";

            // Act
            logger.Fatal(message);

            // Assert
            mockLog.Verify(l => l.Fatal(message), Times.Once);
        }
        [Test]
        public void LogWarn()
        {
            // Arrange
            var mockLog = new Mock<ILog>();
            var logger = new Log4NetWrapper(mockLog.Object);
            var message = "Test warn message";

            // Act
            logger.Warn(message);

            // Assert
            mockLog.Verify(l => l.Warn(message), Times.Once);
        }
        [Test]
        public void FailWithInvalidFile()
        {
            var invalidPath = "nonexistent.config";

            var ex = Assert.Throws<ArgumentException>(() =>
                Log4NetWrapper.CreateLogger(invalidPath));

            Assert.That(ex.ParamName, Is.EqualTo("path"));
        }

    }
}

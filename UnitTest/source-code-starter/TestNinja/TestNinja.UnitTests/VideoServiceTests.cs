using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;

namespace TestNinja.UnitTests
{
    [TestFixture]
    class VideoServiceTests
    {
        VideoService _videoService;
        Mock<IFileReader> _mockFileReader;
        Mock <IVideoRepository> _repository;

        [SetUp]
        public void SetUp() // if each dependency is used only in one method then its more flexible to inject it in the method, not in constructor
        { // like I did here but its ok also if there are not too many dependencies
            _mockFileReader = new Mock<IFileReader>();
            _repository = new Mock<IVideoRepository>();
            _videoService = new VideoService(_mockFileReader.Object, _repository.Object);
        }

        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnError()
        {
            _mockFileReader.Setup(fr => fr.Read("video.txt")).Returns("");

            var result = _videoService.ReadVideoTitle();

            Assert.That(result, Does.Contain("error").IgnoreCase);
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_WhenListOfVideosIsempty_ReturnsEmptyString()
        {
            _repository.Setup(vr => vr.GetUnprocessedVideos()).Returns(new List<Video>());

            var result = _videoService.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo(""));
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_WhenListContainsVideos_ReturnsJoinedString()
        {
            var videoList = new List<Video>() { new Video() { IsProcessed = false, Id = 2 }, new Video() { IsProcessed = false, Id = 3 } };
            _repository.Setup(vr => vr.GetUnprocessedVideos()).Returns(new List<Video>()
            {
                new Video() { IsProcessed = false, Id = 2 },
                new Video() { IsProcessed = false, Id = 3 }
            });

            var result = _videoService.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo("2,3"));
        }
    }
}

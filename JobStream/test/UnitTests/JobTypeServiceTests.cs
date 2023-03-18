using AutoFixture;
using JobStream.API.Controllers;
using JobStream.Business.DTOs.JobTypeDTO;
using JobStream.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTests
{
    [TestClass]
    public class JobTypeServiceTests 
    {
        private Mock<IJobTypeService> _jobTypeServiceMock;
        private Fixture _fixture;
        private JobTypesController _controller ;
        public JobTypeServiceTests()
        {
            _fixture= new Fixture();
            _jobTypeServiceMock = new Mock<IJobTypeService>();
        }

        [TestMethod]
        public async Task Get_JobTypes_ReturnOk() 
        {
             var jobTypeList=_fixture.CreateMany<JobTypeDTO>(3).ToList();
              _jobTypeServiceMock.Setup(repo => repo.GetAllJobTypesAsync()).ReturnsAsync(jobTypeList);
            _controller = new JobTypesController(_jobTypeServiceMock.Object);

            var result=await _controller.GetAllJobTypes();
            var obj = result as ObjectResult;
            Assert.AreEqual(200, obj.StatusCode);
        }

        
    }
}
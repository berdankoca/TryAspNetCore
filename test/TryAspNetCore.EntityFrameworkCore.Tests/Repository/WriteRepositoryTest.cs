using System;
using Xunit;
using FluentAssertions;
using AutoFixture;
using AutoFixture.Xunit2;
using Moq;
using TryAspNetCore.Core;
using Microsoft.EntityFrameworkCore;
using TryAspNetCore.EntityFrameworkCore.Repository;

namespace TryAspNetCore.EntityFrameworkCore.Tests
{
    //TODO: Create new base class for test implement all of test classes
    //https://fullstackmark.com/post/20/painless-integration-testing-with-aspnet-core-web-api
    public class WriteRepositoryTest
    {
        //[Theory, AutoData]
        //public void Add_Should_Add_Data_And_Should_Return_Expected_Data(Mock<ISessionManager> mockSessionManager, DefaultSession session, Dummy data)
        //{
        //    //Arrange
        //    mockSessionManager.Setup(m => m.Current)
        //                    .Returns(session);

        //    var options = new DbContextOptionsBuilder<DummyContext>()
        //        .UseInMemoryDatabase("DummyDb")
        //        .Options;

        //    //Act
        //    var context = new DummyContext(options, mockSessionManager.Object);
        //    var repository = new WriteRepository<DummyContext, Dummy>(context);
        //    repository.Add(data);

        //    //Assert
        //    Dummy dummyData = repository.GetById(data.Id);
        //    dummyData.Should().BeEquivalentTo(data);

        //    context.Dispose();
        //}
        //[Theory, AutoData]
        //public void Add_Should_Save_Context_When_AutoSave_Is_True(Mock<ISessionManager> mockSessionManager, DefaultSession session, Dummy data)
        //{
        //    //Arrange
        //    mockSessionManager.Setup(m => m.Current)
        //                    .Returns(session);

        //    var options = new DbContextOptionsBuilder<DummyContext>()
        //        .UseInMemoryDatabase("DummyDb")
        //        .Options;

        //    //Act
        //    var context = new DummyContext(options, mockSessionManager.Object);
        //    var repository = new WriteRepository<DummyContext, Dummy>(context);
        //    repository.AutoSave = true;
        //    repository.Add(data);

        //    //Assert
        //    Dummy dummyData = repository.GetById(data.Id);
        //    dummyData.Should().BeEquivalentTo(data);

        //    context.Dispose();
        //}
        //[Theory, AutoData]
        //public void Add_Should_Dont_Save_Context_When_AutoSave_Is_False(Mock<ISessionManager> mockSessionManager, DefaultSession session, Dummy data)
        //{
        //    //Arrange
        //    mockSessionManager.Setup(m => m.Current)
        //                    .Returns(session);

        //    var options = new DbContextOptionsBuilder<DummyContext>()
        //        .UseInMemoryDatabase("DummyDb")
        //        .Options;

        //    //Act
        //    var context = new DummyContext(options, mockSessionManager.Object);
        //    var repository = new WriteRepository<DummyContext, Dummy>(context);
        //    repository.AutoSave = false;
        //    repository.Add(data);

        //    //Assert
        //    Dummy dummyData = repository.GetById(data.Id);
        //    dummyData.Should().BeNull();

        //    context.Dispose();
        //}
        //[Theory, AutoData]
        //public void AddAsync_Should_Add_Data_And_Should_Return_Exptected(Mock<ISessionManager> mockSessionManager, DefaultSession session, Dummy data)
        //{
        //    //Arrange
        //    mockSessionManager.Setup(m => m.Current)
        //        .Returns(session);

        //    var options = new DbContextOptionsBuilder<DummyContext>()
        //        .UseInMemoryDatabase("DummyDb")
        //        .Options;

        //    //Act
        //    var context = new DummyContext(options, mockSessionManager.Object);
        //    var repository = new WriteRepository<DummyContext, Dummy>(context);
        //    repository.AddAsync(data);

        //    //Assert
        //    Dummy dummyData = repository.GetById(data.Id);
        //    dummyData.Should().BeEquivalentTo(data);

        //    context.Dispose();
        //}
        //[Theory, AutoData]
        //public void Add_Data_Without_Name_Should_Throw_CustomValidationException(Mock<ISessionManager> mockSessionManager, DefaultSession session, Dummy data)
        //{
        //    //Arrange
        //    mockSessionManager.Setup(m => m.Current)
        //        .Returns(session);

        //    var options = new DbContextOptionsBuilder<DummyContext>()
        //        .UseInMemoryDatabase("DummyDb")
        //        .Options;
        //    data.Name = null;

        //    //Act
        //    var context = new DummyContext(options, mockSessionManager.Object);
        //    var repository = new WriteRepository<DummyContext, Dummy>(context);
        //    Action action = () => repository.Add(data);

        //    //Assert
        //    action.Should().Throw<CustomValidationException>();

        //    context.Dispose();
        //}

        //[Theory, AutoData]
        //public void Update_Should_Update_Data(Mock<ISessionManager> mockSessionManager, DefaultSession session, Dummy data, string newName)
        //{
        //    //Arrange
        //    mockSessionManager.Setup(m => m.Current)
        //        .Returns(session);

        //    var options = new DbContextOptionsBuilder<DummyContext>()
        //        .UseInMemoryDatabase("DummyDb")
        //        .Options;

        //    var context = new DummyContext(options, mockSessionManager.Object);
        //    var repository = new WriteRepository<DummyContext, Dummy>(context);
        //    repository.Add(data);
        //    Dummy dummyData = repository.GetById(data.Id);

        //    //Act
        //    dummyData.Name = newName;
        //    repository.Update(dummyData);

        //    //Assert
        //    Dummy newDummyData = repository.GetById(data.Id);
        //    dummyData.Name.Should().Be(newName);

        //    context.Dispose();
        //}
        //[Theory, AutoData]
        //public void UpdateAsync_Should_Update_Data(Mock<ISessionManager> mockSessionManager, DefaultSession session, Dummy data, string newName)
        //{
        //    //Arrange
        //    mockSessionManager.Setup(m => m.Current)
        //        .Returns(session);

        //    var options = new DbContextOptionsBuilder<DummyContext>()
        //        .UseInMemoryDatabase("DummyDb")
        //        .Options;

        //    var context = new DummyContext(options, mockSessionManager.Object);
        //    var repository = new WriteRepository<DummyContext, Dummy>(context);
        //    repository.Add(data);
        //    Dummy dummyData = repository.GetById(data.Id);

        //    //Act
        //    dummyData.Name = newName;
        //    repository.UpdateAsync(dummyData);

        //    //Assert
        //    Dummy newDummyData = repository.GetById(data.Id);
        //    dummyData.Name.Should().Be(newName);

        //    context.Dispose();
        //}

        //[Theory, AutoData]
        //public void Delete_Should_Delete_Data_And_Should_Return_Null(Mock<ISessionManager> mockSessionManager, DefaultSession session, Dummy data)
        //{
        //    //Arrange
        //    mockSessionManager.Setup(m => m.Current)
        //        .Returns(session);

        //    var options = new DbContextOptionsBuilder<DummyContext>()
        //        .UseInMemoryDatabase("DummyDb")
        //        .Options;

        //    var context = new DummyContext(options, mockSessionManager.Object);
        //    var repository = new WriteRepository<DummyContext, Dummy>(context);
        //    repository.Add(data);

        //    //Act
        //    repository.Delete(data.Id);

        //    //Assert
        //    Dummy dummyData = repository.GetById(data.Id);
        //    dummyData.Should().BeNull();

        //    context.Dispose();
        //}
        //[Theory, AutoData]
        //public void DeleteAsync_Should_Delete_Data_And_Should_Return_Null(Mock<ISessionManager> mockSessionManager, DefaultSession session, Dummy data)
        //{
        //    //Arrange
        //    mockSessionManager.Setup(m => m.Current)
        //        .Returns(session);

        //    var options = new DbContextOptionsBuilder<DummyContext>()
        //        .UseInMemoryDatabase("DummyDb")
        //        .Options;

        //    var context = new DummyContext(options, mockSessionManager.Object);
        //    var repository = new WriteRepository<DummyContext, Dummy>(context);
        //    repository.Add(data);

        //    //Act
        //    repository.DeleteAsync(data.Id);

        //    //Assert
        //    Dummy dummyData = repository.GetById(data.Id);
        //    dummyData.Should().BeNull();

        //    context.Dispose();
        //}

        //[Theory, AutoData]
        //public void Save_Should_Save_Context_And_Should_Return_Exptected(Mock<ISessionManager> mockSessionManager, DefaultSession session, Dummy data)
        //{
        //    //Arrange
        //    mockSessionManager.Setup(m => m.Current)
        //                    .Returns(session);

        //    var options = new DbContextOptionsBuilder<DummyContext>()
        //        .UseInMemoryDatabase("DummyDb")
        //        .Options;

        //    //Act
        //    var context = new DummyContext(options, mockSessionManager.Object);
        //    var repository = new WriteRepository<DummyContext, Dummy>(context);
        //    repository.AutoSave = false;
        //    repository.Add(data);
        //    var result = repository.Save();

        //    //Assert
        //    result.Should().BeGreaterThan(0);

        //    Dummy dummyData = repository.GetById(data.Id);
        //    dummyData.Should().BeEquivalentTo(data);

        //    context.Dispose();
        //}
        //[Theory, AutoData]
        //public void SaveAsync_Should_Save_Context_And_Should_Return_Expected(Mock<ISessionManager> mockSessionManager, DefaultSession session, Dummy data)
        //{
        //    //Arrange
        //    mockSessionManager.Setup(m => m.Current)
        //                    .Returns(session);

        //    var options = new DbContextOptionsBuilder<DummyContext>()
        //        .UseInMemoryDatabase("DummyDb")
        //        .Options;

        //    //Act
        //    var context = new DummyContext(options, mockSessionManager.Object);
        //    var repository = new WriteRepository<DummyContext, Dummy>(context);
        //    repository.AutoSave = false;
        //    repository.Add(data);
        //    var result = repository.SaveAsync().Result;

        //    //Assert
        //    result.Should().BeGreaterThan(0);

        //    Dummy dummyData = repository.GetById(data.Id);
        //    dummyData.Should().BeEquivalentTo(data);

        //    context.Dispose();
        //}
    }
}
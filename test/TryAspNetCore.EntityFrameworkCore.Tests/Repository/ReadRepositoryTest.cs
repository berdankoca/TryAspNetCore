using Moq;
using System;
using Xunit;
using FluentAssertions;
using AutoFixture;
using AutoFixture.Xunit2;
using TryAspNetCore.EntityFrameworkCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TryAspNetCore.Core;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace TryAspNetCore.EntityFrameworkCore.Tests
{
    public class ReadRepositoryTest : EntityFrameworkCoreTestBase
    {
        [Theory, AutoData]
        public void GetById_Should_Return_The_Expected_Data(Mock<ISessionManager> mockSessionManager, DefaultSession session, Dummy data)
        {
            //Arrange
            mockSessionManager.Setup(m => m.Current)
                .Returns(session);

            var options = new DbContextOptionsBuilder<DummyContext>()
                .UseInMemoryDatabase("DummyDb")
                .Options;

            using (var context = new DummyContext(options, mockSessionManager.Object))
            {
                //var repository = new WriteRepository<DummyContext, Dummy>(context);

                //repository.Add(data);
            }

            //Act
            Dummy dummyData;
            using (var context = new DummyContext(options, mockSessionManager.Object))
            {
                //var repository = new ReadRepository<DummyContext, Dummy>(context);
                //dummyData = repository.GetById(data.Id);
            }

            //Assert
            //dummyData.Should().BeEquivalentTo(data);
        }

        //[Theory, AutoData]
        //public void GetByIdAsync_Should_Return_The_Expected_Data(Mock<ISessionManager> mockSessionManager, DefaultSession session, Dummy data)
        //{
        //    //Arrange
        //    mockSessionManager.Setup(m => m.Current)
        //        .Returns(session);

        //    var options = new DbContextOptionsBuilder<DummyContext>()
        //        .UseInMemoryDatabase("DummyDb")
        //        .Options;

        //    using (var context = new DummyContext(options, mockSessionManager.Object))
        //    {
        //        var repository = new WriteRepository<DummyContext, Dummy>(context);

        //        repository.Add(data);
        //    }

        //    //Act
        //    Task<Dummy> dummyData;
        //    using (var context = new DummyContext(options, mockSessionManager.Object))
        //    {
        //        var repository = new ReadRepository<DummyContext, Dummy>(context);
        //        dummyData = repository.GetByIdAsync(data.Id);
        //    }

        //    //Assert
        //    dummyData.Result.Should().BeEquivalentTo(data);
        //}

        //[Theory, AutoData]
        //public void FindBy_Should_Return_The_Dummy_Data_And_All_Count_As_One(Mock<ISessionManager> mockSessionManager, DefaultSession session, Dummy data)
        //{
        //    //Arrange
        //    mockSessionManager.Setup(m => m.Current)
        //                    .Returns(session);

        //    var options = new DbContextOptionsBuilder<DummyContext>()
        //        .UseInMemoryDatabase("DummyDb")
        //        .Options;

        //    using (var context = new DummyContext(options, mockSessionManager.Object))
        //    {
        //        var repository = new WriteRepository<DummyContext, Dummy>(context);

        //        repository.Add(data);
        //    }

        //    //Act
        //    List<Dummy> list;
        //    using (var context = new DummyContext(options, mockSessionManager.Object))
        //    {
        //        var repository = new ReadRepository<DummyContext, Dummy>(context);
        //        list = repository.FindBy(e => e.Id == data.Id).ToList();
        //    }

        //    //Assert
        //    list.Count.Should().Be(1);
        //    list.First().Should().BeEquivalentTo(data);
        //}

        //[Theory, AutoData]
        //public void FindBy_Should_Return_All_Count_As_Greater_Than_One(Mock<ISessionManager> mockSessionManager, DefaultSession session, Dummy data1, Dummy data2)
        //{
        //    //Arrange
        //    mockSessionManager.Setup(m => m.Current)
        //        .Returns(session);

        //    var options = new DbContextOptionsBuilder<DummyContext>()
        //        .UseInMemoryDatabase("DummyDb")
        //        .Options;
        //    string name = "berdan";
        //    data1.Name = name;
        //    data2.Name = name;
        //    using (var context = new DummyContext(options, mockSessionManager.Object))
        //    {
        //        var repository = new WriteRepository<DummyContext, Dummy>(context);

        //        repository.Add(data1);
        //        repository.Add(data2);
        //    }

        //    //Act
        //    List<Dummy> list;
        //    using (var context = new DummyContext(options, mockSessionManager.Object))
        //    {
        //        var repository = new ReadRepository<DummyContext, Dummy>(context);
        //        list = repository.FindBy(e => e.Name == name).ToList();
        //    }

        //    //Assert
        //    list.Count.Should().BeGreaterThan(1);
        //}

        //[Theory, AutoData]
        //public void FindByAsync_Should_Return_The_Dummy_Data_And_All_Count_As_One(Mock<ISessionManager> mockSessionManager, DefaultSession session, Dummy data)
        //{
        //    //Arrange
        //    mockSessionManager.Setup(m => m.Current)
        //                    .Returns(session);

        //    var options = new DbContextOptionsBuilder<DummyContext>()
        //        .UseInMemoryDatabase("DummyDb")
        //        .Options;

        //    using (var context = new DummyContext(options, mockSessionManager.Object))
        //    {
        //        var repository = new WriteRepository<DummyContext, Dummy>(context);

        //        repository.Add(data);
        //    }

        //    //Act
        //    List<Dummy> list;
        //    using (var context = new DummyContext(options, mockSessionManager.Object))
        //    {
        //        var repository = new ReadRepository<DummyContext, Dummy>(context);
        //        list = repository.FindByAsync(e => e.Id == data.Id).Result;
        //    }

        //    //Assert
        //    list.Count.Should().Be(1);
        //    list.First().Should().BeEquivalentTo(data);
        //}

        //[Theory, AutoData]
        //public void FindByAsync_Should_Should_Return_The_Dummy_Data_And_All_Count_As_More_Than_One(Mock<ISessionManager> mockSessionManager, DefaultSession session, Dummy data1, Dummy data2)
        //{
        //    //Arrange
        //    mockSessionManager.Setup(m => m.Current)
        //        .Returns(session);

        //    var options = new DbContextOptionsBuilder<DummyContext>()
        //        .UseInMemoryDatabase("DummyDb")
        //        .Options;
        //    string name = "berdan";
        //    data1.Name = name;
        //    data2.Name = name;
        //    using (var context = new DummyContext(options, mockSessionManager.Object))
        //    {
        //        var repository = new WriteRepository<DummyContext, Dummy>(context);

        //        repository.Add(data1);
        //        repository.Add(data2);
        //    }

        //    //Act
        //    List<Dummy> list;
        //    using (var context = new DummyContext(options, mockSessionManager.Object))
        //    {
        //        var repository = new ReadRepository<DummyContext, Dummy>(context);
        //        list = repository.FindByAsync(e => e.Name == name).Result;
        //    }

        //    //Assert
        //    list.Count.Should().BeGreaterThan(1);
        //}
    }
}

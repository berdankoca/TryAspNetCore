using System;
using Xunit;
using FluentAssertions;
using AutoFixture;
using AutoFixture.Xunit2;

namespace TryAspNetCore.Api.Tests
{
    public class FirstTest
    {
        [Fact]
        public void Test1()
        {
            //Arrange
            int a = 2;
            int b = 3;

            //Act
            int c = a + b;

            //Assert
            c.Should().Be(4);
        }

        [Theory]
        [InlineData(1, 2, 3)]
        public void Test2(int a, int b, int x)
        {
            //Arrange
            // int a = 2;
            // int b = 3;

            //Act
            int c = a + b;

            //Assert
            c.Should().Be(x);
        }



        [Theory, AutoData]
        public void Test3(int a, int b)
        {
            //Arrange
            // int a = 2;
            // int b = 3;

            //Act
            int c = a + b;

            //Assert
            c.Should().Be(a + b);
        }


        [Theory, AutoData]
        public void Test4(Deneme deneme)
        {
            //Arrange
            // int a = 2;
            // int b = 3;

            //Act

            //Assert
            deneme.Should().NotBeNull();
        }
    }

    public class Deneme
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}

using CloudCustomers.UnitTests.Fixtures;
using CloudCustomers.UnitTests.Helpers;
using CloudCustomersAPI.Config;
using CloudCustomersAPI.Models;
using CloudCustomersAPI.Services;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CloudCustomers.UnitTests.Systems.Services
{
    public class TestUsersService
    {
        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokesHttpGetRequerst()
        {
            //Arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>.SetUpBasicGetResourceList(expectedResponse);
           var httpClient = new HttpClient(handlerMock.Object);

            var endpoint = "https://example.com/users";
            var config = Options.Create(new UsersApiOptions
            {
                Endpoint = endpoint
            });

            var sut = new UsersService(httpClient,config);

            //Act
            await sut.GetAllUsers();

            //Assert
            handlerMock
                .Protected()
                .Verify(
                    "SendAsync",
                    Times.Exactly(1),
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                    ItExpr.IsAny<CancellationToken>());

        }


        [Fact]
        public async Task GetAllUsers_WhenCalled_ReturnsListOfUsers()
        {
            //Arrange
            var handlerMock = MockHttpMessageHandler<User>.SetUpReturn404();
            var httpClient = new HttpClient(handlerMock.Object);

            var endpoint = "https://example.com/users";
            var config = Options.Create(new UsersApiOptions
            {
                Endpoint = endpoint
            });

            var sut = new UsersService(httpClient,config);

            //Act
            var result = await sut.GetAllUsers();

            //Assert
            result.Count.Should().Be(0);    
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_ReturnsListOfUsers_OfExpectedSize()
        {
            //Arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>.SetUpBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);

            var endpoint = "https://example.com/users";
            var config = Options.Create(new UsersApiOptions
            {
                Endpoint = endpoint
            });

            var sut = new UsersService(httpClient,config);

            //Act
            var result = await sut.GetAllUsers();

            //Assert
            result.Count.Should().Be(expectedResponse.Count);
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokesConfiguredUrl()
        {
            //Arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var endpoint = "https://example.com/users";
            var handlerMock = MockHttpMessageHandler<User>.SetUpBasicGetResourceList(expectedResponse, endpoint);
            var httpClient = new HttpClient(handlerMock.Object);

            var config = Options.Create(new UsersApiOptions
            {
                Endpoint = endpoint
            });

            var sut = new UsersService(httpClient,config);

            //Act
            var result = await sut.GetAllUsers();

          
            //Assert
            handlerMock
                .Protected()
                .Verify(
                    "SendAsync",
                    Times.Exactly(1),
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get && req.RequestUri.ToString() == endpoint),
                    ItExpr.IsAny<CancellationToken>());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using Xunit;
using System.Net;
using Server.Models.Client;
using Server.Models.Core;

namespace FH.Test.Integration.Api.User
{
  public class CreateClientTests : IClassFixture<IntegrationTestFixture>
  {
    private readonly IntegrationTestFixture _fixture;

    public CreateClientTests(IntegrationTestFixture fixture)
    {
      _fixture = fixture;
    }

    [Fact]
    public void client_should_be_created()
    {
      var client = new Client
      {
        Gender = Gender.Male,
        GivenName = "Bob",
        FamilyName = "Mohammed",
        Email = "bob@bob.com",
        Weight = 100,
        Height = 200,
        DateOfBirth = DateTime.Now,
        TelephoneNumber = "phone",
        EmergencyContactTelephoneNumber = "emergency phone",
        EmergencyContactName = "emergency contact",
        Avatar = new Image { Uri = "some/url" },
        Injuries = new List<Injury> { new Injury { Name = "peg leg" } }
      };

      var response =
          _fixture
            .CallApi<CreatedResult, Client>(
                "http://localhost/api/v1/clients",
                 HttpMethod.Post,
                 client
                 ).Result;

      response.Should().NotBeNull();
      response.HttpResponse.StatusCode.Should().Be(HttpStatusCode.Created);

      //var findClientResponse = _fixture.CallApi<Client>(response.HttpResponse.Headers.Location.ToString(), HttpMethod.Get).Result;
      //   findClientResponse.Result.Id.Should().BePositive();
    }
  }
}



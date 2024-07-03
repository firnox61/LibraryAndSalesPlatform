using Entities.Concrete;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Tests
{
    public class BookIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public BookIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

       /* [Fact]
        public async Task AddBook_ShouldReturnSuccess_WhenBookIsValid()
        {
            // Arrange
            var filePath = "path/to/your/image.jpg"; // Set your image path here
            var fileStream = File.OpenRead(filePath);
            var fileContent = new StreamContent(fileStream);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");

            var formData = new MultipartFormDataContent();
            formData.Add(new StringContent("Integration Test Book"), "Title");
            formData.Add(new StringContent("Test Genre"), "Genre");
            formData.Add(new StringContent("Test Description"), "Description");
            formData.Add(new StringContent("1"), "ShelfId");
            formData.Add(fileContent, "CoverImageUrl", Path.GetFileName(filePath));

            // Act
            var response = await _client.PostAsync("/api/books", formData);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseMessage = await response.Content.ReadAsStringAsync();
            Assert.Equal("Book successfully added", responseMessage); // Adjust this message as per your Messages class
        }*/
    }
}
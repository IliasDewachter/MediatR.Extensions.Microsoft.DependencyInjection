using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace MediatR.Extensions.Microsoft.DependencyInjection.Tests
{
    public class InheritanceNotificationHandlerTests
    {
        private readonly IServiceProvider _provider;
        private readonly Logger _logger;

        public InheritanceNotificationHandlerTests()
        {
            _logger = new Logger();
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton(_logger);
            services.AddMediatR(typeof(Kinged));
            _provider = services.BuildServiceProvider();
        }

        [Fact]
        public void ShouldResolveBaseNotificationHandler_WhenPublishedEventIsNotRegistered()
        {
            var mediatr = _provider.GetRequiredService<IMediator>();
            mediatr.Publish(new Konged());
            
            Assert.Single(_logger.Messages);
            Assert.Equal(KingedHandler.HandledLog, _logger.Messages.Single());
        }
    }
}
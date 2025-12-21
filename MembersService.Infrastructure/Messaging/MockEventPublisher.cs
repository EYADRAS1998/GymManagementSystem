using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembersService.Infrastructure.Messaging
{
    public class MockEventPublisher : IEventPublisher
    {
        public Task PublishAsync<TEvent>(TEvent @event)
        {
            Console.WriteLine($"Event published: {typeof(TEvent).Name}");
            return Task.CompletedTask;
        }
    }

}

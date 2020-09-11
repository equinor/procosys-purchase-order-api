using Equinor.ProCoSys.PO.Domain;
using Equinor.ProCoSys.PO.Domain.Events;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Equinor.ProCoSys.PO.Infrastructure.Tests
{
    public class ContextHelper
    {
        public ContextHelper()
        {
            DbOptions = new DbContextOptions<POContext>();
            EventDispatcherMock = new Mock<IEventDispatcher>();
            PlantProviderMock = new Mock<IPlantProvider>();
            CurrentUserProviderMock = new Mock<ICurrentUserProvider>();
            ContextMock = new Mock<POContext>(DbOptions, PlantProviderMock.Object, EventDispatcherMock.Object, CurrentUserProviderMock.Object);
        }

        public DbContextOptions<POContext> DbOptions { get; }
        public Mock<IEventDispatcher> EventDispatcherMock { get; }
        public Mock<IPlantProvider> PlantProviderMock { get; }
        public Mock<POContext> ContextMock { get; }
        public Mock<ICurrentUserProvider> CurrentUserProviderMock { get; }
    }
}

using funcInfrastructureAsCode.Functions.Commands;

namespace funcInfrastructureAsCode.Functions.Abstraction
{
    public interface IMappedEntity<T>
    {
        public void Map(
            CreateVirtualMachineCommand command);
    }
}
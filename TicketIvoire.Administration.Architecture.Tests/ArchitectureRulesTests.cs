using NetArchTest.Rules;

namespace TicketIvoire.Administration.Architecture.Tests;

public class ArchitectureRulesTests
{
    [Fact]
    public void DomainLayer_ShouldNotHaveDependencyOn_InfrastructureLayer()
    {
        TestResult result = Types.InAssembly(typeof(Domain.Membres.Membre).Assembly)
            .That()
            .ResideInNamespace("TicketIvoire.Administration.Domain")
            .ShouldNot()
            .HaveDependencyOn("TicketIvoire.Administration.Infrastructure")
            .GetResult();

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void DomainLayer_ShouldNotHaveDependencyOn_PresentationLayer()
    {
        TestResult result = Types.InAssembly(typeof(Domain.Membres.Membre).Assembly)
            .That()
            .ResideInNamespace("TicketIvoire.Administration.Domain")
            .ShouldNot()
            .HaveDependencyOn("TicketIvoire.Administration.Api")
            .GetResult();

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void DomainLayer_ShouldNotHaveDependencyOn_ApplicationLayer()
    {
        TestResult result = Types.InAssembly(typeof(Domain.Membres.Membre).Assembly)
            .That()
            .ResideInNamespace("TicketIvoire.Administration.Domain")
            .ShouldNot()
            .HaveDependencyOn("TicketIvoire.Administration.Application")
            .GetResult();

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void ApplicationLayer_ShouldNotHaveDependencyOn_InfrastructureLayer()
    {
        TestResult result = Types.InAssembly(typeof(Application.ServicesExtensions).Assembly)
            .That()
            .ResideInNamespace("TicketIvoire.Administration.Application")
            .ShouldNot()
            .HaveDependencyOn("TicketIvoire.Administration.Infrastructure")
            .GetResult();

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void ApplicationLayer_ShouldNotHaveDependencyOn_PresentationLayer()
    {
        TestResult result = Types.InAssembly(typeof(Application.ServicesExtensions).Assembly)
            .That()
            .ResideInNamespace("TicketIvoire.Administration.Application")
            .ShouldNot()
            .HaveDependencyOn("TicketIvoire.Administration.Api")
            .GetResult();

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void Repositories_ShouldHaveDependency_OnDomainLayer()
    {
        TestResult result = Types.InAssembly(typeof(Infrastructure.ServicesExtensions).Assembly)
            .That()
            .HaveNameEndingWith("Repository", StringComparison.Ordinal)
            .Should()
            .HaveDependencyOn("TicketIvoire.Administration.Domain")
            .GetResult();

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void ApplicationServicesExtensions_ShouldBe_Static()
    {
        TestResult result = Types.InAssembly(typeof(Application.ServicesExtensions).Assembly)
             .That()
             .HaveNameEndingWith("ServicesExtensions", StringComparison.Ordinal)
             .Should()
             .BeStatic()
             .GetResult();

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void InfrastructureServicesExtensions_ShouldBe_Static()
    {
        TestResult result = Types.InAssembly(typeof(Infrastructure.ServicesExtensions).Assembly)
             .That()
             .HaveNameEndingWith("ServicesExtensions", StringComparison.Ordinal)
             .Should()
             .BeStatic()
             .GetResult();

        Assert.True(result.IsSuccessful);
    }
}
